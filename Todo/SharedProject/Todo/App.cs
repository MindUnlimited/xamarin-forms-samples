using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Todo
{
	public class App
	{
		public static Page GetMainPage ()
		{
            TodoListPage listPage = new TodoListPage();
            listPage.Refresh();

			var mainNav = new NavigationPage (listPage);
			return mainNav;
		}

        public static void createDatabase()
        {
            App.database = new TodoItemDatabase();
        }

		static TodoItemDatabase database;
		public static TodoItemDatabase Database {
			get { return App.database; }
		}
	}
}

