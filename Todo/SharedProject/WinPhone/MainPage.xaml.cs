using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

using Xamarin.Forms;
using Xamarin.Contacts;

using System.IO;
using Windows.Storage;
using Microsoft.WindowsAzure.MobileServices;
using System.Net.Http;

// Store token for authentication
using System.IO.IsolatedStorage;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Diagnostics;     

namespace Todo.WinPhone
{
    public partial class MainPage : global::Xamarin.Forms.Platform.WinPhone.FormsApplicationPage // superclass new in 1.3
    {
        private bool justAuthenticated = false;
        private bool useToken = true;

        // Isolated storage for the app.
        IsolatedStorageSettings settings =
            IsolatedStorageSettings.ApplicationSettings;

        //private async Task Authenticate()
        //{
        //    string message;

        //    // For now we use the Microsoft provider.
        //    var provider = "Microsoft";
        //    // Provide some additional app-specific security for the encryption.
        //    byte[] entropy = { 1, 8, 3, 6, 5 };

        //    //// Authorization credential.
        //    //MobileServiceUser user = null;

        //    // Isolated storage for the app.
        //    IsolatedStorageSettings settings =
        //        IsolatedStorageSettings.ApplicationSettings;

        //    // Try to get an existing encrypted credential from isolated storage.                    
        //    if (useToken && settings != null && settings.Contains(provider))
        //    {
        //        // Get the encrypted byte array, decrypt and deserialize the user.
        //        var encryptedUser = settings[provider] as byte[];
        //        var userBytes = ProtectedData.Unprotect(encryptedUser, entropy);
        //        Todo.App.Database.mobileServiceUser = JsonConvert.DeserializeObject<MobileServiceUser>(
        //            System.Text.Encoding.Unicode.GetString(userBytes, 0, userBytes.Length));
        //    }
        //    if (Todo.App.Database.mobileServiceUser != null)
        //    {
        //        // Set the user from the stored credentials.
        //        Todo.App.Database.client.CurrentUser = Todo.App.Database.mobileServiceUser;
        //        //App.MobileService.CurrentUser = user;

        //        try
        //        {
        //            // Try to return an item now to determine if the cached credential has expired.
        //            //var test = await Todo.App.Database.client.GetTable<Item>().Take(1).ToListAsync();
        //            var test2 = await Todo.App.Database.client.InvokeApiAsync("userInfo", HttpMethod.Get, null);
        //            justAuthenticated = true;
        //        }
        //        catch (Exception ex)
        //        {
        //            justAuthenticated = false;
        //            //if (ex is MobileServiceInvalidOperationException)
        //            //    Debug.WriteLine(ex.InnerException.ToString());

        //            // Remove the credential with the expired token.
        //            if (settings != null)
        //            {
        //                settings.Remove(provider);
        //                settings.Clear();
        //            }
        //            if (Todo.App.Database.mobileServiceUser != null)
        //                Todo.App.Database.mobileServiceUser = null;
        //            if (Todo.App.Database.client != null)
        //                Todo.App.Database.client.CurrentUser = null;

        //        }
        //    }
        //    else
        //    {
        //        try
        //        {
        //            // Login with the identity provider.
        //            //Todo.App.Database.mobileServiceUser = await Todo.App.Database.client.LoginAsync(provider);

        //            Todo.App.Database.mobileServiceUser =
        //            await Todo.App.Database.client.
        //                LoginAsync(MobileServiceAuthenticationProvider.MicrosoftAccount);

        //            //var test = await Todo.App.Database.client.GetTable<Item>().Take(1).ToListAsync();
        //            var test2 = await Todo.App.Database.client.InvokeApiAsync("userInfo", HttpMethod.Get, null);

        //            justAuthenticated = true;

        //            if (useToken)
        //            {
        //                // Serialize the user into an array of bytes and encrypt with DPAPI.
        //                var userBytes = System.Text.Encoding.Unicode
        //                    .GetBytes(JsonConvert.SerializeObject(Todo.App.Database.mobileServiceUser));
        //                byte[] encryptedUser = ProtectedData.Protect(userBytes, entropy);

        //                // Store the encrypted user credentials in local settings.
        //                settings.Add(provider, encryptedUser);
        //                settings.Save();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            justAuthenticated = false;
        //            //if (ex is MobileServiceInvalidOperationException)
        //            //    Debug.WriteLine(ex.InnerException.ToString());

        //            // Remove the credential with the expired token.
        //            if (settings != null)
        //            {
        //                settings.Remove(provider);
        //                settings.Clear();
        //            }
        //            if (Todo.App.Database.mobileServiceUser != null)
        //                Todo.App.Database.mobileServiceUser = null;
        //            if (Todo.App.Database.client != null)
        //                Todo.App.Database.client.CurrentUser = null;

        //        }


        //        //Todo.App.Database.mobileServiceUser =
        //        //await Todo.App.Database.client.
        //        //    LoginAsync(MobileServiceAuthenticationProvider.MicrosoftAccount);


        //    }

        //    if (justAuthenticated)
        //    {
        //        try
        //        {
        //            await Todo.App.Database.InitLocalStoreAsync();
        //            await Todo.App.Database.newUser(Todo.App.Database.mobileServiceUser.UserId);
        //            await Todo.App.Database.OnRefreshItemsSelected(); // pull database tables

        //            await Todo.App.Database.getContactsThatUseApp();

        //            //message = string.Format("You are now logged in - {0}", Todo.App.Database.mobileServiceUser.UserId);
        //            //MessageBox.Show(message);
        //        }
        //        catch (Exception ex)
        //        {
        //            justAuthenticated = false;
        //            //if (ex is MobileServiceInvalidOperationException)
        //            //    Debug.WriteLine(ex.InnerException.ToString());

        //            // Remove the credential with the expired token.
        //            if (settings != null)
        //            {
        //                settings.Remove(provider);
        //                settings.Clear();
        //            }
        //            if (Todo.App.Database.mobileServiceUser != null)
        //                Todo.App.Database.mobileServiceUser = null;
        //            if (Todo.App.Database.client != null)
        //                Todo.App.Database.client.CurrentUser = null;

        //        }

                
        //    }



            
        //}

        async Task MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            //await Authenticate();

            // Try to use the latest used oauth provider           
            if (settings.Contains("LastUsedProvider"))
            {
                MobileServiceAuthenticationProvider provider = (MobileServiceAuthenticationProvider)settings["LastUsedProvider"];
                var auth = new Authenticate_WinPhone();
                await auth.Authenticate(provider);
            }
            else
            {
                await Todo.App.Navigation.PushModalAsync(new Todo.Views.SelectLoginProviderPage());
            }

            await Todo.App.importantDPage.Refresh();

            ////refresh the page if just Authenticated, to update the items/groups
            //if (justAuthenticated)
            //{
            //    //Content = Todo.App.GetMainPage().ConvertPageToUIElement(this); // Refresh items
            //    await Todo.App.importantDPage.Refresh();
            //    justAuthenticated = false;
            //}
        }

        public MainPage()
        {
            InitializeComponent();

            Forms.Init();

            Todo.App.createDatabase();

            this.Loaded += (async (o, e) =>
                {  
                    await MainPage_Loaded(o,e);
                }); // when loaded authenticate

            LoadApplication(new Todo.App()); // new in 1.3
            //Content = Todo.App.GetMainPage().ConvertPageToUIElement(this);
        }
    }
}
