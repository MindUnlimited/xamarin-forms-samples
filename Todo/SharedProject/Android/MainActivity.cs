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


namespace Todo.Android
{
	[Activity (Label = "Todo.Android.Android", MainLauncher = true)]
	public class MainActivity : AndroidActivity
	{
        private MobileServiceUser user;

        private async Task Authenticate()
        {
            try
            {
                user = await App.Database.client.
                    LoginAsync(this, MobileServiceAuthenticationProvider.MicrosoftAccount);
                //CreateAndShowDialog(string.Format("you are now logged in - {0}", user.UserId), "Logged in!");
                await App.Database.
                    newUser(user.UserId);
            }
            catch (Exception ex)
            {
                //CreateAndShowDialog(ex, "Authentication failed");
            }
        }

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Xamarin.Forms.Forms.Init (this, bundle);

            var mainpage = App.GetMainPage();

            //Authenticate();
            Task.Run(async () => { await Authenticate(); }); //task.run part is necessary, behaves as await

			SetPage (mainpage);
		}
	}
}

