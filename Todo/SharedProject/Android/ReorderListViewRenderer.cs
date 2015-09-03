﻿using Android.Widget;
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

[assembly: Xamarin.Forms.ExportRenderer(typeof(ReorderListView), typeof(ReorderListViewRenderer))]

namespace Todo.Android
{
    //NativeListViewRenderer : ViewRenderer<NativeListView, global::Android.Widget.ListView>
    public class ReorderListViewRenderer : ViewRenderer<ReorderListView, MyListView>
    {
        private DraggableListAdapter adapter;
        private double currentHeight;

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
                adapter = new DraggableListAdapter(Forms.Context as Activity, Element.ItemCollection);
                Control.Adapter = adapter;// new ArrayAdapter<Item>(Forms.Context, Resource.Layout.listitem, Element.ItemCollection); //draggableListAdapter;// new DraggableListAdapter(this, items);
                Control.ItemClick  += clicked;

                currentHeight = e.NewElement.Height;

                // make the listview invisible if its being collapsed
                e.NewElement.SizeChanged += (sender, args) =>
                {
                    if(currentHeight <= e.NewElement.Height)
                    {
                        currentHeight = e.NewElement.Height;
                        e.NewElement.Opacity = 1;
                    }
                    else
                    {
                        currentHeight = e.NewElement.Height;
                        e.NewElement.Opacity = 0;
                    }
                };

            }
		}

        private async void clicked(object sender, AdapterView.ItemClickEventArgs e)
        {
            Item item = adapter.Items[e.Position];

            var todoPage = new TodoItemPage();
            todoPage.BindingContext = item;
            await Todo.App.selectedDomainPage.Navigation.PushAsync(todoPage);
        }


		protected override void OnElementPropertyChanged (object sender, PropertyChangedEventArgs e)
		{
            base.OnElementPropertyChanged(sender, e);

            if (this.Element == null || this.Control == null)
                return;
            if (e.PropertyName == ReorderListView.ReorderProperty.PropertyName)
            {
                Control.ReorderingEnabled = Element.ReorderEnabled;
            }
            else if (e.PropertyName == ReorderListView.ItemsProperty.PropertyName)
            {
                adapter = new DraggableListAdapter(Forms.Context as Activity, Element.ItemCollection);
                Control.Adapter = adapter;
            }           


            //var listv = (ReorderListView) sender;

            //if (this.Element == null || this.Control == null)
            //    return;
            ////if (e.PropertyName == ReorderListView.ReorderProperty.PropertyName)
            ////{
            ////    Control.IsReorderEnabled = Element.ReorderEnabled;
            ////}
            //if (e.PropertyName == ReorderListView.ItemsProperty.PropertyName)
            //{
            //    //Control.Adapter = new ArrayAdapter<Item>(Forms.Context, Resource.Layout.listitem, listv.ItemCollection);//Forms.Context as Activity, Resource.Layout.Main) { };
            //    items = draggableListAdapter.Items;
            //    Control.Adapter = new DraggableListAdapter(Forms.Context as Activity, items);// new DraggableListAdapter(this, items);
            //}           

            //base.OnElementPropertyChanged (sender, e);
            //    // update the Items list in the UITableViewSource

            //Control.Adapter = new ArrayAdapter<Item>(Forms.Context as Activity, Resource.Layout.Main);//new NativeListViewAdapter (Forms.Context as Activity, Element);
		}
    
    }
    
}