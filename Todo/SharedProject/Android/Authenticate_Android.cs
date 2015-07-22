using Android.Content;
using Android.Preferences;
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
using Todo.Android;
using Xamarin.Auth;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(Authenticate_Android))]

namespace Todo.Android
{
    public class Authenticate_Android : IAuthenticate
    {
        public async Task Authenticate(MobileServiceAuthenticationProvider provider)
        {
            string message;
            string providerName = provider.ToString();

            // Authorization credential.
            MobileServiceUser user = null;

            var accountStore = AccountStore.Create(Forms.Context); // Xamarin.Android

            // Sharedpreferences (local storage) for storing the last used oauth provider
            var preferences = PreferenceManager.GetDefaultSharedPreferences(Forms.Context);
            var editor = preferences.Edit();

            while (user == null)
            {
                Account accountFound = accountStore.FindAccountsForService(providerName).FirstOrDefault();
                // Try to get an existing encrypted credential from isolated storage.        
                if (accountFound != null)
                {
                    user = new MobileServiceUser(accountFound.Username);
                    user.MobileServiceAuthenticationToken = accountFound.Properties["token"];
                }
                if (user != null)
                {
                    // Set the user from the stored credentials.
                    Todo.App.Database.client.CurrentUser = user;

                    try
                    {
                        // Try to return an item now to determine if the cached credential has expired.
                        await App.Database.client.GetTable<Item>().Take(1).ToListAsync();
                        await App.Database.client.InvokeApiAsync("userInfo", HttpMethod.Get, null); // also gather extra user information
                    }
                    catch (MobileServiceInvalidOperationException ex)
                    {
                        if (ex.Response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        {
                            // Remove the credential with the expired token.
                            accountStore.Delete(accountFound, providerName);
                            user = null;
                            continue;
                        }
                    }
                }
                else
                {
                    try
                    {

                        var test = Todo.App.Database.client;
                        // Login with the identity provider.
                        user = await Todo.App.Database.client
                            .LoginAsync(Forms.Context, provider);

                        // Store the encrypted user credentials in local settings.
                        Account currentAccount = new Account(user.UserId, new Dictionary<string, string> { { "token", user.MobileServiceAuthenticationToken } });
                        accountStore.Save(currentAccount, providerName);
                    }
                    catch (MobileServiceInvalidOperationException ex)
                    {
                        message = "You must log in. Login Required";
                    }
                }

                // add last user provider, so that you don't have to click on which login provider you want to use
                editor.PutString("LastUsedProvider", providerName);
                editor.Apply();

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