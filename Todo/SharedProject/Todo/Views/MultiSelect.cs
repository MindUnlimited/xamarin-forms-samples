using System;
using Xamarin.Forms;

namespace Todo
{
    public class MultiSelect
    {
        public string cIconPath { get; set; } // Name of the Icon
        public string Text { get; set; } // Text to display in ListView
        public bool Selected { get; set; } // Flag, whether item is selected or not
        public int iIndex { get; set; }  // Own index to handle the entrys -> see code
    }
}