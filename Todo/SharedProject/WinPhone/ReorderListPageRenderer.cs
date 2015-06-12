using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Views;
using Todo.WinPhone;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;

[assembly: ExportRenderer(typeof(ReorderListPage), typeof(ReorderListPageRenderer))]

namespace Todo.WinPhone
{
    public class ReorderListPageRenderer : ViewRenderer<View, ReorderListBox.ReorderListBox>
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var listbox = new ReorderListBox.ReorderListBox();

            string[] items = { "een", "twee" };
            listbox.ItemsSource = items;
            listbox.IsReorderEnabled = true;


            //var p = new MyThirdNativePage();
            this.Children.Add(listbox);
        }



        //protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        //{
        //    base.OnElementChanged(e);
        //}
    }
}
