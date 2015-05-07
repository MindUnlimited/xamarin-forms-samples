using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GridLayoutDemo
{
    public class GridLayout : ContentPage
    {
        int count = 1;
        bool topleftExpanded = false;
        bool toprightExpanded = false;
        bool bottomleftExpanded = false;
        bool bottomrightExpanded = false;
        bool expanded = false;
        ListView selected = null;

        public GridLayout()
        {
            var layout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = 0,
                Spacing = 0
            };

            var grid = new Grid
            {
                RowSpacing = 0,
                Padding = 0
            };



            ListView familyLV = new ListView { Header = "Family" };
            string[] array = { "item1", "item2", "item3", "item4", "item5", "item6", "item7", "item8", "item9", "item10" };
            familyLV.ItemsSource = array;

            ListView friendsLV = new ListView { Header = "Friends" };
            string[] array2 = { "item5", "item6", "item7", "item8" };
            friendsLV.ItemsSource = array2;

            ListView workLV = new ListView { Header = "Work" };
            string[] array3 = { "item9", "item10", "item11", "item12" };
            workLV.ItemsSource = array3;

            ListView communityLV = new ListView { Header = "Community" };
            string[] array4 = { "item13", "item14", "item15", "item16" };
            communityLV.ItemsSource = array4;

            MainPageRow top = new MainPageRow(familyLV, friendsLV, 1);
            MainPageRow bottom = new MainPageRow(workLV, communityLV, 1);

            grid.Children.Add(top, 0, 2, 0, 1);
            grid.Children.Add(bottom, 0, 2, 1, 2);

            //grid.Children.Add(familyLV, 0, 0); // Left, First element
            //grid.Children.Add(friendsLV, 1, 0); // Right, First element
            //grid.Children.Add(workLV, 0, 1); // Left, Second element
            //grid.Children.Add(communityLV, 1, 1); // Right, Second element

            var familyButtonOverlay = new Button { Opacity = 0 };
            familyButtonOverlay.Clicked += ((obj, ev) =>
            {
                if (expanded)
                {
                    //familyDomain.expand(false);
                    top.showItems();
                    bottom.showItems();
                    //objRelativeLayout.Children.Remove(familyLV2);

                    //foreach (var lv in objRelativeLayout.Children)
                    //{
                    //    if (lv is ListView)
                    //    {
                    //        ListView listV = (ListView)lv;
                    //        //if (lv != familyLV)
                    //        //{
                    //        listV.ItemsSource = itemStorage[listV];
                    //        //}
                    //    }
                    //}

                    //itemStorage.Clear();

                    expanded = false;
                    //objRelativeLayout.ForceLayout();
                }
                else
                {
                    //System.Diagnostics.Debug.WriteLine(familyLV.X.ToString() + ' ' + familyLV.Y.ToString() + ' ' + familyLV.Width.ToString() + ' ' + familyLV.Height.ToString());

                    expanded = true;
                    //objRelativeLayout.ForceLayout();

                    //familyLV2 = new ListView();
                    //familyLV2.ItemsSource = familyLV.ItemsSource;
                    //familyLV2.BackgroundColor = Color.Blue;
                    //familyLV2.SizeChanged += ((o2, e2) =>
                    //{
                    //    objRelativeLayout.ForceLayout();
                    //});

                    //familyDomain.expand(true);
                    top.hideItems();

                    //foreach (var lv in objRelativeLayout.Children)
                    //{
                    //    if (lv is ListView)
                    //    {
                    //        ListView listV = (ListView)lv;
                    //        //if (lv != familyLV)
                    //        //{
                    //        itemStorage[listV] = (IEnumerable<string>)listV.ItemsSource;
                    //        listV.ItemsSource = null;
                    //        //}
                    //        //listV.HeightRequest = 100;
                    //    }
                    //}



                    //objRelativeLayout.Children.Add(familyLV2
                    //    ,
                    //    xConstraint: Constraint.Constant(0),
                    //    yConstraint: Constraint.RelativeToView(familyDomain,
                    //    new Func<RelativeLayout, View, double>((pobjRelativeLayout, pobjView) =>
                    //    {
                    //        //System.Diagnostics.Debug.WriteLine("Y: " + (pobjView.Y + pobjView.Height).ToString());
                    //        //pobjView.IsVisible = false;
                    //        return pobjView.Y + pobjView.Height;
                    //    }))
                    //    ,
                    //    //heightConstraint: Constraint.RelativeToParent((parent) =>
                    //    //{
                    //    //    return (parent.Height / rows);
                    //    //}),
                    //    heightConstraint: Constraint.RelativeToParent((parent) =>
                    //    {
                    //        return parent.Height - familyDomain.Height - familyDomain.Height;
                    //    }),
                    //    widthConstraint: Constraint.RelativeToParent((parent) =>
                    //    {
                    //        return (parent.Width / 2);
                    //    })

                    //    );

                    //objRelativeLayout.RaiseChild(familyLV2);

                    //System.Diagnostics.Debug.WriteLine(familyLV.X.ToString() + ' ' + familyLV.Y.ToString() + ' ' + familyLV.Width.ToString() + ' ' + familyLV.Height.ToString());

                    //objRelativeLayout.ForceLayout();
                }

                //throw new NotImplementedException();
            });

            grid.Children.Add(familyButtonOverlay, 0, 1, 0, 1);

            var biginfo = new Button
            {
                Text = "",
                TextColor = Color.Green,
                HeightRequest = 200
            };


            biginfo.Clicked += delegate
            {
                biginfo.Text = "To the items!";
                //biginfo.TextColor = Color.Olive;
            };

            //grid.Children.Add(biginfo, 0, 2, 2, 3);

            //topleft.Clicked += delegate
            //{
            //    if (topleftExpanded)
            //    {
            //        topleft.HeightRequest = 150;
            //        topright.HeightRequest = 150;
            //        bottomleft.HeightRequest = 150;
            //        bottomright.HeightRequest = 150;

            //        topleft.TextColor = Color.White;
            //        grid.Children.Remove(biginfo);

            //        grid.Children.Remove(bottomleft);
            //        grid.Children.Add(bottomleft, 0, 1, 1, 2);

            //        grid.Children.Remove(bottomright);
            //        grid.Children.Add(bottomright, 1, 2, 1, 2);

            //        topleftExpanded = false;
            //        expanded = false;
            //    }
            //    else
            //    {
            //        if (expanded)
            //        {
            //            grid.Children.Remove(biginfo);
            //            if (topleftExpanded)
            //            {
            //                topleftExpanded = false;
            //                topleft.TextColor = Color.White;
            //            }
            //            if (toprightExpanded)
            //            {
            //                toprightExpanded = false;
            //                topright.TextColor = Color.White;
            //            }
            //            if (bottomleftExpanded)
            //            {
            //                bottomleftExpanded = false;
            //                bottomleft.TextColor = Color.White;
            //            }
            //            if (bottomrightExpanded)
            //            {
            //                bottomrightExpanded = false;
            //                bottomright.TextColor = Color.White;
            //            }
            //        }

            //        topleft.TextColor = Color.Green;
            //        topleft.HeightRequest = 100;
            //        topright.HeightRequest = 100;
            //        bottomleft.HeightRequest = 100;
            //        bottomright.HeightRequest = 100;

            //        biginfo.Text = topleft.Text + " expanded";
            //        grid.Children.Add(biginfo, 0, 2, 1, 3);

            //        grid.Children.Remove(bottomleft);
            //        grid.Children.Add(bottomleft, 0, 1, 3, 4);

            //        grid.Children.Remove(bottomright);
            //        grid.Children.Add(bottomright, 1, 2, 3, 4);

            //        topleftExpanded = true;
            //        expanded = true;
            //    }
            //    //gridButton.HeightRequest = 100;

            //};

            //topright.Clicked += delegate
            //{
            //    if (toprightExpanded)
            //    {
            //        topleft.HeightRequest = 150;
            //        topright.HeightRequest = 150;
            //        bottomleft.HeightRequest = 150;
            //        bottomright.HeightRequest = 150;

            //        topright.TextColor = Color.White;
            //        grid.Children.Remove(biginfo);

            //        grid.Children.Remove(bottomleft);
            //        grid.Children.Add(bottomleft, 0, 1, 1, 2);

            //        grid.Children.Remove(bottomright);
            //        grid.Children.Add(bottomright, 1, 2, 1, 2);

            //        toprightExpanded = false;
            //        expanded = false;
            //    }
            //    else
            //    {
            //        if (expanded)
            //        {
            //            grid.Children.Remove(biginfo);
            //            if (topleftExpanded)
            //            {
            //                topleftExpanded = false;
            //                topleft.TextColor = Color.White;
            //            }
            //            if (toprightExpanded)
            //            {
            //                toprightExpanded = false;
            //                topright.TextColor = Color.White;
            //            }
            //            if (bottomleftExpanded)
            //            {
            //                bottomleftExpanded = false;
            //                bottomleft.TextColor = Color.White;
            //            }
            //            if (bottomrightExpanded)
            //            {
            //                bottomrightExpanded = false;
            //                bottomright.TextColor = Color.White;
            //            }
            //        }

            //        topright.TextColor = Color.Green;
            //        topleft.HeightRequest = 100;
            //        topright.HeightRequest = 100;
            //        bottomleft.HeightRequest = 100;
            //        bottomright.HeightRequest = 100;

            //        biginfo.Text = topright.Text + " expanded";
            //        grid.Children.Add(biginfo, 0, 2, 1, 3);

            //        grid.Children.Remove(bottomleft);
            //        grid.Children.Add(bottomleft, 0, 1, 3, 4);

            //        grid.Children.Remove(bottomright);
            //        grid.Children.Add(bottomright, 1, 2, 3, 4);

            //        toprightExpanded = true;
            //        expanded = true;
            //    }
            //    //gridButton.HeightRequest = 100;

            //};

            //bottomleft.Clicked += delegate
            //{
            //    if (bottomleftExpanded)
            //    {
            //        topleft.HeightRequest = 150;
            //        topright.HeightRequest = 150;
            //        bottomleft.HeightRequest = 150;
            //        bottomright.HeightRequest = 150;

            //        bottomleft.TextColor = Color.White;
            //        grid.Children.Remove(biginfo);

            //        grid.Children.Remove(bottomleft);
            //        grid.Children.Add(bottomleft, 0, 1, 1, 2);

            //        grid.Children.Remove(bottomright);
            //        grid.Children.Add(bottomright, 1, 2, 1, 2);

            //        bottomleftExpanded = false;
            //        expanded = false;
            //    }
            //    else
            //    {
            //        if (expanded)
            //        {
            //            grid.Children.Remove(biginfo);
            //            if (topleftExpanded)
            //            {
            //                topleftExpanded = false;
            //                topleft.TextColor = Color.White;
            //            }
            //            if (toprightExpanded)
            //            {
            //                toprightExpanded = false;
            //                topright.TextColor = Color.White;
            //            }
            //            if (bottomleftExpanded)
            //            {
            //                bottomleftExpanded = false;
            //                bottomleft.TextColor = Color.White;
            //            }
            //            if (bottomrightExpanded)
            //            {
            //                bottomrightExpanded = false;
            //                bottomright.TextColor = Color.White;
            //            }
            //        }

            //        bottomleft.TextColor = Color.Green;
            //        topleft.HeightRequest = 100;
            //        topright.HeightRequest = 100;
            //        bottomleft.HeightRequest = 100;
            //        bottomright.HeightRequest = 100;

            //        biginfo.Text = bottomleft.Text + " expanded";
            //        grid.Children.Add(biginfo, 0, 2, 2, 4);

            //        grid.Children.Remove(bottomleft);
            //        grid.Children.Add(bottomleft, 0, 1, 1, 2);

            //        grid.Children.Remove(bottomright);
            //        grid.Children.Add(bottomright, 1, 2, 1, 2);

            //        bottomleftExpanded = true;
            //        expanded = true;
            //    }
            //    //gridButton.HeightRequest = 100;

            //};

            //bottomright.Clicked += delegate
            //{
            //    if (bottomrightExpanded)
            //    {
            //        topleft.HeightRequest = 150;
            //        topright.HeightRequest = 150;
            //        bottomleft.HeightRequest = 150;
            //        bottomright.HeightRequest = 150;

            //        bottomright.TextColor = Color.White;
            //        grid.Children.Remove(biginfo);

            //        grid.Children.Remove(bottomleft);
            //        grid.Children.Add(bottomleft, 0, 1, 1, 2);

            //        grid.Children.Remove(bottomright);
            //        grid.Children.Add(bottomright, 1, 2, 1, 2);

            //        bottomrightExpanded = false;
            //        expanded = false;
            //    }
            //    else
            //    {
            //        if (expanded)
            //        {
            //            grid.Children.Remove(biginfo);
            //            if (topleftExpanded)
            //            {
            //                topleftExpanded = false;
            //                topleft.TextColor = Color.White;
            //            }
            //            if (toprightExpanded)
            //            {
            //                toprightExpanded = false;
            //                topright.TextColor = Color.White;
            //            }
            //            if (bottomleftExpanded)
            //            {
            //                bottomleftExpanded = false;
            //                bottomleft.TextColor = Color.White;
            //            }
            //            if (bottomrightExpanded)
            //            {
            //                bottomrightExpanded = false;
            //                bottomright.TextColor = Color.White;
            //            }
            //        }
            //        bottomright.TextColor = Color.Green;
            //        topleft.HeightRequest = 100;
            //        topright.HeightRequest = 100;
            //        bottomleft.HeightRequest = 100;
            //        bottomright.HeightRequest = 100;

            //        biginfo.Text = bottomright.Text + " expanded";
            //        grid.Children.Add(biginfo, 0, 2, 2, 4);

            //        grid.Children.Remove(bottomleft);
            //        grid.Children.Add(bottomleft, 0, 1, 1, 2);

            //        grid.Children.Remove(bottomright);
            //        grid.Children.Add(bottomright, 1, 2, 1, 2);

            //        bottomrightExpanded = true;
            //        expanded = true;
            //    }
            //    //gridButton.HeightRequest = 100;

            //};




            //var gridButton = new Button { Text = "So is this Button! Click me." };
            //gridButton.Clicked += delegate
            //{
            //    gridButton.Text = string.Format("Thanks! {0} clicks.", count++);
            //    //gridButton.HeightRequest = 100;
            //    grid.Children.Remove(gridButton);
            //    grid.Children.Add(gridButton, 0, 1, 2, 6);
            //};
            //grid.Children.Add(gridButton, 0, 1, 2, 3); // Left, Third element


            //var listpage = new GridLayoutDemo.TodoListPage().Content;

            //grid.Children.Add(listpage);

            layout.Children.Add(grid);
            Content = layout;

            Debug.WriteLine(familyLV.Height);
        }
    }
}
