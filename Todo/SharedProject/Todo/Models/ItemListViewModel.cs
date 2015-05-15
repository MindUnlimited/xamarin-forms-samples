using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Linq;
using Xamarin.Forms;

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

        public StackLayout Footer 
        {
            get
            {
                var footer = new StackLayout { Spacing = 2 };
                footer.Children.Add(new Label { Text = Reports != null ? Reports.Count.ToString() + " items" : "0 items" });
                return footer;
            } 
        }

        public static void Sort<T>(ObservableCollection<T> collection, Func<T, int> keySelector)
        {
            List<T> sorted = collection.OrderBy(keySelector).ToList();
            for (int i = 0; i < sorted.Count(); i++)
                collection.Move(collection.IndexOf(sorted[i]), i);
        }

        public void FilterAndSort(Todo.App.DomainPages domainPage)
        {
            if (domainPage == Todo.App.DomainPages.Completed)
                FilterOn(domainPage);
            else
                SortOn(domainPage);
        }

        public void FilterOn(Todo.App.DomainPages filterOn)
        {
            switch (filterOn)
            {
                case App.DomainPages.Completed:
                    int j = 0;
                    while (j < _reports.Count())
                    {
                        if (_reports[j].Status != 7) // status 7 means completed
                            _reports.RemoveAt(j);
                        else
                            j += 1;
                    }
                    //_reports = (ObservableCollection<Item>)_reports.Where(it => it.Status == 7);
                    break;
                default:
                    break;
            }
        }

        public void SortOn(Todo.App.DomainPages sortOn)
        {

            switch (sortOn)
            {
                case App.DomainPages.Important:
                    List<Item> importantSorted = _reports.OrderByDescending(it => it.Importance).ToList();
                    for (int i = 0; i < importantSorted.Count(); i++)
                        _reports.Move(_reports.IndexOf(importantSorted[i]), i);
                    break;
                case App.DomainPages.Urgent:
                    List<Item> urgentSorted = _reports.OrderByDescending(it => it.Urgency).ToList();
                    for (int i = 0; i < urgentSorted.Count(); i++)
                        _reports.Move(_reports.IndexOf(urgentSorted[i]), i);
                    break;
                case App.DomainPages.Current:
                    List<Item> currentSorted = _reports.OrderByDescending(it => it.EndDate).ToList();
                    for (int i = 0; i < currentSorted.Count(); i++)
                        _reports.Move(_reports.IndexOf(currentSorted[i]), i);
                    break;
                default:
                    break;
            }
        }

        public ItemListViewModel(Item domain)
        {
            Reports = new ObservableCollection<Item>();

            var childElements = Todo.App.Database.GetChildItems(domain);
            if (childElements != null)
            {
                foreach (Item it in childElements.Result)
                {
                    Reports.Add(it);
                }
            }
            else
                Reports = null;

            //var domains = (List<Item>) Todo.App.Database.GetDomains().Result;
            //var personal = domains[0];
            //var friends = domains[1];
            //var work = domains[2];
            //var community = domains[3];

            //if (domain.Equals("personal"))
            //{
            //    var childElements = Todo.App.Database.GetChildItems(personal);
            //    if (Todo.App.Database.GetChildItems(personal) != null)
            //    {
            //        foreach (Item it in childElements.Result)
            //        {
            //            Reports.Add(it);
            //        }
            //    }
            //}
            //else if (domain.Equals("friends"))
            //{
            //    var childElements = Todo.App.Database.GetChildItems(friends);
            //    if (childElements != null)
            //    {
            //        foreach (Item it in childElements.Result)
            //        {
            //            Reports.Add(it);
            //        }
            //    }
            //}
            //else if (domain.Equals("work"))
            //{
            //    var childElements = Todo.App.Database.GetChildItems(work);
            //    if (Todo.App.Database.GetChildItems(personal) != null)
            //    {
            //        foreach (Item it in childElements.Result)
            //        {
            //            Reports.Add(it);
            //        }
            //    }
            //}
            //else if (domain.Equals("community"))
            //{
            //    var childElements = Todo.App.Database.GetChildItems(community);
            //    if (Todo.App.Database.GetChildItems(personal) != null)
            //    {
            //        foreach (Item it in childElements.Result)
            //        {
            //            Reports.Add(it);
            //        }
            //    }
            //}
            //else
            //    Reports = null;
        }
    }
}