using Microsoft.Phone.Controls;
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
using System.Windows.Controls;
using Todo.Views;
using Todo.WinPhone;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;

[assembly: ExportRenderer(typeof(SelectContactsView), typeof(SelectContactsViewRenderer))]

namespace Todo.WinPhone
{
    public class SelectContactsViewRenderer : ViewRenderer<SelectContactsView, LongListMultiSelector>
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (this.Element == null || this.Control == null)
                return;
            else if (e.PropertyName == SelectContactsView.contactsProperty.PropertyName)
            {
                //Control.ItemsSource = Element.ItemCollection;
                var itembind = new System.Windows.Data.Binding("ContactsCollection");
                itembind.Mode = System.Windows.Data.BindingMode.TwoWay;

                Control.SetBinding(LongListMultiSelector.ItemsSourceProperty, itembind);
            }           
        }

        protected override void OnElementChanged(ElementChangedEventArgs<SelectContactsView> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || this.Element == null)
                return;


            LongListMultiSelector llms = new LongListMultiSelector();
            llms.DataContext = Element;
            var itembind = new System.Windows.Data.Binding("ContactsCollection");
            itembind.Mode = System.Windows.Data.BindingMode.OneWay;

            llms.SetBinding(LongListMultiSelector.ItemsSourceProperty, itembind);

            llms.LayoutMode = LongListSelectorLayoutMode.List;

            System.Windows.DataTemplate dataTemp = System.Windows.Markup.XamlReader.Load(
    @"<DataTemplate xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"">
                    <StackPanel Orientation=""Horizontal"">
                        <TextBlock Text = ""{Binding Path = Name}""></TextBlock>
                    </StackPanel>
                </DataTemplate>") as System.Windows.DataTemplate;

            //System.Windows.DataTemplate dataTemp = System.Windows.Markup.XamlReader.Load(
            //@"
            //        <DataTemplate xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"">
            //            <StackPanel Orientation = ""Horizontal"" >
            //                <TextBlock Text = ""{ Binding Path = Name} "" Margin = ""8,4,8,4"" FontSize = ""26"" TextTrimming = ""WordEllipsis"" ></TextBlock>
            //            </StackPanel >
            //        </DataTemplate>") as System.Windows.DataTemplate;

            llms.ItemTemplate = dataTemp;

            llms.Name = "Contacts";
            llms.IsSelectionEnabled = true;


            SetNativeControl(llms);
        }
    }
}
