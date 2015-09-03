using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Todo.Views.Controls
{
    public class TypeToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string source = null;

            int type;
            int.TryParse(value.ToString(), out type);


            //-1: Cancelled
            //0: Conceived
            //1: Planned
            //2: Initiated (started)
            //3: <25% completed
            //4: <50%
            //5: <75%
            //6: On hold / Blocked
            //7: Completed

            switch ((int)type)
            {
                case 2: // Goal
                    source = Device.OnPlatform(
                            iOS: "Images/Goal64.png",
                            Android: "Goal64.png",
                            WinPhone: "Assets/ItemIcons/Goal64.png");
                    break;
                case 3: // Task
                    source = Device.OnPlatform(
                            iOS: "Images/Task64.png",
                            Android: "Resources/drawable/Task64.png",
                            WinPhone: "Assets/ItemIcons/Task64.png");
                    break;
                case 4: // Subtask
                    source = Device.OnPlatform(
                            iOS: "Images/Subtask64.png",
                            Android: "Subtask64.png",
                            WinPhone: "Assets/ItemIcons/Subtask64.png");
                    break;
                default:
                    break;
            }


            return ImageSource.FromFile(source);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
