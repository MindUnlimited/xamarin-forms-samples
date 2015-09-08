using System;
using Xamarin.Forms;
using System.Diagnostics;

namespace WorkingWithListview
{
	public class CustomCellPage : ContentPage
	{
		public CustomCellPage ()
		{
			var listView = new ListView ();
            listView.HasUnevenRows = true;
            listView.SeparatorColor = Color.Transparent;

			listView.ItemsSource = new [] { "Cards UI afmaken", "Android Material design afmaken", "iOS oriënteren" };
			listView.ItemTemplate = new DataTemplate(typeof(CustomCell));

			listView.ItemTapped += (sender, e) => {
				DisplayAlert("Tapped", e.Item + " row was tapped", "OK");
				((ListView)sender).SelectedItem = null; // de-select the row
			};

			//Padding = new Thickness (20,0,20,0);
            BackgroundColor = Color.FromHex("d3d3d3");
            Content = listView;
		}
	}
}

