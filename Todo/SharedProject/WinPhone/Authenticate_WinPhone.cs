using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
using Todo.Models;
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
                    Todo.App.Current.MainPage.IsBusy = true;

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

                    Todo.App.Current.MainPage.IsBusy = false;
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
                    catch (System.InvalidOperationException ex)
                    {
                        if (ex.Message.Contains("Authentication was cancelled by the user"))
                        {
                            // user probably pushed back button, return to select login page
                            //await Todo.App.Navigation.PushModalAsync(new Views.SelectLoginProviderPage());
                            //await Todo.App.Navigation.PopModalAsync();
                            return;
                            //return;
                        }

                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("Authentication was cancelled by the user"))
                        {
                            // user probably pushed back button, return to select login page
                            //await Todo.App.Navigation.PushModalAsync(new Views.SelectLoginProviderPage());
                            //await Todo.App.Navigation.PopModalAsync();
                            return;
                            //return;
                        }
                    }
                }
                
                if (user != null)
                {
                    Todo.App.Current.MainPage.IsBusy = true;
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

                    Todo.App.Current.MainPage.IsBusy = false;
                }

            }

            if (user != null)
            {
                Todo.App.Current.MainPage.IsBusy = true;

                JObject response = (JObject)await Todo.App.Database.client.InvokeApiAsync("getcontacts", HttpMethod.Get, null);
                List<Contact> contactList = new List<Contact>();
                if (provider == MobileServiceAuthenticationProvider.MicrosoftAccount)
                {
                    foreach (JObject usr in response["data"])
                    {
                        string name = usr["name"].ToString();
                        string id = usr["id"].ToString();
                        var pictureUrl = string.Format("https://apis.live.net/v5.0/{0}/picture", usr["id"]);

                        contactList.Add(new Contact { Id = id, Name = name, PictureUrl = pictureUrl });
                    }
                }
                else if (provider == MobileServiceAuthenticationProvider.Google)
                {
                    JToken identity = await Todo.App.Database.client.InvokeApiAsync("getIdentities", HttpMethod.Get, null);
                    string accessToken = identity["google"]["accessToken"].ToString();

                    foreach (JObject usr in response["feed"]["entry"])
                    {
                        string name = usr["title"]["$t"].ToString();
                        string id = usr["id"]["$t"].ToString().Split('/').Last();
                        var pictureUrl = string.Format("https://www.google.com/m8/feeds/photos/media/default/{0}?access_token={1}", id, accessToken); // may not exist

                        contactList.Add(new Contact { Id = id, Name = name, PictureUrl = pictureUrl });
                    }
                }
                else if (provider == MobileServiceAuthenticationProvider.Facebook)
                {
                    JToken identity = await Todo.App.Database.client.InvokeApiAsync("getIdentities", HttpMethod.Get, null);
                    string accessToken = identity["facebook"]["accessToken"].ToString();

                    JArray contacts = (JArray)response["data"];
                    for (int i = 0; i < contacts.Count; i++)
                    {
                        JObject contact = (JObject)contacts.ElementAt(i);
                        string name = contact["name"].ToString();
                        string id = contact["id"].ToString();
                        //var pictureUrl = string.Format(contact["picture"]["data"]["url"].ToString() + "?access_token={0}", accessToken);
                        var pictureUrl = String.Format("https://graph.facebook.com/{0}/picture?type=large&access_token={1}", id, accessToken);

                        contactList.Add(new Contact { Id = id, Name = name, PictureUrl = pictureUrl });
                    }
                }

                Todo.App.Current.MainPage.IsBusy = false;
            }

        }


    }
}