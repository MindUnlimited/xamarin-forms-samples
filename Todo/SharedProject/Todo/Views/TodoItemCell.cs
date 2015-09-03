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

            var typeSize = 25;
            var typeImg = new Image { HeightRequest = typeSize, WidthRequest = typeSize, VerticalOptions = LayoutOptions.Center };
            typeImg.SetBinding(Image.SourceProperty, new Binding("Type", BindingMode.OneWay, new Todo.Views.Controls.TypeToImageSourceConverter()));


            int statusSize = Device.OnPlatform(iOS: 25, Android: 15, WinPhone: 25);
            var statusImg = new Image { HeightRequest = statusSize, WidthRequest = statusSize, VerticalOptions = LayoutOptions.Center };
            statusImg.SetBinding(Image.SourceProperty, new Binding("Status", BindingMode.OneWay, new Todo.Views.Controls.StatusToImageSourceConverter()));

            var label = new Label
            {
                YAlign = TextAlignment.Center,
                LineBreakMode = LineBreakMode.TailTruncation,
                FontSize = 15
            };
            label.SetBinding(Label.TextProperty, "Name");

            var layout = new StackLayout
            {
                Padding = new Thickness(0, 0, 0, 0),
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = { typeImg, statusImg, label }
            };
            View = layout;

            base.OnBindingContextChanged();
		}
	}
}

