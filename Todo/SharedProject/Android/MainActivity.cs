using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Contacts;
using Xamarin.Auth;

using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using System.Net.Http;
using Android.Content.PM;
using System.Collections.Generic;
using System.Linq;

using Gcm.Client;
using Android.Preferences;// google cloud messaging (push)

[assembly: Dependency(typeof(Todo.Android.MainActivity))]
namespace Todo.Android
{
	[Activity (Label = "MindSet", Icon = "@drawable/LogoMindSet128x128", MainLauncher = true, ConfigurationChanges = 
        ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity, Logout // superclass new in 1.3
	{
        private bool justAuthenticated = false;
        //private MobileServiceUser mobServiceUser;// = new MobileServiceUser(null);
        AccountStore accountStore; // for saving the authentication token
        
        Account currentAccount; // for logout purposes
        string currentServiceId = "Microsoft"; // for logout purposes

        // NEEDED FOR PUSH

        // Create a new instance field for this activity.
        static MainActivity instance = new MainActivity();

        // Return the current activity instance.
        public static MainActivity CurrentActivity
        {
            get
            {
                return instance;
            }
        }

        // Return the Mobile Services client.
        public MobileServiceClient CurrentClient
        {
            get
            {
                return Todo.App.Database.client;
            }
        }




        //private async Task Authenticate()
        //{
        //    //mobServiceUser = Todo.App.Database.mobileServiceUser;
        //    accountStore = AccountStore.Create(this);
        //    currentAccount = null;
        //    bool useToken = true;

        //    try
        //    {
        //        var accounts = accountStore.FindAccountsForService("Microsoft").ToArray();
        //        // Log in 
        //        if (useToken)
        //        {
        //            if (accounts.Length != 0)
        //            {
        //                Todo.App.Database.mobileServiceUser = new MobileServiceUser(accounts[0].Username);
        //                Todo.App.Database.mobileServiceUser.MobileServiceAuthenticationToken = accounts[0].Properties["token"];

        //                Todo.App.Database.client.CurrentUser = Todo.App.Database.mobileServiceUser;
        //            }
        //        }
        //        if (Todo.App.Database.mobileServiceUser != null) // Set the user from the stored credentials.
        //        {
        //            Todo.App.Database.client.CurrentUser = Todo.App.Database.mobileServiceUser;
        //            //App.MobileService.CurrentUser = user;

        //            try
        //            {
        //                // Try to return an item now to determine if the cached credential has expired.
        //                //var test = await Todo.App.Database.client.GetTable<Item>().Take(1).ToListAsync();
        //                var userInfo = await Todo.App.Database.client.InvokeApiAsync("userInfo", HttpMethod.Get, null);

        //                //CreateAndShowDialog(string.Format("you are now logged in - {0}", Todo.App.Database.mobileServiceUser.UserId), "Logged in!");
        //                justAuthenticated = true;
        //                currentAccount = accounts[0];

        //                await Todo.App.Database.InitLocalStoreAsync();
        //                await Todo.App.Database.newUser(Todo.App.Database.mobileServiceUser.UserId);
        //                await Todo.App.Database.OnRefreshItemsSelected(); // pull database tables                 
        //            }
        //            catch (MobileServiceInvalidOperationException ex)
        //            {
        //                //System.Diagnostics.Debug.WriteLine(ex.InnerException.ToString());
        //                if (ex.Response.StatusCode == System.Net.HttpStatusCode.Unauthorized || ex.Response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
        //                {
        //                    // Remove the credential with the expired token.
        //                    accountStore.Delete(accounts[0], "Microsoft");
        //                    Todo.App.Database.mobileServiceUser = null;
        //                    Todo.App.Database.client.CurrentUser = null;
        //                    justAuthenticated = false;
        //                }
        //            }
        //        }
        //        else // Regular login flow
        //        {
        //            //user = new MobileServiceuser( await client
        //            //    .LoginAsync(MobileServiceAuthenticationProvider.Facebook, token);
        //            //var token = new JObject();
        //            //// Replace access_token_value with actual value of your access token
        //            //token.Add("access_token", "access_token_value");

        //            Todo.App.Database.mobileServiceUser = await Todo.App.Database.client.LoginAsync(this, MobileServiceAuthenticationProvider.MicrosoftAccount);

        //            if (useToken)
        //            {
        //                // After logging in
        //                currentAccount = new Account(Todo.App.Database.mobileServiceUser.UserId, new Dictionary<string, string> { { "token", Todo.App.Database.mobileServiceUser.MobileServiceAuthenticationToken } });
        //                accountStore.Save(currentAccount, "Microsoft");
        //            }

        //            //CreateAndShowDialog(string.Format("you are now logged in - {0}", Todo.App.Database.mobileServiceUser.UserId), "Logged in!");
        //            justAuthenticated = true;

        //            await Todo.App.Database.InitLocalStoreAsync();
        //            await Todo.App.Database.newUser(Todo.App.Database.mobileServiceUser.UserId);
        //            await Todo.App.Database.OnRefreshItemsSelected(); // pull database tables
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        CreateAndShowDialog(ex, "Authentication failed");
        //    }
            

        //    //// Log out
        //    //client.Logout();
        //    //accountStore.Delete(account, "Facebook");
        //}

        void CreateAndShowDialog(Exception exception, String title)
        {
            CreateAndShowDialog(exception.Message, title);
        }

        void CreateAndShowDialog(string message, string title)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);

            builder.SetMessage(message);
            builder.SetTitle(title);
            builder.Create().Show();
        }

		protected async override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Xamarin.Forms.Forms.Init (this, bundle);



            //var mainpage = App.GetMainPage();

            Todo.App.createDatabase();

            LoadApplication(new App()); // method is new in 1.3

            //await Todo.App.selectedDomainPage.Navigation.PushModalAsync(loginpage);

            //await Todo.App.Navigation.PushModalAsync(loginpage);
            //Debug.WriteLine("The modal page is now on screen");
            //var poppedPage = await Navigation.PopModalAsync();
            //Debug.WriteLine("The modal page is dismissed");

            //await App.Navigation.PushModalAsync(loginpage);
            //Todo.App.Navigation.PushAsync(loginpage);
            //Todo.App.selectedDomainPage.Navigation.PushAsync(new Todo.Views.SelectLoginProviderPage());

            //PushAsync(new Todo.Views.SelectLoginProviderPage());

            //await Todo.App.Navigation.PushAsync(new Todo.Views.SelectLoginProviderPage());

            //Task.Run(() => { Authenticate(); LoadApplication(new App()); }).Wait(); //task.run part is necessary, behaves as await     

            //// PUSH
            //// Set the current instance of TodoActivity.
            //instance = this;

            //// Make sure the GCM client is set up correctly.
            //GcmClient.CheckDevice(this);
            //GcmClient.CheckManifest(this);

            //// Register the app for push notifications.
            //GcmClient.Register(this, ToDoBroadcastReceiver.senderIDs);

            //var todoItemTable = Todo.App.Database.client.GetSyncTable<TodoItem>();
            //var item = new TodoItem();

            //item.Text = "Push test!";
            //todoItemTable.InsertAsync(item);

            //Todo.App.Database.client.SyncContext.PushAsync();

            //SetPage(App.GetMainPage());
            //LoadApplication(new App()); // method is new in 1.3
            //Todo.App.domainPage.Refresh();
        }

        protected async override void OnStart()
        {
            base.OnStart();
            //Todo.App.Navigation.PushModalAsync(new Todo.Views.SelectLoginProviderPage());
            //Todo.App.domainPage.Refresh();

            // Shared Preferences are the local saved value for the app. Used here to access the last used provider
            var preferences = PreferenceManager.GetDefaultSharedPreferences(this);

            // Try to use the latest used oauth provider           
            if (preferences.Contains("LastUsedProvider"))
            {
                string providerName = preferences.GetString("LastUsedProvider", "");
                MobileServiceAuthenticationProvider provider;
                var auth = new Authenticate_Android();

                switch (providerName)
                {
                    case "Facebook":
                        provider = MobileServiceAuthenticationProvider.Facebook;
                        await auth.Authenticate(provider);
                        break;
                    case "Google":
                        provider = MobileServiceAuthenticationProvider.Google;
                        await auth.Authenticate(provider);
                        break;
                    case "MicrosoftAccount":
                        provider = MobileServiceAuthenticationProvider.MicrosoftAccount;
                        await auth.Authenticate(provider);
                        break;
                    case "Twitter":
                        provider = MobileServiceAuthenticationProvider.Twitter;
                        await auth.Authenticate(provider);
                        break;
                    case "WindowsAzureActiveDirectory":
                        provider = MobileServiceAuthenticationProvider.WindowsAzureActiveDirectory;
                        await auth.Authenticate(provider);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                await App.Navigation.PushModalAsync(new Views.SelectLoginProviderPage());
            }
        }

        public void Logout()
        {
            try
            {
                //Todo.App.Database.mobileServiceUser = null;
                //Todo.App.Database.client.CurrentUser = null;
                //justAuthenticated = false;

                //var accounts = accountStore.FindAccountsForService(currentServiceId).ToArray();
                //currentAccount = accounts[0];

                //accountStore.Delete(currentAccount, currentServiceId);

                //Todo.App.Database.client.Logout();
                //Authenticate();

            }
            catch (Exception ex)
            {
                CreateAndShowDialog(ex, "Logout failed");
            }
        }
    }
}

