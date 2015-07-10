using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Todo.Models;
using Xamarin.Forms;

namespace Todo.Views
{
    class DomainRow : StackLayout
    {
        private StackLayout leftLVWithFooter;
        private ListView leftLV;
        private IEnumerable<Item> leftItems;
        //private StackLayout leftFooter;

        private StackLayout rightLVWithFooter;
        private ListView rightLV;
        private IEnumerable<Item> rightItems;
        //private StackLayout rightFooter;

        public BoxView middleBorder;

        //Bindable property for the progress color
        public static readonly BindableProperty LeftFooterProperty =
          BindableProperty.Create<DomainRow, StackLayout>(p => p.LeftFooter, new StackLayout());
        //Gets or sets the color of the progress bar
        public StackLayout LeftFooter
        {
            get { return (StackLayout)GetValue(LeftFooterProperty); }
            set { SetValue(LeftFooterProperty, value); }
        }

        //Bindable property for the progress color
        public static readonly BindableProperty RightFooterProperty =
          BindableProperty.Create<DomainRow, StackLayout>(p => p.RightFooter, new StackLayout());
        //Gets or sets the color of the progress bar
        public StackLayout RightFooter
        {
            get { return (StackLayout)GetValue(RightFooterProperty); }
            set { SetValue(RightFooterProperty, value); }
        }



        public DomainRow(ListView leftLV, ItemListViewModel leftViewModel, ListView rightLV, ItemListViewModel rightViewModel, uint borderSize, RelativeLayout parentLayout, uint rows)
        {
            this.leftLV = leftLV;
            this.rightLV = rightLV;

            //this.expandedLV = new ListView { IsVisible = false };

            leftItems = (IEnumerable<Item>)leftLV.ItemsSource;
            rightItems = (IEnumerable<Item>)rightLV.ItemsSource;


            this.Spacing = 0;

            StackLayout row = new StackLayout { Spacing = 10, Padding = new Thickness(10, 0, 10, 0), VerticalOptions = LayoutOptions.FillAndExpand};

            row.Orientation = StackOrientation.Horizontal;
            middleBorder = new BoxView { Color = Color.White };
            middleBorder.HeightRequest = leftLV.Height;
            middleBorder.WidthRequest = borderSize;

            //BoxView leftBottomBorder = new BoxView { Color = Color.White };
            //leftBottomBorder.HeightRequest = borderSize;
            //leftBottomBorder.WidthRequest = leftLV.Width;

            //StackLayout left = new StackLayout { Spacing = 0 , Orientation};
            //left.Children.Add(leftLV);
            //left.Children.Add(leftBottomBorder);

            //BoxView rightBottomBorder = new BoxView { Color = Color.White };
            //leftBottomBorder.HeightRequest = borderSize;
            //leftBottomBorder.WidthRequest = rightLV.Width;

            //StackLayout right = new StackLayout { Spacing = 0 };
            //left.Children.Add(rightLV);
            //left.Children.Add(rightBottomBorder);


            //label.SetBinding(Label.TextProperty, "Name");
            //label.BindingContext = new { Name = "John Doe", Company = "Xamarin" };



            //rightLV.BindingContext = Todo.App.currentDPage.friendsItemsList;
            //rightLV.SetBinding(ListView.FooterProperty, "Footer");



            //leftLV.SetBinding(ListView.FooterProperty, "footL", BindingMode.TwoWay);
            //leftLV.BindingContext = new { footL = LeftFooter };
            //rightLV.SetBinding(ListView.FooterProperty, "footR", BindingMode.TwoWay);
            //rightLV.BindingContext = new { footR = RightFooter };

            //StackLayout leftFooter = new StackLayout { Spacing = 0 };
            //LeftFooter.SetBinding(StackLayout.)

            //setLeftFooter(leftViewModel);
            //setRightFooter(rightViewModel);

            leftLVWithFooter = new StackLayout { Spacing = 0};
            leftLVWithFooter.Children.Add(leftLV);
            leftLVWithFooter.Children.Add(LeftFooter);

            //rightFooter = new StackLayout { Orientation = StackOrientation.Horizontal };

            rightLVWithFooter = new StackLayout { Spacing = 0 };
            rightLVWithFooter.Children.Add(rightLV);
            rightLVWithFooter.Children.Add(RightFooter);

            row.Children.Add(leftLV);
            row.Children.Add(middleBorder);
            row.Children.Add(rightLV);

            //Debug.WriteLine(leftLV.Bounds.ToString());

            // spans both listviews
            BoxView topBorder = new BoxView { Color = Color.White };
            topBorder.HeightRequest = borderSize;
            topBorder.WidthRequest = leftLV.Width + borderSize + rightLV.Width;

            Children.Add(topBorder);
            Children.Add(row);

            HeightRequest = parentLayout.Height / rows;

            //leftLV.PropertyChanged += (o, e) => {
            //    leftItems = (IEnumerable<Item>)leftLV.ItemsSource;
            //};

            //rightLV.PropertyChanged += (o, e) =>
            //{
            //    rightItems = (IEnumerable<Item>)rightLV.ItemsSource;
            //};


        }


        Style invisibleListView = new Style(typeof(ListView))
        {
            Setters =   {
                            new Setter {Property = ListView.OpacityProperty, Value = 0}
                        }
        };

        Style visibleListView = new Style(typeof(ListView))
        {
            Setters =   {
                            new Setter {Property = ListView.OpacityProperty, Value = 1}
                        }
        };

        // MAYBE ALS A LABEL INSTEAD OF HEADER?

        //public StackLayout LeftFooter
        //{
        //    get
        //    {
        //        var footer = new StackLayout { Spacing = 0 };
        //        footer.Children.Add(new Label { Text = leftLV.ItemsSource != null ? ((ICollection<Item>)leftLV.ItemsSource).Count.ToString() + " items" : "0 items" });
        //        return footer;
        //    }
        //}

        //public string RightFooter { get { return rightLV.ItemsSource != null ? ((ICollection<Item>)rightLV.ItemsSource).Count.ToString() + " items" : "0 items"; } }



        public void changeRightHeader(StackLayout header)
        {
            rightLV.Header = header;
        }

        public void changeLeftHeader(StackLayout header)
        {
            leftLV.Header = header;
        }

        public void setLeftFooter(ItemListViewModel leftViewModel)
        {
            leftLV.BindingContext = leftViewModel;
            leftLV.SetBinding(ListView.FooterProperty, "Footer");

            //LeftFooter.BindingContext = leftViewModel;

            //leftViewModel.PropertyChanged += (o,e) => 
            //{
            //    LeftFooter.Children.Clear();
            //    foreach (var child in leftViewModel.Footer.Children)
            //    {
            //        LeftFooter.Children.Add(child);
            //    }
            //    LeftFooter.Children.Add(new Label { Text = "testtest" });
            //    //LeftFooter = leftViewModel.Footer;
            //};
        }

        public void setRightFooter(ItemListViewModel rightViewModel)
        {
            rightLV.BindingContext = rightViewModel;
            rightLV.SetBinding(ListView.FooterProperty, "Footer");
            var test = rightLV.Footer;
        }

        //public void changeLeftFooter(StackLayout footer)
        //{
        //    //leftLVWithFooter.Children[leftLVWithFooter.Children.IndexOf(leftFooter)] = footer;

        //    //leftLVWithFooter.Children.Remove(leftFooter);
        //    //leftLVWithFooter.Children.Add(footer);

        //    //leftLVWithFooter.Children[1] = footer;

        //    //leftLVWithFooter = new StackLayout { Spacing = 0 };
        //    //leftLVWithFooter.Children.Add(leftLV);
        //    //leftLVWithFooter.Children.Add(footer);

        //    leftFooter = footer;
        //}



        //public void changeRightFooter(StackLayout footer)
        //{
        //    //var test = rightLVWithFooter.Children.IndexOf(rightFooter);
        //    //rightLVWithFooter.Children[rightLVWithFooter.Children.IndexOf(rightFooter)] = footer;

        //    //rightLVWithFooter.Children[1] = footer;

        //    //rightLVWithFooter = new StackLayout { Spacing = 0 };
        //    //rightLVWithFooter.Children.Add(rightLV);
        //    //rightLVWithFooter.Children.Add(footer);

        //    rightFooter = footer;
        //}

        public void hideItems()
        {
            leftLV.ItemTemplate = new DataTemplate(typeof (TodoItemCellInvisible));
            rightLV.ItemTemplate = new DataTemplate(typeof(TodoItemCellInvisible));
        }

        public void showItems()
        {
            leftLV.ItemTemplate = new DataTemplate(typeof(TodoItemCell));
            rightLV.ItemTemplate = new DataTemplate(typeof(TodoItemCell));

            //middleBorder.HeightRequest = leftLV.Height;
        }

    }
}
