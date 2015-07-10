using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Todo.Views.Controls
{
    public class StatusToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string source = null;

            int status;
            int.TryParse(value.ToString(), out status);

            switch ((int) status)
            {
                case -1:
                    source = Device.OnPlatform(
                        iOS: "Images/TaskCancelled64.png",
                        Android: "TaskCancelled64.png",
                        WinPhone: "Assets/ItemIcons/TaskCancelled64.png");
                    break;
                case 0:
                // no icon for conceived
                case 1:
                    source = Device.OnPlatform(
                            iOS: "Images/TaskNotStarted64.png",
                            Android: "TaskNotStarted64.png",
                            WinPhone: "Assets/ItemIcons/TaskNotStarted64.png");
                    break;
                case 2:
                    source = Device.OnPlatform(
                            iOS: "Images/TaskInitiated64.png",
                            Android: "TaskInitiated64.png",
                            WinPhone: "Assets/ItemIcons/TaskInitiated64.png");
                    break;
                case 3:
                    source = Device.OnPlatform(
                            iOS: "Images/Task25pComplete64.png",
                            Android: "Resources/drawable/Task25pComplete64.png",
                            WinPhone: "Assets/ItemIcons/Task25pComplete64.png");
                    break;
                case 4:
                    source = Device.OnPlatform(
                            iOS: "Images/Task50pComplete64.png",
                            Android: "Task50pComplete64.png",
                            WinPhone: "Assets/ItemIcons/Task50pComplete64.png");
                    break;
                case 5:
                    source = Device.OnPlatform(
                            iOS: "Images/Task75pComplete64.png",
                            Android: "Task75pComplete64.png",
                            WinPhone: "Assets/ItemIcons/Task75pComplete64.png");
                    break;
                case 6:
                    source =Device.OnPlatform(
                            iOS: "Images/TaskOnHold64.png",
                            Android: "TaskOnHold64.png",
                            WinPhone: "Assets/ItemIcons/TaskOnHold64.png");
                    break;
                case 7:
                    source =Device.OnPlatform(
                            iOS: "Images/TaskCompleted64.png",
                            Android: "TaskCompleted64.png",
                            WinPhone: "Assets/ItemIcons/TaskCompleted64.png");
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
