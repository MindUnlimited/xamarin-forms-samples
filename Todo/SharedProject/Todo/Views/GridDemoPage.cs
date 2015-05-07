using System;
using System.Threading.Tasks;
using Todo;
using Xamarin.Forms;

namespace FormsGallery
{
    class GridDemoPage : ContentPage
    {
        public GridDemoPage()
        {
            var layout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                //Padding = 20
            };



            //var grid = new Grid
            //{
            //    RowSpacing = 50
            //};

            //grid.Children.Add(new Label { Text = "This" }, 0, 0); // Left, First element
            //grid.Children.Add(new Label { Text = "text is" }, 1, 0); // Right, First element
            //grid.Children.Add(new Label { Text = "in a" }, 0, 1); // Left, Second element
            //grid.Children.Add(new Label { Text = "grid!" }, 1, 1); // Right, Second element

            //var gridButton = new Button { Text = "So is this Button! Click me." };
            //gridButton.Clicked += delegate
            //{
            //    gridButton.Text = string.Format("Thanks! {0} clicks.", count++);
            //};
            //grid.Children.Add(gridButton, 0, 2); // Left, Third element

            int count = 1;

            Grid grid = new Grid();
            grid.VerticalOptions = LayoutOptions.StartAndExpand;

            TodoListPage itemList = new TodoListPage();
            itemList.Refresh();
            TodoListPage itemList2 = new TodoListPage();
            itemList2.Refresh();
            TodoListPage itemList3 = new TodoListPage();
            itemList3.Refresh();
            TodoListPage itemList4 = new TodoListPage();
            itemList4.Refresh();


            //var navPage = new NavigationPage(itemList) { Title = "Items" };
            var stack1 = new StackLayout();
            stack1.Children.Add(itemList.Content);


            // column, row
            grid.Children.Add(new Label { Text = "Personal" }, 0, 0); // Left, First row
            grid.Children.Add(new Label { Text = "Family" }, 1, 0); // Right, First row
            grid.Children.Add(stack1, 0, 1); // Left, Second row
            grid.Children.Add(itemList2.Content, 1, 1); // Right, Second row

            grid.Children.Add(new Label { Text = "Work" }, 0, 2); // Left, Third row
            grid.Children.Add(new Label { Text = "Other" }, 1, 2); // Right, Third row
            grid.Children.Add(itemList3.Content, 0, 3); // Left, Fourth row
            grid.Children.Add(itemList4.Content, 1, 3); // Right, Fourt row

            var refreshButton = new Button { Text = "Go to Items" };
            refreshButton.Clicked += delegate
            {
                itemList.Refresh();
                itemList2.Refresh();
                itemList3.Refresh();
                itemList4.Refresh();
                
                //Navigation.PushAsync(new NavigationPage(itemList));
                Navigation.PushAsync(itemList);
            };
            grid.Children.Add(refreshButton, 0, 4); // Left, Fifth row

            var logoutButton = new Button { Text = "Logout" };
            logoutButton.Clicked += delegate
            {
                DependencyService.Get<Logout>().Logout();
                //Logout();
                //Forms.Context.
                //var test = Todo.App.Database.client;
            };
            grid.Children.Add(logoutButton, 1, 4); // Right, Fifth row


            //grid.Children.Add(new Label
            //{
            //    Text = "Fixed 100x100",
            //    TextColor = Color.Aqua,
            //    BackgroundColor = Color.Red,
            //    XAlign = TextAlignment.Center,
            //    YAlign = TextAlignment.Center
            //}, 2, 3);

            //// Accomodate iPhone status bar.
            //this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

            // Build the page.

            //layout.Children.Add(grid);
            //Content = layout;
            Content = grid;
        }
    }
}