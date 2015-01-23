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
using System.IO;
using Windows.Storage;
using Microsoft.WindowsAzure.MobileServices;


namespace Todo.WinPhone
{
    public partial class MainPage : PhoneApplicationPage
    {
        private MobileServiceUser user;
        private async System.Threading.Tasks.Task Authenticate()
        {
            while (user == null)
            {
                string message;
                try
                {
                    user = await Todo.App.Database.client.
                        LoginAsync(MobileServiceAuthenticationProvider.MicrosoftAccount);
                    message =
                        string.Format("You are now logged in - {0}", user.UserId);
                }
                catch (InvalidOperationException)
                {
                    message = "You must log in. Login Required";
                }

                MessageBox.Show(message);
            }
        }

        async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            await Authenticate();
            //RefreshTodoItems();
        }

        public MainPage()
        {
            InitializeComponent();

            Forms.Init();

            this.Loaded += MainPage_Loaded; // when loaded authenticate

            Content = Todo.App.GetMainPage().ConvertPageToUIElement(this);
        }
    }
}
