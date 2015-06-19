using ReorderListBox;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Todo.Views;
using Todo.WinPhone;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;

[assembly: ExportRenderer(typeof(ReorderListView), typeof(ReorderListViewRenderer))]

namespace Todo.WinPhone
{
    public class ReorderListViewRenderer : ViewRenderer<ReorderListView, MyReorderListBox>
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (this.Element == null || this.Control == null)
                return;
            if (e.PropertyName == ReorderListView.ReorderProperty.PropertyName)
            {
                Control.IsReorderEnabled = Element.ReorderEnabled;
            }
            else if (e.PropertyName == ReorderListView.ItemsProperty.PropertyName)
            {
                Control.ItemsSource = Element.ItemCollection;
            }           
        }

        protected override void OnElementChanged(ElementChangedEventArgs<ReorderListView> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || this.Element == null)
                return;

            var rlv = e.NewElement as ReorderListView;
            var listbox = new MyReorderListBox();

            System.Windows.DataTemplate itemTemplate = System.Windows.Markup.XamlReader.Load(
                @"<DataTemplate xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"">
                    <StackPanel Orientation=""Horizontal"">
                        <TextBlock Text=""{Binding Path=Name}"" Margin=""12,4,12,4"" FontSize=""26"" TextTrimming=""WordEllipsis""></TextBlock>
                    </StackPanel>
                </DataTemplate>") as System.Windows.DataTemplate;

            System.Windows.DataTemplate moveTemplate = System.Windows.Markup.XamlReader.Load(
                @"<DataTemplate xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"">
                    <StackPanel Orientation=""Horizontal"">
                        <Image Width=""52"" Height=""52"" Source=""/Move.png"" />
                    </StackPanel>
                </DataTemplate>") as System.Windows.DataTemplate;
            System.Windows.Style ListBoxItemStyle = new System.Windows.Style(typeof(ReorderListBoxItem));
            ListBoxItemStyle.Setters.Add(new System.Windows.Setter(ReorderListBoxItem.DragHandleTemplateProperty, moveTemplate));

            listbox.ItemContainerStyle = ListBoxItemStyle;
            listbox.IsReorderEnabled = rlv.ReorderEnabled;
            listbox.ItemTemplate = itemTemplate;
            listbox.ItemsSource = rlv.ItemCollection;

            SetNativeControl(listbox);
        }
    }
}
