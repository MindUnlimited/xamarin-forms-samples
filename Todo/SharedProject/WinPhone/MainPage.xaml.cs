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
        private bool justAuthenticated;

        private async System.Threading.Tasks.Task Authenticate()
        {
            while (user == null)
            {
                string message;
                try
                {
                    user = await Todo.App.Database.client.
                        LoginAsync(MobileServiceAuthenticationProvider.MicrosoftAccount);
                    await Todo.App.Database.InitLocalStoreAsync();
                    await Todo.App.Database.newUser(user.UserId);
                    Todo.App.Database.OnRefreshItemsSelected(); // pull database tables
                    
                    message =
                        string.Format("You are now logged in - {0}", user.UserId);
                    justAuthenticated = true;
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
            //await Todo.App.Database.getTables();
            if (justAuthenticated)
            {
                Content = Todo.App.GetMainPage().ConvertPageToUIElement(this); // Refresh items
                justAuthenticated = false;
            }
        }

        public MainPage()
        {
            InitializeComponent();

            Forms.Init();

            Todo.App.createDatabase();

            this.Loaded += MainPage_Loaded; // when loaded authenticate

            Content = Todo.App.GetMainPage().ConvertPageToUIElement(this);
        }
    }
}
