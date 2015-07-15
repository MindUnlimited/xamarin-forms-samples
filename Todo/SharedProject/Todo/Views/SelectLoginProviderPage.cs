using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace Todo.Views
{
    public class SelectLoginProviderPage : ContentPage
    {
        public SelectLoginProviderPage()
        {
            Image googleButton = new Image { Source = "Red-signin_Long_base_44dp.png"};
            var gGestureRecognizer = new TapGestureRecognizer();
            gGestureRecognizer.Tapped += async (s, e) =>
            {
                await DependencyService.Get<IAuthenticate>().Authenticate(Microsoft.WindowsAzure.MobileServices.MobileServiceAuthenticationProvider.Google);
                await Navigation.PopAsync();
            };
            googleButton.GestureRecognizers.Add(gGestureRecognizer);


            Image facebookButton = new Image { Source = "facebook_fat.png"};
            var fbGestureRecognizer = new TapGestureRecognizer();
            fbGestureRecognizer.Tapped += async (s, e) =>
            {
                await DependencyService.Get<IAuthenticate>().Authenticate(Microsoft.WindowsAzure.MobileServices.MobileServiceAuthenticationProvider.Facebook);
                await Navigation.PopAsync();
            };
            facebookButton.GestureRecognizers.Add(fbGestureRecognizer);

            Button microsoftButton = new Button { Text = "Sign in with Microsoft", WidthRequest = googleButton.Width};
            microsoftButton.Clicked += async (o, e) => 
            { 
                await DependencyService.Get<IAuthenticate>().Authenticate(Microsoft.WindowsAzure.MobileServices.MobileServiceAuthenticationProvider.MicrosoftAccount);
                await Navigation.PopAsync();
            };

            Content = new StackLayout { Children = { facebookButton, googleButton, microsoftButton } , VerticalOptions = LayoutOptions.EndAndExpand, HorizontalOptions = LayoutOptions.Center, Spacing = 10, Padding = 30 };
        }

    }
}
