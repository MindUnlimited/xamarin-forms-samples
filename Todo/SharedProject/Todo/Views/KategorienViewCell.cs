using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Todo.Views
{
    class KategorienViewCell : ViewCell // Typ für Anzeige der ListView Freizeitangebote
    {
        public KategorienViewCell() // Spezial-definition 
        {
            var AnzeigeLabel = new Label();
            AnzeigeLabel.SetBinding(Label.TextProperty, "cText");
            AnzeigeLabel.VerticalOptions = LayoutOptions.Center;
            AnzeigeLabel.BindingContextChanged += (sender, e) =>
            {
            };
            var IconAnzeige = new Image();
            // Old Version
            //IconAnzeige.SetBinding(Image.SourceProperty, "cIconPfad");
            // New Version
            // Note: as IconAnzeige.SetBinding(Image.SourceProperty, "cIconPfad"); don't have worked in my Project with WP, I had to implement a special StringToImageConverter for the Binding
            IconAnzeige.SetBinding(Image.SourceProperty, new Binding("cIconPfad", BindingMode.OneWay, new Todo.Views.Controls.StringToImageConverter()));
            //                 
            // pattform-specific settings -> depends on Icon -> not yet finished
            switch (Device.OS)
            {
                case TargetPlatform.WinPhone:
                    IconAnzeige.VerticalOptions = LayoutOptions.Center;
                    AnzeigeLabel.Font = Font.SystemFontOfSize(30);
                    break;
                case TargetPlatform.iOS:
                    IconAnzeige.HeightRequest = 15;
                    IconAnzeige.VerticalOptions = LayoutOptions.Center;
                    break;
                case TargetPlatform.Android:
                    IconAnzeige.HeightRequest = 15;
                    IconAnzeige.VerticalOptions = LayoutOptions.Center;
                    break;

            }
            var s = new StackLayout();
            s.Orientation = StackOrientation.Horizontal; // Element horizontal anordnen
            //var s = new TableView();
            s.Children.Add(IconAnzeige);
            s.Children.Add(AnzeigeLabel);
            this.View = s;
        }
    } 
}
