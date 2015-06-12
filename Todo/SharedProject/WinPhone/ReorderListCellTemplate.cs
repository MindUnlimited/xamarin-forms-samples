using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Todo.WinPhone
{
	public class ReorderListCellTemplate : DataTemplate
	{
        private uint windowsPhoneFontSize = 25;
        private int spacesBeforeDot = 0;

        public ReorderListCellTemplate()
		{
            string spaces = string.Concat(Enumerable.Repeat(' ', spacesBeforeDot));
            TextBlock dotLabel = new TextBlock { Text = spaces + "•", VerticalAlignment = VerticalAlignment.Center};
            //Label dotLabel = new Label { Text = spaces + "•", YAlign = TextAlignment.Center };

            TextBlock label = new TextBlock
            {
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = windowsPhoneFontSize,
                TextTrimming = System.Windows.TextTrimming.WordEllipsis
            };

            Binding myBinding = new Binding();
            myBinding.Path = new PropertyPath("Name");
            myBinding.Mode = BindingMode.TwoWay;
            //BindingOperations.SetBinding(txtText, TextBox.TextProperty, myBinding);

            label.SetBinding(TextBlock.TextProperty, myBinding);

            dotLabel.FontSize = windowsPhoneFontSize;

            //var tick = new Image {
            //    Source = FileImageSource.FromFile ("check.png"),
            //};
            //tick.SetBinding (Image.IsVisibleProperty, "Status");

			var layout = new StackPanel {
				//Padding = new Thickness(20, 0, 0, 0),
				//Orientation = StackOrientation.Horizontal,
				//HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = { dotLabel, label }
			};
			//View = layout;
		}

        //protected override void OnBindingContextChanged ()
        //{
        //    // Fixme : this is happening because the View.Parent is getting 
        //    // set after the Cell gets the binding context set on it. Then it is inheriting
        //    // the parents binding context.
        //    View.BindingContext = BindingContext;
			
        //    int spacesBeforeDot = 0;
        //    // Example Dictionary again.
        //    Dictionary<int, string> letterDict = new Dictionary<int, string>()
        //    {
        //        {2, "G"}, // Goal
        //        {3, "P"}, // Project
        //        {4, "T"} // Task
        //    };
        //    string letter = "";


        //    var item = (Item)BindingContext;
        //    if (item.Type == 3)
        //        spacesBeforeDot = 1;
        //    if (item.Type == 4)
        //        spacesBeforeDot = 6;
        //    //spacesBeforeDot = (item.Type - 2) * 3 + 2 * (item.Type - 2);

        //    if (letterDict.ContainsKey(item.Type))
        //        letter = letterDict[item.Type];
        //    else
        //        letter = "";

        //    string spaces = string.Concat(Enumerable.Repeat(" ", spacesBeforeDot));
        //    if (item.Type > 2)
        //        spaces += "└╴";

        //    var hierarchyAlignment = new Label
        //    {
        //        //Text = spaces + "• " + letter,
        //        Text = spaces,
        //        YAlign = TextAlignment.Center
        //    };

        //    var itemType = new Label
        //    {
        //        //Text = spaces + "• " + letter,
        //        Text = letter,
        //        YAlign = TextAlignment.Center
        //    };


        //    Label label =  new Label
        //    {
        //        YAlign = TextAlignment.Center,
        //        FontSize = windowsPhoneFontSize,
        //        LineBreakMode = LineBreakMode.TailTruncation
        //    };
        //    label.SetBinding(Label.TextProperty, "Name");

        //    itemType.FontSize = windowsPhoneFontSize;

        //    var tick = new Image
        //    {
        //        Source = FileImageSource.FromFile("check.png"),
        //    };
        //    tick.SetBinding(Image.IsVisibleProperty, "Status");

        //    var layout = new StackLayout
        //    {
        //        Padding = new Thickness(0, 0, 0, 0),
        //        Orientation = StackOrientation.Horizontal,
        //        HorizontalOptions = LayoutOptions.FillAndExpand,
        //        Children = { hierarchyAlignment, itemType, label, tick }
        //    };
        //    View = layout;

        //    base.OnBindingContextChanged();
        //}
	}
}

