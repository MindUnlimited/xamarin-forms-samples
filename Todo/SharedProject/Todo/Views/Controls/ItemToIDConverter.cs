using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Todo.Views.Controls
{
    public class ItemToIDConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string output = "";
            if (value is string)
                output = (string) value;
            else if (value is Item)
            {
                Debug.WriteLine(value.ToString());
                Item it = (Item)value;
                output = it.Parent;
            }
            return output;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string id = (string)value;
            Item it = Todo.App.Database.GetItem(id).Result;

            return it;
        }
    }
}