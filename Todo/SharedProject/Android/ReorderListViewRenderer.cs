﻿using Android.Widget;
using System.ComponentModel;
using Todo.Android;
using Todo.Views;
using Xamarin.Forms.Platform.Android;

[assembly: Xamarin.Forms.ExportRenderer(typeof(ReorderListView), typeof(ReorderListViewRenderer))]

namespace Todo.Android
{
    public class ReorderListViewRenderer : ViewRenderer<Xamarin.Forms.View, ListView>
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            var rlv = (ReorderListView)sender;

            var context = Xamarin.Forms.Forms.Context;



            ListView monthsListView = FindViewById<ListView>(Resource.Id.months_list);// new ListView(context);//(ListView)FindViewById<ListView>(Resource.Layout.ListView);

            ArrayAdapter arrayAdapter = new ArrayAdapter(context, Resource.Layout.ListView, rlv.Items);
            var listView = new ListView(context) { Adapter = arrayAdapter };

            this.SetNativeControl(listView);

//            //Debug.WriteLine("rlv width: " + rlv.Width.ToString());

//            //this.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;

//            var listbox = new ReorderListBox.ReorderListBox();

//            if (rlv.Width > 0)
//                listbox.Width = rlv.Width;
//            if (rlv.Height > 0)
//                listbox.Height = rlv.Height;

//            //Debug.WriteLine("listbox width: " + listbox.Width.ToString());
//            //listbox.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;

//            //rlv.PropertyChanged += ((o, ev) =>
//            //{
//            //    listbox.ItemsSource = rlv.Items;
//            //});

//            System.Windows.DataTemplate template = System.Windows.Markup.XamlReader.Load(
//                @"<DataTemplate xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"">
//                    <StackPanel Orientation=""Horizontal"">
//                        <TextBlock Text=""{Binding Path=Name}"" Margin=""12,4,12,4"" FontSize=""26"" TextTrimming=""WordEllipsis""></TextBlock>
//                    </StackPanel>
//                </DataTemplate>") as System.Windows.DataTemplate;

//            listbox.ItemsSource = rlv.Items;
//            listbox.IsReorderEnabled = true;
//            listbox.ItemTemplate = template;

//            this.Children.Clear();
//            this.Children.Add(listbox);
        }
    
    }
    
}