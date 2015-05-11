using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace Todo.Views
{
    class DomainRow : StackLayout
    {
        private ListView leftLV;
        IEnumerable<Item> leftItems;
        private ListView rightLV;
        IEnumerable<Item> rightItems;
        public BoxView middleBorder;

        Style invisibleListView = new Style(typeof(ListView))
        {
            Setters =   {
                            new Setter {Property = ListView.OpacityProperty, Value = 0}
                        }
        };

        Style visibleListView = new Style(typeof(ListView))
        {
            Setters =   {
                            new Setter {Property = ListView.OpacityProperty, Value = 1}
                        }
        };

        // MAYBE ALS A LABEL INSTEAD OF HEADER?



        public DomainRow(ListView leftLV, ListView rightLV, int borderSize, RelativeLayout parentLayout, int rows)
        {
            this.leftLV = leftLV;
            this.rightLV = rightLV;
            //this.expandedLV = new ListView { IsVisible = false };

            leftItems = (IEnumerable<Item>) leftLV.ItemsSource;
            rightItems = (IEnumerable<Item>) rightLV.ItemsSource;


            this.Spacing = 0;

            StackLayout row = new StackLayout { Spacing = 0 };

            row.Orientation = StackOrientation.Horizontal;
            middleBorder = new BoxView { Color = Color.White };
            middleBorder.HeightRequest = leftLV.Height;
            middleBorder.WidthRequest = borderSize;

            //BoxView leftBottomBorder = new BoxView { Color = Color.White };
            //leftBottomBorder.HeightRequest = borderSize;
            //leftBottomBorder.WidthRequest = leftLV.Width;

            //StackLayout left = new StackLayout { Spacing = 0 , Orientation};
            //left.Children.Add(leftLV);
            //left.Children.Add(leftBottomBorder);

            //BoxView rightBottomBorder = new BoxView { Color = Color.White };
            //leftBottomBorder.HeightRequest = borderSize;
            //leftBottomBorder.WidthRequest = rightLV.Width;

            //StackLayout right = new StackLayout { Spacing = 0 };
            //left.Children.Add(rightLV);
            //left.Children.Add(rightBottomBorder);

            row.Children.Add(leftLV);
            row.Children.Add(middleBorder);
            row.Children.Add(rightLV);

            Debug.WriteLine(leftLV.Bounds.ToString());

            // spans both listviews
            BoxView topBorder = new BoxView { Color = Color.White };
            topBorder.HeightRequest = borderSize;
            topBorder.WidthRequest = leftLV.Width + borderSize + rightLV.Width;

            Children.Add(topBorder);
            Children.Add(row);

            HeightRequest = parentLayout.Height / rows;
        }

        public void changeLeftHeader(StackLayout header)
        {
            leftLV.Header = header;
        }

        public void changeRightHeader(StackLayout header)
        {
            rightLV.Header = header;
        }

        public void hideItems()
        {
            leftLV.ItemTemplate = new DataTemplate(typeof (TodoItemCellInvisible));
            rightLV.ItemTemplate = new DataTemplate(typeof(TodoItemCellInvisible));
        }

        public void showItems()
        {
            leftLV.ItemTemplate = new DataTemplate(typeof(TodoItemCell));
            rightLV.ItemTemplate = new DataTemplate(typeof(TodoItemCell));

            //middleBorder.HeightRequest = leftLV.Height;
        }

    }
}
