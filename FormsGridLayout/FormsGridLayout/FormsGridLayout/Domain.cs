using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GridLayoutDemo
{
    class Domain : StackLayout
    {
        private ListView lv;
        private ListView expandedLV;

        public void expand(bool b)
        {
            if (b)
            {
                lv.ItemsSource = null;
                expandedLV.IsVisible = true;
            }
            else
            {
                lv.ItemsSource = expandedLV.ItemsSource;
                expandedLV.IsVisible = false;
            }
        }

        public Domain(ListView lv, int borderSize)
        {
            this.lv = lv;
            this.expandedLV = new ListView { ItemsSource = lv.ItemsSource, IsVisible = false };

            this.Spacing = 0;

            BoxView topBorder = new BoxView { Color = Color.White };
            topBorder.HeightRequest = borderSize;
            topBorder.WidthRequest = lv.Width;

            Children.Add(topBorder);
            Children.Add(lv);
            Children.Add(expandedLV);
        }

    }


}
