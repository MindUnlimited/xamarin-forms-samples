using Android.App;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using DraggableListView;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Todo;
using Todo.Android;

namespace DraggableListView
{
    public class JavaObjectWrapper<T> : Java.Lang.Object
    {
        public T Obj { get; set; }
    }

    public class DraggableListAdapter : BaseAdapter, IDraggableListAdapter
    {
        public ObservableCollection<Item> Items { get; set; }


        public int mMobileCellPosition { get; set; }

        Activity context;

        public DraggableListAdapter(Activity context, ObservableCollection<Item> items)
            : base()
        {
            Items = items;
            this.context = context;
            mMobileCellPosition = int.MinValue;
        }

        public override Java.Lang.Object GetItem(int position)
        {
            Item it = Items[position];
            return new JavaObjectWrapper<Item> { Obj = it };
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
                //Android.Resource.Id.
                cell = context.LayoutInflater.Inflate(Android.Resource.Layout.ActivityListItem, parent, false);//SimpleListItem1, parent, false);
                //cell = context.LayoutInflater.Inflate(Android.Resource.Layout.listitem, parent, false);//  .listitem, parent, false);
  //              context.LayoutInflater.Inflate(Android.Resource.Layout. .inflate(R.layout.rowlayout, parent, false);
 
                //TextView textView = (TextView) cell.FindViewById(Android.Resource.Id.Text1);
                ImageView image = (ImageView)cell.FindViewById(Android.Resource.Id.Icon);

                //textView.setText(values[positon]);

                //image.setImageResource(R.drawable.no);


                //cell.SetMinimumHeight(150);
                //cell.SetBackgroundColor(Color.DarkViolet);
            }

            var icon = cell.FindViewById<ImageView>(Android.Resource.Id.Icon);
            

            int iconId = -2;
            switch (Items[position].Status)
            {
                case -1:
                    iconId = Resource.Drawable.TaskCancelled64;
                    break;
                case 0:
                // no icon for conceived
                case 1:
                    iconId = Resource.Drawable.TaskNotStarted64;
                    break;
                case 2:
                    iconId = Resource.Drawable.TaskInitiated64;
                    break;
                case 3:
                    iconId = Resource.Drawable.Task25pComplete64;
                    break;
                case 4:
                    iconId = Resource.Drawable.Task50pComplete64;
                    break;
                case 5:
                    iconId = Resource.Drawable.Task75pComplete64;
                    break;
                case 6:
                    iconId = Resource.Drawable.TaskOnHold64;
                    break;
                case 7:
                    iconId = Resource.Drawable.TaskCompleted64;
                    break;
                default:
                    break;
            }

            icon.SetImageResource(iconId);


            var text = cell.FindViewById<TextView>(Android.Resource.Id.Text1);
            if (text != null)
            {
                text.Text = Items[position].Name;// position.ToString();
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