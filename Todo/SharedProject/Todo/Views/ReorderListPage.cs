using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace Todo.Views
{
    public class ReorderListPage : View
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Item> _items;
        public ObservableCollection<Item> Items
        {
            get { return _items; }
            set { _items = value; OnPropertyChanged("Items"); } 
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public ReorderListPage()
        {

        }

        public ReorderListPage(List<Item> itemList)
		{
            foreach(Item it in itemList)
            {
                Items.Add(it);
            }

			// rendering of this page is done natively on each platform, otherwise it will just be blank
		}
    }
}
