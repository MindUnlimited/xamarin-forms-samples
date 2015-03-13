using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Todo
{
    public class App : Application // superclass new in 1.3
    {
        public App()
        {
            // The root page of your application
            MainPage = GetMainPage(); // property new in 1.3
        }


		public static Page GetMainPage ()
		{
            TodoListPage listPage = new TodoListPage();
            listPage.Refresh();

            var itemNav = new NavigationPage(listPage) { Title = "Items"};

            TodoGroupListPage groupListPage = new TodoGroupListPage();
            groupListPage.Refresh();

            var groupNav = new NavigationPage(groupListPage) { Title = "Groups"};

            TabbedPage tabbedPage = new TabbedPage();
            tabbedPage.Children.Add(itemNav);
            tabbedPage.Children.Add(groupNav);

            return tabbedPage;
		}

        public static void createDatabase()
        {
            App.database = new TodoItemDatabase();
        }

        //protected override void OnStart()
        //{
        //    MainPage = GetMainPage();
        //}

		static TodoItemDatabase database;
		public static TodoItemDatabase Database {
			get { return App.database; }
		}
	}
}

