using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Xamarin.Forms.Platform.Android;
using Xamarin.Contacts;
using Xamarin.Auth;

using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using System.Net.Http;
using Android.Content.PM;
using System.Collections.Generic;
using System.Linq;

using Gcm.Client; // google cloud messaging (push)


namespace Todo.Android
{
	[Activity (Label = "MindSet", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = 
        ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity // superclass new in 1.3
	{
        private bool justAuthenticated = false;
        private MobileServiceUser mobServiceUser;// = new MobileServiceUser(null);
        AccountStore accountStore; // for saving the authentication token
        Account currentAccount; // for logout purposes

        private async Task Authenticate()
        {
            //mobServiceUser = Todo.App.Database.mobileServiceUser;
            accountStore = AccountStore.Create(this); 

            while (mobServiceUser == null)
            {
                try
                {
                    // Log in 
                    var accounts = accountStore.FindAccountsForService ("Microsoft").ToArray();
                    if (accounts.Length != 0)
                    {
                        mobServiceUser = new MobileServiceUser (accounts[0].Username);
                        mobServiceUser.MobileServiceAuthenticationToken = accounts[0].Properties["token"];

                        Todo.App.Database.client.CurrentUser = mobServiceUser;
                    }
                    else
                    {
                        //// Regular login flow
                        //user = new MobileServiceuser( await client
                        //    .LoginAsync(MobileServiceAuthenticationProvider.Facebook, token);
                        //var token = new JObject();
                        //// Replace access_token_value with actual value of your access token
                        //token.Add("access_token", "access_token_value");

                        mobServiceUser = await Todo.App.Database.client.LoginAsync(this, MobileServiceAuthenticationProvider.MicrosoftAccount);

                        // After logging in
                        currentAccount = new Account(mobServiceUser.UserId, new Dictionary<string, string> { { "token", mobServiceUser.MobileServiceAuthenticationToken } });
                        accountStore.Save(currentAccount, "Microsoft");
                    }

                    Todo.App.Database.mobileServiceUser = mobServiceUser;
                    

                    CreateAndShowDialog(string.Format("you are now logged in - {0}", mobServiceUser.UserId), "Logged in!");
                    justAuthenticated = true;

                    await Todo.App.Database.InitLocalStoreAsync();
                    await Todo.App.Database.newUser(mobServiceUser.UserId);
                    await Todo.App.Database.OnRefreshItemsSelected(); // pull database tables
                }
                catch (Exception ex)
                {
                    CreateAndShowDialog(ex, "Authentication failed");
                }
            }
            

            //// Log out
            //client.Logout();
            //accountStore.Delete(account, "Facebook");
        }

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

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Xamarin.Forms.Forms.Init (this, bundle);

            //var mainpage = App.GetMainPage();

            Todo.App.createDatabase();

            Authenticate();
            //Task.Run(() => { Authenticate(); LoadApplication(new App()); }).Wait(); //task.run part is necessary, behaves as await     

            //SetPage(App.GetMainPage());
            LoadApplication(new App()); // method is new in 1.3
		}

        protected override void OnStart()
        {
            base.OnStart();
        }
	}
}

