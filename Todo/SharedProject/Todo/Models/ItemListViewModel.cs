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

        public static void Sort<T>(ObservableCollection<T> collection, List<T> sorted)
        {
            for (int i = 0; i < sorted.Count(); i++)
                collection.Move(collection.IndexOf(sorted[i]), i);
        }

        public List<Item> getSortedList(Todo.App.DomainPages sortOn)
        {
            List<Item> sorted = new List<Item>();
            Func<Item,int> integerSelector = null;
            Func<Item, string> stringSelector = null;

            switch (sortOn)
            {
                case App.DomainPages.Important:
                    integerSelector = (it => it.Importance);
                    break;
                case App.DomainPages.Urgent:
                    integerSelector = (it => it.Urgency);
                    break;
                case App.DomainPages.Current:
                    stringSelector = (it => it.EndDate);
                    break;
                case App.DomainPages.Completed:
                    stringSelector = (it => it.Name);
                    break;
                case App.DomainPages.Inbox:
                    stringSelector = (it => it.Name);
                    break;
                default:
                    break;
            }

            if (integerSelector != null)
            {
                IEnumerable<Item> goals = _reports.Where(x => x.Type == 2);
                List<Item> goals_sorted = goals.OrderByDescending(integerSelector).ToList();

                if (goals.Count() > 0)
                {
                    foreach (Item goal in goals_sorted)
                    {
                        sorted.Add(goal);

                        IEnumerable<Item> projects = _reports.Where(x => x.Type == 3 && x.Parent == goal.ID);

                        if (projects.Count() > 0)
                        {
                            List<Item> projects_sorted = projects.OrderByDescending(integerSelector).ToList();

                            foreach (Item project in projects_sorted)
                            {
                                sorted.Add(project);

                                IEnumerable<Item> tasks = _reports.Where(x => x.Type == 4 && x.Parent == project.ID);

                                if (tasks.Count() > 0)
                                {
                                    List<Item> tasks_sorted = tasks.OrderByDescending(integerSelector).ToList();
                                    sorted.AddRange(tasks_sorted);
                                }
                            }

                        }
                    }
                }
            }
            else if (stringSelector != null)
            {
                IEnumerable<Item> goals = _reports.Where(x => x.Type == 2);
                List<Item> goals_sorted = goals.OrderByDescending(stringSelector).ToList();

                if (goals.Count() > 0)
                {
                    foreach (Item goal in goals_sorted)
                    {
                        sorted.Add(goal);

                        IEnumerable<Item> projects = _reports.Where(x => x.Type == 3 && x.Parent == goal.ID);

                        if (projects.Count() > 0)
                        {
                            List<Item> projects_sorted = projects.OrderByDescending(stringSelector).ToList();

                            foreach (Item project in projects_sorted)
                            {
                                sorted.Add(project);

                                IEnumerable<Item> tasks = _reports.Where(x => x.Type == 4 && x.Parent == project.ID);

                                if (tasks.Count() > 0)
                                {
                                    List<Item> tasks_sorted = tasks.OrderByDescending(stringSelector).ToList();
                                    sorted.AddRange(tasks_sorted);
                                }
                            }

                        }
                    }
                }
            }

            return sorted;
        }

        public void FilterAndSort(Todo.App.DomainPages domainPage)
        {
            if (domainPage == Todo.App.DomainPages.Completed || domainPage == Todo.App.DomainPages.Inbox)
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
                case App.DomainPages.Inbox:
                    int k = 0;
                    while (k < _reports.Count())
                    {
                        if (_reports[k].Parent != null || _reports[k].Type == 1) // remove items with parents and domains
                            _reports.RemoveAt(k);
                        else
                            k += 1;
                    }
                    break;
                default:
                    break;
            }
        }

        public void SortOn(Todo.App.DomainPages sortOn)
        {
            Sort(_reports, getSortedList(sortOn));

            //switch (sortOn)
            //{
            //    case App.DomainPages.Important:
            //        List<Item> sorted = new List<Item>();

            //        IEnumerable<Item> goals = _reports.Where(x => x.Type == 2);
            //        List<Item> goals_sorted = goals.OrderByDescending(it => it.Importance).ToList();
                    
            //        if (goals.Count() > 0)
            //        {
            //            foreach (Item goal in goals_sorted)
            //            {
            //                sorted.Add(goal);

            //                IEnumerable<Item> projects = _reports.Where(x => x.Type == 3 && x.Parent == goal.ID);

            //                if (projects.Count() > 0)
            //                {
            //                    List<Item> projects_sorted = projects.OrderByDescending(it => it.Importance).ToList();

            //                    foreach (Item project in projects_sorted)
            //                    {
            //                        sorted.Add(project);

            //                        IEnumerable<Item> tasks = _reports.Where(x => x.Type == 4 && x.Parent == project.ID);

            //                        if (tasks.Count() > 0)
            //                        {
            //                            List<Item> tasks_sorted = tasks.OrderByDescending(it => it.Importance).ToList();
            //                            sorted.AddRange(tasks_sorted);
            //                        }
            //                    }

            //                }
            //            }
            //        }

            //        Sort(_reports, sorted);


            //        //List<Item> importantSorted = _reports.OrderByDescending(it => it.Importance).ToList();
            //        //for (int i = 0; i < importantSorted.Count(); i++)
            //        //    _reports.Move(_reports.IndexOf(importantSorted[i]), i);
            //        break;
            //    case App.DomainPages.Urgent:
            //        List<Item> urgentSorted = _reports.OrderByDescending(it => it.Urgency).ToList();
            //        for (int i = 0; i < urgentSorted.Count(); i++)
            //            _reports.Move(_reports.IndexOf(urgentSorted[i]), i);
            //        break;
            //    case App.DomainPages.Current:
            //        List<Item> currentSorted = _reports.OrderByDescending(it => it.EndDate).ToList();
            //        for (int i = 0; i < currentSorted.Count(); i++)
            //            _reports.Move(_reports.IndexOf(currentSorted[i]), i);
            //        break;
            //    default:
            //        break;
            //}
        }

        public ItemListViewModel()
        {
            Reports = new ObservableCollection<Item>();

            var items = Todo.App.Database.GetItems();

            if (items != null)
            {
                foreach (Item it in items.Result)
                {
                    Reports.Add(it);
                }
            }
            else
                Reports = null;
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