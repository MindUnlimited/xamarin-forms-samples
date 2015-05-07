using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GridLayoutDemo
{
    public class RelativeLayoutPage2 : ContentPage
    {
        private int rows = 2;
        private int borderSize = 1; // pixels
        Dictionary<ListView, IEnumerable<string>> itemStorage = new Dictionary<ListView, IEnumerable<string>>();
        public RelativeLayoutPage2()
        {
            //List<ListView> listViews = new List<ListView>();
            RelativeLayout objRelativeLayout = new RelativeLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            StackLayout objStackLayout = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
            };

            //var labelFormatted = new Label ();
            //var fs = new FormattedString ();
            //fs.Spans.Add (new Span { Text="Friends\n"});
            //fs.Spans.Add (new Span { Text="item3\n", ForegroundColor = Color.Blue, FontSize = 32 });
            //fs.Spans.Add (new Span { Text="item4", ForegroundColor = Color.Green, FontSize = 32 });
            //labelFormatted.FormattedText = fs;

            //labelFormatted.GestureRecognizers.Add(new TapGestureRecognizer
            //{
            //    Command = new Command(() => OnLabelClicked()),
            //});


            ListView familyLV = new ListView{Header = "Family"};
            string[] array = { "item1", "item2", "item3", "item4", "item5", "item6", "item7", "item8", "item9", "item10" };
            familyLV.ItemsSource = array;
            familyLV.BackgroundColor = Color.Red;
            //listViews.Add(familyLV);

            Domain familyDomain = new Domain(familyLV, 1);
            


            ListView friendsLV = new ListView { Header = "Friends" };
            string[] array2 = { "item5", "item6", "item7", "item8" };
            friendsLV.ItemsSource = array2;
            //listViews.Add(friendsLV);

            ListView workLV = new ListView { Header = "Work" };
            string[] array3 = { "item9", "item10", "item11", "item12" };
            workLV.ItemsSource = array3;
            //listViews.Add(workLV);

            ListView communityLV = new ListView { Header = "Community" };
            string[] array4 = { "item13", "item14", "item15", "item16" };
            communityLV.ItemsSource = array4;
            //listViews.Add(communityLV);

            MainPageRow top = new MainPageRow(familyLV, friendsLV, 1);

            ListView familyLV2 = new ListView();
            familyLV2.ItemsSource = familyLV2.ItemsSource;
            familyLV2.SizeChanged += ((o2, e2) =>
            {
                objRelativeLayout.ForceLayout();
            });
            //familyLV2.BackgroundColor = Color.Blue;

            ListView friendsLV2 = new ListView();
            friendsLV2.ItemsSource = friendsLV.ItemsSource;
            friendsLV2.SizeChanged += ((o2, e2) =>
            {
                objRelativeLayout.ForceLayout();
            });

            
            var familyButtonOverlay = new Button();
            bool expanded = false;
            familyButtonOverlay.Opacity = 0;
            familyButtonOverlay.Clicked += ((obj, ev) =>
            {
                if (expanded)
                {
                    //familyDomain.expand(false);
                    top.showItems();
                    //objRelativeLayout.Children.Remove(familyLV2);

                    //foreach (var lv in objRelativeLayout.Children)
                    //{
                    //    if (lv is ListView)
                    //    {
                    //        ListView listV = (ListView)lv;
                    //        //if (lv != familyLV)
                    //        //{
                    //        listV.ItemsSource = itemStorage[listV];
                    //        //}
                    //    }
                    //}

                    //itemStorage.Clear();

                    expanded = false;
                    objRelativeLayout.ForceLayout();
                }
                else
                {
                    //System.Diagnostics.Debug.WriteLine(familyLV.X.ToString() + ' ' + familyLV.Y.ToString() + ' ' + familyLV.Width.ToString() + ' ' + familyLV.Height.ToString());
   
                    expanded = true;
                    //objRelativeLayout.ForceLayout();

                    //familyLV2 = new ListView();
                    //familyLV2.ItemsSource = familyLV.ItemsSource;
                    //familyLV2.BackgroundColor = Color.Blue;
                    //familyLV2.SizeChanged += ((o2, e2) =>
                    //{
                    //    objRelativeLayout.ForceLayout();
                    //});

                    //familyDomain.expand(true);
                    top.hideItems();

                    //foreach (var lv in objRelativeLayout.Children)
                    //{
                    //    if (lv is ListView)
                    //    {
                    //        ListView listV = (ListView)lv;
                    //        //if (lv != familyLV)
                    //        //{
                    //        itemStorage[listV] = (IEnumerable<string>)listV.ItemsSource;
                    //        listV.ItemsSource = null;
                    //        //}
                    //        //listV.HeightRequest = 100;
                    //    }
                    //}

                    

                    //objRelativeLayout.Children.Add(familyLV2
                    //    ,
                    //    xConstraint: Constraint.Constant(0),
                    //    yConstraint: Constraint.RelativeToView(familyDomain,
                    //    new Func<RelativeLayout, View, double>((pobjRelativeLayout, pobjView) =>
                    //    {
                    //        //System.Diagnostics.Debug.WriteLine("Y: " + (pobjView.Y + pobjView.Height).ToString());
                    //        //pobjView.IsVisible = false;
                    //        return pobjView.Y + pobjView.Height;
                    //    }))
                    //    ,
                    //    //heightConstraint: Constraint.RelativeToParent((parent) =>
                    //    //{
                    //    //    return (parent.Height / rows);
                    //    //}),
                    //    heightConstraint: Constraint.RelativeToParent((parent) =>
                    //    {
                    //        return parent.Height - familyDomain.Height - familyDomain.Height;
                    //    }),
                    //    widthConstraint: Constraint.RelativeToParent((parent) =>
                    //    {
                    //        return (parent.Width / 2);
                    //    })

                    //    );

                    //objRelativeLayout.RaiseChild(familyLV2);

                    //System.Diagnostics.Debug.WriteLine(familyLV.X.ToString() + ' ' + familyLV.Y.ToString() + ' ' + familyLV.Width.ToString() + ' ' + familyLV.Height.ToString());

                    objRelativeLayout.ForceLayout();
                }

                //throw new NotImplementedException();
            });

            var friendsButtonOverlay = new Button();
            friendsButtonOverlay.Opacity = 0;
            friendsButtonOverlay.Clicked += ((obj, ev) =>
            {
                if (expanded)
                {
                    expanded = false;
                    objRelativeLayout.Children.Remove(friendsLV2);

                    foreach (var lv in objRelativeLayout.Children)
                    {
                        if (lv is ListView)
                        {
                            ListView listV = (ListView)lv;
                            //if (lv != friendsLV)
                            //{
                            listV.ItemsSource = itemStorage[listV];
                            //}
                        }
                    }

                    itemStorage.Clear();

                }
                else
                {
                    //System.Diagnostics.Debug.WriteLine(familyLV.X.ToString() + ' ' + familyLV.Y.ToString() + ' ' + familyLV.Width.ToString() + ' ' + familyLV.Height.ToString());

                    expanded = true;
                    objRelativeLayout.ForceLayout();

                    friendsLV2 = new ListView();
                    friendsLV2.ItemsSource = friendsLV.ItemsSource;
                    friendsLV2.SizeChanged += ((o2, e2) =>
                    {
                        objRelativeLayout.ForceLayout();
                    });

                    foreach (var lv in objRelativeLayout.Children)
                    {
                        if (lv is ListView)
                        {
                            ListView listV = (ListView)lv;
                            //if (lv != familyLV)
                            //{
                            itemStorage[listV] = (IEnumerable<string>)listV.ItemsSource;
                            listV.ItemsSource = null;
                            //}
                            //listV.HeightRequest = 100;
                        }
                    }



                    objRelativeLayout.Children.Add(friendsLV2
                        ,
                        xConstraint: Constraint.RelativeToParent((parent) =>
                        {
                            return (parent.Width / 2);
                        }),
                        //yConstraint: Constraint.RelativeToView(familyLV,
                        //new Func<RelativeLayout, View, double>((pobjRelativeLayout, pobjView) =>
                        //{
                        //    System.Diagnostics.Debug.WriteLine("Y: " + (pobjView.Y + pobjView.Height).ToString());
                        //    //pobjView.IsVisible = false;
                        //    return pobjView.Y + pobjView.Height;
                        //}))
                        //,
                        yConstraint: Constraint.Constant(40),
                        heightConstraint: Constraint.RelativeToParent((parent) =>
                        {
                            return (parent.Height / rows);
                        }),
                        widthConstraint: Constraint.RelativeToParent((parent) =>
                        {
                            return (parent.Width / 2);
                        })

                        );

                    //objRelativeLayout.RaiseChild(familyLV2);

                    //System.Diagnostics.Debug.WriteLine(familyLV.X.ToString() + ' ' + familyLV.Y.ToString() + ' ' + familyLV.Width.ToString() + ' ' + familyLV.Height.ToString());

                    var familyList = friendsLV2;
                    objRelativeLayout.ForceLayout();
                }

                //throw new NotImplementedException();
            });

            var workButtonOverlay = new Button();
            workButtonOverlay.Opacity = 0;
            workButtonOverlay.Clicked += ((obj, ev) =>
            {
                //throw new NotImplementedException();
            });

            var communityButtonOverlay = new Button();
            communityButtonOverlay.Opacity = 0;
            communityButtonOverlay.Clicked += ((obj, ev) =>
            {
                //throw new NotImplementedException();
            });

            //communityLV.GestureRecognizers.Add(new TapGestureRecognizer
            //{
            //    Command = new Command(() => OnList4Clicked()),
            //});

            //familyLV.SizeChanged += ((o2, e2) =>
            //{
            //    objRelativeLayout.ForceLayout();
            //});

            //familyLV.SizeChanged += ((ob, e) => {
            //    familyButtonOverlay.WidthRequest = familyLV.Width;
            //    familyButtonOverlay.HeightRequest = familyLV.Height;
            //});

            
            objRelativeLayout.Children.Add(top,
                xConstraint: Constraint.Constant(0),
                yConstraint: Constraint.Constant(0),
                widthConstraint: Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width;
                })
                //heightConstraint: Constraint.RelativeToParent((parent) =>
                //{
                //    //return (expanded ? (parent.Height / 10) - borderSize : (parent.Height / rows) - borderSize);
                //    return (parent.Height / rows);
                //}
                //)
                );

            //objRelativeLayout.Children.Add(familyDomain,
            //    xConstraint: Constraint.Constant(0),
            //    yConstraint: Constraint.Constant(borderSize),
            //    widthConstraint: Constraint.RelativeToParent((parent) =>
            //    {
            //        return (parent.Width / 2);
            //    }),
            //    heightConstraint: Constraint.RelativeToParent((parent) =>
            //    {
            //        return (expanded ? (parent.Height / 10) - borderSize : (parent.Height / rows) - borderSize );
            //        //return (parent.Height / rows);
            //    }));

            objRelativeLayout.Children.Add(familyButtonOverlay,
                xConstraint: Constraint.Constant(0),
                yConstraint: Constraint.Constant(borderSize),
                widthConstraint: Constraint.RelativeToParent((parent) =>
                {
                    return (parent.Width / 2) - borderSize;
                })
                //heightConstraint: Constraint.RelativeToParent((parent) =>
                //{
                //    return (expanded ? (parent.Height / 10) - borderSize : (parent.Height / rows) - borderSize);
                //    //return (parent.Height / rows);
                //})
                );

            //// line on top of the row
            //objRelativeLayout.Children.Add(new BoxView { Color = Color.White },
            //    xConstraint: Constraint.Constant(0),
            //    yConstraint: Constraint.Constant(0),
            //    widthConstraint: Constraint.RelativeToParent((parent) =>
            //    {
            //        return parent.Width;
            //    })
            //    ,
            //    heightConstraint: Constraint.Constant(borderSize)
            //    );

            //// line in between the two domains of the row
            //objRelativeLayout.Children.Add(new BoxView { Color = Color.White },
            //    xConstraint: Constraint.RelativeToParent((parent) =>
            //    {
            //        return parent.Width / 2;
            //    }),
            //    yConstraint: Constraint.Constant(0),
            //    widthConstraint: Constraint.Constant(borderSize)
            //    ,
            //    heightConstraint: Constraint.RelativeToView(familyDomain,
            //    new Func<RelativeLayout, View, double>((pobjRelativeLayout, pobjView) =>
            //    {
            //        return pobjView.Height;
            //    }))
            //    );

            //objRelativeLayout.Children.Add(friendsLV,
            //    xConstraint: Constraint.RelativeToParent((parent) =>
            //    {
            //        return (parent.Width / 2) + borderSize;
            //    }),
            //    yConstraint: Constraint.Constant(borderSize),
            //    widthConstraint: Constraint.RelativeToParent((parent) =>
            //    {
            //        return (parent.Width / 2) - borderSize;
            //    }),
            //    heightConstraint: Constraint.RelativeToParent((parent) =>
            //    {
            //        return (expanded ? (parent.Height / rows * 10) - borderSize : (parent.Height / rows) - borderSize);
            //        //return (parent.Height / rows);
            //    }));

            objRelativeLayout.Children.Add(friendsButtonOverlay,
                xConstraint: Constraint.RelativeToParent((parent) =>
                {
                    return (parent.Width / 2) - borderSize;
                }),
                yConstraint: Constraint.Constant(borderSize),
                widthConstraint: Constraint.RelativeToParent((parent) =>
                {
                    return (parent.Width / 2) - borderSize;
                })
                //heightConstraint: Constraint.RelativeToParent((parent) =>
                //{
                //    return (expanded ? (parent.Height / rows * 10) - borderSize : (parent.Height / rows) - borderSize);
                //    //return (parent.Height / rows);
                //})
                
                );





            objRelativeLayout.Children.Add(workLV,
                xConstraint: Constraint.Constant(0),
                //yConstraint: Constraint.RelativeToView(familyLV,
                //new Func<RelativeLayout, View, double>((pobjRelativeLayout, pobjView) =>
                //{
                //    System.Diagnostics.Debug.WriteLine("Y: " + (pobjView.Y + pobjView.Height).ToString());
                //    //pobjView.IsVisible = false;
                //    return pobjView.Y + pobjView.Height;
                //}))
                //,
                yConstraint: Constraint.RelativeToParent((parent) =>
                {
                    //return (expanded ? (parent.Height * 3/4) : (parent.Height / 2));
                    return (parent.Height / 2) + borderSize;
                }),
                widthConstraint: Constraint.RelativeToParent((parent) =>
                {
                    return (parent.Width / 2) - borderSize;
                })
                ,
                heightConstraint: Constraint.RelativeToParent((parent) =>
                {
                    return (expanded ? (parent.Height / rows * 10) - borderSize : (parent.Height / rows) - borderSize);
                    //return (parent.Height / rows);
                })
                //,
                //heightConstraint: Constraint.RelativeToParent((parent) =>
                //{
                //    return (expanded ? (parent.Height / rows * 2) : (parent.Height / rows));
                //})
                );

            // line on top of the row
            objRelativeLayout.Children.Add(new BoxView { Color = Color.White },
                xConstraint: Constraint.Constant(0),
                yConstraint: Constraint.RelativeToParent((parent) =>
                {
                    //return (expanded ? (parent.Height * 3 / 4) : (parent.Height / 2));
                    return parent.Height / 2;
                }),
                widthConstraint: Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width;
                })
                ,
                heightConstraint: Constraint.Constant(borderSize)
                //,
                //heightConstraint: Constraint.RelativeToParent((parent) =>
                //{
                //    return (expanded ? (parent.Height / rows * 2) : (parent.Height / rows));
                //})
                );

            // line in between the two domains of the row
            objRelativeLayout.Children.Add(new BoxView { Color = Color.White },
                xConstraint: Constraint.RelativeToParent((parent) =>
                {
                    return parent.Width / 2;
                }),
                yConstraint: Constraint.RelativeToParent((parent) =>
                {
                    //return (expanded ? (parent.Height * 3 / 4) : (parent.Height / 2));
                    return parent.Height / 2;
                }),
                widthConstraint: Constraint.Constant(borderSize)
                ,
                heightConstraint: Constraint.RelativeToView(workLV,
                new Func<RelativeLayout, View, double>((pobjRelativeLayout, pobjView) =>
                {
                    return pobjView.Height;
                }))
                );

            objRelativeLayout.Children.Add(workButtonOverlay,
                xConstraint: Constraint.Constant(0),
                yConstraint: Constraint.RelativeToParent((parent) =>
                {
                    //return (expanded ? (parent.Height * 3 / 4) : (parent.Height / 2));
                    return (parent.Height / 2) + borderSize;
                }),
                widthConstraint: Constraint.RelativeToParent((parent) =>
                {
                    return (parent.Width / 2) - borderSize;
                })
                
                //heightConstraint: Constraint.RelativeToParent((parent) =>
                //{
                //    return (expanded ? (parent.Height / rows * 10) - borderSize : (parent.Height / rows) - borderSize);
                //    //return (parent.Height / rows);
                //})
                //,
                //heightConstraint: Constraint.RelativeToParent((parent) =>
                //{
                //    return (expanded ? (parent.Height / rows * 2) : (parent.Height / rows));
                //})
                );

            objRelativeLayout.Children.Add(communityLV,
                xConstraint: Constraint.RelativeToParent((parent) =>
                {
                    return (parent.Width / 2);
                }),
                yConstraint: Constraint.RelativeToParent((parent) =>
                {
                    //return (expanded ? (parent.Height * 3 / 4) : (parent.Height / 2));
                    return (parent.Height / 2) + borderSize;
                }),
                widthConstraint: Constraint.RelativeToParent((parent) =>
                {
                    return (parent.Width / 2) - borderSize;
                })
                //,
                //heightConstraint: Constraint.RelativeToParent((parent) =>
                //{
                //    return (expanded ? (parent.Height / rows * 10) - borderSize : (parent.Height / rows) - borderSize);
                //    //return (parent.Height / rows);
                //})
                //,
                //heightConstraint: Constraint.RelativeToParent((parent) =>
                //{
                //    return (expanded ? (parent.Height / rows * 2) : (parent.Height / rows));
                //})
                );

            objRelativeLayout.Children.Add(communityButtonOverlay,
                xConstraint: Constraint.RelativeToParent((parent) =>
                {
                    return (parent.Width / 2);
                }),
                yConstraint: Constraint.RelativeToParent((parent) =>
                {
                    //return (expanded ? (parent.Height * 3 / 4) : (parent.Height / 2));
                    return (parent.Height / 2) + borderSize;
                }),
                widthConstraint: Constraint.RelativeToParent((parent) =>
                {
                    return (parent.Width / 2) - borderSize;
                })
                //,
                //heightConstraint: Constraint.RelativeToParent((parent) =>
                //{
                //    return (expanded ? (parent.Height / rows * 10) - borderSize : (parent.Height / rows) - borderSize);
                //    //return (parent.Height / rows);
                //})
                //,
                //heightConstraint: Constraint.RelativeToParent((parent) =>
                //{
                //    return (expanded ? (parent.Height / rows * 2) : (parent.Height / rows) );
                //})
                );


            //Label objLabel1 = new Label();
            //objLabel1.BackgroundColor = Color.Red;
            //objLabel1.Text = "This is a label";
            
            ////objLabel1.IsVisible = false;
            //objLabel1.SizeChanged += ((o2, e2) =>
            //{
            //    objRelativeLayout.ForceLayout();
            //});
            //objRelativeLayout.Children.Add(objLabel1,
            //    xConstraint: Constraint.RelativeToParent((parent) =>
            //    {
            //        return ((parent.Width - objLabel1.Width) / 2);
            //    }));

            //Label objLabel2 = new Label();
            //objLabel2.BackgroundColor = Color.Blue;
            //objLabel2.Text = "Hi";
            //objRelativeLayout.Children.Add(objLabel2,
            //    xConstraint: Constraint.RelativeToView(objLabel1,
            //    new Func<RelativeLayout, View, double>((pobjRelativeLayout, pobjView) =>
            //    {
            //        return pobjView.X + pobjView.Width;
            //    })));

            //MainPageRow secondRow = new MainPageRow(workLV, communityLV, 1);

            //objRelativeLayout.Children.Add(secondRow,
            //    xConstraint: Constraint.Constant(0),
            //    yConstraint: Constraint.RelativeToParent((parent) =>
            //    {
            //        //return (expanded ? (parent.Height * 3 / 4) : (parent.Height / 2));
            //        return (parent.Height / 2);
            //    }),
            //    widthConstraint: Constraint.RelativeToParent((parent) =>
            //    {
            //        return parent.Width;
            //    })
            //    ,
            //    heightConstraint: Constraint.RelativeToParent((parent) =>
            //    {
            //        return (expanded ? (parent.Height / rows * 2) : (parent.Height / rows));
            //        //return (parent.Height / rows);
            //    })
            //    //,
            //    //heightConstraint: Constraint.RelativeToParent((parent) =>
            //    //{
            //    //    return (expanded ? (parent.Height / rows * 2) : (parent.Height / rows) );
            //    //})
            //    );


            objStackLayout.Children.Add(objRelativeLayout);

            Button objButton1 = new Button();

            objButton1.Text = "Test1";
            objButton1.Clicked += ((o2, e2) =>
            {
                //objLabel1.Text = "This is some other label text";
            });

            objButton1.SizeChanged += ((o2, e2) =>
            {
                objRelativeLayout.ForceLayout();
            });

            objStackLayout.Children.Add(objButton1);
            

            this.Content = objStackLayout;
        }

        private object OnList4Clicked()
        {
            throw new NotImplementedException();
        }

        private object OnLabelClicked()
        {
            throw new NotImplementedException();
        }
    }
}
