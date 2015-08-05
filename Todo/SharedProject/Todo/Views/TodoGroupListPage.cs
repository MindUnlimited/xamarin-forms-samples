using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Todo
{
    public class TodoGroupListPage : ContentPage
    {
        ListView listView;
        public TodoGroupListPage()
        {
            Title = "Groups";

            NavigationPage.SetHasNavigationBar(this, true);

            listView = new ListView
            {
                RowHeight = 40
            };
            listView.ItemTemplate = new DataTemplate(typeof(TodoGroupCell));

            // These commented-out lines were used to test the ListView prior to integrating the database
            //			listView.ItemSource = new string [] { "Buy pears", "Buy oranges", "Buy mangos", "Buy apples", "Buy bananas" };
            //			listView.ItemSource = new TodoItem [] { 
            //				new TodoItem {Name = "Buy pears`"}, 
            //				new TodoItem {Name = "Buy oranges`", Done=true},
            //				new TodoItem {Name = "Buy mangos`"}, 
            //				new TodoItem {Name = "Buy apples`", Done=true},
            //				new TodoItem {Name = "Buy bananas`", Done=true}
            //			};

            // HACK: workaround issue #894 for now
            if (Device.OS == TargetPlatform.iOS)
                listView.ItemsSource = new string[1] { "" };

            listView.ItemSelected += (sender, e) =>
            {
                var Group = (Group)e.SelectedItem;
                var groupPage = new TodoGroupPage();
                groupPage.BindingContext = Group;
                Navigation.PushAsync(groupPage);
            };

            var layout = new StackLayout();
            //if (Device.OS == TargetPlatform.WinPhone)
            //{ // WinPhone doesn't have the title showing
            //    layout.Children.Add(new Label { Text = "Todo", Font = Font.BoldSystemFontOfSize(NamedSize.Large) });
            //}
            layout.Children.Add(listView);
            layout.VerticalOptions = LayoutOptions.FillAndExpand;
            Content = layout;


            ToolbarItem tbi = null;
            if (Device.OS == TargetPlatform.iOS)
            {
                tbi = new ToolbarItem("+", null, () =>
                {
                    var Group = new Group();
                    var groupPage = new TodoGroupPage();
                    groupPage.BindingContext = Group;
                    Navigation.PushAsync(groupPage);
                }, 0, 0);
            }
            if (Device.OS == TargetPlatform.Android)
            { // BUG: Android doesn't support the icon being null
                tbi = new ToolbarItem("+", "add", () =>
                {
                    var Group = new Group();
                    var groupPage = new TodoGroupPage();
                    groupPage.BindingContext = Group;
                    Navigation.PushAsync(groupPage);
                }, 0, 0);
            }
            if (Device.OS == TargetPlatform.WinPhone)
            {
                tbi = new ToolbarItem("Add", "add.png", () =>
                {
                    var Group = new Group();
                    var groupPage = new TodoGroupPage();
                    groupPage.BindingContext = Group;
                    Navigation.PushAsync(groupPage);
                }, 0, 0);
            }

            ToolbarItems.Add(tbi);

            if (Device.OS == TargetPlatform.iOS)
            {
                var tbi2 = new ToolbarItem("?", null, () =>
                {
                    var groups = App.Database.getGroups(Todo.App.Database.userID).Result;
                    var tospeak = "";
                    foreach (var t in groups)
                        tospeak += t.Name + " ";
                    if (tospeak == "") tospeak = "there are no groups";

                    DependencyService.Get<ITextToSpeech>().Speak(tospeak);
                }, 0, 0);
                ToolbarItems.Add(tbi2);
            }

        }

        public async void Refresh()
        {
            if (Todo.App.Database != null && Todo.App.Database.userID != null)
            {
                listView.ItemsSource = await App.Database.getGroups(Todo.App.Database.userID);
            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = await App.Database.getGroups(Todo.App.Database.userID);
        }
    }
}

