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
    public class StatusToImageSourceConverterWP : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string source = null;

            int status;
            int.TryParse(value.ToString(), out status);

            switch ((int) status)
            {
                case -1:
                    source = "Assets/ItemIcons/TaskCancelled64.png";
                    break;
                case 0:
                // no icon for conceived
                case 1:
                    source = "Assets/ItemIcons/TaskNotStarted64.png";
                    break;
                case 2:
                    source = "Assets/ItemIcons/TaskInitiated64.png";
                    break;
                case 3:
                    source = "Assets/ItemIcons/Task25pComplete64.png";
                    break;
                case 4:
                    source = "Assets/ItemIcons/Task50pComplete64.png";
                    break;
                case 5:
                    source = "Assets/ItemIcons/Task75pComplete64.png";
                    break;
                case 6:
                    source ="Assets/ItemIcons/TaskOnHold64.png";
                    break;
                case 7:
                    source ="Assets/ItemIcons/TaskCompleted64.png";
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
