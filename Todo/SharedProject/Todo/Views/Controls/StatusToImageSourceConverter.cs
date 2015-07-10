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
                    source = Device.OnPlatform(
                        iOS: "Images/TaskCancelled64.png",
                        Android: "TaskCancelled64.png",
                        WinPhone: "Assets/ItemIcons/TaskCancelled64.png");
                    break;
                case 0:
                    source = Device.OnPlatform(
                            iOS: "Images/TaskConceived64.png",
                            Android: "TaskConceived64.png",
                            WinPhone: "Assets/ItemIcons/TaskConceived64.png");
                    break;
                case 1:
                    source = Device.OnPlatform(
                            iOS: "Images/TaskNotStarted64.png",
                            Android: "TaskNotStarted64.png",
                            WinPhone: "Assets/ItemIcons/TaskNotStarted64.png");
                    break;
                case 2:
                    source = Device.OnPlatform(
                            iOS: "Images/TaskStarted64.png",
                            Android: "TaskStarted64.png",
                            WinPhone: "Assets/ItemIcons/TaskStarted64.png");
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
