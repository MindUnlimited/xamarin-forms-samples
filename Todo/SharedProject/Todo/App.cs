using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Views;
using Xamarin.Forms;

namespace Todo
{
    public class RootPage : MasterDetailPage
    {
        public RootPage()
        {
            var menuPage = new MenuPage();

            menuPage.Menu.ItemSelected += (sender, e) => NavigateTo(e.SelectedItem as MenuItem);

            Master = menuPage;
            Detail = new NavigationPage(App.importantDPage);
        }

        async void NavigateTo(MenuItem menu)
        {
            if (menu.Title != "Inbox")
            {
                App.selectedDomainPage = (DomainPage)menu.associatedPage;
                await App.selectedDomainPage.Refresh();
            }
            else
                App.selectedDomainPage = null;

            var navPage = new NavigationPage(menu.associatedPage);
            navPage.Title = menu.Title;

            Detail = navPage;

            IsPresented = false;
        }
    }

    public class MenuPage : ContentPage
    {
        public ListView Menu { get; set; }

        public MenuPage()
        {
            Icon = "LogoMindSet32";
            Title = "menu"; // The Title property must be set.
            BackgroundColor = Color.FromHex("333333");

            Menu = new MenuListView();

            var menuLabel = new ContentView
            {
                Padding = new Thickness(10, 36, 0, 5),
                Content = new Label
                {
                    TextColor = Color.FromHex("AAAAAA"),
                    Text = "Sort On",
                }
            };

            var settingsLabel = new ContentView
            {
                Padding = new Thickness(10, 36, 0, 5),
                Content = new Label
                {
                    TextColor = Color.FromHex("AAAAAA"),
                    Text = "Settings",
                }
            };
            var helpLabel = new ContentView
            {
                Padding = new Thickness(10, 36, 0, 5),
                Content = new Label
                {
                    TextColor = Color.FromHex("AAAAAA"),
                    Text = "Help and feedback",
                }
            };

            var layout = new StackLayout
            {
                Spacing = 0,
                VerticalOptions = LayoutOptions.Start
            };
            layout.Children.Add(menuLabel);
            layout.Children.Add(Menu);
            layout.Children.Add(new BoxView() { Color = Color.Gray, WidthRequest = 100, HeightRequest = 1});
            layout.Children.Add(settingsLabel);
            layout.Children.Add(helpLabel);

            Content = layout;
        }
    }

    public class MenuItem
    {
        public string Title { get; set; }

        public string IconSource { get; set; }

        public Page associatedPage { get; set; }
    }

    public class MenuListView : ListView
    {
        public MenuListView()
        {
            List<MenuItem> data = new MenuListData();

            ItemsSource = data;
            VerticalOptions = LayoutOptions.StartAndExpand;
            BackgroundColor = Color.Transparent;

            var cell = new DataTemplate(typeof(ImageCell));
            cell.SetBinding(TextCell.TextProperty, "Title");
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
                Title = "Important",
                //Icon = "contracts.png",
                associatedPage = App.importantDPage
            });

            this.Add(new MenuItem()
            {
                Title = "Urgent",
                //Icon = "Lead.png",
                associatedPage = App.urgentDPage
            });

            this.Add(new MenuItem()
            {
                Title = "Current",
                //Icon = "Accounts.png",
                associatedPage = App.currentDPage
            });

            this.Add(new MenuItem()
            {
                Title = "Completed",
                //Icon = "Opportunity.png",
                associatedPage = App.completedDPage
            });

            this.Add(new MenuItem()
            {
                Title = "Inbox",
                //Icon = "Opportunity.png",
                associatedPage = App.inboxDPage
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
        public static NavigationPage selectedNavigationPage;

        public static DomainPage importantDPage = new DomainPage(DomainPages.Important);
        public static DomainPage urgentDPage = new DomainPage(DomainPages.Urgent);
        public static DomainPage currentDPage = new DomainPage(DomainPages.Current);
        public static DomainPage completedDPage = new DomainPage(DomainPages.Completed);
        public static InboxPage inboxDPage = new InboxPage();

        //public static NavigationPage importantPage;
        //public static NavigationPage urgentPage;
        //public static NavigationPage currentPage;
        //public static NavigationPage completedPage;
        //public static NavigationPage inboxPage;

        public static MasterDetailPage masterDetailPage;

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
            NavigationPage importantPage = new NavigationPage(importantDPage);
            importantPage.Title = "Important";

            NavigationPage urgentPage = new NavigationPage(urgentDPage);
            urgentPage.Title = "Urgent";

            NavigationPage currentPage = new NavigationPage(currentDPage);
            currentPage.Title = "Current";

            NavigationPage completedPage = new NavigationPage(completedDPage);
            completedPage.Title = "Completed";

            NavigationPage inboxPage = new NavigationPage(inboxDPage);
            inboxPage.Title = "Inbox";

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
                        selectedNavigationPage = importantPage;
                        break;
                    case "Urgent":
                        selectedDomainPage = urgentDPage;
                        selectedNavigationPage = urgentPage;
                        break;
                    case "Current":
                        selectedDomainPage = currentDPage;
                        selectedNavigationPage = currentPage;
                        break;
                    case "Completed":
                        selectedDomainPage = completedDPage;
                        selectedNavigationPage = completedPage;
                        break;
                    case "Inbox":
                        selectedDomainPage = null;
                        selectedNavigationPage = inboxPage;
                        break;
                    default:
                        break;
                }

                if (domainTabsPage.CurrentPage.Title != "Inbox")
                    await selectedDomainPage.Refresh();
                else
                    inboxDPage.Refresh();


            });

            switch (domainTabsPage.CurrentPage.Title)
            {
                case "Important":
                    selectedDomainPage = importantDPage;
                    selectedNavigationPage = importantPage;
                    break;
                case "Urgent":
                    selectedDomainPage = urgentDPage;
                    selectedNavigationPage = urgentPage;
                    break;
                case "Current":
                    selectedDomainPage = currentDPage;
                    selectedNavigationPage = currentPage;
                    break;
                case "Completed":
                    selectedDomainPage = completedDPage;
                    selectedNavigationPage = completedPage;
                    break;
                case "Inbox":
                    selectedDomainPage = null;
                    selectedNavigationPage = inboxPage;
                    break;
                default:
                    break;
            }

            

            if (Device.OS == TargetPlatform.WinPhone)
            {
                MainPage = domainTabsPage;
            }
            else
            {
                masterDetailPage = new RootPage();// new MasterDetailPage { MasterBehavior = MasterBehavior.Popover, Master = new MenuPage {}, Detail = importantPage };
                MainPage = masterDetailPage;
            }
                

            Navigation = MainPage.Navigation;

            //MainPage = domainTabsPage;

            ////MainPage = new NavigationPage(domainTabsPage); // The root page of your application
            //Navigation = MainPage.Navigation;

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

