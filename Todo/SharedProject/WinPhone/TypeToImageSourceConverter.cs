using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Todo.WinPhone
{
    public class TypeToImageSourceConverterWP : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string source = null;

            int type;
            int.TryParse(value.ToString(), out type);

            switch ((int)type)
            {
                case 2: // Goal
                    source = "Assets/ItemIcons/Goal64.png";
                    break;
                case 3: // Task
                    source = "Assets/ItemIcons/Project64.png";
                    break;
                case 4: // SubTask
                    source = "Assets/ItemIcons/Task64.png";
                    break;
                default:
                    break;
            }


            //return new ImageSource().  .FromFile(source);
            return source;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
