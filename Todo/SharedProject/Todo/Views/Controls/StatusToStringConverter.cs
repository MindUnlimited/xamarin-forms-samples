using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Todo.Views.Controls
{
    public class StatusToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string name = null;

            int status;
            int.TryParse(value.ToString(), out status);


            //-1: Cancelled
            //0: Conceived
            //1: Planned
            //2: Initiated (started)
            //3: <25% completed
            //4: <50%
            //5: <75%
            //6: On hold / Blocked
            //7: Completed

            switch ((int) status)
            {
                case -1:
                    name = "Cancelled";
                    break;
                case 0:
                    name = "Conceived";
                    break;
                case 1:
                    name = "Not started";
                    break;
                case 2:
                    name = "Started";
                    break;
                case 3:
                    name = "25% Complete";
                    break;
                case 4:
                    name = "50% Complete";
                    break;
                case 5:
                    name = "75% Complete";
                    break;
                case 6:
                    name = "On Hold";
                    break;
                case 7:
                    name = "Completed";
                    break;
                default:
                    break;
            }


            return name;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
