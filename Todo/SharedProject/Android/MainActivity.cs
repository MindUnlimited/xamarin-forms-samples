using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Xamarin.Forms.Platform.Android;
using Xamarin.Contacts;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using System.Net.Http;
using Android.Content.PM;


namespace Todo.Android
{
	[Activity (Label = "Mind Unlimited", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = 
        ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity // superclass new in 1.3
	{
        private bool justAuthenticated = false;
        private MobileServiceUser mobServiceUser = new MobileServiceUser(null);

        private async Task Authenticate()
        {
            mobServiceUser = Todo.App.Database.mobileServiceUser;
            while (mobServiceUser == null)
            {
                try
                {
                    mobServiceUser = await Todo.App.Database.client.LoginAsync(this, MobileServiceAuthenticationProvider.MicrosoftAccount);
                    //mobServiceUser = await Todo.App.Database.client.
                    //    LoginAsync(this, MobileServiceAuthenticationProvider.MicrosoftAccount);

                    await Todo.App.Database.InitLocalStoreAsync();
                    await Todo.App.Database.newUser(mobServiceUser.UserId);
                    Todo.App.Database.OnRefreshItemsSelected(); // pull database tables
                    CreateAndShowDialog(string.Format("you are now logged in - {0}", mobServiceUser.UserId), "Logged in!");
                    justAuthenticated = true;


                }
                catch (Exception ex)
                {
                    CreateAndShowDialog(ex, "Authentication failed");
                }
            }
            Todo.App.Database.mobileServiceUser = mobServiceUser;
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

