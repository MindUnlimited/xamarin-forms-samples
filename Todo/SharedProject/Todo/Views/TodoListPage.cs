using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
//using SolTech.Forms;

namespace Todo
{
	public class TodoListPage : ContentPage
	{
		ListView listView;
		public TodoListPage ()
		{
			//Title = "Items";

			//NavigationPage.SetHasNavigationBar (this, true);
            NavigationPage.SetHasNavigationBar(this, false);

			listView = new ListView {
			    RowHeight = 40
			};
			listView.ItemTemplate = new DataTemplate (typeof (TodoItemCell));

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
				listView.ItemsSource = new string [1] {""};

			listView.ItemSelected += async (sender, e) => {
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

				var todoPage = new TodoItemPage();
				todoPage.BindingContext = Item;
				await Navigation.PushAsync(todoPage);
			};

			var layout = new StackLayout();
            //if (Device.OS == TargetPlatform.WinPhone) { // WinPhone doesn't have the title showing
            //    layout.Children.Add(new Label{Text="Todo", Font=Font.BoldSystemFontOfSize(NamedSize.Large)});
            //}
			layout.Children.Add(listView);
			layout.VerticalOptions = LayoutOptions.FillAndExpand;
			Content = layout;


			ToolbarItem tbi = null;
			if (Device.OS == TargetPlatform.iOS)
			{
				tbi = new ToolbarItem("+", null, async () =>
					{
						var Item = new Item();
						var todoPage = new TodoItemPage();

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

						todoPage.BindingContext = Item;
						await Navigation.PushAsync(todoPage);
					}, 0, 0);
			}
			if (Device.OS == TargetPlatform.Android) { // BUG: Android doesn't support the icon being null
				tbi = new ToolbarItem ("+", "plus", async () => {
                    var Item = new Item();
                    var todoPage = new TodoItemPage();

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

                        todoPage.BindingContext = Item;
                        await Navigation.PushAsync(todoPage);
					}, 0, 0);
			}

			ToolbarItems.Add (tbi);



			if (Device.OS == TargetPlatform.iOS) {
				var tbi2 = new ToolbarItem ("?", null, () => {
                    var todos = App.Database.GetItemsNotDone().Result;
					var tospeak = "";
                    foreach (var t in todos)
                        tospeak += t.Name + " ";
                    if (tospeak == "") tospeak = "there are no tasks to do";

					DependencyService.Get<ITextToSpeech>().Speak(tospeak);
				}, 0, 0);
				ToolbarItems.Add (tbi2);
			}

		}

        public async void Refresh ()
        {
            if (Todo.App.Database != null && Todo.App.Database.userID != null)
            {
                listView.ItemsSource = await App.Database.GetItems();
            }
        }

		protected async override void OnAppearing ()
		{
			base.OnAppearing ();
			listView.ItemsSource = await App.Database.GetItems();
		}
	}
}

