using Android.Widget;
using System.ComponentModel;
using Todo.Android;
using Todo.Views;
using Todo.Models;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using WorkingWithListviewNative.Droid;
using Android.App;

[assembly: Xamarin.Forms.ExportRenderer(typeof(ReorderListView), typeof(ReorderListViewRenderer))]

namespace Todo.Android
{

    //NativeListViewRenderer : ViewRenderer<NativeListView, global::Android.Widget.ListView>
    public class ReorderListViewRenderer : ViewRenderer<ReorderListView, MyListView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ReorderListView> e)
		{
			base.OnElementChanged (e);

			if (Control == null) {
				SetNativeControl (new MyListView(Forms.Context));
			}

			if (e.OldElement != null) {
				// unsubscribe
				Control.ItemClick -= clicked;
			}

			if (e.NewElement != null) {
				// subscribe
                Control.Adapter = new ArrayAdapter<Item>(Forms.Context as Activity, Resource.Layout.listitem, e.NewElement.ItemCollection);// Resource.Id.months_list);//new NativeListViewAdapter (Forms.Context as Activity, e.NewElement);
                Control.ItemClick += clicked;
			}
		}

        private void clicked(object sender, AdapterView.ItemClickEventArgs e)
        {
            throw new System.NotImplementedException();
        }


		protected override void OnElementPropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
            var listv = (ReorderListView) sender;

            if (this.Element == null || this.Control == null)
                return;
            //if (e.PropertyName == ReorderListView.ReorderProperty.PropertyName)
            //{
            //    Control.IsReorderEnabled = Element.ReorderEnabled;
            //}
            if (e.PropertyName == ReorderListView.ItemsProperty.PropertyName)
            {
                Control.Adapter = new ArrayAdapter<Item>(Forms.Context, Resource.Layout.listitem, listv.ItemCollection);//Forms.Context as Activity, Resource.Layout.Main) { };
            }           

            //base.OnElementPropertyChanged (sender, e);
            //    // update the Items list in the UITableViewSource

            //Control.Adapter = new ArrayAdapter<Item>(Forms.Context as Activity, Resource.Layout.Main);//new NativeListViewAdapter (Forms.Context as Activity, Element);
		}
    
    }
    
}