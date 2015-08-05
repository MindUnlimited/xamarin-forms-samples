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
                cell = context.LayoutInflater.Inflate(Resource.Layout.row, parent, false);//SimpleListItem1, parent, false);
                //cell = context.LayoutInflater.Inflate(Android.Resource.Layout.listitem, parent, false);//  .listitem, parent, false);
  //              context.LayoutInflater.Inflate(Android.Resource.Layout. .inflate(R.layout.rowlayout, parent, false);
 
                //TextView textView = (TextView) cell.FindViewById(Android.Resource.Id.Text1);
                //ImageView image = (ImageView)cell.FindViewById(Android.Resource.Id.Icon1);

                //textView.setText(values[positon]);

                //image.setImageResource(R.drawable.no);


                //cell.SetMinimumHeight(150);
                //cell.SetBackgroundColor(Color.DarkViolet);
            }

            var level = cell.FindViewById<ImageView>(Resource.Id.level);
            var progress = cell.FindViewById<ImageView>(Resource.Id.progress);


            int levelResID;
            switch (Items[position].Type)
            {
                case 2: // Goal
                    levelResID = Resource.Drawable.Goal64;
                    level.SetImageResource(levelResID);
                    break;
                case 3: // Project
                    levelResID = Resource.Drawable.Project64;
                    level.SetImageResource(levelResID);
                    break;
                case 4: // Task
                    levelResID = Resource.Drawable.Task64;
                    level.SetImageResource(levelResID);
                    break;
                default:
                    break;
            }

            //-1: Cancelled
            //0: Conceived
            //1: Planned
            //2: Initiated (started)
            //3: <25% completed
            //4: <50%
            //5: <75%
            //6: On hold / Blocked
            //7: Completed

            int progressResID;
            switch (Items[position].Status)
            {
                case -1:
                    progressResID = Resource.Drawable.TaskCancelled64;
                    progress.SetImageResource(progressResID);
                    break;
                case 0:
                    progressResID = Resource.Drawable.TaskConceived64;
                    progress.SetImageResource(progressResID);
                    break;
                case 1:
                    progressResID = Resource.Drawable.TaskNotStarted64;
                    progress.SetImageResource(progressResID);
                    break;
                case 2:
                    progressResID = Resource.Drawable.TaskStarted64;
                    progress.SetImageResource(progressResID);
                    break;
                case 3:
                    progressResID = Resource.Drawable.Task25pComplete64;
                    progress.SetImageResource(progressResID);
                    break;
                case 4:
                    progressResID = Resource.Drawable.Task50pComplete64;
                    progress.SetImageResource(progressResID);
                    break;
                case 5:
                    progressResID = Resource.Drawable.Task75pComplete64;
                    progress.SetImageResource(progressResID);
                    break;
                case 6:
                    progressResID = Resource.Drawable.TaskOnHold64;
                    progress.SetImageResource(progressResID);
                    break;
                case 7:
                    progressResID = Resource.Drawable.TaskCompleted64;
                    progress.SetImageResource(progressResID);
                    break;
                default:
                    break;
            }


            var text = cell.FindViewById<TextView>(Resource.Id.title);
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