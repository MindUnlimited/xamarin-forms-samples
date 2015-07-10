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

[assembly: ExportRenderer(typeof(ReorderListView), typeof(ReorderListViewRenderer))]

namespace Todo.WinPhone
{
    public class ReorderListViewRenderer : ViewRenderer<ReorderListView, ReorderListBox.ReorderListBox>
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


            //var listboxTest = System.Windows.Markup.XamlReader.Load( as MyReorderListBox;


            ReorderListBox.ReorderListBox rlb = System.Windows.Markup.XamlReader.Load(
                @"<rlb:ReorderListBox 
                    xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
                    xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""
                    xmlns:d=""http://schemas.microsoft.com/expression/blend/2008""
                    xmlns:mc=""http://schemas.openxmlformats.org/markup-compatibility/2006""
                    xmlns:wp=""clr-namespace:Todo.WinPhone;assembly=Todo.WinPhone""
                    xmlns:rlb=""clr-namespace:ReorderListBox;assembly=ReorderListBox""
                    mc:Ignorable=""d""
                    FontFamily=""{StaticResource PhoneFontFamilyNormal}""
                    FontSize=""{StaticResource PhoneFontSizeNormal}""
                    Foreground=""{StaticResource PhoneForegroundBrush}"">

                    <rlb:ReorderListBox.Resources>
                    <wp:StatusToImageSourceConverterWP x:Key=""converter"" />
                    </rlb:ReorderListBox.Resources>

                    <rlb:ReorderListBox.ItemTemplate>
                        <DataTemplate>

                            <StackPanel Orientation=""Horizontal"">
                                <Image Width=""25"" Height=""25"" Source=""{Binding Status, Mode=OneWay, Converter={StaticResource converter}, ConverterParameter=\{0:d\}}""></Image>
                                <TextBlock Text=""{Binding Path=Name}"" Margin=""12,4,12,4"" FontSize=""26"" TextTrimming=""WordEllipsis""></TextBlock>
                            </StackPanel>
                        </DataTemplate>
                    </rlb:ReorderListBox.ItemTemplate>
                </rlb:ReorderListBox>") as ReorderListBox.ReorderListBox;

            System.Windows.DataTemplate moveTemplate = System.Windows.Markup.XamlReader.Load(
                @"<DataTemplate xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"">
                    <StackPanel Orientation=""Horizontal"">
                        <Image Width=""52"" Height=""52"" Source=""/Move.png"" />
                    </StackPanel>
                </DataTemplate>") as System.Windows.DataTemplate;
            System.Windows.Style ListBoxItemStyle = new System.Windows.Style(typeof(ReorderListBoxItem));
            ListBoxItemStyle.Setters.Add(new System.Windows.Setter(ReorderListBoxItem.DragHandleTemplateProperty, moveTemplate));

            rlb.ItemContainerStyle = ListBoxItemStyle;
            rlb.IsReorderEnabled = Element.ReorderEnabled;
            rlb.ItemsSource = Element.ItemCollection;

            rlb.SelectionChanged += async (obj, ev) =>
            {
                var Item = (Item)ev.AddedItems[0];

                var todoPage = new TodoItemPage();
                todoPage.BindingContext = Item;
                await Todo.App.Navigation.PushAsync(todoPage);
                //await Navigation.PushAsync(todoPage);
            };



            //var rlv = e.NewElement as ReorderListView;
            var listbox = new MyReorderListBox();


            System.Windows.Controls.Image img = new System.Windows.Controls.Image();
            System.Windows.Data.Binding bind = new System.Windows.Data.Binding("Status");
            bind.Converter = new StatusToImageSourceConverterWP();
            img.SetBinding(System.Windows.Controls.Image.SourceProperty, bind);

            TextBlock text = new TextBlock();
            text.FontSize = 26;
            text.TextTrimming = TextTrimming.WordEllipsis;
            text.SetBinding(TextBlock.TextProperty, new System.Windows.Data.Binding("Name"));

            System.Windows.Controls.StackPanel stack = new System.Windows.Controls.StackPanel { Children = { img, text } };
            
            System.Windows.DataTemplate itemTemplate = System.Windows.Markup.XamlReader.Load(
            @"<DataTemplate
                xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
                xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""
                xmlns:d=""http://schemas.microsoft.com/expression/blend/2008""
                xmlns:mc=""http://schemas.openxmlformats.org/markup-compatibility/2006""
                xmlns:conv=""clr-namespace:Todo.Winphone""
                xmlns:local=""using:Todo.Winphone"" >
    
                <StackPanel Orientation=""Horizontal"">

                    <!-- <Setter Property=""Background"" Value=""Black"" />
                    <Style.Triggers>
                        <DataTrigger Binding=""{Binding IsSelected}"" Value=""True"">
                            <Setter Property=""Background"" Value=""White""/>
                        </DataTrigger> -->

                     <Image Width=""52"" Height=""52"" Source=""{Binding Path=Status }"" />
                        <TextBlock Text=""{Binding Path=Name}"" Margin=""12,4,12,4"" FontSize=""26"" TextTrimming=""WordEllipsis""></TextBlock>
                    </StackPanel>
            </DataTemplate>") as System.Windows.DataTemplate;



//            System.Windows.DataTemplate itemTemplate = System.Windows.Markup.XamlReader.Load(
//                @"<DataTemplate xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"">
//                    <StackPanel Orientation=""Horizontal"">
//                        <Image Width=""25"" Height=""25"" Source=""{Binding Status, Mode=OneWay, Converter={StaticResource Todo.Views.Controls:StatusToImageSourceConverter}, ConverterParameter=\{0:d\}}"" />
//                        <TextBlock Text=""{Binding Path=Name}"" Margin=""12,4,12,4"" FontSize=""26"" TextTrimming=""WordEllipsis""></TextBlock>
//                    </StackPanel>
//                </DataTemplate>") as System.Windows.DataTemplate;

            System.Windows.DataTemplate itemTemplat = new System.Windows.DataTemplate();
            //itemTemplat.

//            System.Windows.DataTemplate itemTemplate = System.Windows.Markup.XamlReader.Load(@"<?xml version=""1.0"" encoding=""utf-8"" ?>
//                <DataTemplate xmlns=
//""http://schemas.microsoft.com/winfx/2006/xaml/presentation""     
//xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml"" 
//xmlns:custom=""clr-namespace:Todo.Views.Controls"">  
//                <StackPanel Orientation=""Horizontal"">
//                  <Image Width=""52"" Height=""52"" Source=""{Binding Path=Status, Mode=OneWay, Converter={StaticResource custom:StatusToImageSourceConverter}, ConverterParameter=\{0:d\}}"" /> 
//                  <!--Text=""{Binding Path=ReleaseDate, Mode=OneWay,-->
//                </StackPanel>
//                </DataTemplate>") as System.Windows.DataTemplate;

            
            //itemTemplat.


//            System.Windows.DataTemplate moveTemplate = System.Windows.Markup.XamlReader.Load(
//                @"<DataTemplate xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"">
//                    <StackPanel Orientation=""Horizontal"">
//                        <Image Width=""52"" Height=""52"" Source=""/Move.png"" />
//                    </StackPanel>
//                </DataTemplate>") as System.Windows.DataTemplate;
//            System.Windows.Style ListBoxItemStyle = new System.Windows.Style(typeof(ReorderListBoxItem));
//            ListBoxItemStyle.Setters.Add(new System.Windows.Setter(ReorderListBoxItem.DragHandleTemplateProperty, moveTemplate));

//            listbox.ItemContainerStyle = ListBoxItemStyle;
//            listbox.IsReorderEnabled = Element.ReorderEnabled;
//            listbox.ItemTemplate = itemTemplate;
//            listbox.ItemsSource = Element.ItemCollection;

//            listbox.SelectionChanged += async (obj, ev) =>
//            {
//                var Item = (Item)ev.AddedItems[0];

//                var todoPage = new TodoItemPage();
//                todoPage.BindingContext = Item;
//                await Todo.App.Navigation.PushAsync(todoPage);
//                //await Navigation.PushAsync(todoPage);
//            };

            SetNativeControl(rlb);
        }
    }
}
