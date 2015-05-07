using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GridLayoutDemo
{
    public class GridLayoutPage2 : ContentPage
    {
        int count = 1;
        bool topleftExpanded = false;
        bool toprightExpanded = false;
        bool bottomleftExpanded = false;
        bool bottomrightExpanded = false;
        bool expanded = false;

        public GridLayoutPage2()
        {
            var layout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = 0
            };

            var grid = new Grid
            {
                RowSpacing = 0
            };

            var topleft = new Button { 
                Text = "Family",
                HeightRequest = 150
            };
            var topright = new Button {
                Text = "Friends",
                HeightRequest = 150
            };
            var bottomleft = new Button {
                Text = "Work",
                HeightRequest = 150 
            };
            var bottomright = new Button {
                Text = "Community",
                HeightRequest = 150
            };
            var biginfo = new Button {
                Text = "",
                TextColor = Color.Green,
                HeightRequest = 200 
            };


            biginfo.Clicked += delegate
            {
                biginfo.Text = "To the items!";
                //biginfo.TextColor = Color.Olive;
            };

            topleft.Clicked += delegate
            {
                if (topleftExpanded)
                {
                    topleft.HeightRequest = 150;
                    topright.HeightRequest = 150;
                    bottomleft.HeightRequest = 150;
                    bottomright.HeightRequest = 150;

                    topleft.TextColor = Color.White;
                    grid.Children.Remove(biginfo);

                    grid.Children.Remove(bottomleft);
                    grid.Children.Add(bottomleft, 0, 1, 1, 2);

                    grid.Children.Remove(bottomright);
                    grid.Children.Add(bottomright, 1, 2, 1, 2);

                    topleftExpanded = false;
                    expanded = false;
                }
                else
                {
                    if (expanded)
                    {
                        grid.Children.Remove(biginfo);
                        if (topleftExpanded)
                        {
                            topleftExpanded = false;
                            topleft.TextColor = Color.White;
                        }
                        if (toprightExpanded)
                        {
                            toprightExpanded = false;
                            topright.TextColor = Color.White;
                        }
                        if (bottomleftExpanded)
                        {
                            bottomleftExpanded = false;
                            bottomleft.TextColor = Color.White;
                        }
                        if (bottomrightExpanded)
                        {
                            bottomrightExpanded = false;
                            bottomright.TextColor = Color.White;
                        }
                    }

                    topleft.TextColor = Color.Green;
                    topleft.HeightRequest = 100;
                    topright.HeightRequest = 100;
                    bottomleft.HeightRequest = 100;
                    bottomright.HeightRequest = 100;

                    biginfo.Text = topleft.Text + " expanded";
                    grid.Children.Add(biginfo, 0, 2, 1, 3);

                    grid.Children.Remove(bottomleft);
                    grid.Children.Add(bottomleft, 0, 1, 3, 4);

                    grid.Children.Remove(bottomright);
                    grid.Children.Add(bottomright, 1, 2, 3, 4);

                    topleftExpanded = true;
                    expanded = true;
                }
                //gridButton.HeightRequest = 100;

            };

            topright.Clicked += delegate
            {
                if (toprightExpanded)
                {
                    topleft.HeightRequest = 150;
                    topright.HeightRequest = 150;
                    bottomleft.HeightRequest = 150;
                    bottomright.HeightRequest = 150;

                    topright.TextColor = Color.White;
                    grid.Children.Remove(biginfo);

                    grid.Children.Remove(bottomleft);
                    grid.Children.Add(bottomleft, 0, 1, 1, 2);

                    grid.Children.Remove(bottomright);
                    grid.Children.Add(bottomright, 1, 2, 1, 2);

                    toprightExpanded = false;
                    expanded = false;
                }
                else
                {
                    if (expanded)
                    {
                        grid.Children.Remove(biginfo);
                        if (topleftExpanded)
                        {
                            topleftExpanded = false;
                            topleft.TextColor = Color.White;
                        }
                        if (toprightExpanded)
                        {
                            toprightExpanded = false;
                            topright.TextColor = Color.White;
                        }
                        if (bottomleftExpanded)
                        {
                            bottomleftExpanded = false;
                            bottomleft.TextColor = Color.White;
                        }
                        if (bottomrightExpanded)
                        {
                            bottomrightExpanded = false;
                            bottomright.TextColor = Color.White;
                        }
                    }

                    topright.TextColor = Color.Green;
                    topleft.HeightRequest = 100;
                    topright.HeightRequest = 100;
                    bottomleft.HeightRequest = 100;
                    bottomright.HeightRequest = 100;

                    biginfo.Text = topright.Text + " expanded";
                    grid.Children.Add(biginfo, 0, 2, 1, 3);

                    grid.Children.Remove(bottomleft);
                    grid.Children.Add(bottomleft, 0, 1, 3, 4);

                    grid.Children.Remove(bottomright);
                    grid.Children.Add(bottomright, 1, 2, 3, 4);

                    toprightExpanded = true;
                    expanded = true;
                }
                //gridButton.HeightRequest = 100;

            };

            bottomleft.Clicked += delegate
            {
                if (bottomleftExpanded)
                {
                    topleft.HeightRequest = 150;
                    topright.HeightRequest = 150;
                    bottomleft.HeightRequest = 150;
                    bottomright.HeightRequest = 150;

                    bottomleft.TextColor = Color.White;
                    grid.Children.Remove(biginfo);

                    grid.Children.Remove(bottomleft);
                    grid.Children.Add(bottomleft, 0, 1, 1, 2);

                    grid.Children.Remove(bottomright);
                    grid.Children.Add(bottomright, 1, 2, 1, 2);

                    bottomleftExpanded = false;
                    expanded = false;
                }
                else
                {
                    if (expanded)
                    {
                        grid.Children.Remove(biginfo);
                        if (topleftExpanded)
                        {
                            topleftExpanded = false;
                            topleft.TextColor = Color.White;
                        }
                        if (toprightExpanded)
                        {
                            toprightExpanded = false;
                            topright.TextColor = Color.White;
                        }
                        if (bottomleftExpanded)
                        {
                            bottomleftExpanded = false;
                            bottomleft.TextColor = Color.White;
                        }
                        if (bottomrightExpanded)
                        {
                            bottomrightExpanded = false;
                            bottomright.TextColor = Color.White;
                        }
                    }

                    bottomleft.TextColor = Color.Green;
                    topleft.HeightRequest = 100;
                    topright.HeightRequest = 100;
                    bottomleft.HeightRequest = 100;
                    bottomright.HeightRequest = 100;

                    biginfo.Text = bottomleft.Text + " expanded";
                    grid.Children.Add(biginfo, 0, 2, 2, 4);

                    grid.Children.Remove(bottomleft);
                    grid.Children.Add(bottomleft, 0, 1, 1, 2);

                    grid.Children.Remove(bottomright);
                    grid.Children.Add(bottomright, 1, 2, 1, 2);

                    bottomleftExpanded = true;
                    expanded = true;
                }
                //gridButton.HeightRequest = 100;

            };

            bottomright.Clicked += delegate
            {
                if (bottomrightExpanded)
                {
                    topleft.HeightRequest = 150;
                    topright.HeightRequest = 150;
                    bottomleft.HeightRequest = 150;
                    bottomright.HeightRequest = 150;

                    bottomright.TextColor = Color.White;
                    grid.Children.Remove(biginfo);

                    grid.Children.Remove(bottomleft);
                    grid.Children.Add(bottomleft, 0, 1, 1, 2);

                    grid.Children.Remove(bottomright);
                    grid.Children.Add(bottomright, 1, 2, 1, 2);

                    bottomrightExpanded = false;
                    expanded = false;
                }
                else
                {
                    if (expanded)
                    {
                        grid.Children.Remove(biginfo);
                        if (topleftExpanded)
                        {
                            topleftExpanded = false;
                            topleft.TextColor = Color.White;
                        }
                        if (toprightExpanded)
                        {
                            toprightExpanded = false;
                            topright.TextColor = Color.White;
                        }
                        if (bottomleftExpanded)
                        {
                            bottomleftExpanded = false;
                            bottomleft.TextColor = Color.White;
                        }
                        if (bottomrightExpanded)
                        {
                            bottomrightExpanded = false;
                            bottomright.TextColor = Color.White;
                        }
                    }
                    bottomright.TextColor = Color.Green;
                    topleft.HeightRequest = 100;
                    topright.HeightRequest = 100;
                    bottomleft.HeightRequest = 100;
                    bottomright.HeightRequest = 100;

                    biginfo.Text = bottomright.Text + " expanded";
                    grid.Children.Add(biginfo, 0, 2, 2, 4);

                    grid.Children.Remove(bottomleft);
                    grid.Children.Add(bottomleft, 0, 1, 1, 2);

                    grid.Children.Remove(bottomright);
                    grid.Children.Add(bottomright, 1, 2, 1, 2);

                    bottomrightExpanded = true;
                    expanded = true;
                }
                //gridButton.HeightRequest = 100;

            };

            grid.Children.Add(topleft, 0, 0); // Left, First element
            grid.Children.Add(topright, 1, 0); // Right, First element
            grid.Children.Add(bottomleft, 0, 1); // Left, Second element
            grid.Children.Add(bottomright, 1, 1); // Right, Second element


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
        }
    }
}
