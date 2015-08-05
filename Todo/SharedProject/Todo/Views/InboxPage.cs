using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Todo.Models;
using Xamarin.Forms;

namespace Todo.Views
{
    public class InboxPage : ContentPage
    {
        public Todo.App.DomainPages domainPageType;
        public ItemListViewModel viewModel;
        private ListView listView;
        private RelativeLayout objRelativeLayout = new RelativeLayout();
        private bool listsInitialized;

        public InboxPage()
        {
            domainPageType = Todo.App.DomainPages.Inbox;
            listsInitialized = false;

            StackLayout objStackLayout = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
            };

            listView = new ListView { ItemTemplate = new DataTemplate(typeof(TodoItemCellBig)) };

            listView.ItemSelected += async (sender, e) =>
            {
                var Item = (Item)e.SelectedItem;

                List<Group> availableGroups = new List<Group>();
                if (Todo.App.Database.userID != null)
                {
                    var _groups = await Todo.App.Database.getGroups(Todo.App.Database.userID);
                    availableGroups.AddRange(_groups);
                }

                Dictionary<string, string> groups = new Dictionary<string, string>();
                foreach (Group group in availableGroups)
                {
                    groups[group.ID] = group.Name;
                }

                if (Item.OwnedBy != null && groups.ContainsKey(Item.OwnedBy))
                {
                    Item.OwnedBy = groups[Item.OwnedBy];
                }

                Dictionary<string, string> parentsDict = new Dictionary<string, string>();

                parentsDict[Todo.App.selectedDomainPage.selectedDomain.ID] = Todo.App.selectedDomainPage.selectedDomain.Name;
                foreach (Item item in viewModel.Reports)
                {
                    parentsDict[item.ID] = item.Name;
                }

                if (Item.Parent != null && parentsDict.ContainsKey(Item.Parent))
                {
                    Item.Parent = parentsDict[Item.Parent];
                }

                var todoPage = new TodoItemPage();
                todoPage.BindingContext = Item;
                await Navigation.PushAsync(todoPage);
            };


            objStackLayout.Children.Add(listView);

            this.Content = objStackLayout;


            ToolbarItem tbi = null;
            if (Device.OS == TargetPlatform.iOS)
            {
                tbi = new ToolbarItem("+", null, () =>
                {
                    var Item = new Item();
                    var todoPage = new TodoItemPage();
                    todoPage.BindingContext = Item;
                    Navigation.PushAsync(todoPage);
                }, 0, 0);
            }
            if (Device.OS == TargetPlatform.Android)
            { // BUG: Android doesn't support the icon being null
                tbi = new ToolbarItem("+", "add", async () =>
                {
                    var Item = new Item();

                    var todoPage = new TodoItemPage();
                    todoPage.BindingContext = Item;
                    await Navigation.PushAsync(todoPage);
                }, 0, 0);
            }
            if (Device.OS == TargetPlatform.WinPhone)
            {
                tbi = new ToolbarItem("Add", "add.png", async () =>
                {

                    var Item = new Item();

                    var todoPage = new TodoItemPage();
                    todoPage.BindingContext = Item;
                    await Navigation.PushAsync(todoPage);
                }, 0, 0);
            }

            ToolbarItems.Add(tbi);

            if (Device.OS == TargetPlatform.iOS)
            {
                var tbi2 = new ToolbarItem("?", null, () =>
                {
                    var todos = App.Database.GetItemsNotDone().Result;
                    var tospeak = "";
                    foreach (var t in todos)
                        tospeak += t.Name + " ";
                    if (tospeak == "") tospeak = "there are no tasks to do";

                    DependencyService.Get<ITextToSpeech>().Speak(tospeak);
                }, 0, 0);
                ToolbarItems.Add(tbi2);
            }

            Appearing += ((o, e) =>
                {
                    Refresh();
                });
        }

        public void Refresh()
        {
            if (Todo.App.Database != null && Todo.App.Database.userID != null && !listsInitialized)
            {
                IsBusy = true;
                viewModel = new ItemListViewModel();
                viewModel.FilterAndSort(domainPageType);
                listView.ItemsSource = viewModel.Reports;

                listsInitialized = true;
                IsBusy = false;
            }
        }
    }
}
