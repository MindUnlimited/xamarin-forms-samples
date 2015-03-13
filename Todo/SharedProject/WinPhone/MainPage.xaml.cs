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
using Xamarin.Contacts;
using System.IO;
using Windows.Storage;
using Microsoft.WindowsAzure.MobileServices;
using System.Net.Http;



namespace Todo.WinPhone
{
    public partial class MainPage : global::Xamarin.Forms.Platform.WinPhone.FormsApplicationPage // superclass new in 1.3
    {
        private bool justAuthenticated = false;

        private async System.Threading.Tasks.Task Authenticate()
        {
            while (!justAuthenticated)
            {
                string message;
                try
                {
                    Todo.App.Database.mobileServiceUser = 
                    await Todo.App.Database.client.
                        LoginAsync(MobileServiceAuthenticationProvider.MicrosoftAccount);

                    await Todo.App.Database.InitLocalStoreAsync();
                    await Todo.App.Database.newUser(Todo.App.Database.mobileServiceUser.UserId);
                    Todo.App.Database.OnRefreshItemsSelected(); // pull database tables
                    
                    message =
                        string.Format("You are now logged in - {0}", Todo.App.Database.mobileServiceUser.UserId);
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
            if (Todo.App.Database.mobileServiceUser == null)
                await Authenticate();

            // refresh the page if just Authenticated, to update the items/groups
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

            LoadApplication(new Todo.App()); // new in 1.3
            //Content = Todo.App.GetMainPage().ConvertPageToUIElement(this);
        }
    }
}
