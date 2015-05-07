using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace GridLayoutDemo
{
    public class App
    {
        public static Page GetMainPage()
        {
            //return new GridLayout();
            return new NavigationPage(new RelativeLayoutPage3());
        }
    }
}
