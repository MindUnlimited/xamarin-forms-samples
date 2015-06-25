using Android.App;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using DraggableListView;
using System.Collections.Generic;

namespace DraggableListView
{
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

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View cell = convertView;
            if (cell == null)
            {
                cell = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, parent, false);
                cell.SetMinimumHeight(150);
                cell.SetBackgroundColor(Color.DarkViolet);
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
}