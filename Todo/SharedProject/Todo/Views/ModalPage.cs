using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Todo.Views
{
    public class ModalPage : ContentPage
    {
        public ModalPage()
        {
            Button btn = new Button{ Text = "Go Back!"};

            HeightRequest = 400;

            btn.Clicked += btn_Clicked;

            Content = btn;
        }

        async void btn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
