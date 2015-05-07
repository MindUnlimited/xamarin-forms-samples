﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Todo.Models;
using Xamarin.Forms;

namespace Todo.Views
{
    public class DomainPage : ContentPage
    {
        enum Domains
        {
            None,
            Family,
            Friends,
            Work,
            Community
        };
        private bool expanded = false;
        private bool friendsInit = false;
        private Domains selected = Domains.None;

        private RelativeLayout objRelativeLayout;

        private ListView famItems;
        private ListView friendsItems;
        private ListView workItems;
        private ListView communityItems;

        BoxView familyBottomBorder;
        BoxView friendsBottomBorder;
        BoxView workBottomBorder;
        BoxView communityBottomBorder;

        private DomainRow top;
        ListView topLV;
        private DomainRow bottom;
        ListView bottomLV;

        private int rows = 2;
        private int borderSize = 1; // pixels
        Dictionary<ListView, IEnumerable<string>> itemStorage = new Dictionary<ListView, IEnumerable<string>>();
        

        public DomainPage()
        {
            //List<ListView> listViews = new List<ListView>();
            objRelativeLayout = new RelativeLayout
            {
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            StackLayout objStackLayout = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
            };

            famItems = new ListView
            {
                RowHeight = 40
            };
            famItems.ItemTemplate = new DataTemplate(typeof(TodoItemCell));

            StackLayout famHead = new StackLayout { Padding = 2, Spacing = 1 };
            famHead.Children.Add(new Label { Text = "Family", TextColor = Color.Yellow, FontSize = 20, FontAttributes = FontAttributes.Bold });
            //famHead.Children.Add(new Label { Text = ' ' + ((ICollection<TodoItemCell>)famItems.ItemsSource).Count.ToString() + " items" });
            famItems.Header = famHead;

            famItems.ChildAdded += ((o, e) => 
            {
                famHead = new StackLayout { Padding = 2, Spacing = 1 };
                famHead.Children.Add(new Label { Text = "Family", TextColor = Color.Yellow, FontSize = 20, FontAttributes = FontAttributes.Bold });
                if (famItems.ItemsSource != null)
                    famHead.Children.Add(new Label { Text = ' ' + ((ICollection<Item>)famItems.ItemsSource).Count.ToString() + " items" });
                famItems.Header = famHead;
            });

            famItems.ChildRemoved += ((o, e) =>
            {
                famHead = new StackLayout { Padding = 2, Spacing = 1 };
                famHead.Children.Add(new Label { Text = "Family", TextColor = Color.Yellow, FontSize = 20, FontAttributes = FontAttributes.Bold });
                famHead.Children.Add(new Label { Text = ' ' + ((ICollection<Item>)famItems.ItemsSource).Count.ToString() + " items" });
                famItems.Header = famHead;
            });


            friendsItems = new ListView
            {
                RowHeight = 40
            };
            friendsItems.ItemTemplate = new DataTemplate(typeof(TodoItemCell));

            StackLayout friendsHead = new StackLayout { Padding = 2, Spacing = 1 };
            friendsHead.Children.Add(new Label { Text = "Friends", TextColor = Color.FromRgb(255, 105, 0), FontSize = 20, FontAttributes = FontAttributes.Bold });
            //famHead.Children.Add(new Label { Text = ' ' + ((ICollection<TodoItemCell>)famItems.ItemsSource).Count.ToString() + " items" });
            friendsItems.Header = friendsHead;

            friendsItems.ChildAdded += ((o, e) =>
            {
                friendsHead = new StackLayout { Padding = 2, Spacing = 1 };
                friendsHead.Children.Add(new Label { Text = "Friends", TextColor = Color.FromRgb(255, 105, 0), FontSize = 20, FontAttributes = FontAttributes.Bold });
                if (friendsItems.ItemsSource != null)
                    friendsHead.Children.Add(new Label { Text = ' ' + ((ICollection<Item>)friendsItems.ItemsSource).Count.ToString() + " items" });
                friendsItems.Header = famHead;
            });

            friendsItems.ChildRemoved += ((o, e) =>
            {
                friendsHead = new StackLayout { Padding = 2, Spacing = 1 };
                friendsHead.Children.Add(new Label { Text = "Friends", TextColor = Color.FromRgb(255, 105, 0), FontSize = 20, FontAttributes = FontAttributes.Bold });
                if (friendsItems.ItemsSource != null)
                    friendsHead.Children.Add(new Label { Text = ' ' + ((ICollection<Item>)friendsItems.ItemsSource).Count.ToString() + " items" });
                friendsItems.Header = famHead;
            });

            workItems = new ListView
            {
                RowHeight = 40
            };
            workItems.ItemTemplate = new DataTemplate(typeof(TodoItemCell));

            StackLayout workHead = new StackLayout { Padding = 2, Spacing = 1 };
            workHead.Children.Add(new Label { Text = "Work", TextColor = Color.FromRgb(32, 178, 170), FontSize = 20, FontAttributes = FontAttributes.Bold });
            //workHead.Children.Add(new Label { Text = ' ' + ((ICollection<TodoItemCell>)workItems.ItemsSource).Count.ToString() + " items" });
            workItems.Header = workHead;

            workItems.ChildAdded += ((o, e) =>
            {
                workHead = new StackLayout { Padding = 2, Spacing = 1 };
                workHead.Children.Add(new Label { Text = "Work", TextColor = Color.FromRgb(32, 178, 170), FontSize = 20, FontAttributes = FontAttributes.Bold });
                if (friendsItems.ItemsSource != null)
                    workHead.Children.Add(new Label { Text = ' ' + ((ICollection<Item>)workItems.ItemsSource).Count.ToString() + " items" });
                workItems.Header = workHead;
            });

            workItems.ChildRemoved += ((o, e) =>
            {
                workHead = new StackLayout { Padding = 2, Spacing = 1 };
                workHead.Children.Add(new Label { Text = "Work", TextColor = Color.FromRgb(32, 178, 170), FontSize = 20, FontAttributes = FontAttributes.Bold });
                if (friendsItems.ItemsSource != null)
                    workHead.Children.Add(new Label { Text = ' ' + ((ICollection<Item>)workItems.ItemsSource).Count.ToString() + " items" });
                workItems.Header = workHead;
            });



            communityItems = new ListView
            {
                RowHeight = 40
            };
            communityItems.ItemTemplate = new DataTemplate(typeof(TodoItemCell));

            StackLayout communityHead = new StackLayout { Padding = 2, Spacing = 1 };
            communityHead.Children.Add(new Label { Text = "Community", TextColor = Color.FromRgb(153, 50, 204), FontSize = 20, FontAttributes = FontAttributes.Bold });
            //communityHead.Children.Add(new Label { Text = ' ' + ((ICollection<TodoItemCell>)communityItems.ItemsSource).Count.ToString() + " items" });
            communityItems.Header = communityHead;

            communityItems.ChildAdded += ((o, e) =>
            {
                communityHead = new StackLayout { Padding = 2, Spacing = 1 };
                communityHead.Children.Add(new Label { Text = "Community", TextColor = Color.FromRgb(153, 50, 204), FontSize = 20, FontAttributes = FontAttributes.Bold });
                if (friendsItems.ItemsSource != null)
                    communityHead.Children.Add(new Label { Text = ' ' + ((ICollection<Item>)communityItems.ItemsSource).Count.ToString() + " items" });
                communityItems.Header = communityHead;
            });

            communityItems.ChildRemoved += ((o, e) =>
            {
                communityHead = new StackLayout { Padding = 2, Spacing = 1 };
                communityHead.Children.Add(new Label { Text = "Community", TextColor = Color.FromRgb(153, 50, 204), FontSize = 20, FontAttributes = FontAttributes.Bold });
                if (friendsItems.ItemsSource != null)
                    communityHead.Children.Add(new Label { Text = ' ' + ((ICollection<Item>)communityItems.ItemsSource).Count.ToString() + " items" });
                communityItems.Header = communityHead;
            });






            ListView familyLV = new ListView{Header = "Family"};
            string[] array = { "fam1", "fam2", "fam3", "fam4", "fam5", "fam6", "fam7", "fam8", "fam9", "fam10" };
            familyLV.ItemsSource = array;
            //familyLV.BackgroundColor = Color.Red;    

            //StackLayout famHead = new StackLayout { Padding = 2, Spacing = 1 };
            //famHead.Children.Add(new Label { Text = "Family", TextColor = Color.Yellow, Font = Font.BoldSystemFontOfSize(30) });
            //famHead.Children.Add(new Label { Text = ' ' + ((ICollection<string>)familyLV.ItemsSource).Count.ToString() + " items" });
            //familyLV.Header = famHead;


            ListView friendsLV = new ListView { Header = "Friends" };
            string[] array2 = { "friends1", "friends2", "friends3", "friends4" };
            friendsLV.ItemsSource = array2;

            //StackLayout friendsHead = new StackLayout { Padding = 2, Spacing = 1 };
            //friendsHead.Children.Add(new Label { Text = "Friends", TextColor = Color.FromRgb(255,105,0), FontSize = 20, FontAttributes = FontAttributes.Bold });
            //friendsHead.Children.Add(new Label { Text = ' ' + ((ICollection<string>)friendsLV.ItemsSource).Count.ToString() + " items" });
            //friendsLV.Header = friendsHead;

            ListView workLV = new ListView { Header = "Work" };
            string[] array3 = { "work1", "work2", "work3", "work4"};
            workLV.ItemsSource = array3;

            //StackLayout workHead = new StackLayout { Padding = 2, Spacing = 1 };
            //workHead.Children.Add(new Label { Text = "Work", TextColor = Color.FromRgb(32, 178, 170), FontSize = 20, FontAttributes = FontAttributes.Bold });
            //workHead.Children.Add(new Label { Text = ' ' + ((ICollection<string>)workLV.ItemsSource).Count.ToString() + " items" });
            //workLV.Header = workHead;

            ListView communityLV = new ListView { Header = "Community" };
            string[] array4 = { "commu1", "commu2", "commu3", "commu4", "commu5" };
            communityLV.ItemsSource = array4;

            //StackLayout communityHead = new StackLayout { Padding = 2, Spacing = 1 };
            //communityHead.Children.Add(new Label { Text = "Community", TextColor = Color.FromRgb(153, 50, 204), FontSize = 20, FontAttributes = FontAttributes.Bold });
            //communityHead.Children.Add(new Label { Text = ' ' + ((ICollection<string>)communityLV.ItemsSource).Count.ToString() + " items" });
            //communityLV.Header = communityHead;

            top = new DomainRow(famItems, friendsItems, borderSize);
            bottom = new DomainRow(workItems, communityItems, borderSize);


            topLV = new ListView{ItemTemplate = new DataTemplate(typeof(TodoItemCell))};

            topLV.ItemSelected += (sender, e) =>
            {
                var Item = (Item)e.SelectedItem;
                var todoPage = new TodoItemPage();
                todoPage.BindingContext = Item;
                Navigation.PushAsync(todoPage);
            };

            bottomLV = new ListView{ ItemTemplate = new DataTemplate(typeof(TodoItemCell)) };

            bottomLV.ItemSelected += (sender, e) =>
            {
                var Item = (Item)e.SelectedItem;
                var todoPage = new TodoItemPage();
                todoPage.BindingContext = Item;
                Navigation.PushAsync(todoPage);
            };



            familyBottomBorder = new BoxView { Color = Color.White };
            friendsBottomBorder = new BoxView { Color = Color.White };
            workBottomBorder = new BoxView { Color = Color.White };
            communityBottomBorder = new BoxView { Color = Color.White };

            Button familyButtonOverlay = new Button { Opacity = 0 };
            Button friendsButtonOverlay = new Button { Opacity = 0 };
            Button workButtonOverlay = new Button { Opacity = 0 };
            Button communityButtonOverlay = new Button { Opacity = 0 };




            familyButtonOverlay.Clicked += (async (obj, ev) =>
            {
                // collapse
                if (expanded && selected == Domains.Family)
                {
                    await expandAnimation();
                }
                // collapse family expand selected
                else if (expanded)
                {
                    if (selected == Domains.Friends)
                    {
                        selected = Domains.Family;
                        //topLV.ItemsSource = array;      

                        if (Todo.App.Database != null && Todo.App.Database.userID != null)
                        {
                            topLV.ItemsSource = await App.Database.GetItems();
                        }
                    }
                    else
                    {
                        selected = Domains.Family;
                        top.hideItems();
                        bottom.hideItems();

                        //topLV.ItemsSource = array;

                        if (Todo.App.Database != null && Todo.App.Database.userID != null)
                        {
                            topLV.ItemsSource = await App.Database.GetItems();
                        }

                        // Sets the bottom borders to their new values
                        Rectangle familyBottomBorderBounds = familyBottomBorder.Bounds;
                        familyBottomBorderBounds.Y = objRelativeLayout.Height / 10 * 9;

                        Rectangle workBottomBorderBounds = workBottomBorder.Bounds;
                        workBottomBorderBounds.Y = objRelativeLayout.Height;

                        Rectangle communityBottomBorderBounds = communityBottomBorder.Bounds;
                        communityBottomBorderBounds.Y = objRelativeLayout.Height;

                        // Set the bottom and top row to their right positions
                        Rectangle topBorderBounds = top.Bounds;
                        topBorderBounds.Height = objRelativeLayout.Height / 10;

                        Rectangle bottomBorderBounds = bottom.Bounds;
                        bottomBorderBounds.Height = objRelativeLayout.Height / 10;
                        bottomBorderBounds.Y = objRelativeLayout.Height / 10 * 9;

                        // Reduces bottom Listview to nothing
                        Rectangle bottomLVBounds = bottomLV.Bounds;
                        bottomLVBounds.Height = 0;
                        bottomLVBounds.Y = objRelativeLayout.Height;

                        await Task.WhenAll(familyBottomBorder.LayoutTo(familyBottomBorderBounds, 250, Easing.Linear), workBottomBorder.LayoutTo(workBottomBorderBounds, 250, Easing.Linear), communityBottomBorder.LayoutTo(communityBottomBorderBounds, 250, Easing.Linear), bottom.LayoutTo(bottomBorderBounds, 250, Easing.Linear), top.LayoutTo(topBorderBounds, 250, Easing.Linear), bottomLV.LayoutTo(bottomLVBounds, 250, Easing.Linear));  

                    }
                }
                else
                {
                    selected = Domains.Family;
                    expanded = true;

                    top.hideItems();
                    bottom.hideItems();

                    //topLV.ItemsSource = array;

                    if (Todo.App.Database != null && Todo.App.Database.userID != null)
                    {
                        topLV.ItemsSource = await App.Database.GetItems();
                    }

                    // Sets the bottom borders of friends and family to their new values
                    Rectangle familyBottomBorderBounds = familyBottomBorder.Bounds;
                    familyBottomBorderBounds.Y = objRelativeLayout.Height / 10 * 9;

                    Rectangle friendsBottomBorderBounds = friendsBottomBorder.Bounds;
                    friendsBottomBorderBounds.Y = friendsButtonOverlay.Height * 2 / 10;

                    // Set the bottom and top row to their right positions
                    Rectangle topBorderBounds = top.Bounds;
                    topBorderBounds.Height = objRelativeLayout.Height / 10;

                    Rectangle bottomBorderBounds = bottom.Bounds;
                    bottomBorderBounds.Height = objRelativeLayout.Height / 10;
                    bottomBorderBounds.Y = objRelativeLayout.Height / 10 * 9;

                    await Task.WhenAll(familyBottomBorder.LayoutTo(familyBottomBorderBounds, 250, Easing.Linear), friendsBottomBorder.LayoutTo(friendsBottomBorderBounds, 250, Easing.Linear), bottom.LayoutTo(bottomBorderBounds, 250, Easing.Linear), top.LayoutTo(topBorderBounds, 250, Easing.Linear));  

                }
                objRelativeLayout.ForceLayout();
            });


            friendsButtonOverlay.Clicked += ( async (obj, ev) =>
            {
                // collapse
                if (expanded && selected == Domains.Friends)
                {
                    await expandAnimation();
                }
                // collapse family expand selected
                else if (expanded)
                {
                    if (selected == Domains.Family)
                    {
                        selected = Domains.Friends;

                        if (Todo.App.Database != null && Todo.App.Database.userID != null)
                        {
                            var domains = (List<Item>)await Todo.App.Database.GetDomains();
                            var friends = domains[1];
                            topLV.ItemsSource = await Todo.App.Database.GetChildItems(friends);
                        }
                        
                        //topLV.ItemsSource = array2;
                    }
                    else
                    {
                        selected = Domains.Friends;
                        bottom.hideItems();
                        top.hideItems();


                        if (Todo.App.Database != null && Todo.App.Database.userID != null)
                        {
                            var domains = (List<Item>)await Todo.App.Database.GetDomains();
                            var friends = domains[1];
                            topLV.ItemsSource = await Todo.App.Database.GetChildItems(friends);
                        }

                        //topLV.ItemsSource = array2;

                        // Sets the bottom borders to their new values
                        Rectangle friendsBottomBorderBounds = friendsBottomBorder.Bounds;
                        friendsBottomBorderBounds.Y = objRelativeLayout.Height / 10 * 9;

                        Rectangle workBottomBorderBounds = workBottomBorder.Bounds;
                        workBottomBorderBounds.Y = objRelativeLayout.Height;

                        Rectangle communityBottomBorderBounds = communityBottomBorder.Bounds;
                        communityBottomBorderBounds.Y = objRelativeLayout.Height;

                        // Set the bottom and top row to their right positions
                        Rectangle topBorderBounds = top.Bounds;
                        topBorderBounds.Height = objRelativeLayout.Height / 10;

                        Rectangle bottomBorderBounds = bottom.Bounds;
                        bottomBorderBounds.Height = objRelativeLayout.Height / 10;
                        bottomBorderBounds.Y = objRelativeLayout.Height / 10 * 9;

                        // Reduces bottom Listview to nothing
                        Rectangle bottomLVBounds = bottomLV.Bounds;
                        bottomLVBounds.Height = 0;
                        bottomLVBounds.Y = objRelativeLayout.Height;

                        await Task.WhenAll(friendsBottomBorder.LayoutTo(friendsBottomBorderBounds, 250, Easing.Linear), workBottomBorder.LayoutTo(workBottomBorderBounds, 250, Easing.Linear), communityBottomBorder.LayoutTo(communityBottomBorderBounds, 250, Easing.Linear), bottom.LayoutTo(bottomBorderBounds, 250, Easing.Linear), top.LayoutTo(topBorderBounds, 250, Easing.Linear), bottomLV.LayoutTo(bottomLVBounds, 250, Easing.Linear));  

                    }
                }
                else
                {
                    selected = Domains.Friends;
                    expanded = true;
                    top.hideItems();
                    bottom.hideItems();

                    // Sets the bottom borders of friends and family to their new values
                    Rectangle familyBottomBorderBounds = familyBottomBorder.Bounds;
                    familyBottomBorderBounds.Y = objRelativeLayout.Height / 10; 

                    Rectangle friendsBottomBorderBounds = friendsBottomBorder.Bounds;
                    friendsBottomBorderBounds.Y = objRelativeLayout.Height / 10 * 9; 

                    // Set the bottom and top row to their right positions
                    Rectangle topBorderBounds = top.Bounds;
                    topBorderBounds.Height = objRelativeLayout.Height / 10;

                    Rectangle bottomBorderBounds = bottom.Bounds;
                    bottomBorderBounds.Height = objRelativeLayout.Height / 10;
                    bottomBorderBounds.Y = objRelativeLayout.Height / 10 * 9;

                    await Task.WhenAll(familyBottomBorder.LayoutTo(familyBottomBorderBounds, 250, Easing.Linear), friendsBottomBorder.LayoutTo(friendsBottomBorderBounds, 250, Easing.Linear), bottom.LayoutTo(bottomBorderBounds, 250, Easing.Linear), top.LayoutTo(topBorderBounds, 250, Easing.Linear));

                    if (Todo.App.Database != null && Todo.App.Database.userID != null)
                    {
                        var domains = (List<Item>)await Todo.App.Database.GetDomains();
                        var friends = domains[1];
                        topLV.ItemsSource = await Todo.App.Database.GetChildItems(friends);
                    }

                    //topLV.ItemsSource = array2;
                }
                objRelativeLayout.ForceLayout();
            });

            
            workButtonOverlay.Clicked += (async (obj, ev) =>
            {
                // collapse
                if (expanded && selected == Domains.Work)
                {
                    await expandAnimation();
                }
                // collapse family expand selected
                else if (expanded)
                {
                    if (selected == Domains.Community)
                    {
                        selected = Domains.Work;

                        if (Todo.App.Database != null && Todo.App.Database.userID != null)
                        {
                            var domains = (List<Item>)await Todo.App.Database.GetDomains();
                            var work = domains[2];
                            bottomLV.ItemsSource = await Todo.App.Database.GetChildItems(work);
                        }

                        //bottomLV.ItemsSource = array3;
                    }
                    else
                    {
                        selected = Domains.Work;
                        top.hideItems();
                        bottom.hideItems();
                        if (Todo.App.Database != null && Todo.App.Database.userID != null)
                        {
                            var domains = (List<Item>)await Todo.App.Database.GetDomains();
                            var work = domains[2];
                            bottomLV.ItemsSource = await Todo.App.Database.GetChildItems(work);
                        }

                        //bottomLV.ItemsSource = array3;

                        // Sets all the borders on the bottom of the domains to their right positions
                        Rectangle familyBottomBorderBounds = familyBottomBorder.Bounds;
                        familyBottomBorderBounds.Y = objRelativeLayout.Height / 10;

                        Rectangle friendsBottomBorderBounds = friendsBottomBorder.Bounds;
                        friendsBottomBorderBounds.Y = objRelativeLayout.Height / 10;

                        Rectangle communityBottomBorderBounds = communityBottomBorder.Bounds;
                        communityBottomBorderBounds.Y = objRelativeLayout.Height / 10 * 2;

                        // Set the bottom and top row to their right positions
                        Rectangle topBorderBounds = top.Bounds;
                        topBorderBounds.Height = objRelativeLayout.Height / 10;

                        Rectangle bottomBorderBounds = bottom.Bounds;
                        bottomBorderBounds.Height = objRelativeLayout.Height / 10;
                        bottomBorderBounds.Y = objRelativeLayout.Height / 10;

                        // Reduces Top Listview to nothing
                        Rectangle topLVBounds = topLV.Bounds;
                        topLVBounds.Height = 0;

                        // Actually moves the elements over
                        await Task.WhenAll(familyBottomBorder.LayoutTo(familyBottomBorderBounds, 250, Easing.Linear), friendsBottomBorder.LayoutTo(friendsBottomBorderBounds, 250, Easing.Linear), communityBottomBorder.LayoutTo(communityBottomBorderBounds, 250, Easing.Linear), bottom.LayoutTo(bottomBorderBounds, 250, Easing.Linear), top.LayoutTo(topBorderBounds, 250, Easing.Linear), topLV.LayoutTo(topLVBounds, 250, Easing.Linear));

                    }
                }
                else
                {
                    selected = Domains.Work;
                    expanded = true;
                    bottom.hideItems();
                    top.hideItems();
                    if (Todo.App.Database != null && Todo.App.Database.userID != null)
                    {
                        var domains = (List<Item>)await Todo.App.Database.GetDomains();
                        var work = domains[2];
                        bottomLV.ItemsSource = await Todo.App.Database.GetChildItems(work);
                    }

                    //bottomLV.ItemsSource = array3;

                    // Sets all the borders on the bottom of the domains to their right positions
                    Rectangle familyBottomBorderBounds = familyBottomBorder.Bounds;
                    familyBottomBorderBounds.Y = objRelativeLayout.Height / 10;

                    Rectangle friendsBottomBorderBounds = friendsBottomBorder.Bounds;
                    friendsBottomBorderBounds.Y = objRelativeLayout.Height / 10;

                    Rectangle communityBottomBorderBounds = communityBottomBorder.Bounds;
                    communityBottomBorderBounds.Y = objRelativeLayout.Height / 10 * 2;

                    // Set the bottom and top row to their right positions
                    Rectangle topBorderBounds = top.Bounds;
                    topBorderBounds.Height = objRelativeLayout.Height / 10;

                    Rectangle bottomBorderBounds = bottom.Bounds;
                    bottomBorderBounds.Height = objRelativeLayout.Height / 10;
                    bottomBorderBounds.Y = objRelativeLayout.Height / 10;

                    // Actually moves the elements over
                    await Task.WhenAll(familyBottomBorder.LayoutTo(familyBottomBorderBounds, 250, Easing.Linear), friendsBottomBorder.LayoutTo(friendsBottomBorderBounds, 250, Easing.Linear), communityBottomBorder.LayoutTo(communityBottomBorderBounds, 250, Easing.Linear), bottom.LayoutTo(bottomBorderBounds, 250, Easing.Linear), top.LayoutTo(topBorderBounds, 250, Easing.Linear));

                }
                objRelativeLayout.ForceLayout();
            });

            
            communityButtonOverlay.Clicked += (async (obj, ev) =>
            {
                // collapse
                if (expanded && selected == Domains.Community)
                {
                    await expandAnimation();
                }
                // collapse family expand selected
                else if (expanded)
                {
                    if (selected == Domains.Work)
                    {
                        selected = Domains.Community;

                        if (Todo.App.Database != null && Todo.App.Database.userID != null)
                        {
                            var domains = (List<Item>)await Todo.App.Database.GetDomains();
                            var community = domains[3];
                            bottomLV.ItemsSource = await Todo.App.Database.GetChildItems(community);
                        }

                        //bottomLV.ItemsSource = array4;
                    }
                    else                     
                    {
                        selected = Domains.Community;
                        top.hideItems();
                        bottom.hideItems();
                        if (Todo.App.Database != null && Todo.App.Database.userID != null)
                        {
                            var domains = (List<Item>)await Todo.App.Database.GetDomains();
                            var community = domains[3];
                            bottomLV.ItemsSource = await Todo.App.Database.GetChildItems(community);
                        }

                        //bottomLV.ItemsSource = array4;

                        // Sets all the borders on the bottom of the domains to their right positions
                        Rectangle familyBottomBorderBounds = familyBottomBorder.Bounds;
                        familyBottomBorderBounds.Y = objRelativeLayout.Height / 10;

                        Rectangle friendsBottomBorderBounds = friendsBottomBorder.Bounds;
                        friendsBottomBorderBounds.Y = objRelativeLayout.Height / 10;

                        Rectangle workBottomBorderBounds = workBottomBorder.Bounds;
                        workBottomBorderBounds.Y = objRelativeLayout.Height / 10 * 2;

                        // Set the bottom and top row to their right positions
                        Rectangle topBorderBounds = top.Bounds;
                        topBorderBounds.Height = objRelativeLayout.Height / 10;

                        Rectangle bottomBorderBounds = bottom.Bounds;
                        bottomBorderBounds.Height = objRelativeLayout.Height / 10;
                        bottomBorderBounds.Y = objRelativeLayout.Height / 10;

                        // Reduces Top Listview to nothing
                        Rectangle topLVBounds = topLV.Bounds;
                        topLVBounds.Height = 0;

                        // Actually moves the elements over
                        await Task.WhenAll(familyBottomBorder.LayoutTo(familyBottomBorderBounds, 250, Easing.Linear), friendsBottomBorder.LayoutTo(friendsBottomBorderBounds, 250, Easing.Linear), workBottomBorder.LayoutTo(workBottomBorderBounds, 250, Easing.Linear), bottom.LayoutTo(bottomBorderBounds, 250, Easing.Linear), top.LayoutTo(topBorderBounds, 250, Easing.Linear), topLV.LayoutTo(topLVBounds, 250, Easing.Linear));

                    }
                }
                else
                {
                    selected = Domains.Community;
                    expanded = true;
                    bottom.hideItems();
                    top.hideItems();

                    // Sets all the borders on the bottom of the domains to their right positions
                    Rectangle familyBottomBorderBounds = familyBottomBorder.Bounds;
                    familyBottomBorderBounds.Y = objRelativeLayout.Height / 10;

                    Rectangle friendsBottomBorderBounds = friendsBottomBorder.Bounds;
                    friendsBottomBorderBounds.Y = objRelativeLayout.Height / 10;

                    Rectangle workBottomBorderBounds = workBottomBorder.Bounds;
                    workBottomBorderBounds.Y = objRelativeLayout.Height / 10 * 2;

                    // Set the bottom and top row to their right positions
                    Rectangle topBorderBounds = top.Bounds;
                    topBorderBounds.Height = objRelativeLayout.Height / 10;

                    Rectangle bottomBorderBounds = bottom.Bounds;
                    bottomBorderBounds.Height = objRelativeLayout.Height / 10;
                    bottomBorderBounds.Y = objRelativeLayout.Height / 10;

                    // Actually moves the elements over
                    await Task.WhenAll(familyBottomBorder.LayoutTo(familyBottomBorderBounds, 250, Easing.Linear), friendsBottomBorder.LayoutTo(friendsBottomBorderBounds, 250, Easing.Linear), workBottomBorder.LayoutTo(workBottomBorderBounds, 250, Easing.Linear), bottom.LayoutTo(bottomBorderBounds, 250, Easing.Linear), top.LayoutTo(topBorderBounds, 250, Easing.Linear));

                    if (Todo.App.Database != null && Todo.App.Database.userID != null)
                    {
                        var domains = (List<Item>)await Todo.App.Database.GetDomains();
                        var community = domains[3];
                        bottomLV.ItemsSource = await Todo.App.Database.GetChildItems(community);
                    }

                    //bottomLV.ItemsSource = array4;
                }
                objRelativeLayout.ForceLayout();
            });

            
            objRelativeLayout.Children.Add(top,
                xConstraint: Constraint.Constant(0),
                yConstraint: Constraint.Constant(0),
                widthConstraint: Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width;
                })
                ,
                heightConstraint: Constraint.RelativeToParent((parent) =>
                {
                    return (expanded ? (parent.Height / 10) : (parent.Height / 2) );
                    //return parent.Height / 2;
                }
                )
                );


            objRelativeLayout.Children.Add(familyBottomBorder,
                xConstraint: Constraint.Constant(0),
                yConstraint: Constraint.RelativeToView(top,
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
                    return (selected == Domains.Family ? 0 : borderSize);
                })
                );

            objRelativeLayout.Children.Add(friendsBottomBorder,
                xConstraint: Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width / 2;
                }),
                yConstraint: Constraint.RelativeToView(top,
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
                    return (selected == Domains.Friends ? 0 : borderSize);
                })
                );

            objRelativeLayout.Children.Add(topLV,
                xConstraint: Constraint.Constant(0),
                yConstraint: Constraint.RelativeToView(top,
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
                    return (expanded && selected == Domains.Family || selected == Domains.Friends ? parent.Height*0.8 : 0);
                })
                );



            objRelativeLayout.Children.Add(bottom,
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
                    return (expanded ? (parent.Height / 10) : (parent.Height / 2) );
                }
                )
                );


            objRelativeLayout.Children.Add(workBottomBorder,
                xConstraint: Constraint.Constant(0),
                yConstraint: Constraint.RelativeToView(bottom,
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
                    return (selected == Domains.Work ? 0 : borderSize);
                })
                );

            objRelativeLayout.Children.Add(communityBottomBorder,
                xConstraint: Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width / 2;
                }),
                yConstraint: Constraint.RelativeToView(bottom,
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
                    return (selected == Domains.Community ? 0 : borderSize);
                })
                );

            objRelativeLayout.Children.Add(bottomLV,
                xConstraint: Constraint.Constant(0),
                yConstraint: Constraint.RelativeToView(bottom,
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
                    return (expanded && selected == Domains.Work || selected == Domains.Community ? parent.Height * 0.8 : 0);
                })
                );




















            objRelativeLayout.Children.Add(familyButtonOverlay,
                xConstraint: Constraint.RelativeToView(top,
                new Func<RelativeLayout, View, double>((pobjRelativeLayout, pobjView) =>
                {
                    return pobjView.X;
                })),
                yConstraint: Constraint.RelativeToView(top,
                new Func<RelativeLayout, View, double>((pobjRelativeLayout, pobjView) =>
                {
                    return pobjView.Y;
                })),
                widthConstraint: Constraint.RelativeToView(top,
                new Func<RelativeLayout, View, double>((pobjRelativeLayout, pobjView) =>
                {
                    return pobjView.Width / 2 - borderSize;
                })),
                heightConstraint: Constraint.RelativeToView(top,
                new Func<RelativeLayout, View, double>((pobjRelativeLayout, pobjView) =>
                {
                    return pobjView.Height;
                }))
                );

            objRelativeLayout.Children.Add(friendsButtonOverlay,
                xConstraint: Constraint.RelativeToView(top,
                new Func<RelativeLayout, View, double>((pobjRelativeLayout, pobjView) =>
                {
                    return pobjView.Width / 2 + borderSize;
                })),
                yConstraint: Constraint.RelativeToView(top,
                new Func<RelativeLayout, View, double>((pobjRelativeLayout, pobjView) =>
                {
                    return pobjView.Y;
                })),
                widthConstraint: Constraint.RelativeToView(top,
                new Func<RelativeLayout, View, double>((pobjRelativeLayout, pobjView) =>
                {
                    return pobjView.Width / 2 - borderSize;
                })),
                heightConstraint: Constraint.RelativeToView(top,
                new Func<RelativeLayout, View, double>((pobjRelativeLayout, pobjView) =>
                {
                    return pobjView.Height;
                }))
                );

            objRelativeLayout.Children.Add(workButtonOverlay,
                xConstraint: Constraint.RelativeToView(bottom,
                new Func<RelativeLayout, View, double>((pobjRelativeLayout, pobjView) =>
                {
                    return pobjView.X;
                })),
                yConstraint: Constraint.RelativeToView(bottom,
                new Func<RelativeLayout, View, double>((pobjRelativeLayout, pobjView) =>
                {
                    return pobjView.Y;
                })),
                widthConstraint: Constraint.RelativeToView(bottom,
                new Func<RelativeLayout, View, double>((pobjRelativeLayout, pobjView) =>
                {
                    return pobjView.Width / 2 - borderSize;
                })),
                heightConstraint: Constraint.RelativeToView(bottom,
                new Func<RelativeLayout, View, double>((pobjRelativeLayout, pobjView) =>
                {
                    return pobjView.Height;
                }))
                );

            objRelativeLayout.Children.Add(communityButtonOverlay,
                xConstraint: Constraint.RelativeToView(bottom,
                new Func<RelativeLayout, View, double>((pobjRelativeLayout, pobjView) =>
                {
                    return pobjView.Width / 2 + borderSize;
                })),
                yConstraint: Constraint.RelativeToView(bottom,
                new Func<RelativeLayout, View, double>((pobjRelativeLayout, pobjView) =>
                {
                    return pobjView.Y;
                })),
                widthConstraint: Constraint.RelativeToView(bottom,
                new Func<RelativeLayout, View, double>((pobjRelativeLayout, pobjView) =>
                {
                    return pobjView.Width / 2 - borderSize;
                })),
                heightConstraint: Constraint.RelativeToView(bottom,
                new Func<RelativeLayout, View, double>((pobjRelativeLayout, pobjView) =>
                {
                    return pobjView.Height;
                }))
                );


            objStackLayout.Children.Add(objRelativeLayout);          

            this.Content = objStackLayout;


            ToolbarItem tbi = null;
            if (Device.OS == TargetPlatform.iOS)
            {
                tbi = new ToolbarItem("+", null, () =>
                {
                    if (selected != Domains.None)
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
                    if (selected != Domains.None)
                    {
                        var Item = new Item { Type = 2 };

                        var domains = (List<Item>)await Todo.App.Database.GetDomains();
                        if (selected == Domains.Family)
                        {
                            var friends = domains[0];
                            Item.Parent = friends.ID;
                        }
                        else if (selected == Domains.Friends)
                        {
                            var friends = domains[1];
                            Item.Parent = friends.ID;
                        }
                        else if (selected == Domains.Work)
                        {
                            var friends = domains[2];
                            Item.Parent = friends.ID;
                        }
                        else if (selected == Domains.Community)
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
                    if (selected != Domains.None)
                    {
                        var Item = new Item { Type = 2 };

                        var domains = (List<Item>)await Todo.App.Database.GetDomains();
                        if (selected == Domains.Family)
                        {
                            var friends = domains[0];
                            Item.Parent = friends.ID;
                        }
                        else if (selected == Domains.Friends)
                        {
                            var friends = domains[1];
                            Item.Parent = friends.ID;
                        }
                        else if (selected == Domains.Work)
                        {
                            var friends = domains[2];
                            Item.Parent = friends.ID;
                        }
                        else if (selected == Domains.Community)
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
                    await Refresh();
                    //topLV.BeginRefresh();
                    //bottomLV.BeginRefresh();

                });

        }



        public async Task expandAnimation()
        {
            selected = Domains.None;
            top.showItems();
            bottom.showItems();
            await Refresh();

            topLV.ItemsSource = null;
            bottomLV.ItemsSource = null;

            // Sets the bottom borders of friends and family to their new values
            Rectangle familyBottomBorderBounds = familyBottomBorder.Bounds;
            familyBottomBorderBounds.Y = objRelativeLayout.Height / 2;

            Rectangle friendsBottomBorderBounds = friendsBottomBorder.Bounds;
            friendsBottomBorderBounds.Y = objRelativeLayout.Height / 2;

            // Sets the bottom borders of friends and family to their new values
            Rectangle workBottomBorderBounds = workBottomBorder.Bounds;
            workBottomBorderBounds.Y = objRelativeLayout.Height / 2;

            Rectangle communityBottomBorderBounds = communityBottomBorder.Bounds;
            communityBottomBorderBounds.Y = objRelativeLayout.Height / 2;

            // Set the bottom and top row to their right positions
            Rectangle topBorderBounds = top.Bounds;
            topBorderBounds.Height = objRelativeLayout.Height / 2;

            // Set the bottom and top row to their right positions
            Rectangle topLVBorderBounds = topLV.Bounds;
            topLVBorderBounds.Height = 0;

            // Set the bottom and top row to their right positions
            Rectangle bottomLVBorderBounds = bottomLV.Bounds;
            bottomLVBorderBounds.Height = 0;
            bottomLVBorderBounds.Y = objRelativeLayout.Height;

            // Set the bottom and top row to their right positions
            Rectangle bottomBorderBounds = bottom.Bounds;
            bottomBorderBounds.Height = objRelativeLayout.Height / 2;
            bottomBorderBounds.Y = objRelativeLayout.Height / 2;

            await Task.WhenAll(familyBottomBorder.LayoutTo(familyBottomBorderBounds, 250, Easing.Linear), friendsBottomBorder.LayoutTo(friendsBottomBorderBounds, 250, Easing.Linear), workBottomBorder.LayoutTo(workBottomBorderBounds, 250, Easing.Linear), communityBottomBorder.LayoutTo(communityBottomBorderBounds, 250, Easing.Linear), bottom.LayoutTo(bottomBorderBounds, 250, Easing.Linear), bottomLV.LayoutTo(bottomLVBorderBounds, 250, Easing.Linear), top.LayoutTo(topBorderBounds, 250, Easing.Linear), topLV.LayoutTo(topLVBorderBounds, 250, Easing.Linear));
            expanded = false;
        }

        public async Task Refresh()
        {
            if (Todo.App.Database != null && Todo.App.Database.userID != null)
            {
                IsBusy = true;
                var domains = (List<Item>) await Todo.App.Database.GetDomains();
                var friends = domains[1];
                var work = domains[2];
                var community = domains[3];

                var friendsGoals = await Todo.App.Database.GetChildItems(friends);
                friendsItems.ItemsSource = friendsGoals;

                //if (!friendsInit)
                //{
                //    var friendsItemsList = new ItemListViewModel("friends");

                //    friendsItems.BindingContext = friendsItemsList;
                //    //friendsItems.SetBinding(friendsItems.BindingContext, friendsItemsList.Reports);
                //    //friendsItems.ItemsSource = new ItemListViewModel("friends").Reports;
                //    friendsInit = true;
                //}

                StackLayout friendsHead = new StackLayout { Padding = 2, Spacing = 1 };
                friendsHead.Children.Add(new Label { Text = "Friends", TextColor = Color.FromRgb(255, 105, 0), FontSize = 20, FontAttributes = FontAttributes.Bold});
                friendsHead.Children.Add(new Label { Text = ' ' + ((ICollection<Item>)friendsGoals).Count.ToString() + " items", TextColor = Color.White });
                friendsItems.Header = friendsHead;

                var workGoals = await Todo.App.Database.GetChildItems(work);
                workItems.ItemsSource = workGoals;
                StackLayout workHead = new StackLayout { Padding = 2, Spacing = 1 };
                workHead.Children.Add(new Label { Text = "Work", TextColor = Color.FromRgb(32, 178, 170), FontSize = 20, FontAttributes = FontAttributes.Bold });
                workHead.Children.Add(new Label { Text = ' ' + ((ICollection<Item>)workGoals).Count.ToString() + " items", TextColor = Color.White });
                workItems.Header = workHead;

                //workItems.PropertyChanged => 

                var communityGoals = await Todo.App.Database.GetChildItems(community);
                communityItems.ItemsSource = communityGoals;
                StackLayout communityHead = new StackLayout { Padding = 2, Spacing = 1 };
                communityHead.Children.Add(new Label { Text = "Community", TextColor = Color.FromRgb(153, 50, 204), FontSize = 20, FontAttributes = FontAttributes.Bold });
                communityHead.Children.Add(new Label { Text = ' ' + ((ICollection<Item>)communityGoals).Count.ToString() + " items", TextColor = Color.White });
                communityItems.Header = communityHead;

                


                famItems.ItemsSource = await App.Database.GetItems();
                StackLayout famHead = new StackLayout { Padding = 2, Spacing = 1 };
                famHead.Children.Add(new Label { Text = "Family", TextColor = Color.Yellow, FontSize = 20, FontAttributes = FontAttributes.Bold });
                famHead.Children.Add(new Label { Text = ' ' + ((ICollection<Item>)famItems.ItemsSource).Count.ToString() + " items", TextColor = Color.White });
                famItems.Header = famHead;

                //if (top != null)
                //    top.changeRightHeader(friendsHead);


                this.ForceLayout();
                IsBusy = false;
            }
        }

    }


}
