using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Todo.Views
{
    public class ContactsPage : ContentPage
    {
        public ContactsPage()
        {
            ListView contactsLV = new ListView();
            contactsLV.ItemsSource = Todo.App.Database.contacts;

            Content = contactsLV;
        }
    }
}
