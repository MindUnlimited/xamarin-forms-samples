using System;
using Xamarin.Forms;
using System.Diagnostics;

namespace WorkingWithListview
{
	/// <summary>
	/// For custom renderer on Android (only)
	/// </summary>
	public class ListButton : Button { }


	public class CustomCell : ViewCell
	{
		public CustomCell ()
		{
			var label1 = new Label { Text = "Label 1", Font = Font.SystemFontOfSize(NamedSize.Small, FontAttributes.Bold) };
			label1.SetBinding(Label.TextProperty, new Binding("."));

			var label2 = new Label { Text = "Label 2", Font = Font.SystemFontOfSize(NamedSize.Small) };

            var comments = new Label { Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum." };

            var slider = new Slider();

			var button = new ListButton { 
				Text = "X",
				BackgroundColor = Color.Gray,
				HorizontalOptions = LayoutOptions.EndAndExpand
			};
			button.SetBinding(Button.CommandParameterProperty, new Binding("."));
			button.Clicked += (sender, e) => {
				var b = (Button)sender;
				var t = b.CommandParameter;
				//((ContentPage)((ListView)((StackLayout)b.ParentView).ParentView).ParentView).DisplayAlert("Clicked", t + " button was clicked", "OK");
				Debug.WriteLine("clicked" + t);
			};

            var cell = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Padding = new Thickness(15, 5, 5, 15),

                Children = {
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            new StackLayout {
                            Orientation = StackOrientation.Vertical,
                            Children = { label1, label2 }
                            },
                            button
                        }
                    },
                    comments,
                    slider

                }
            };

            var frame = new Frame
            {
                Padding = new Thickness(50, 50, 50, 50),
                OutlineColor = Color.White,
                BackgroundColor = Color.White,
                Content = cell
            };

            View = cell;
		}
	}
}

