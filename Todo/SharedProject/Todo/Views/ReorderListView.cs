using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace Todo.Views
{
    public class ReorderListView : View
    {
        //Bindable property for the progress color
        public static readonly BindableProperty ReorderProperty =
          BindableProperty.Create<ReorderListView, Boolean>(p => p.ReorderEnabled, false);
        //Gets or sets the color of the progress bar
        public Boolean ReorderEnabled
        {
            get { return (Boolean)GetValue(ReorderProperty); }
            set { SetValue(ReorderProperty, value); }
        }

        //Bindable property for the progress color
        public static readonly BindableProperty ItemsProperty =
          BindableProperty.Create<ReorderListView, ObservableCollection<Item>>(p => p.ItemCollection, new ObservableCollection<Item>());
        //Gets or sets the color of the progress bar
        public ObservableCollection<Item> ItemCollection
        {
            get { return (ObservableCollection<Item>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        public void addItems(IEnumerable<Item> itemList)
        {
            if (ItemCollection == null)
                ItemCollection = new ObservableCollection<Item>();

            foreach (Item it in itemList)
            {
                ItemCollection.Add(it);
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
