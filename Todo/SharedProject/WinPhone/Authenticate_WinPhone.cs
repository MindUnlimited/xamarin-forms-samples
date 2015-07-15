using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Todo.WinPhone;

[assembly: Xamarin.Forms.Dependency(typeof(Authenticate_WinPhone))]

namespace Todo.WinPhone
{
    public class Authenticate_WinPhone : IAuthenticate
    {
        public async Task Authenticate(MobileServiceAuthenticationProvider provider)
        {
            string message;
            string providerName = provider.ToString();

            // Provide some additional app-specific security for the encryption.
            byte[] entropy = { 1, 8, 3, 6, 5 };

            // Authorization credential.
            MobileServiceUser user = null;

            // Isolated storage for the app.
            IsolatedStorageSettings settings =
                IsolatedStorageSettings.ApplicationSettings;

            while (user == null)
            {
                // Try to get an existing encrypted credential from isolated storage.                    
                if (settings.Contains(providerName))
                {
                    // Get the encrypted byte array, decrypt and deserialize the user.
                    var encryptedUser = settings[providerName] as byte[];
                    var userBytes = ProtectedData.Unprotect(encryptedUser, entropy);
                    user = JsonConvert.DeserializeObject<MobileServiceUser>(
                        System.Text.Encoding.Unicode.GetString(userBytes, 0, userBytes.Length));
                }
                if (user != null)
                {
                    // Set the user from the stored credentials.
                    Todo.App.Database.client.CurrentUser = user;

                    try
                    {
                        // Try to return an item now to determine if the cached credential has expired.
                        await Todo.App.Database.client.GetTable<Item>().Take(1).ToListAsync();
                        await Todo.App.Database.client.InvokeApiAsync("userInfo", HttpMethod.Get, null); // also gather extra user information
                    }
                    catch (MobileServiceInvalidOperationException ex)
                    {
                        if (ex.Response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        {
                            // Remove the credential with the expired token.
                            settings.Remove(providerName);
                            user = null;
                            continue;
                        }
                    }
                }
                else
                {
                    try
                    {
                        // Login with the identity provider.
                        user = await Todo.App.Database.client
                            .LoginAsync(provider);

                        // Serialize the user into an array of bytes and encrypt with DPAPI.
                        var userBytes = System.Text.Encoding.Unicode
                            .GetBytes(JsonConvert.SerializeObject(user));
                        byte[] encryptedUser = ProtectedData.Protect(userBytes, entropy);

                        // Store the encrypted user credentials in local settings.
                        settings.Add(providerName, encryptedUser);
                        settings.Save();
                    }
                    catch (MobileServiceInvalidOperationException ex)
                    {
                        message = "You must log in. Login Required";
                    }
                }
                
                // add last user provider, so that you don't have to click on which login provider you want to use
                if (settings.Contains("LastUsedProvider"))
                {
                    settings["LastUsedProvider"] = provider;
                }
                else
                {
                    settings.Add("LastUsedProvider", provider);
                }
                settings.Save();

                Todo.App.Database.mobileServiceUser = user;
                Todo.App.Database.userID = user.UserId;

                await Todo.App.Database.InitLocalStoreAsync();
                await Todo.App.Database.newUser(Todo.App.Database.mobileServiceUser.UserId, provider);
                await Todo.App.Database.OnRefreshItemsSelected(); // pull database tables

                await Todo.App.Database.getContactsThatUseApp();

                message = string.Format("You are now logged in - {0}", user.UserId);
                Debug.WriteLine(message);
                //MessageBox.Show(message);
            }
        }
    }
}