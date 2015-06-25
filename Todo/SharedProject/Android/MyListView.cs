using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Todo.Views;
using Todo.Android;

[assembly: ExportRenderer(typeof(DragableListView.DragableListView), typeof(ReorderListViewRenderer))]

namespace Todo.Android
{
    public class MyListView : DragableListView.DragableListView
    {
        public MyListView(Context context) : base(context) {}
    }
}