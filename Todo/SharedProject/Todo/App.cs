﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Todo
{
    public class App : Application // superclass new in 1.3
    {
        public App()
        {
            // The root page of your application
            MainPage = new FormsGallery.GridDemoPage();//GetMainPage(); // property new in 1.3
        }


		public static Page GetMainPage ()
		{
            return new FormsGallery.GridDemoPage();

            //TodoListPage listPage = new TodoListPage();
            //listPage.Refresh();

            //var itemNav = new NavigationPage(listPage) { Title = "Items" };

            //TodoGroupListPage groupListPage = new TodoGroupListPage();
            //groupListPage.Refresh();

            //var groupNav = new NavigationPage(groupListPage) { Title = "Groups" };

            //TabbedPage tabbedPage = new TabbedPage();
            //tabbedPage.Children.Add(itemNav);
            //tabbedPage.Children.Add(groupNav);

            //return tabbedPage;





            //var layout = new StackLayout();
            ////if (Device.OS == TargetPlatform.WinPhone)
            ////{ // WinPhone doesn't have the title showing
            ////    layout.Children.Add(new Label { Text = "Todo", Font = Font.BoldSystemFontOfSize(NamedSize.Large) });
            ////}
            //layout.Children.Add(getMultiSelect());
            //layout.VerticalOptions = LayoutOptions.FillAndExpand;

            //var listView = getMultiSelect();

            //var page = new ContentPage
            //{
            //    Title = "Multi Select",
            //    Content = listView
            //};

            //var itemNav = new NavigationPage(page) { Title = "Items" };

            //return itemNav;
		}

        public static ListView getMultiSelect()
        {
            var L_Kategorien = new List<Todo.MultiSelect>();
            int index = 0;

            //
            var Kategorie = new Todo.MultiSelect() { cIconPath = "", Text = "Action & Abenteuer", Selected = false, iIndex = index};
            L_Kategorien.Add(Kategorie);
            index += 1;
            //
            Kategorie = new Todo.MultiSelect() { cIconPath = "", Text = "Sport", Selected = false, iIndex = index };
            L_Kategorien.Add(Kategorie);
            index += 1;

            var LVKategorien = new ListView() { HeightRequest = 200 };
            LVKategorien.ItemTemplate = new DataTemplate(typeof(Todo.Views.KategorienViewCell)); // Update page
            LVKategorien.ItemsSource = L_Kategorien;

            LVKategorien.ItemTapped += async (sender, e) =>
            {
                var LVElement = (MultiSelect)e.Item;
                if (LVElement.Selected) // Item is selected already
                {
                    L_Kategorien[LVElement.iIndex].Selected = false;
                    L_Kategorien[LVElement.iIndex].cIconPath = "";
                }
                else
                {
                    //if (GV.SucheFreizeitGuide.bFreizeitAngebotIstSelektiert) // relevant only for my App
                    //{
                    //    var AntwortJa = await DisplayAlert("Hinweis", "Sie haben bereits ein Freizeitangebot als Suchbegriff erfasst. Wenn Sie nach Kategorie suchen, wird das Freizeitangebot (oben) zurückgesetzt", "OK", "Abbruch");
                    //    if (!AntwortJa)
                    //    {
                    //        return; // Abbruch
                    //    }
                    //    // Freizeitangebot zurücksetzen
                    //    GV.SucheFreizeitGuide.cKeyFreizeitAngebot = "";
                    //    FZ_AngebotLabel.Text = "Noch kein Freizeitangebot gewählt...";
                    //    GV.SucheFreizeitGuide.bFreizeitAngebotIstSelektiert = false;
                    //}
                    L_Kategorien[LVElement.iIndex].Selected = true;
                    // For iOS -> black check-icon, for Android and WP -> white check-icon
                    if (Device.OS == TargetPlatform.iOS) { L_Kategorien[LVElement.iIndex].cIconPath = "CheckSchwarz.png"; } else { L_Kategorien[LVElement.iIndex].cIconPath = "CheckWeiss.png"; }
                }
                LVKategorien.ItemTemplate = new DataTemplate(typeof(Todo.Views.KategorienViewCell)); // Update Page
                // Steuervariable zurücksetzen und neu setzen
                //GV.SucheFreizeitGuide.bKategorieIstSelektiert = false; // relevant only for my App
                //foreach (GV.MehrfachSelektion oKategorie in L_Kategorien)
                //{
                //    if (oKategorie.bSelektiert)
                //    { GV.SucheFreizeitGuide.bKategorieIstSelektiert = true; }
                //}
            };

            return LVKategorien;
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

