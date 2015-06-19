using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Views;
using Todo.WinPhone;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;

[assembly: ExportRenderer(typeof(ReorderListView), typeof(ReorderListViewRenderer))]

namespace Todo.WinPhone
{
    public class ReorderListViewRenderer : ViewRenderer<View, ReorderListBox.ReorderListBox>
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            var rlv = (ReorderListView)sender;
            //Debug.WriteLine("rlv width: " + rlv.Width.ToString());

            //this.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;

            var listbox = new ReorderListBox.ReorderListBox();

            if (rlv.Width > 0)
                listbox.Width = rlv.Width;
            if (rlv.Height > 0)
                listbox.Height = rlv.Height;

            //Debug.WriteLine("listbox width: " + listbox.Width.ToString());
            //listbox.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;


            //rlv.PropertyChanged += ((o, ev) =>
            //{
            //    if (ev.PropertyName == "ReorderEnabled")
            //        listbox.IsReorderEnabled = rlv.ReorderEnabled;
            //    //listbox.ItemsSource = rlv.Items;
            //});

            System.Windows.DataTemplate template = System.Windows.Markup.XamlReader.Load(
                @"<DataTemplate xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"">
                    <StackPanel Orientation=""Horizontal"">
                        <TextBlock Text=""{Binding Path=Name}"" Margin=""12,4,12,4"" FontSize=""26"" TextTrimming=""WordEllipsis""></TextBlock>
                    </StackPanel>
                </DataTemplate>") as System.Windows.DataTemplate;

            System.Windows.Data.Binding reorderBinding = new System.Windows.Data.Binding("ReorderEnabled");
            reorderBinding.ElementName = "rlv";
            reorderBinding.Mode = System.Windows.Data.BindingMode.TwoWay;

            listbox.ItemsSource = rlv.Items;
            listbox.SetBinding(ReorderListBox.ReorderListBox.IsReorderEnabledProperty, reorderBinding);
            listbox.IsReorderEnabled = rlv.ReorderEnabled;
            listbox.ItemTemplate = template;

            this.Children.Clear();
            this.Children.Add(listbox);
        }



        //protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        //{
        //    base.OnElementChanged(e);
        //}
    }
}
