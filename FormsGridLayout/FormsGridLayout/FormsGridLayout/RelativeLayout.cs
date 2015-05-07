using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GridLayoutDemo
{
    public class RelativeLayoutPage3 : ContentPage
    {
        enum Domains
        {
            None,
            Family,
            Friends,
            Work,
            Community,
        };
        private bool expanded = false;
        private Domains selected = Domains.None;

        private int rows = 2;
        private int borderSize = 1; // pixels
        Dictionary<ListView, IEnumerable<string>> itemStorage = new Dictionary<ListView, IEnumerable<string>>();
        public RelativeLayoutPage3()
        {
            //List<ListView> listViews = new List<ListView>();
            RelativeLayout objRelativeLayout = new RelativeLayout
            {
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            StackLayout objStackLayout = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
            };



            ListView familyLV = new ListView{Header = "Family"};
            string[] array = { "fam1", "fam2", "fam3", "fam4", "fam5", "fam6", "fam7", "fam8", "fam9", "fam10" };
            familyLV.ItemsSource = array;
            //familyLV.BackgroundColor = Color.Red;    

            StackLayout famHead = new StackLayout { Padding = 2 , Spacing= 1};
            famHead.Children.Add(new Label { Text = "Family", TextColor = Color.Yellow, Font = Font.BoldSystemFontOfSize(30) });
            famHead.Children.Add(new Label { Text = ' ' + ((ICollection<string>) familyLV.ItemsSource).Count.ToString() + " items"});
            familyLV.Header = famHead;


            ListView friendsLV = new ListView { Header = "Friends" };
            string[] array2 = { "friends1", "friends2", "friends3", "friends4" };
            friendsLV.ItemsSource = array2;

            StackLayout friendsHead = new StackLayout { Padding = 2, Spacing = 1 };
            friendsHead.Children.Add(new Label { Text = "Friends", TextColor = Color.FromRgb(255,105,0), Font = Font.BoldSystemFontOfSize(30) });
            friendsHead.Children.Add(new Label { Text = ' ' + ((ICollection<string>)friendsLV.ItemsSource).Count.ToString() + " items" });
            friendsLV.Header = friendsHead;

            ListView workLV = new ListView { Header = "Work" };
            string[] array3 = { "work1", "work2", "work3", "work4"};
            workLV.ItemsSource = array3;

            StackLayout workHead = new StackLayout { Padding = 2, Spacing = 1 };
            workHead.Children.Add(new Label { Text = "Work", TextColor = Color.FromRgb(32, 178, 170), Font = Font.BoldSystemFontOfSize(30) });
            workHead.Children.Add(new Label { Text = ' ' + ((ICollection<string>)workLV.ItemsSource).Count.ToString() + " items" });
            workLV.Header = workHead;

            ListView communityLV = new ListView { Header = "Community" };
            string[] array4 = { "commu1", "commu2", "commu3", "commu4", "commu5" };
            communityLV.ItemsSource = array4;

            StackLayout communityHead = new StackLayout { Padding = 2, Spacing = 1 };
            communityHead.Children.Add(new Label { Text = "Community", TextColor = Color.FromRgb(153, 50, 204), Font = Font.BoldSystemFontOfSize(30) });
            communityHead.Children.Add(new Label { Text = ' ' + ((ICollection<string>)communityLV.ItemsSource).Count.ToString() + " items" });
            communityLV.Header = communityHead;

            MainPageRow top = new MainPageRow(familyLV, friendsLV, 1);
            MainPageRow bottom = new MainPageRow(workLV, communityLV, 1);

            ListView topLV = new ListView();
            ListView bottomLV = new ListView();

            BoxView familyBottomBorder = new BoxView { Color = Color.White };
            BoxView friendsBottomBorder = new BoxView { Color = Color.White };
            BoxView workBottomBorder = new BoxView { Color = Color.White };
            BoxView communityBottomBorder = new BoxView { Color = Color.White };

            Button familyButtonOverlay = new Button { Opacity = 0 };
            Button friendsButtonOverlay = new Button { Opacity = 0 };
            Button workButtonOverlay = new Button { Opacity = 0 };
            Button communityButtonOverlay = new Button { Opacity = 0 };

            familyButtonOverlay.Clicked += (async (obj, ev) =>
            {
                // collapse
                if (expanded && selected == Domains.Family)
                {
                    selected = Domains.None;
                    top.showItems();
                    bottom.showItems();

                    topLV.ItemsSource = null;

                    expanded = false;
                }
                // collapse family expand selected
                else if (expanded)
                {
                    if (selected == Domains.Friends)
                    {
                        selected = Domains.Family;
                        topLV.ItemsSource = array;      
                    }
                    else
                    {
                        selected = Domains.Family;
                        top.hideItems();
                        bottom.hideItems();

                        topLV.ItemsSource = array;

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

                    topLV.ItemsSource = array;     

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
                    selected = Domains.None;
                    top.showItems();
                    bottom.showItems();

                    topLV.ItemsSource = null;

                    expanded = false;
                }
                // collapse family expand selected
                else if (expanded)
                {
                    if (selected == Domains.Family)
                    {
                        selected = Domains.Friends;
                        topLV.ItemsSource = array2;
                    }
                    else
                    {
                        selected = Domains.Friends;
                        bottom.hideItems();
                        top.hideItems();
                        topLV.ItemsSource = array2;

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

                    topLV.ItemsSource = array2;
                }
                objRelativeLayout.ForceLayout();
            });

            
            workButtonOverlay.Clicked += (async (obj, ev) =>
            {
                // collapse
                if (expanded && selected == Domains.Work)
                {
                    selected = Domains.None;
                    top.showItems();
                    bottom.showItems();

                    bottomLV.ItemsSource = null;

                    expanded = false;
                }
                // collapse family expand selected
                else if (expanded)
                {
                    if (selected == Domains.Community)
                    {
                        selected = Domains.Work;
                        bottomLV.ItemsSource = array3;
                    }
                    else
                    {
                        selected = Domains.Work;
                        top.hideItems();
                        bottom.hideItems();
                        bottomLV.ItemsSource = array3;

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
                    bottomLV.ItemsSource = array3;

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
                    selected = Domains.None;
                    top.showItems();
                    bottom.showItems();

                    bottomLV.ItemsSource = null;

                    expanded = false;
                }
                // collapse family expand selected
                else if (expanded)
                {
                    if (selected == Domains.Work)
                    {
                        selected = Domains.Community;
                        bottomLV.ItemsSource = array4;
                    }
                    else                     
                    {
                        selected = Domains.Community;
                        top.hideItems();
                        bottom.hideItems();
                        bottomLV.ItemsSource = array4;

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

                    bottomLV.ItemsSource = array4;
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
                    if (expanded)
                    {
                        //Rectangle bottomBounds = bottom.Bounds;
                        //bottomBounds.Height = parent.Height / 10;
                        //bottom.LayoutTo(bottomBounds);
                        return parent.Height / 10;
                    }
                    else
                    {
                        //Rectangle bottomBounds = bottom.Bounds;
                        //bottomBounds.Height = parent.Height / 2;
                        //bottom.LayoutTo(bottomBounds);
                        //return bottomBounds.Height;
                        return parent.Height / 2;
                    }
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
        }

        private object OnList4Clicked()
        {
            throw new NotImplementedException();
        }

        private object OnLabelClicked()
        {
            throw new NotImplementedException();
        }
    }
}
