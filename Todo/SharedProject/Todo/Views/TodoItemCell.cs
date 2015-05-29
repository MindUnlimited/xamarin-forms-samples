using System;
using System.Collections.Generic;
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

            int spacesBeforeDot = 0;
            // Example Dictionary again.
            Dictionary<int, string> letterDict = new Dictionary<int, string>()
            {
                {2, "G"}, // Goal
                {3, "P"}, // Project
                {4, "T"} // Task
            };
            string letter = "";


            var item = (Item)BindingContext;
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
                Children = { hierarchy, itemType, label, tick }
            };
            View = layout;

            base.OnBindingContextChanged();
		}
	}
}

