using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Todo.Models;
using Xamarin.Forms;

namespace Todo.Views
{
    public class DomainPage : ContentPage
    {
        public Todo.App.DomainPages domainPageType;

        public List<Item> domains;
        public Dictionary<Item, ItemListViewModel> viewModels;
        private Dictionary<Item, ListView> listViews;
        private Dictionary<Item, BoxView> bottomBorders;

        List<DomainRow> rowList;

        enum DefaultDomains
        {
            None,
            Personal,
            Friends,
            Work,
            Community
        };

        private bool expanded = false;
        public Item selectedDomain = new Item { Name = "" };

        private RelativeLayout objRelativeLayout = new RelativeLayout();

        //private DomainRow top;
        private ReorderListView topLV;
        //private DomainRow bottom;
        private ReorderListView bottomLV;

        private uint rows = 2;
        private uint columns = 2;
        private uint animationms = 200;

        private uint borderSize = 2; // pixels

        private bool listsInitialized = false;

        public DomainPage(Todo.App.DomainPages type)
        {
            domainPageType = type;

            //List<ListView> listViews = new List<ListView>();
            objRelativeLayout = new RelativeLayout
            {
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            StackLayout objStackLayout = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
            };

            topLV = new ReorderListView();//new ListView { ItemTemplate = new DataTemplate(typeof(TodoItemCellBig))};

            //topLV.PropertyChanged += ((o, ev) =>
            //{
            //    if (ev.PropertyName == "ReorderEnabled")
                    
            //});


            //topLV.ItemSelected += async (sender, e) =>
            //{
            //    var Item = (Item)e.SelectedItem;

            //    List<Group> availableGroups = new List<Group>();
            //    if (Todo.App.Database.userID != null)
            //    {
            //        var _groups = await Todo.App.Database.getGroups(Todo.App.Database.userID);
            //        availableGroups.AddRange(_groups);
            //    }

            //    Dictionary<string, string> groups = new Dictionary<string, string>();
            //    foreach (Group group in availableGroups)
            //    {
            //        groups[group.ID] = group.Name;
            //    }

            //    if (Item.OwnedBy != null && groups.ContainsKey(Item.OwnedBy))
            //    {
            //        Item.OwnedBy = groups[Item.OwnedBy];
            //    }

            //    Dictionary<string, string> parentsDict = new Dictionary<string, string>();

            //    parentsDict[Todo.App.selectedDomainPage.selectedDomain.ID] = Todo.App.selectedDomainPage.selectedDomain.Name;
            //    foreach (Item item in viewModels[selectedDomain].Reports)
            //    {
            //        parentsDict[item.ID] = item.Name;
            //    }

            //    if (Item.Parent != null && parentsDict.ContainsKey(Item.Parent))
            //    {
            //        Item.Parent = parentsDict[Item.Parent];
            //    }

            //    var todoPage = new TodoItemPage();
            //    todoPage.BindingContext = Item;
            //    await Navigation.PushAsync(todoPage);
            //};

            bottomLV = new ReorderListView();//new ListView { ItemTemplate = new DataTemplate(typeof(TodoItemCellBig)) };

            //bottomLV.ItemSelected += async (sender, e) =>
            //{
            //    var Item = (Item)e.SelectedItem;

            //    List<Group> availableGroups = new List<Group>();
            //    if (Todo.App.Database.userID != null)
            //    {
            //        var _groups = await Todo.App.Database.getGroups(Todo.App.Database.userID);
            //        availableGroups.AddRange(_groups);
            //    }

            //    Dictionary<string, string> groups = new Dictionary<string, string>();
            //    foreach (Group group in availableGroups)
            //    {
            //        groups[group.ID] = group.Name;
            //    }

            //    if (Item.OwnedBy != null && groups.ContainsKey(Item.OwnedBy))
            //    {
            //        Item.OwnedBy = groups[Item.OwnedBy];
            //    }

            //    Dictionary<string, string> parentsDict = new Dictionary<string, string>();

            //    parentsDict[Todo.App.selectedDomainPage.selectedDomain.ID] = Todo.App.selectedDomainPage.selectedDomain.Name;
            //    foreach (Item item in viewModels[selectedDomain].Reports)
            //    {
            //        parentsDict[item.ID] = item.Name;
            //    }

            //    if (Item.Parent != null && parentsDict.ContainsKey(Item.Parent))
            //    {
            //        Item.Parent = parentsDict[Item.Parent];
            //    }

            //    var todoPage = new TodoItemPage();
            //    todoPage.BindingContext = Item;
            //    await Navigation.PushAsync(todoPage);
            //};

            objStackLayout.Children.Add(objRelativeLayout);          

            this.Content = objStackLayout;


            ToolbarItem tbi = null;
            if (Device.OS == TargetPlatform.iOS)
            {
                tbi = new ToolbarItem("+", null, () =>
                {
                    if (selectedDomain != null && selectedDomain.Name != null && selectedDomain.Name != "")
                    {
                        var Item = new Item();
                        var todoPage = new TodoItemPage();
                        todoPage.BindingContext = Item;
                        Navigation.PushAsync(todoPage);
                    }
                }, 0, 0);
            }
            if (Device.OS == TargetPlatform.Android)
            { // BUG: Android doesn't support the icon being null
                tbi = new ToolbarItem("+", "plus", async () =>
                {
                    if (selectedDomain != null && selectedDomain.Name != null && selectedDomain.Name != "")
                    {
                        var Item = new Item { Type = 2 };

                        var domains = Todo.App.selectedDomainPage.domains;
                        if (selectedDomain.Name == "Personal")
                        {
                            var friends = domains[0];
                            Item.Parent = friends.ID;
                        }
                        else if (selectedDomain.Name == "Friends & Family")
                        {
                            var friends = domains[1];
                            Item.Parent = friends.ID;
                        }
                        else if (selectedDomain.Name == "Work")
                        {
                            var friends = domains[2];
                            Item.Parent = friends.ID;
                        }
                        else if (selectedDomain.Name == "Community")
                        {
                            var friends = domains[3];
                            Item.Parent = friends.ID;
                        }
                        
                        var todoPage = new TodoItemPage();
                        todoPage.BindingContext = Item;
                        await Navigation.PushAsync(todoPage);
                    }
                }, 0, 0);
            }
            if (Device.OS == TargetPlatform.WinPhone)
            {
                tbi = new ToolbarItem("Add", "add.png", async () =>
                {
                    if (selectedDomain != null && selectedDomain.Name != null && selectedDomain.Name != "")
                    {
                        var Item = new Item { Type = 2 };

                        var domains = Todo.App.selectedDomainPage.domains;
                        if (selectedDomain.Name == "Personal")
                        {
                            var friends = domains[0];
                            Item.Parent = friends.ID;
                        }
                        else if (selectedDomain.Name == "Friends & Family")
                        {
                            var friends = domains[1];
                            Item.Parent = friends.ID;
                        }
                        else if (selectedDomain.Name == "Work")
                        {
                            var friends = domains[2];
                            Item.Parent = friends.ID;
                        }
                        else if (selectedDomain.Name == "Community")
                        {
                            var friends = domains[3];
                            Item.Parent = friends.ID;
                        }

                        var todoPage = new TodoItemPage();
                        todoPage.BindingContext = Item;
                        await Navigation.PushAsync(todoPage);
                    }
                }, 0, 0);
            }

            ToolbarItems.Add(tbi);

            ToolbarItem reorder = null;

            if (Device.OS == TargetPlatform.Android)
            { // BUG: Android doesn't support the icon being null
                reorder = new ToolbarItem("reorder", "Reorder", () => topLV.ReorderEnabled = !topLV.ReorderEnabled, 0, 0);
            }
            if (Device.OS == TargetPlatform.WinPhone)
            {
                reorder = new ToolbarItem("Reorder", "Reorder.png", () => topLV.ReorderEnabled = !topLV.ReorderEnabled, 0, 0);
            }

            ToolbarItems.Add(reorder);


            ToolbarItem modalTest = null;

            if (Device.OS == TargetPlatform.Android)
            { // BUG: Android doesn't support the icon being null
                modalTest = new ToolbarItem("modal", "plus", async () =>
                {
                    await Navigation.PushAsync(new Todo.Views.ModalPage());
                }, 0, 0);
            }
            if (Device.OS == TargetPlatform.WinPhone)
            {
                modalTest = new ToolbarItem("Modal", "add.png", async () =>
                {
                    await Navigation.PushModalAsync(new Todo.Views.ModalPage());
                }, 0, 0);
            }

            //ToolbarItems.Add(modalTest);

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

            Appearing += (async (o, e) =>
                {
                    //Todo.App.selectedDomainPage = this;
                    await Refresh();
                    //topLV.BeginRefresh();
                    //bottomLV.BeginRefresh();

                });
        }



        public async Task expandAnimation()
        {
            selectedDomain = new Item { Name = "" };

            foreach (var row in rowList)
            {
                row.showItems();
            }

            //await Refresh();

            //topLV.ItemsSource = null;
            //bottomLV.ItemsSource = null;

            // Sets the bottom borders of friends and personal to their new values
            Rectangle personalBottomBorderBounds = bottomBorders[domains[0]].Bounds;
            personalBottomBorderBounds.Y = objRelativeLayout.Height / 2;

            Rectangle friendsBottomBorderBounds = bottomBorders[domains[1]].Bounds;
            friendsBottomBorderBounds.Y = objRelativeLayout.Height / 2;

            // Sets the bottom borders of friends and personal to their new values
            Rectangle workBottomBorderBounds = bottomBorders[domains[2]].Bounds;
            workBottomBorderBounds.Y = objRelativeLayout.Height / 2;

            Rectangle communityBottomBorderBounds = bottomBorders[domains[3]].Bounds;
            communityBottomBorderBounds.Y = objRelativeLayout.Height / 2;

            // Set the bottom and top row to their right positions
            Rectangle topBorderBounds = rowList[0].Bounds;
            topBorderBounds.Height = objRelativeLayout.Height / 2;

            // Set the bottom and top row to their right positions
            Rectangle topLVBorderBounds = topLV.Bounds;
            topLVBorderBounds.Height = 0;

            // Set the bottom and top row to their right positions
            Rectangle bottomLVBorderBounds = bottomLV.Bounds;
            bottomLVBorderBounds.Height = 0;
            bottomLVBorderBounds.Y = objRelativeLayout.Height;

            // Set the bottom and top row to their right positions
            Rectangle bottomBorderBounds = rowList[1].Bounds;
            bottomBorderBounds.Height = objRelativeLayout.Height / 2;
            bottomBorderBounds.Y = objRelativeLayout.Height / 2;

            await Task.WhenAll(bottomBorders[domains[0]].LayoutTo(personalBottomBorderBounds, animationms, Easing.Linear), bottomBorders[domains[1]].LayoutTo(friendsBottomBorderBounds, animationms, Easing.Linear), bottomBorders[domains[2]].LayoutTo(workBottomBorderBounds, animationms, Easing.Linear), bottomBorders[domains[3]].LayoutTo(communityBottomBorderBounds, animationms, Easing.Linear), rowList[1].LayoutTo(bottomBorderBounds, animationms, Easing.Linear), bottomLV.LayoutTo(bottomLVBorderBounds, animationms, Easing.Linear), rowList[0].LayoutTo(topBorderBounds, animationms, Easing.Linear), topLV.LayoutTo(topLVBorderBounds, animationms, Easing.Linear));
            expanded = false;
        }

        public async Task Refresh()
        {
            if (Todo.App.Database != null && Todo.App.Database.userID != null)
            {
                IsBusy = true;


                //var friendsGoals = await Todo.App.Database.GetChildItems(friends);
                //friendsItems.ItemsSource = friendsGoals;

                if (!listsInitialized)
                {
                    domains = (List<Item>)await Todo.App.Database.GetDomains();



                    rowList = new List<DomainRow>();
                    viewModels = new Dictionary<Item, ItemListViewModel>();
                    listViews = new Dictionary<Item, ListView>();
                    bottomBorders = new Dictionary<Item, BoxView>();

                    foreach (Item it in domains)
                    {
                        var test = new ItemListViewModel(it);
                        viewModels[it] = new ItemListViewModel(it);
                        viewModels[it].FilterAndSort(domainPageType);
                        listViews[it] = new ListView
                        {
                            RowHeight = 40,
                            ItemTemplate = new DataTemplate(typeof(TodoItemCell)),
                            ItemsSource = viewModels[it].Reports,
                        };
                        bottomBorders[it] = new BoxView { Color = Color.White };
                    }



                    //for (int i = 0; i < domains.Count - 1; i++)
                    //{
                    //    if (i % 2 == 0) // is even
                    //    {
                    //        rowList.Add(new DomainRow(listViews[domains[i - 1]], listViews[domains[i]], borderSize, objRelativeLayout, rows));
                    //        //arrPairs.Add(new[] { arr[i], arr[i + 1] });
                    //    }
                    //    else if (i == domains.Count - 1)
                    //    {
                    //        // last one left, only a ListView on the left none at the right side
                    //        rowList.Add(new DomainRow(listViews[domains[i]], null, borderSize, objRelativeLayout, rows));
                    //    }
                    //}

                    rowList.Add(new DomainRow(listViews[domains[0]], viewModels[domains[0]], listViews[domains[1]], viewModels[domains[1]], borderSize, objRelativeLayout, rows));
                    rowList.Add(new DomainRow(listViews[domains[2]], viewModels[domains[2]], listViews[domains[3]], viewModels[domains[3]], borderSize, objRelativeLayout, rows));//rowList.Add(new DomainRow(listViews[domains[2]], listViews[domains[3]], borderSize, objRelativeLayout, rows));

                    //top = new DomainRow(personalItems, friendsItems, borderSize, objRelativeLayout, rows);
                    //bottom = new DomainRow(workItems, communityItems, borderSize, objRelativeLayout, rows);

                    

                    objRelativeLayout.Children.Add(rowList[0],
                        xConstraint: Constraint.Constant(0),
                        yConstraint: Constraint.Constant(0),
                        widthConstraint: Constraint.RelativeToParent((parent) =>
                        {
                            return parent.Width;
                        })
                        ,
                        heightConstraint: Constraint.RelativeToParent((parent) =>
                        {
                            return (expanded ? (parent.Height / 10) : (parent.Height / 2));
                            //return parent.Height / 2;
                        }
                        )
                        );


                    objRelativeLayout.Children.Add(bottomBorders[domains[0]],
                        xConstraint: Constraint.Constant(0),
                        yConstraint: Constraint.RelativeToView(rowList[0],
                        new Func<RelativeLayout, View, double>((pobjRelativeLayout, pobjView) =>
                        {
                            return pobjView.Y + pobjView.Height;
                        })),
                        widthConstraint: Constraint.RelativeToParent((parent) =>
                        {
                            return parent.Width / 2;
                        })
                        ,
                        heightConstraint: Constraint.RelativeToParent((parent) =>
                        {
                            return (selectedDomain.Name == "Personal" ? 0 : borderSize);
                        })
                        );

                    objRelativeLayout.Children.Add(bottomBorders[domains[1]],
                        xConstraint: Constraint.RelativeToParent((parent) =>
                        {
                            return parent.Width / 2;
                        }),
                        yConstraint: Constraint.RelativeToView(rowList[0],
                        new Func<RelativeLayout, View, double>((pobjRelativeLayout, pobjView) =>
                        {
                            return pobjView.Y + pobjView.Height;
                        })),
                        widthConstraint: Constraint.RelativeToParent((parent) =>
                        {
                            return parent.Width / 2;
                        })
                        ,
                        heightConstraint: Constraint.RelativeToParent((parent) =>
                        {
                            return (selectedDomain.Name == "Friends & Family" ? 0 : borderSize);
                        })
                        );

                    objRelativeLayout.Children.Add(topLV,
                        xConstraint: Constraint.Constant(0),
                        yConstraint: Constraint.RelativeToView(rowList[0],
                        new Func<RelativeLayout, View, double>((pobjRelativeLayout, pobjView) =>
                        {
                            return pobjView.Y + pobjView.Height;
                        })),
                        widthConstraint: Constraint.RelativeToParent((parent) =>
                        {
                            return parent.Width;
                        })
                        ,
                        heightConstraint: Constraint.RelativeToParent((parent) =>
                        {
                            return (expanded && selectedDomain.Name == "Personal" || selectedDomain.Name == "Friends & Family" ? parent.Height * 0.8 : 0);
                        })
                        );



                    objRelativeLayout.Children.Add(rowList[1],
                        xConstraint: Constraint.Constant(0),
                        yConstraint: Constraint.RelativeToView(topLV,
                        new Func<RelativeLayout, View, double>((pobjRelativeLayout, pobjView) =>
                        {
                            double Y = pobjView.Y + pobjView.Height;
                            if (expanded)
                            {
                                //Rectangle bottomBounds = bottom.Bounds;
                                //bottomBounds.Y = Y;
                                //bottom.LayoutTo(bottomBounds);

                            }

                            return Y;
                        })),
                        widthConstraint: Constraint.RelativeToParent((parent) =>
                        {
                            return parent.Width;
                        }),
                        heightConstraint: Constraint.RelativeToParent((parent) =>
                        {
                            //Debug.WriteLine((parent.Height/2).ToString());
                            return (expanded ? (parent.Height / 10) : (parent.Height / 2));
                        }
                        )
                        );


                    objRelativeLayout.Children.Add(bottomBorders[domains[2]],
                        xConstraint: Constraint.Constant(0),
                        yConstraint: Constraint.RelativeToView(rowList[1],
                        new Func<RelativeLayout, View, double>((pobjRelativeLayout, pobjView) =>
                        {
                            return pobjView.Y + pobjView.Height;
                        })),
                        widthConstraint: Constraint.RelativeToParent((parent) =>
                        {
                            return parent.Width / 2;
                        })
                        ,
                        heightConstraint: Constraint.RelativeToParent((parent) =>
                        {
                            return (selectedDomain.Name == "Work" ? 0 : borderSize);
                        })
                        );

                    objRelativeLayout.Children.Add(bottomBorders[domains[3]],
                        xConstraint: Constraint.RelativeToParent((parent) =>
                        {
                            return parent.Width / 2;
                        }),
                        yConstraint: Constraint.RelativeToView(rowList[1],
                        new Func<RelativeLayout, View, double>((pobjRelativeLayout, pobjView) =>
                        {
                            return pobjView.Y + pobjView.Height;
                        })),
                        widthConstraint: Constraint.RelativeToParent((parent) =>
                        {
                            return parent.Width / 2;
                        })
                        ,
                        heightConstraint: Constraint.RelativeToParent((parent) =>
                        {
                            return (selectedDomain.Name == "Community" ? 0 : borderSize);
                        })
                        );

                    objRelativeLayout.Children.Add(bottomLV,
                        xConstraint: Constraint.Constant(0),
                        yConstraint: Constraint.RelativeToView(rowList[1],
                        new Func<RelativeLayout, View, double>((pobjRelativeLayout, pobjView) =>
                        {
                            return pobjView.Y + pobjView.Height;
                        })),
                        widthConstraint: Constraint.RelativeToParent((parent) =>
                        {
                            return parent.Width;
                        })
                        ,
                        heightConstraint: Constraint.RelativeToParent((parent) =>
                        {
                            return (expanded && selectedDomain.Name == "Work" || selectedDomain.Name == "Community" ? parent.Height * 0.8 : 0);
                        })
                        );



                    for (int i = 0; i < rowList.Count; i++)
                    {
                        Button leftOverlay = new Button { Opacity = 0 };
                        Button rightOverlay = new Button { Opacity = 0 };


                        if (i == 0) // top row
                        {
                            leftOverlay.Clicked += (async (obj, ev) =>
                            {
                                // collapse
                                if (expanded && selectedDomain.Name == "Personal")
                                {
                                    await expandAnimation();
                                }
                                // collapse personal expand selected
                                else if (expanded)
                                {
                                    if (selectedDomain.Name == "Friends & Family")
                                    {
                                        selectedDomain = domains.Find(x => x.Name == "Personal");// DefaultDomains.Personal;
                                        //topLV.ItemsSource = array;      

                                        if (listViews[domains[0]] != null)
                                        {
                                            ////topLV.ItemsSource = listViews[domains[0]].ItemsSource;
                                            //List<Item> items = new List<Item>();

                                            //foreach (var item in listViews[domains[0]].ItemsSource)
                                            //{
                                            //    items.Add((Item)item);
                                            //}

                                            //var collection = (ObservableCollection<Item>)listViews[domains[0]].ItemsSource;
                                            //topLV.ItemCollection = (ObservableCollection<Item>)listViews[domains[0]].ItemsSource;
                                            //Debug.WriteLine(collection.Count.ToString());
                                            ////topLV.addItems(items);

                                            topLV.ItemCollection = (ObservableCollection<Item>)listViews[domains[0]].ItemsSource;
                                        }
                                    }
                                    else
                                    {
                                        selectedDomain = domains.Find(x => x.Name == "Personal");
                                        rowList[0].hideItems();
                                        rowList[1].hideItems();

                                        //topLV.ItemsSource = array;

                                        if (listViews[domains[0]] != null)
                                        {
                                            ////topLV.ItemsSource = listViews[domains[0]].ItemsSource;
                                            //List<Item> items = new List<Item>();

                                            //foreach (var item in listViews[domains[0]].ItemsSource)
                                            //{
                                            //    items.Add((Item)item);
                                            //}

                                            //var collection = (ObservableCollection<Item>)listViews[domains[0]].ItemsSource;
                                            //topLV.ItemCollection = (ObservableCollection<Item>)listViews[domains[0]].ItemsSource;
                                            //Debug.WriteLine(collection.Count.ToString());
                                            ////topLV.addItems(items);

                                            topLV.ItemCollection = (ObservableCollection<Item>)listViews[domains[0]].ItemsSource;
                                        }

                                        // for all the bottom borders except the neighbour to the right (friends and family)
                                        Rectangle personalBottomBorderBounds = bottomBorders[domains[0]].Bounds;
                                        personalBottomBorderBounds.Y = objRelativeLayout.Height / 10 * 9;

                                        Rectangle workBottomBorderBounds = bottomBorders[domains[2]].Bounds;
                                        workBottomBorderBounds.Y = objRelativeLayout.Height;

                                        Rectangle communityBottomBorderBounds = bottomBorders[domains[3]].Bounds;
                                        communityBottomBorderBounds.Y = objRelativeLayout.Height;

                                        // Set the bottom and top row to their right positions
                                        Rectangle topBorderBounds = rowList[0].Bounds;
                                        topBorderBounds.Height = objRelativeLayout.Height / 10;

                                        Rectangle bottomBorderBounds = rowList[1].Bounds;
                                        bottomBorderBounds.Height = objRelativeLayout.Height / 10;
                                        bottomBorderBounds.Y = objRelativeLayout.Height / 10 * 9;

                                        // Reduces bottom Listview to nothing
                                        Rectangle bottomLVBounds = bottomLV.Bounds;
                                        bottomLVBounds.Height = 0;
                                        bottomLVBounds.Y = objRelativeLayout.Height;

                                        await Task.WhenAll(bottomBorders[domains[0]].LayoutTo(personalBottomBorderBounds, animationms, Easing.Linear), bottomBorders[domains[2]].LayoutTo(workBottomBorderBounds, animationms, Easing.Linear), bottomBorders[domains[3]].LayoutTo(communityBottomBorderBounds, animationms, Easing.Linear), rowList[1].LayoutTo(bottomBorderBounds, animationms, Easing.Linear), rowList[0].LayoutTo(topBorderBounds, animationms, Easing.Linear), bottomLV.LayoutTo(bottomLVBounds, animationms, Easing.Linear));

                                    }
                                }
                                else
                                {
                                    selectedDomain = domains.Find(x => x.Name == "Personal");
                                    expanded = true;

                                    rowList[0].hideItems();
                                    rowList[1].hideItems();

                                    //topLV.ItemsSource = array;

                                    if (listViews[domains[0]] != null)
                                    {
                                        ////topLV.ItemsSource = listViews[domains[0]].ItemsSource;
                                        //List<Item> items = new List<Item>();

                                        //foreach (var item in listViews[domains[0]].ItemsSource)
                                        //{
                                        //    items.Add((Item)item);
                                        //}

                                        //var collection = (ObservableCollection<Item>)listViews[domains[0]].ItemsSource;
                                        //topLV.ItemCollection = (ObservableCollection<Item>)listViews[domains[0]].ItemsSource;
                                        //Debug.WriteLine(collection.Count.ToString());
                                        ////topLV.addItems(items);

                                        topLV.ItemCollection = (ObservableCollection<Item>)listViews[domains[0]].ItemsSource;
                                    }

                                    // Sets the bottom borders of friends and personal to their new values
                                    Rectangle personalBottomBorderBounds = bottomBorders[domains[0]].Bounds;
                                    personalBottomBorderBounds.Y = objRelativeLayout.Height / 10 * 9;

                                    Rectangle friendsBottomBorderBounds = bottomBorders[domains[1]].Bounds;
                                    friendsBottomBorderBounds.Y = rightOverlay.Height * 2 / 10;

                                    // Set the bottom and top row to their right positions
                                    Rectangle topBorderBounds = rowList[0].Bounds;
                                    topBorderBounds.Height = objRelativeLayout.Height / 10;

                                    Rectangle bottomBorderBounds = rowList[1].Bounds;
                                    bottomBorderBounds.Height = objRelativeLayout.Height / 10;
                                    bottomBorderBounds.Y = objRelativeLayout.Height / 10 * 9;

                                    await Task.WhenAll(bottomBorders[domains[0]].LayoutTo(personalBottomBorderBounds, animationms, Easing.Linear), bottomBorders[domains[1]].LayoutTo(friendsBottomBorderBounds, animationms, Easing.Linear), rowList[1].LayoutTo(bottomBorderBounds, animationms, Easing.Linear), rowList[0].LayoutTo(topBorderBounds, animationms, Easing.Linear));

                                }
                                objRelativeLayout.ForceLayout();
                            });

                            rightOverlay.Clicked += (async (obj, ev) =>
                            {
                                // collapse
                                if (expanded && selectedDomain.Name == "Friends & Family")
                                {
                                    await expandAnimation();
                                }
                                // collapse personal expand selected
                                else if (expanded)
                                {
                                    if (selectedDomain.Name == "Personal")
                                    {
                                        selectedDomain = domains.Find(x => x.Name == "Friends & Family");

                                        if (listViews[domains[1]] != null)
                                        {
                                            topLV.ItemCollection = (ObservableCollection<Item>)listViews[domains[1]].ItemsSource;
                                        }

                                        //topLV.ItemsSource = array2;
                                    }
                                    else
                                    {
                                        selectedDomain = domains.Find(x => x.Name == "Friends & Family");
                                        rowList[1].hideItems();
                                        rowList[0].hideItems();


                                        if (listViews[domains[1]] != null)
                                        {
                                            topLV.ItemCollection = (ObservableCollection<Item>)listViews[domains[1]].ItemsSource;
                                        }

                                        //topLV.ItemsSource = array2;

                                        // Sets the bottom borders to their new values
                                        Rectangle friendsBottomBorderBounds = bottomBorders[domains[1]].Bounds;
                                        friendsBottomBorderBounds.Y = objRelativeLayout.Height / 10 * 9;

                                        Rectangle workBottomBorderBounds = bottomBorders[domains[2]].Bounds;
                                        workBottomBorderBounds.Y = objRelativeLayout.Height;

                                        Rectangle communityBottomBorderBounds = bottomBorders[domains[3]].Bounds;
                                        communityBottomBorderBounds.Y = objRelativeLayout.Height;

                                        // Set the bottom and top row to their right positions
                                        Rectangle topBorderBounds = rowList[0].Bounds;
                                        topBorderBounds.Height = objRelativeLayout.Height / 10;

                                        Rectangle bottomBorderBounds = rowList[1].Bounds;
                                        bottomBorderBounds.Height = objRelativeLayout.Height / 10;
                                        bottomBorderBounds.Y = objRelativeLayout.Height / 10 * 9;

                                        // Reduces bottom Listview to nothing
                                        Rectangle bottomLVBounds = bottomLV.Bounds;
                                        bottomLVBounds.Height = 0;
                                        bottomLVBounds.Y = objRelativeLayout.Height;

                                        await Task.WhenAll(bottomBorders[domains[1]].LayoutTo(friendsBottomBorderBounds, animationms, Easing.Linear), bottomBorders[domains[2]].LayoutTo(workBottomBorderBounds, animationms, Easing.Linear), bottomBorders[domains[3]].LayoutTo(communityBottomBorderBounds, animationms, Easing.Linear), rowList[1].LayoutTo(bottomBorderBounds, animationms, Easing.Linear), rowList[0].LayoutTo(topBorderBounds, animationms, Easing.Linear), bottomLV.LayoutTo(bottomLVBounds, animationms, Easing.Linear));

                                    }
                                }
                                else
                                {
                                    selectedDomain = domains.Find(x => x.Name == "Friends & Family");
                                    expanded = true;
                                    rowList[0].hideItems();
                                    rowList[1].hideItems();

                                    // Sets the bottom borders of friends and personal to their new values
                                    Rectangle personalBottomBorderBounds = bottomBorders[domains[0]].Bounds;
                                    personalBottomBorderBounds.Y = objRelativeLayout.Height / 10;

                                    Rectangle friendsBottomBorderBounds = bottomBorders[domains[1]].Bounds;
                                    friendsBottomBorderBounds.Y = objRelativeLayout.Height / 10 * 9;

                                    // Set the bottom and top row to their right positions
                                    Rectangle topBorderBounds = rowList[0].Bounds;
                                    topBorderBounds.Height = objRelativeLayout.Height / 10;

                                    Rectangle bottomBorderBounds = rowList[1].Bounds;
                                    bottomBorderBounds.Height = objRelativeLayout.Height / 10;
                                    bottomBorderBounds.Y = objRelativeLayout.Height / 10 * 9;

                                    await Task.WhenAll(bottomBorders[domains[0]].LayoutTo(personalBottomBorderBounds, animationms, Easing.Linear), bottomBorders[domains[1]].LayoutTo(friendsBottomBorderBounds, animationms, Easing.Linear), rowList[1].LayoutTo(bottomBorderBounds, animationms, Easing.Linear), rowList[0].LayoutTo(topBorderBounds, animationms, Easing.Linear));

                                    if (listViews[domains[1]] != null)
                                    {
                                        topLV.ItemCollection = (ObservableCollection<Item>)listViews[domains[1]].ItemsSource;
                                    }

                                    //topLV.ItemsSource = array2;
                                }
                                objRelativeLayout.ForceLayout();
                            });

                            objRelativeLayout.Children.Add(leftOverlay,
                                xConstraint: Constraint.RelativeToView(rowList[0],
                                new Func<RelativeLayout, View, double>((pobjRelativeLayout, pobjView) =>
                                {
                                    return pobjView.X;
                                })),
                                yConstraint: Constraint.RelativeToView(rowList[0],
                                new Func<RelativeLayout, View, double>((pobjRelativeLayout, pobjView) =>
                                {
                                    return pobjView.Y;
                                })),
                                widthConstraint: Constraint.RelativeToView(rowList[0],
                                new Func<RelativeLayout, View, double>((pobjRelativeLayout, pobjView) =>
                                {
                                    return pobjView.Width / 2 - borderSize;
                                })),
                                heightConstraint: Constraint.RelativeToView(rowList[0],
                                new Func<RelativeLayout, View, double>((pobjRelativeLayout, pobjView) =>
                                {
                                    return pobjView.Height;
                                }))
                                );

                            objRelativeLayout.Children.Add(rightOverlay,
                                xConstraint: Constraint.RelativeToView(rowList[0],
                                new Func<RelativeLayout, View, double>((pobjRelativeLayout, pobjView) =>
                                {
                                    return pobjView.Width / 2 + borderSize;
                                })),
                                yConstraint: Constraint.RelativeToView(rowList[0],
                                new Func<RelativeLayout, View, double>((pobjRelativeLayout, pobjView) =>
                                {
                                    return pobjView.Y;
                                })),
                                widthConstraint: Constraint.RelativeToView(rowList[0],
                                new Func<RelativeLayout, View, double>((pobjRelativeLayout, pobjView) =>
                                {
                                    return pobjView.Width / 2 - borderSize;
                                })),
                                heightConstraint: Constraint.RelativeToView(rowList[0],
                                new Func<RelativeLayout, View, double>((pobjRelativeLayout, pobjView) =>
                                {
                                    return pobjView.Height;
                                }))
                                );
                        }
                        else if (i == 1) // second row (bottom for now)
                        {
                            leftOverlay.Clicked += (async (obj, ev) =>
                            {
                                // collapse
                                if (expanded && selectedDomain.Name == "Work")
                                {
                                    await expandAnimation();
                                }
                                // collapse personal expand selected
                                else if (expanded)
                                {
                                    if (selectedDomain.Name == "Community")
                                    {
                                        selectedDomain = domains.Find(x => x.Name == "Work");

                                        if (listViews[domains[2]] != null)
                                        {
                                            bottomLV.ItemCollection = (ObservableCollection<Item>)listViews[domains[2]].ItemsSource;
                                        }

                                        //bottomLV.ItemsSource = array3;
                                    }
                                    else
                                    {
                                        selectedDomain = domains.Find(x => x.Name == "Work");
                                        rowList[0].hideItems();
                                        rowList[1].hideItems();

                                        if (listViews[domains[2]] != null)
                                        {
                                            bottomLV.ItemCollection = (ObservableCollection<Item>)listViews[domains[2]].ItemsSource;
                                        }

                                        //bottomLV.ItemsSource = array3;

                                        // Sets all the borders on the bottom of the domains to their right positions
                                        Rectangle personalBottomBorderBounds = bottomBorders[domains[0]].Bounds;
                                        personalBottomBorderBounds.Y = objRelativeLayout.Height / 10;

                                        Rectangle friendsBottomBorderBounds = bottomBorders[domains[1]].Bounds;
                                        friendsBottomBorderBounds.Y = objRelativeLayout.Height / 10;

                                        Rectangle communityBottomBorderBounds = bottomBorders[domains[3]].Bounds;
                                        communityBottomBorderBounds.Y = objRelativeLayout.Height / 10 * 2;

                                        // Set the bottom and top row to their right positions
                                        Rectangle topBorderBounds = rowList[0].Bounds;
                                        topBorderBounds.Height = objRelativeLayout.Height / 10;

                                        Rectangle bottomBorderBounds = rowList[1].Bounds;
                                        bottomBorderBounds.Height = objRelativeLayout.Height / 10;
                                        bottomBorderBounds.Y = objRelativeLayout.Height / 10;

                                        // Reduces Top Listview to nothing
                                        Rectangle topLVBounds = topLV.Bounds;
                                        topLVBounds.Height = 0;

                                        // Actually moves the elements over
                                        await Task.WhenAll(bottomBorders[domains[0]].LayoutTo(personalBottomBorderBounds, animationms, Easing.Linear), bottomBorders[domains[1]].LayoutTo(friendsBottomBorderBounds, animationms, Easing.Linear), bottomBorders[domains[3]].LayoutTo(communityBottomBorderBounds, animationms, Easing.Linear), rowList[1].LayoutTo(bottomBorderBounds, animationms, Easing.Linear), rowList[0].LayoutTo(topBorderBounds, animationms, Easing.Linear), topLV.LayoutTo(topLVBounds, animationms, Easing.Linear));

                                    }
                                }
                                else
                                {
                                    selectedDomain = domains.Find(x => x.Name == "Work");
                                    expanded = true;
                                    rowList[1].hideItems();
                                    rowList[0].hideItems();

                                    if (listViews[domains[2]] != null)
                                    {
                                        bottomLV.ItemCollection = (ObservableCollection<Item>)listViews[domains[2]].ItemsSource;
                                    }

                                    //bottomLV.ItemsSource = array3;

                                    // Sets all the borders on the bottom of the domains to their right positions
                                    Rectangle personalBottomBorderBounds = bottomBorders[domains[0]].Bounds;
                                    personalBottomBorderBounds.Y = objRelativeLayout.Height / 10;

                                    Rectangle friendsBottomBorderBounds = bottomBorders[domains[1]].Bounds;
                                    friendsBottomBorderBounds.Y = objRelativeLayout.Height / 10;

                                    Rectangle communityBottomBorderBounds = bottomBorders[domains[3]].Bounds;
                                    communityBottomBorderBounds.Y = objRelativeLayout.Height / 10 * 2;

                                    // Set the bottom and top row to their right positions
                                    Rectangle topBorderBounds = rowList[0].Bounds;
                                    topBorderBounds.Height = objRelativeLayout.Height / 10;

                                    Rectangle bottomBorderBounds = rowList[1].Bounds;
                                    bottomBorderBounds.Height = objRelativeLayout.Height / 10;
                                    bottomBorderBounds.Y = objRelativeLayout.Height / 10;

                                    // Actually moves the elements over
                                    await Task.WhenAll(bottomBorders[domains[0]].LayoutTo(personalBottomBorderBounds, animationms, Easing.Linear), bottomBorders[domains[1]].LayoutTo(friendsBottomBorderBounds, animationms, Easing.Linear), bottomBorders[domains[3]].LayoutTo(communityBottomBorderBounds, animationms, Easing.Linear), rowList[1].LayoutTo(bottomBorderBounds, animationms, Easing.Linear), rowList[0].LayoutTo(topBorderBounds, animationms, Easing.Linear));

                                }
                                objRelativeLayout.ForceLayout();
                            });


                            rightOverlay.Clicked += (async (obj, ev) =>
                            {
                                // collapse
                                if (expanded && selectedDomain.Name == "Community")
                                {
                                    await expandAnimation();
                                }
                                // collapse personal expand selected
                                else if (expanded)
                                {
                                    if (selectedDomain.Name == "Work")
                                    {
                                        selectedDomain = domains.Find(x => x.Name == "Community");

                                        if (listViews[domains[3]] != null)
                                        {
                                            bottomLV.ItemCollection = (ObservableCollection<Item>)listViews[domains[3]].ItemsSource;
                                        }

                                        //bottomLV.ItemsSource = array4;
                                    }
                                    else
                                    {
                                        selectedDomain = domains.Find(x => x.Name == "Community");
                                        rowList[0].hideItems();
                                        rowList[1].hideItems();
                                        if (listViews[domains[3]] != null)
                                        {
                                            bottomLV.ItemCollection = (ObservableCollection<Item>)listViews[domains[3]].ItemsSource;
                                        }

                                        //bottomLV.ItemsSource = array4;

                                        // Sets all the borders on the bottom of the domains to their right positions
                                        Rectangle personalBottomBorderBounds = bottomBorders[domains[0]].Bounds;
                                        personalBottomBorderBounds.Y = objRelativeLayout.Height / 10;

                                        Rectangle friendsBottomBorderBounds = bottomBorders[domains[1]].Bounds;
                                        friendsBottomBorderBounds.Y = objRelativeLayout.Height / 10;

                                        Rectangle workBottomBorderBounds = bottomBorders[domains[2]].Bounds;
                                        workBottomBorderBounds.Y = objRelativeLayout.Height / 10 * 2;

                                        // Set the bottom and top row to their right positions
                                        Rectangle topBorderBounds = rowList[0].Bounds;
                                        topBorderBounds.Height = objRelativeLayout.Height / 10;

                                        Rectangle bottomBorderBounds = rowList[1].Bounds;
                                        bottomBorderBounds.Height = objRelativeLayout.Height / 10;
                                        bottomBorderBounds.Y = objRelativeLayout.Height / 10;

                                        // Reduces Top Listview to nothing
                                        Rectangle topLVBounds = topLV.Bounds;
                                        topLVBounds.Height = 0;

                                        // Actually moves the elements over
                                        await Task.WhenAll(bottomBorders[domains[0]].LayoutTo(personalBottomBorderBounds, animationms, Easing.Linear), bottomBorders[domains[1]].LayoutTo(friendsBottomBorderBounds, animationms, Easing.Linear), bottomBorders[domains[2]].LayoutTo(workBottomBorderBounds, animationms, Easing.Linear), rowList[1].LayoutTo(bottomBorderBounds, animationms, Easing.Linear), rowList[0].LayoutTo(topBorderBounds, animationms, Easing.Linear), topLV.LayoutTo(topLVBounds, animationms, Easing.Linear));

                                    }
                                }
                                else
                                {
                                    selectedDomain = domains.Find(x => x.Name == "Community");
                                    expanded = true;
                                    rowList[1].hideItems();
                                    rowList[0].hideItems();

                                    // Sets all the borders on the bottom of the domains to their right positions
                                    Rectangle personalBottomBorderBounds = bottomBorders[domains[0]].Bounds;
                                    personalBottomBorderBounds.Y = objRelativeLayout.Height / 10;

                                    Rectangle friendsBottomBorderBounds = bottomBorders[domains[1]].Bounds;
                                    friendsBottomBorderBounds.Y = objRelativeLayout.Height / 10;

                                    Rectangle workBottomBorderBounds = bottomBorders[domains[2]].Bounds;
                                    workBottomBorderBounds.Y = objRelativeLayout.Height / 10 * 2;

                                    // Set the bottom and top row to their right positions
                                    Rectangle topBorderBounds = rowList[0].Bounds;
                                    topBorderBounds.Height = objRelativeLayout.Height / 10;

                                    Rectangle bottomBorderBounds = rowList[1].Bounds;
                                    bottomBorderBounds.Height = objRelativeLayout.Height / 10;
                                    bottomBorderBounds.Y = objRelativeLayout.Height / 10;

                                    // Actually moves the elements over
                                    await Task.WhenAll(bottomBorders[domains[0]].LayoutTo(personalBottomBorderBounds, animationms, Easing.Linear), bottomBorders[domains[1]].LayoutTo(friendsBottomBorderBounds, animationms, Easing.Linear), bottomBorders[domains[2]].LayoutTo(workBottomBorderBounds, animationms, Easing.Linear), rowList[1].LayoutTo(bottomBorderBounds, animationms, Easing.Linear), rowList[0].LayoutTo(topBorderBounds, animationms, Easing.Linear));

                                    if (listViews[domains[3]] != null)
                                    {
                                        bottomLV.ItemCollection = (ObservableCollection<Item>)listViews[domains[3]].ItemsSource;
                                    }

                                    //bottomLV.ItemsSource = array4;
                                }
                                objRelativeLayout.ForceLayout();
                            });



                            objRelativeLayout.Children.Add(leftOverlay,
                                xConstraint: Constraint.RelativeToView(rowList[1],
                                new Func<RelativeLayout, View, double>((pobjRelativeLayout, pobjView) =>
                                {
                                    return pobjView.X;
                                })),
                                yConstraint: Constraint.RelativeToView(rowList[1],
                                new Func<RelativeLayout, View, double>((pobjRelativeLayout, pobjView) =>
                                {
                                    return pobjView.Y;
                                })),
                                widthConstraint: Constraint.RelativeToView(rowList[1],
                                new Func<RelativeLayout, View, double>((pobjRelativeLayout, pobjView) =>
                                {
                                    return pobjView.Width / 2 - borderSize;
                                })),
                                heightConstraint: Constraint.RelativeToView(rowList[1],
                                new Func<RelativeLayout, View, double>((pobjRelativeLayout, pobjView) =>
                                {
                                    return pobjView.Height;
                                }))
                                );

                            objRelativeLayout.Children.Add(rightOverlay,
                                xConstraint: Constraint.RelativeToView(rowList[1],
                                new Func<RelativeLayout, View, double>((pobjRelativeLayout, pobjView) =>
                                {
                                    return pobjView.Width / 2 + borderSize;
                                })),
                                yConstraint: Constraint.RelativeToView(rowList[1],
                                new Func<RelativeLayout, View, double>((pobjRelativeLayout, pobjView) =>
                                {
                                    return pobjView.Y;
                                })),
                                widthConstraint: Constraint.RelativeToView(rowList[1],
                                new Func<RelativeLayout, View, double>((pobjRelativeLayout, pobjView) =>
                                {
                                    return pobjView.Width / 2 - borderSize;
                                })),
                                heightConstraint: Constraint.RelativeToView(rowList[1],
                                new Func<RelativeLayout, View, double>((pobjRelativeLayout, pobjView) =>
                                {
                                    return pobjView.Height;
                                }))
                                );

                        }
                    }



























                    //personalItemsList = new ItemListViewModel(domains);
                    //personalItemsList.FilterAndSort(domainPageType);
                    //personalItems.ItemsSource = (IEnumerable<Item>)personalItemsList.Reports;

                    //top.setLeftFooter(personalItemsList);

                    //friendsItemsList = new ItemListViewModel("friends");
                    //friendsItemsList.FilterAndSort(domainPageType);
                    //friendsItems.ItemsSource = (IEnumerable<Item>) friendsItemsList.Reports;

                    //top.setRightFooter(friendsItemsList);

                    //workItemsList = new ItemListViewModel("work");
                    //workItemsList.FilterAndSort(domainPageType);
                    //workItems.ItemsSource = (IEnumerable<Item>)workItemsList.Reports;

                    //bottom.setLeftFooter(workItemsList);

                    //communityItemsList = new ItemListViewModel("community");
                    //communityItemsList.FilterAndSort(domainPageType);
                    //communityItems.ItemsSource = (IEnumerable<Item>)communityItemsList.Reports;

                    //bottom.setRightFooter(communityItemsList);

                    //personalFooter = new StackLayout { Spacing = 0, Orientation = StackOrientation.Horizontal };
                    //personalFooter.Children.Add(new Label { Text = "Personal footer" });

                    //friendsFooter = new StackLayout { Spacing = 0, Orientation = StackOrientation.Horizontal };
                    //friendsFooter.Children.Add(new Label { Text = "Friends & Family footer" });

                    //top.changeLeftFooter(personalFooter);
                    //top.changeRightFooter(friendsFooter);

                    listsInitialized = true;
                }

                foreach (Item dom in domains)
                {
                    StackLayout head = new StackLayout { Spacing = 1};

                    switch (dom.Name)
                    {
                        case "Personal":
                            head.Children.Add(new Label { Text = dom.Name, TextColor = Color.Yellow, FontSize = 20, FontAttributes = FontAttributes.Bold });
                            break;
                        case "Friends & Family":
                            head.Children.Add(new Label { Text = dom.Name, TextColor = Color.FromRgb(255, 105, 0), FontSize = 20, FontAttributes = FontAttributes.Bold });
                            break;
                        case "Work":
                            head.Children.Add(new Label { Text = dom.Name, TextColor = Color.FromRgb(32, 178, 170), FontSize = 20, FontAttributes = FontAttributes.Bold });
                            break;
                        case "Community":
                            head.Children.Add(new Label { Text = dom.Name, TextColor = Color.FromRgb(153, 50, 204), FontSize = 20, FontAttributes = FontAttributes.Bold });
                            break;
                        default:
                            break;
                    }
                    head.Children.Add(viewModels[dom].Footer);

                    //head.Children.Add(new Label { Text = ' ' + viewModels[dom].Reports.Count.ToString() + " items", TextColor = Color.White });
                    listViews[dom].Header = head;
                }

                this.ForceLayout();
                IsBusy = false;
            }
        }

    }


}
