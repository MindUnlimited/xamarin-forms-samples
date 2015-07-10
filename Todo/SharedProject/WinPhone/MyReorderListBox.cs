using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Views;
using Todo.WinPhone;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(ReorderListView), typeof(ReorderListViewRenderer))]

namespace Todo.WinPhone
{
    public class MyReorderListBox : ReorderListBox.ReorderListBox
    {
        //public MyReorderListBox() : base()
        //{
        //    this.SelectionChanged += async (obj, ev) =>
        //    {
        //        var Item = (Item)ev.AddedItems[0];

        //        var todoPage = new TodoItemPage();
        //        todoPage.BindingContext = Item;
        //        await Todo.App.Navigation.PushAsync(todoPage);
        //        //await Navigation.PushAsync(todoPage);
        //    };
        //}

    }


}
