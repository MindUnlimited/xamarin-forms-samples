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
            Image mindSetLogo = new Image { Source = "LogoMindSet128x128.png", HorizontalOptions = LayoutOptions.Center};
            Label mindSetLabel = new Label { Text = "MindSet", FontSize = 50, HorizontalOptions = LayoutOptions.Center };

            Image googleButton = new Image { Source = "SignInGoogle.png"};
            var gGestureRecognizer = new TapGestureRecognizer();
            gGestureRecognizer.Tapped += async (s, e) =>
            {
                await DependencyService.Get<IAuthenticate>().Authenticate(Microsoft.WindowsAzure.MobileServices.MobileServiceAuthenticationProvider.Google);
                IsBusy = true;
                await Todo.App.Navigation.PopModalAsync();
                IsBusy = false;
            };
            googleButton.GestureRecognizers.Add(gGestureRecognizer);


            Image facebookButton = new Image { Source = "facebook_fat.png"};
            var fbGestureRecognizer = new TapGestureRecognizer();
            fbGestureRecognizer.Tapped += async (s, e) =>
            {
                await DependencyService.Get<IAuthenticate>().Authenticate(Microsoft.WindowsAzure.MobileServices.MobileServiceAuthenticationProvider.Facebook);
                IsBusy = true;
                await Todo.App.Navigation.PopModalAsync();
                IsBusy = false;
            };
            facebookButton.GestureRecognizers.Add(fbGestureRecognizer);

            Button microsoftButton = new Button { Text = "Sign in with Microsoft", WidthRequest = googleButton.Width, HorizontalOptions = LayoutOptions.Center};
            microsoftButton.Clicked += async (o, e) => 
            { 
                await DependencyService.Get<IAuthenticate>().Authenticate(Microsoft.WindowsAzure.MobileServices.MobileServiceAuthenticationProvider.MicrosoftAccount);
                IsBusy = true;
                await Todo.App.Navigation.PopModalAsync();
                IsBusy = false;
            };


            
            StackLayout mindSet = new StackLayout { Children = { mindSetLogo, mindSetLabel }, HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.CenterAndExpand };
            StackLayout oauthProviders = new StackLayout { Children = { facebookButton, googleButton, microsoftButton }, VerticalOptions = LayoutOptions.EndAndExpand, HorizontalOptions = LayoutOptions.Center, Spacing = 10 };

            RelativeLayout relativeLayout = new RelativeLayout { HorizontalOptions = LayoutOptions.Center, Padding = 60};

            relativeLayout.Children.Add(mindSet,
            heightConstraint: Constraint.RelativeToParent((parent) => {
                return parent.Height / 3 * 2;
            }),
            widthConstraint: Constraint.RelativeToParent((parent) => {
                return parent.Width;
            }),
            yConstraint: Constraint.Constant(60)
            );

            relativeLayout.Children.Add(oauthProviders,
            xConstraint: Constraint.Constant(0),
            yConstraint: Constraint.RelativeToParent((parent) => {
                return parent.Height / 3 * 2;
            }),
            widthConstraint: Constraint.RelativeToParent((parent) => {
                return parent.Width;
            }));

            

            Content = relativeLayout;
        }

    }
}
