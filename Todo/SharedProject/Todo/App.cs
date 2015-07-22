using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Views;
using Xamarin.Forms;

namespace Todo
{
    public class MenuPage : ContentPage
    {
        public ListView Menu { get; set; }

        public MenuPage()
        {
            Icon = "settings.png";
            Title = "menu"; // The Title property must be set.
            BackgroundColor = Color.FromHex("333333");

            Menu = new MenuListView();

            var menuLabel = new ContentView
            {
                Padding = new Thickness(10, 36, 0, 5),
                Content = new Label
                {
                    TextColor = Color.FromHex("AAAAAA"),
                    Text = "MENU",
                }
            };

            var layout = new StackLayout
            {
                Spacing = 0,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            layout.Children.Add(menuLabel);
            layout.Children.Add(Menu);

            Content = layout;
        }
    }

    public class MenuListView : ListView
    {
        public MenuListView()
        {
            List<MenuItem> data = new MenuListData();

            ItemsSource = data;
            VerticalOptions = LayoutOptions.FillAndExpand;
            BackgroundColor = Color.Transparent;

            var cell = new DataTemplate(typeof(ImageCell));
            cell.SetBinding(TextCell.TextProperty, "Text");
            cell.SetBinding(ImageCell.ImageSourceProperty, "Icon");

            ItemTemplate = cell;
        }
    }

    public class MenuListData : List<MenuItem>
    {
        public MenuListData()
        {
            this.Add(new MenuItem()
            {
                Text = "Contracts",
                Icon = "contracts.png",
                //TargetType = typeof(ContractsPage)
            });

            this.Add(new MenuItem()
            {
                Text = "Leads",
                Icon = "Lead.png",
                //TargetType = typeof(LeadsPage)
            });

            this.Add(new MenuItem()
            {
                Text = "Accounts",
                Icon = "Accounts.png",
                //TargetType = typeof(AccountsPage)
            });

            this.Add(new MenuItem()
            {
                Text = "Opportunities",
                Icon = "Opportunity.png",
                //TargetType = typeof(OpportunitiesPage)
            });
        }
    }


    public class App : Application // superclass new in 1.3
    {
        public static readonly Color ORANGE = Color.FromRgb(234, 99, 18);
        public static readonly Color PURPLE = Color.FromRgb(158, 94, 155);

        public static readonly Color BLUE = Color.FromRgb(50, 168, 174);
        public static readonly Color YELLOW = Color.FromRgb(255, 192, 0);
        public static readonly Color RED = Color.FromRgb(192, 0, 0);
        public static readonly Color GREEN = Color.FromRgb(146, 208, 80);

        public static DomainPage selectedDomainPage;

        public static DomainPage importantDPage = new DomainPage(DomainPages.Important);
        public static DomainPage urgentDPage = new DomainPage(DomainPages.Urgent);
        public static DomainPage currentDPage = new DomainPage(DomainPages.Current);
        public static DomainPage completedDPage = new DomainPage(DomainPages.Completed);
        public static InboxPage inboxDPage = new InboxPage();

        public enum DomainPages
        {
            None,
            Important,
            Urgent,
            Current,
            Completed,
            Inbox
        };

        public App()
        {
            var importantPage = new NavigationPage(importantDPage);
            importantPage.Title = "Important";

            var urgentPage = new NavigationPage(urgentDPage);
            urgentPage.Title = "Urgent";

            var currentPage = new NavigationPage(currentDPage);
            currentPage.Title = "Current";

            var completedPage = new NavigationPage(completedDPage);
            completedPage.Title = "Completed";

            var inboxPage = new NavigationPage(inboxDPage);
            inboxPage.Title = "Inbox";

            MasterDetailPage masterDetailPage = new MasterDetailPage { MasterBehavior = MasterBehavior.Popover };

            masterDetailPage.Master = new MenuPage();


            TabbedPage domainTabsPage = new TabbedPage();

            domainTabsPage.Children.Add(importantPage);
            domainTabsPage.Children.Add(urgentPage);
            domainTabsPage.Children.Add(currentPage);
            domainTabsPage.Children.Add(completedPage);
            domainTabsPage.Children.Add(inboxPage);

            domainTabsPage.CurrentPageChanged += (async (o, e) =>
            {
                switch (domainTabsPage.CurrentPage.Title)
                {
                    case "Important":
                        selectedDomainPage = importantDPage;
                        await importantDPage.Refresh();
                        break;
                    case "Urgent":
                        selectedDomainPage = urgentDPage;
                        await urgentDPage.Refresh();
                        break;
                    case "Current":
                        selectedDomainPage = currentDPage;
                        await currentDPage.Refresh();
                        break;
                    case "Completed":
                        selectedDomainPage = completedDPage;
                        await completedDPage.Refresh();
                        break;
                    case "Inbox":
                        selectedDomainPage = null;
                        inboxDPage.Refresh();
                        break;
                    default:
                        break;
                }
            });

            switch (domainTabsPage.CurrentPage.Title)
            {
                case "Important":
                    selectedDomainPage = importantDPage;
                    break;
                case "Urgent":
                    selectedDomainPage = urgentDPage;
                    break;
                case "Current":
                    selectedDomainPage = currentDPage;
                    break;
                case "Completed":
                    selectedDomainPage = completedDPage;
                    break;
                case "Inbox":
                    selectedDomainPage = null;
                    break;
                default:
                    break;
            }

            MainPage = domainTabsPage;

            //MainPage = new NavigationPage(domainTabsPage); // The root page of your application
            Navigation = MainPage.Navigation;

            //MainPage = new NavigationPage(new FormsGallery.GridDemoPage());//GetMainPage(); // property new in 1.3
            //MainPage = new FormsGallery.GridDemoPage();//GetMainPage(); // property new in 1.3
        }


        //public static Page GetMainPage ()
        //{
        //    return new NavigationPage(domainPage);
        //    //return new NavigationPage(new FormsGallery.GridDemoPage());
        //    //return new FormsGallery.GridDemoPage();

        //    //TodoListPage listPage = new TodoListPage();
        //    //listPage.Refresh();

        //    //var itemNav = new NavigationPage(listPage) { Title = "Items" };

        //    //TodoGroupListPage groupListPage = new TodoGroupListPage();
        //    //groupListPage.Refresh();

        //    //var groupNav = new NavigationPage(groupListPage) { Title = "Groups" };

        //    //TabbedPage tabbedPage = new TabbedPage();
        //    //tabbedPage.Children.Add(itemNav);
        //    //tabbedPage.Children.Add(groupNav);

        //    //return tabbedPage;





        //    //var layout = new StackLayout();
        //    ////if (Device.OS == TargetPlatform.WinPhone)
        //    ////{ // WinPhone doesn't have the title showing
        //    ////    layout.Children.Add(new Label { Text = "Todo", Font = Font.BoldSystemFontOfSize(NamedSize.Large) });
        //    ////}
        //    //layout.Children.Add(getMultiSelect());
        //    //layout.VerticalOptions = LayoutOptions.FillAndExpand;

        //    //var listView = getMultiSelect();

        //    //var page = new ContentPage
        //    //{
        //    //    Title = "Multi Select",
        //    //    Content = listView
        //    //};

        //    //var itemNav = new NavigationPage(page) { Title = "Items" };

        //    //return itemNav;
        //}

        //public static ListView getMultiSelect()
        //{
        //    var L_Kategorien = new List<Todo.MultiSelect>();
        //    int index = 0;

        //    //
        //    var Kategorie = new Todo.MultiSelect() { cIconPath = "", Text = "Action & Abenteuer", Selected = false, iIndex = index};
        //    L_Kategorien.Add(Kategorie);
        //    index += 1;
        //    //
        //    Kategorie = new Todo.MultiSelect() { cIconPath = "", Text = "Sport", Selected = false, iIndex = index };
        //    L_Kategorien.Add(Kategorie);
        //    index += 1;

        //    var LVKategorien = new ListView() { HeightRequest = 200 };
        //    LVKategorien.ItemTemplate = new DataTemplate(typeof(Todo.Views.KategorienViewCell)); // Update page
        //    LVKategorien.ItemsSource = L_Kategorien;

        //    LVKategorien.ItemTapped += async (sender, e) =>
        //    {
        //        var LVElement = (MultiSelect)e.Item;
        //        if (LVElement.Selected) // Item is selected already
        //        {
        //            L_Kategorien[LVElement.iIndex].Selected = false;
        //            L_Kategorien[LVElement.iIndex].cIconPath = "";
        //        }
        //        else
        //        {
        //            //if (GV.SucheFreizeitGuide.bFreizeitAngebotIstSelektiert) // relevant only for my App
        //            //{
        //            //    var AntwortJa = await DisplayAlert("Hinweis", "Sie haben bereits ein Freizeitangebot als Suchbegriff erfasst. Wenn Sie nach Kategorie suchen, wird das Freizeitangebot (oben) zurückgesetzt", "OK", "Abbruch");
        //            //    if (!AntwortJa)
        //            //    {
        //            //        return; // Abbruch
        //            //    }
        //            //    // Freizeitangebot zurücksetzen
        //            //    GV.SucheFreizeitGuide.cKeyFreizeitAngebot = "";
        //            //    FZ_AngebotLabel.Text = "Noch kein Freizeitangebot gewählt...";
        //            //    GV.SucheFreizeitGuide.bFreizeitAngebotIstSelektiert = false;
        //            //}
        //            L_Kategorien[LVElement.iIndex].Selected = true;
        //            // For iOS -> black check-icon, for Android and WP -> white check-icon
        //            if (Device.OS == TargetPlatform.iOS) { L_Kategorien[LVElement.iIndex].cIconPath = "CheckSchwarz.png"; } else { L_Kategorien[LVElement.iIndex].cIconPath = "CheckWeiss.png"; }
        //        }
        //        LVKategorien.ItemTemplate = new DataTemplate(typeof(Todo.Views.KategorienViewCell)); // Update Page
        //        // Steuervariable zurücksetzen und neu setzen
        //        //GV.SucheFreizeitGuide.bKategorieIstSelektiert = false; // relevant only for my App
        //        //foreach (GV.MehrfachSelektion oKategorie in L_Kategorien)
        //        //{
        //        //    if (oKategorie.bSelektiert)
        //        //    { GV.SucheFreizeitGuide.bKategorieIstSelektiert = true; }
        //        //}
        //    };

        //    return LVKategorien;
        //}

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

        public static INavigation Navigation;


	}
}

