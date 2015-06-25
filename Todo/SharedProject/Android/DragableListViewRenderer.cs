using Android.Widget;
using System.ComponentModel;
using Todo.Android;
using Todo.Views;
using Todo.Models;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using WorkingWithListviewNative.Droid;
using Android.App;
using System.Collections.Generic;
using DraggableListView;
using Android.Views;

[assembly: Xamarin.Forms.ExportRenderer(typeof(DragableListView.DragableListView), typeof(DragableListViewRenderer))]

namespace Todo.Android
{

    //NativeListViewRenderer : ViewRenderer<NativeListView, global::Android.Widget.ListView>
    public class DragableListViewRenderer : ViewRenderer<DragableListView.DragableListView, MyListView>
    {
        List<string> items;

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
                //Control.Adapter = new ArrayAdapter<Item>(Forms.Context as Activity, Resource.Layout.listitem, e.NewElement.ItemCollection);// Resource.Id.months_list);//new NativeListViewAdapter (Forms.Context as Activity, e.NewElement);

                
                SetContentView(Resource.Layout.Main);
                var list = FindViewById<DragableListView.DragableListView>(Resource.Id.months_list);


                items = new List<string> {
				"Vegetables",
				"Fruits",
				"Flower Buds",
				"Legumes",
				"Vegetables",
				"Fruits",
				"Flower Buds",
				"Legumes",
			    };
                list.Adapter = new DraggableListAdapter(this, items);



                Control.ItemClick += clicked;
			}
		}

        public class DraggableListAdapter : BaseAdapter, IDraggableListAdapter
        {
            public List<string> Items { get; set; }


            public int mMobileCellPosition { get; set; }

            Activity context;

            public DraggableListAdapter(Activity context, List<string> items)
                : base()
            {
                Items = items;
                this.context = context;
                mMobileCellPosition = int.MinValue;
            }

            public override Java.Lang.Object GetItem(int position)
            {
                return Items[position];
            }

            public override long GetItemId(int position)
            {
                return position;
            }

            public override global::Android.Views.View GetView(int position, global::Android.Views.View convertView, ViewGroup parent)
            {
                global::Android.Views.View cell = convertView;
                if (cell == null)
                {
                    cell = context.LayoutInflater.Inflate(Android.Resource.Layout.listitem, parent, false);
                    cell.SetMinimumHeight(150);
                    cell.SetBackgroundColor(global::Android.Graphics.Color.DarkViolet);
                }

                var text = cell.FindViewById<TextView>(Android.Resource.Id.Text1);
                if (text != null)
                {
                    text.Text = position.ToString();
                }

                cell.Visibility = mMobileCellPosition == position ? ViewStates.Invisible : ViewStates.Visible;
                cell.TranslationY = 0;

                return cell;
            }

            public override int Count
            {
                get
                {
                    return Items.Count;
                }
            }

            public void SwapItems(int indexOne, int indexTwo)
            {
                var oldValue = Items[indexOne];
                Items[indexOne] = Items[indexTwo];
                Items[indexTwo] = oldValue;
                mMobileCellPosition = indexTwo;
                NotifyDataSetChanged();
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