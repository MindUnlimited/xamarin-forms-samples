using System;
using Xamarin.Forms;

namespace Todo
{
	public class TodoItemCellBig : ViewCell
	{
		public TodoItemCellBig ()
		{
            Label label = null;
            if (Device.OS == TargetPlatform.Android)
            {
                label = new Label
                {
                    YAlign = TextAlignment.Center,
                    FontSize = 20
                };
                label.SetBinding(Label.TextProperty, "Name");
            }
            else if (Device.OS == TargetPlatform.WinPhone)
            {
                label = new Label
                {
                    YAlign = TextAlignment.Center,
                    FontSize = 25
                };
                label.SetBinding(Label.TextProperty, "Name");
            }

			

			var tick = new Image {
				Source = FileImageSource.FromFile ("check.png"),
			};
			tick.SetBinding (Image.IsVisibleProperty, "Status");

			var layout = new StackLayout {
				Padding = new Thickness(20, 0, 0, 5),
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Children = {label, tick}
			};
			View = layout;
		}

		protected override void OnBindingContextChanged ()
		{
			// Fixme : this is happening because the View.Parent is getting 
			// set after the Cell gets the binding context set on it. Then it is inheriting
			// the parents binding context.
			View.BindingContext = BindingContext;
			base.OnBindingContextChanged ();
		}
	}
}

