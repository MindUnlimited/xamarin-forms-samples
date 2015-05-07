using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GridLayoutDemo
{
    class MainPageRow : StackLayout
    {
        private ListView leftLV;
        IEnumerable<string> leftItems;
        private ListView rightLV;
        IEnumerable<string> rightItems;
        public BoxView middleBorder;


        // MAYBE ALS A LABEL INSTEAD OF HEADER?

        

        public MainPageRow(ListView leftLV, ListView rightLV, int borderSize)
        {
            this.leftLV = leftLV;
            this.rightLV = rightLV;
            //this.expandedLV = new ListView { IsVisible = false };

            leftItems = (IEnumerable<string>) leftLV.ItemsSource;
            rightItems = (IEnumerable<string>) rightLV.ItemsSource;


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

        }

        public void hideItems()
        {
            leftLV.ItemsSource = null;
            rightLV.ItemsSource = null;
        }

        public void showItems()
        {
            leftLV.ItemsSource = leftItems;
            rightLV.ItemsSource = rightItems;

            //middleBorder.HeightRequest = leftLV.Height;
        }

    }


}
