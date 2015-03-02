using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Xamarin.Forms.Platform.Android;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using System.Net.Http;


namespace Todo.Android
{
	[Activity (Label = "Todo.Android.Android", MainLauncher = true)]
	public class MainActivity : AndroidActivity
	{
        private bool justAuthenticated = false;

        private async Task Authenticate()
        {
            while (Todo.App.Database.mobileServiceUser == null)
            {
                try
                {
                    Todo.App.Database.mobileServiceUser = await App.Database.client.
                        LoginAsync(this, MobileServiceAuthenticationProvider.MicrosoftAccount);

                    await Todo.App.Database.InitLocalStoreAsync();
                    await Todo.App.Database.newUser(Todo.App.Database.mobileServiceUser.UserId);
                    Todo.App.Database.OnRefreshItemsSelected(); // pull database tables
                    CreateAndShowDialog(string.Format("you are now logged in - {0}", Todo.App.Database.mobileServiceUser.UserId), "Logged in!");
                    justAuthenticated = true;


                }
                catch (Exception ex)
                {
                    CreateAndShowDialog(ex, "Authentication failed");
                }
            }
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
            //Task.Run(() => { Authenticate(); }).Wait(); //task.run part is necessary, behaves as await     

            SetPage(App.GetMainPage());
		}

        protected override void OnStart()
        {
            base.OnStart();
            // something to refresh the page
        }
	}
}

