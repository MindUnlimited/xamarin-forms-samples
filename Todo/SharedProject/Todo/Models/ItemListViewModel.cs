using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace Todo.Models
{
    public class ItemListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<Item> _reports;
        public ObservableCollection<Item> Reports
        {
            get { return _reports; }
            set { _reports = value; OnPropertyChanged("Reports"); } 
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public ItemListViewModel(string domain)
        {
            Reports = new ObservableCollection<Item>();

            var domains = (List<Item>) Todo.App.Database.GetDomains().Result;
            var personal = domains[0];
            var friends = domains[1];
            var work = domains[2];
            var community = domains[3];

            if (domain.Equals("personal"))
            {
                var childElements = Todo.App.Database.GetChildItems(personal);
                if (Todo.App.Database.GetChildItems(personal) != null)
                {
                    foreach (Item it in childElements.Result)
                    {
                        Reports.Add(it);
                    }
                }
            }
            else if (domain.Equals("friends"))
            {
                var childElements = Todo.App.Database.GetChildItems(friends);
                if (childElements != null)
                {
                    foreach (Item it in childElements.Result)
                    {
                        Reports.Add(it);
                    }
                }
            }
            else if (domain.Equals("work"))
            {
                var childElements = Todo.App.Database.GetChildItems(work);
                if (Todo.App.Database.GetChildItems(personal) != null)
                {
                    foreach (Item it in childElements.Result)
                    {
                        Reports.Add(it);
                    }
                }
            }
            else if (domain.Equals("community"))
            {
                var childElements = Todo.App.Database.GetChildItems(community);
                if (Todo.App.Database.GetChildItems(personal) != null)
                {
                    foreach (Item it in childElements.Result)
                    {
                        Reports.Add(it);
                    }
                }
            }
            else
                Reports = null;
        }
    }
}