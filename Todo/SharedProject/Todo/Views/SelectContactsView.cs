using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Todo.Models;
using Xamarin.Forms;

namespace Todo.Views
{
    public class SelectContactsView : View
    {
        ////Bindable property for the Reorder option
        //public static readonly BindableProperty ReorderProperty =
        //  BindableProperty.Create<ReorderListView, Boolean>(p => p.ReorderEnabled, false);
        ////Gets or sets the color of the reorder option
        //public Boolean ReorderEnabled
        //{
        //    get { return (Boolean)GetValue(ReorderProperty); }
        //    set { SetValue(ReorderProperty, value); }
        //}

        //Bindable property for the itemcollection
        public static readonly BindableProperty contactsProperty =
          BindableProperty.Create<SelectContactsView, List<User>>(p => p.ContactsCollection, new List<User>());
        //Gets or sets the itemcollection
        public List<User> ContactsCollection
        {
            get { return (List<User>)GetValue(contactsProperty); }
            set { SetValue(contactsProperty, value); }
        }

        public void addContacts(IEnumerable<User> itemList)
        {
            if (ContactsCollection == null)
                ContactsCollection = new List<User>();

            foreach (User it in itemList)
            {
                ContactsCollection.Add(it);
            }
        }


        //public event PropertyChangedEventHandler PropertyChanged;

        //private ObservableCollection<Item> _items;
        //public ObservableCollection<Item> Items
        //{
        //    get { return _items; }
        //    set { _items = value; OnPropertyChanged("Items"); }
        //}

        //private bool _reorderEnabled;
        //public bool ReorderEnabled
        //{
        //    get { return _reorderEnabled; }
        //    set { _reorderEnabled = value; OnPropertyChanged("ReorderEnabled"); }
        //}

        //protected virtual void OnPropertyChanged(string propertyName)
        //{
        //    if (PropertyChanged == null)
        //        return;

        //    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //}

        //public void addItems(IEnumerable<Item> itemList)
        //{
        //    if (Items == null)
        //        Items = new ObservableCollection<Item>();

        //    foreach (Item it in itemList)
        //    {
        //        Items.Add(it);
        //    }
        //}

        //public ReorderListView()
        //{
        //    ObservableCollection<Item> _items = new ObservableCollection<Item>();
        //}

        //public ReorderListView(IEnumerable<Item> itemList)
        //{
        //    ObservableCollection<Item> _items = new ObservableCollection<Item>();

        //    foreach(Item it in itemList)
        //    {
        //        Items.Add(it);
        //    }

        //    // rendering of this page is done natively on each platform, otherwise it will just be blank
        //}
    }
}
