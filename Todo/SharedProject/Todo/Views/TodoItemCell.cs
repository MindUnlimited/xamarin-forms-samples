using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace Todo
{
	public class TodoItemCell : ViewCell
	{
		public TodoItemCell ()
		{
            var dotLabel = new Label { Text = "•", YAlign = TextAlignment.Center };

			var label = new Label {
                YAlign = TextAlignment.Center,
                LineBreakMode = LineBreakMode.TailTruncation
			};
			label.SetBinding (Label.TextProperty, "Name");

			var tick = new Image {
				Source = FileImageSource.FromFile ("check.png"),
			};
			tick.SetBinding (Image.IsVisibleProperty, "Status");

			var layout = new StackLayout {
				Padding = new Thickness(20, 0, 0, 0),
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.FillAndExpand,
                //Children = {label, tick}
				Children = {dotLabel, label, tick}
			};
			View = layout;
		}

		protected override void OnBindingContextChanged ()
		{
			// Fixme : this is happening because the View.Parent is getting 
			// set after the Cell gets the binding context set on it. Then it is inheriting
			// the parents binding context.
			View.BindingContext = BindingContext;
            var item = (Item)BindingContext;

            //-1: Cancelled
            //0: Conceived
            //1: Planned
            //2: Initiated
            //3: <25% completed
            //4: <50%
            //5: <75%
            //6: On hold / Blocked
            //7: Completed
            

            if (item != null)
            {

                Image itemImage = new Image();
                int imgSize = Device.OnPlatform(iOS: 25, Android: 15, WinPhone: 25);

                switch (item.Status)
                {
                    case -1:
                        itemImage = new Image
                        {
                            HeightRequest = imgSize,
                            WidthRequest = imgSize,
                            Source = Device.OnPlatform(
                            iOS: ImageSource.FromFile("Images/TaskCancelled64.png"),
                            Android: ImageSource.FromFile("TaskCancelled64.png"),
                            WinPhone: ImageSource.FromFile("Assets/ItemIcons/TaskCancelled64.png"))
                        };
                        break;
                    case 0:
                        itemImage = new Image
                        {
                            HeightRequest = imgSize,
                            WidthRequest = imgSize,
                            Source = Device.OnPlatform(
                                iOS: ImageSource.FromFile("Images/TaskConceived64.png"),
                                Android: ImageSource.FromFile("TaskConceived64.png"),
                                WinPhone: ImageSource.FromFile("Assets/ItemIcons/TaskConceived64.png"))
                        };
                        break;
                    case 1:
                        itemImage = new Image
                        {
                            HeightRequest = imgSize,
                            WidthRequest = imgSize,
                            Source = Device.OnPlatform(
                            iOS: ImageSource.FromFile("Images/TaskNotStarted64.png"),
                            Android: ImageSource.FromFile("TaskNotStarted64.png"),
                            WinPhone: ImageSource.FromFile("Assets/ItemIcons/TaskNotStarted64.png"))
                        };
                        break;
                    case 2:
                        itemImage = new Image
                        {
                            HeightRequest = imgSize,
                            WidthRequest = imgSize,
                            Source = Device.OnPlatform(
                            iOS: ImageSource.FromFile("Images/TaskInitiated64.png"),
                            Android: ImageSource.FromFile("TaskInitiated64.png"),
                            WinPhone: ImageSource.FromFile("Assets/ItemIcons/TaskStarted64.png"))
                        };
                        break;
                    case 3:
                        itemImage = new Image
                        {
                            HeightRequest = imgSize,
                            WidthRequest = imgSize,
                            Source = Device.OnPlatform(
                            iOS: ImageSource.FromFile("Images/Task25pComplete64.png"),
                            Android: ImageSource.FromFile("Resources/drawable/Task25pComplete64.png"),
                            WinPhone: ImageSource.FromFile("Assets/ItemIcons/Task25pComplete64.png"))
                        };
                        break;
                    case 4:
                        itemImage = new Image
                        {
                            HeightRequest = imgSize,
                            WidthRequest = imgSize,
                            Source = Device.OnPlatform(
                            iOS: ImageSource.FromFile("Images/Task50pComplete64.png"),
                            Android: ImageSource.FromFile("Task50pComplete64.png"),
                            WinPhone: ImageSource.FromFile("Assets/ItemIcons/Task50pComplete64.png"))
                        };
                        break;
                    case 5:
                        itemImage = new Image
                        {
                            HeightRequest = imgSize,
                            WidthRequest = imgSize,
                            Source = Device.OnPlatform(
                            iOS: ImageSource.FromFile("Images/Task75pComplete64.png"),
                            Android: ImageSource.FromFile("Task75pComplete64.png"),
                            WinPhone: ImageSource.FromFile("Assets/ItemIcons/Task75pComplete64.png"))
                        };
                        break;
                    case 6:
                        itemImage = new Image
                        {
                            HeightRequest = imgSize,
                            WidthRequest = imgSize,
                            Source = Device.OnPlatform(
                            iOS: ImageSource.FromFile("Images/TaskOnHold64.png"),
                            Android: ImageSource.FromFile("TaskOnHold64.png"),
                            WinPhone: ImageSource.FromFile("Assets/ItemIcons/TaskOnHold64.png"))
                        };
                        break;
                    case 7:
                        itemImage = new Image
                        {
                            HeightRequest = imgSize,
                            WidthRequest = imgSize,
                            Source = Device.OnPlatform(
                            iOS: ImageSource.FromFile("Images/TaskCompleted64.png"),
                            Android: ImageSource.FromFile("TaskCompleted64.png"),
                            WinPhone: ImageSource.FromFile("Assets/ItemIcons/TaskCompleted64.png"))
                        };
                        break;
                    default:
                        break;
                }

                //int fontSize = 20;
                //int spacing = 4;

                //StackLayout stackStarted = new StackLayout { Orientation = StackOrientation.Horizontal, Padding = 0, Spacing = spacing, HorizontalOptions = LayoutOptions.CenterAndExpand };
                //stackStarted.Children.Add(new Image { HeightRequest = imgSize, WidthRequest = imgSize, Source = "TaskStarted64.png" });
                //stackStarted.Children.Add(new Label { Text = started.ToString(), YAlign = TextAlignment.Center, FontSize = fontSize });

                //StackLayout stackNotStarted = new StackLayout { Orientation = StackOrientation.Horizontal, Padding = 0, Spacing = spacing, HorizontalOptions = LayoutOptions.CenterAndExpand };
                //stackNotStarted.Children.Add(new Image { HeightRequest = imgSize, WidthRequest = imgSize, Source = "TaskNotStarted64.png" });
                //stackNotStarted.Children.Add(new Label { Text = conceived.ToString(), YAlign = TextAlignment.Center, FontSize = fontSize });

                //StackLayout stackOnHold = new StackLayout { Orientation = StackOrientation.Horizontal, Padding = 0, Spacing = spacing, HorizontalOptions = LayoutOptions.CenterAndExpand };
                //stackOnHold.Children.Add(new Image { HeightRequest = imgSize, WidthRequest = imgSize, Source = "TaskOnHold64.png" });
                //stackOnHold.Children.Add(new Label { Text = blocked.ToString(), YAlign = TextAlignment.Center, FontSize = fontSize });

                //StackLayout stackCompleted = new StackLayout { Orientation = StackOrientation.Horizontal, Padding = 0, Spacing = spacing, HorizontalOptions = LayoutOptions.CenterAndExpand };
                //stackCompleted.Children.Add(new Image { HeightRequest = imgSize, WidthRequest = imgSize, Source = "TaskCompleted64.png" });
                //stackCompleted.Children.Add(new Label { Text = completed.ToString(), YAlign = TextAlignment.Center, FontSize = fontSize });


                int spacesBeforeDot = 0;
                // Example Dictionary again.
                Dictionary<int, string> letterDict = new Dictionary<int, string>()
            {
                {2, "G"}, // Goal
                {3, "P"}, // Project
                {4, "T"} // Task
            };
                string letter = "";



                if (item.Type == 3)
                    spacesBeforeDot = 1;
                if (item.Type == 4)
                    spacesBeforeDot = 6;

                //spacesBeforeDot = (item.Type - 2) * 3 + 2 * (item.Type - 2);

                if (letterDict.ContainsKey(item.Type))
                    letter = letterDict[item.Type];
                else
                    letter = "";

                string spaces = string.Concat(Enumerable.Repeat(" ", spacesBeforeDot));
                if (item.Type > 2)
                    spaces += "└╴";

                var hierarchy = new Label
                {
                    //Text = spaces + "• " + letter,
                    Text = spaces,
                    YAlign = TextAlignment.Center
                };

                var itemType = new Label
                {
                    //Text = spaces + "• " + letter,
                    Text = letter,
                    YAlign = TextAlignment.Center
                };

                var label = new Label
                {
                    YAlign = TextAlignment.Center,
                    LineBreakMode = LineBreakMode.TailTruncation
                };
                label.SetBinding(Label.TextProperty, "Name");

                var tick = new Image
                {
                    Source = FileImageSource.FromFile("check.png"),
                };
                tick.SetBinding(Image.IsVisibleProperty, "Status");

                var layout = new StackLayout
                {
                    Padding = new Thickness(0, 0, 0, 0),
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Children = { itemImage, hierarchy, itemType, label }
                };
                View = layout;
            }
            else
            {
                Debug.WriteLine("item is null?!");
            }


            base.OnBindingContextChanged();
		}
	}
}

