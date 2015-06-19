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
    }
}
