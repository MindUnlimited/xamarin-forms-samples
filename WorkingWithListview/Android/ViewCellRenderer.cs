using Android.Graphics.Drawables;
using System;
using WorkingWithListview.Android;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ListView), typeof(ListViewCustomRenderer))]
namespace WorkingWithListview.Android
{
    public class ListViewCustomRenderer : ListViewRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);

            var listView = Control;
            var divider = Control.Divider;
            divider.SetAlpha(0);

            Control.Divider = divider;
            Control.DividerHeight = 30;
            Control.OffsetLeftAndRight(20);

            Control.SetPadding(30, 30, 30, 30);
            //Control.SetBackgroundColor(Color.Gray.ToAndroid());

            //tableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
            //tableView.BackgroundColor = UIColor.FromRGB(0x2C, 0x3E, 0x50);
        }
    }
}