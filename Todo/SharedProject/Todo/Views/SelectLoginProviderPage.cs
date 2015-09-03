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
            Title = "Login";
            Image mindSetLogo = new Image { Source = "LogoMindSet128x128.png", HorizontalOptions = LayoutOptions.Center};
            Label mindSetLabel = new Label { Text = "MindSet", FontSize = 50, HorizontalOptions = LayoutOptions.Center };

            Image googleButton = new Image { Source = "SignInGoogle.png"};
            var gGestureRecognizer = new TapGestureRecognizer();
            gGestureRecognizer.Tapped += async (s, e) =>
            {
                await DependencyService.Get<IAuthenticate>().Authenticate(Microsoft.WindowsAzure.MobileServices.MobileServiceAuthenticationProvider.Google);

                if (Device.OS == TargetPlatform.WinPhone)
                {
                    if ( Todo.App.Database.mobileServiceUser != null)
                    {
                        IsBusy = true;

                        while(Todo.App.Navigation.ModalStack.Count > 1)
                            await Todo.App.Navigation.PopModalAsync(false);

                        IsBusy = false;
                    }
                }
                else
                {
                    IsBusy = true;
                    await Todo.App.Navigation.PopModalAsync(false);
                    IsBusy = false;
                }

                    //{

                    //}
                //}
                //catch(Microsoft.WindowsAzure.MobileServices.MobileServiceInvalidOperationException) { }



            };
            googleButton.GestureRecognizers.Add(gGestureRecognizer);


            Image facebookButton = new Image { Source = "facebook_fat.png"};
            var fbGestureRecognizer = new TapGestureRecognizer();
            fbGestureRecognizer.Tapped += async (s, e) =>
            {
                //try
                //{
                    await DependencyService.Get<IAuthenticate>().Authenticate(Microsoft.WindowsAzure.MobileServices.MobileServiceAuthenticationProvider.Facebook);

                    if (Device.OS == TargetPlatform.WinPhone)
                    {
                        if (Todo.App.Database.mobileServiceUser != null)
                        {
                            IsBusy = true;

                            while (Todo.App.Navigation.ModalStack.Count > 1)
                                await Todo.App.Navigation.PopModalAsync(false);

                            IsBusy = false;
                        }
                    }
                    else
                    {
                        IsBusy = true;
                        await Todo.App.Navigation.PopModalAsync(false);
                        IsBusy = false;
                    }
                //}
                //catch (Microsoft.WindowsAzure.MobileServices.MobileServiceInvalidOperationException)  {}
            };
            facebookButton.GestureRecognizers.Add(fbGestureRecognizer);

            Button microsoftButton = new Button { Text = "Sign in with Microsoft", WidthRequest = googleButton.Width, HorizontalOptions = LayoutOptions.Center};
            microsoftButton.Clicked += async (o, e) => 
            { 
                //try
                //{
                    await DependencyService.Get<IAuthenticate>().Authenticate(Microsoft.WindowsAzure.MobileServices.MobileServiceAuthenticationProvider.MicrosoftAccount);
                    if (Device.OS == TargetPlatform.WinPhone)
                    {
                        if (Todo.App.Database.mobileServiceUser != null)
                        {
                            IsBusy = true;

                            while (Todo.App.Navigation.ModalStack.Count > 1)
                                await Todo.App.Navigation.PopModalAsync(false);

                            IsBusy = false;
                        }
                    }
                    else
                    {
                        IsBusy = true;
                        await Todo.App.Navigation.PopModalAsync(false);
                        IsBusy = false;
                    }
                //}
                //catch (Microsoft.WindowsAzure.MobileServices.MobileServiceInvalidOperationException) { }
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
