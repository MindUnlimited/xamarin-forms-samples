using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Todo.WinPhone
{
    public class TypeToImageSourceConverterWP : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string source = null;

            int type;
            int.TryParse(value.ToString(), out type);


            //1: Goal Domain
            //2: Goal
            //3: Task -> Project
            //4: Subtask -> Task
            //5: Resource
            //6: TimeSchedule(to be used when a trainee can work on something, e.g.a goal domain)

            switch ((int)type)
            {
                case 2:
                    source = "Assets/ItemIcons/Goal64.png";
                    break;
                case 3:
                    source = "Assets/ItemIcons/Project64.png";
                    break;
                case 4:
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