using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;
using Todo.Views;
using Todo.Views.Controls;
using SolTech.Forms;
using System.Collections.ObjectModel;
//using SolTech.Forms;

namespace Todo
{
    public class TodoItemPage : ContentPage
    {
        public IEnumerable<PickerItem<string>> shareGroups {get; set; }

        public TodoItemPage()
        {
            Item item = null;
            ObservableCollection<Item> availableItems = new ObservableCollection<Item>();
            List<Group> availableGroups = new List<Group>();
            var selectedDomain = Todo.App.selectedDomainPage.selectedDomain;

            Group defGroup;
            //Task.Run(async () => { defGroup = await Todo.App.Database.getDefaultGroup(Todo.App.Database.userID); });
            //Task.Run(async () => { availableGroups = await Todo.App.Database.getGroups(Todo.App.Database.userID); }); //task.run part is necessary, behaves as await

            if (Todo.App.Database.userID != null)
            {
                var _groups = Todo.App.Database.getGroups(Todo.App.Database.userID);
                availableGroups.AddRange(_groups.Result);

                var _defGroup = Todo.App.Database.getDefaultGroup(Todo.App.Database.userID);
                defGroup = _defGroup.Result;


                
                //var keys = Todo.App.selectedDomainPage.viewModels.Keys;
                //var test = Todo.App.selectedDomainPage.viewModels[Todo.App.selectedDomainPage.selectedDomain].Reports;

                //if(selectedDomain != null)
                    availableItems = Todo.App.selectedDomainPage.viewModels[selectedDomain].Reports;
            }


            this.SetBinding(ContentPage.TitleProperty, "Name");

            NavigationPage.SetHasNavigationBar(this, true);
            var nameLabel = new Label { Text = "Name" };
            var nameEntry = new Entry();

            nameEntry.SetBinding(Entry.TextProperty, "Name");

            //-1: Cancelled
            //0: Conceived
            //1: Planned
            //2: Initiated
            //3: <25% completed
            //4: <50%
            //5: <75%
            //6: On hold / Blocked
            //7: Completed


            var statusLabel = new Label { Text = "Status" };

            var slider = new ExtendedSlider
            {
                Minimum = -1,
                Maximum = 7,
                StepValue = 1,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            slider.SetBinding(Slider.ValueProperty, "Status");

            Button share = new Button { Text = "Share!" };
            share.Clicked += async (o, e) =>
            {
                SelectContactsView selectContacts = new SelectContactsView();
                selectContacts.HorizontalOptions = LayoutOptions.FillAndExpand;
                selectContacts.VerticalOptions = LayoutOptions.FillAndExpand;
                selectContacts.ContactsCollection = Todo.App.Database.contacts;

                ToolbarItem SelectContactsButton = new ToolbarItem("Select", "CheckWeiss.png", async () => { }, 0, 0);

                ContentPage SelectContactsPage = new ContentPage { Content = selectContacts };
                SelectContactsPage.ToolbarItems.Add(SelectContactsButton);

                await Navigation.PushAsync(SelectContactsPage);
            };

            //var labelSlideValue = new Label
            //{
            //    HorizontalOptions = LayoutOptions.CenterAndExpand,
            //    BindingContext = slider,
            //};

            //labelSlideValue.SetBinding(Label.TextProperty,
            //                                new Binding("Value", BindingMode.OneWay,
            //                                    null, null, "Current Value: {0}"));

            int imgSize = Device.OnPlatform(iOS: 25, Android: 15, WinPhone: 25);

            Image sliderImage = new Image { BindingContext = slider, HeightRequest = imgSize, WidthRequest = imgSize };
            sliderImage.SetBinding(Image.SourceProperty,
                                        new Binding("Value", BindingMode.OneWay, new StatusToImageSourceConverter()));

            Label sliderName = new Label { BindingContext = slider, HorizontalOptions = LayoutOptions.Center };
            sliderName.SetBinding(Label.TextProperty,
                                        new Binding("Value", BindingMode.OneWay, new StatusToStringConverter()));

            var sliderImageAndText = new StackLayout { Orientation = StackOrientation.Vertical, Children = { sliderImage, sliderName } };

            var sliderRow = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = { sliderImageAndText, slider }
            };

            Image imgCancelled = new Image
            {
                Source = Device.OnPlatform(
                    iOS: ImageSource.FromFile("Images/TaskCancelled64.png"),
                    Android: ImageSource.FromFile("TaskCancelled64.png"),
                    WinPhone: ImageSource.FromFile("Assets/ItemIcons/TaskCancelled64.png")),
                HeightRequest = imgSize,
                WidthRequest = imgSize
            };

            Image imgConceived = new Image
            {
                Source = Device.OnPlatform(
                    iOS: ImageSource.FromFile("Images/TaskConceived64.png"),
                    Android: ImageSource.FromFile("TaskConceived64.png"),
                    WinPhone: ImageSource.FromFile("Assets/ItemIcons/TaskConceived64.png")),
                HeightRequest = imgSize,
                WidthRequest = imgSize
            };



            StackLayout sliderImages = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = { imgCancelled, imgConceived, imgConceived, imgConceived, imgConceived, imgConceived, imgConceived, imgConceived, imgConceived },
                HorizontalOptions = LayoutOptions.Fill
            };



            //var statusEntry = new Entry();

            //statusEntry.SetBinding(Entry.TextProperty, "Status");

            //var ownedLabel = new Label { Text = "Owned By" };
            //var ownedEntry = new Entry();

            //ownedEntry.SetBinding(Entry.TextProperty, "OwnedBy");












            //BoundPicker ownedPicker = new BoundPicker
            //{
            //    Title = "Owned By",
            //    VerticalOptions = LayoutOptions.CenterAndExpand
            //};

            //List<PickerItem<String>> groupNameIDList = new List<PickerItem<String>>();

            //Dictionary<string, String> groups = new Dictionary<string, String>();

            //foreach (Group group in availableGroups)
            //{
            //    PickerItem<String> it = new PickerItem<String>(group.Name, group.ID);
            //    groupNameIDList.Add(it);

            //    groups[group.Name] = group.ID;
            //}

            //var groupNameIDIENumerable = (IEnumerable<PickerItem<String>>)groupNameIDList;

            //ownedPicker.ItemsSource = groupNameIDIENumerable;
            //ownedPicker.SetBinding(BoundPicker.SelectedItemProperty, "OwnedBy", BindingMode.TwoWay);


            //var pickerStyle = new Style (typeof(Picker)) 
            //{
            //    Setters =   {
            //                    new Setter {Property = Picker.OpacityProperty, Value = .5},
            //                    new Setter { Property = Picker.TitleProperty, Value = "test"}
            //                }
            //};

            //pickerStyle.Setters.

            //ownedPicker.Style

            //<Style.Triggers>
            //    <DataTrigger Binding="{Binding IsChecked,ElementName=chk}" Value="True">
            //        <Setter Property="ItemTemplate">
            //            <Setter.Value>
            //                <DataTemplate>
            //                    <StackPanel Orientation="Horizontal">
            //                        <TextBlock Text="{Binding ID}" />
            //                        <TextBlock Text=": " />
            //                        <TextBlock Text="{Binding Name}" />
            //                    </StackPanel>
            //                </DataTemplate>
            //            </Setter.Value>
            //        </Setter>
            //    </DataTrigger>
            //</Style.Triggers>

            var ownedLabel = new Label { Text = "Shared With" };

            BindablePicker ownedPicker = new BindablePicker
            {
                Title = "Shared With",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };



            Dictionary<string, string> groups = new Dictionary<string, string>();
            foreach (Group group in availableGroups)
            {
                ownedPicker.Items.Add(group.Name);
                ownedPicker.Items.Add(group.ID);
                groups[group.Name] = group.ID;
            }
            ////ownedPicker.ItemsSource = groupNameIDIENumerable;
            ////ownedPicker2.ItemsSource = groupNameIDList;

            ownedPicker.SetBinding(BindablePicker.SelectedItemProperty, "OwnedBy", BindingMode.TwoWay);

            ObservableCollection<PickerItem<string>> collectionPickerItems = new ObservableCollection<PickerItem<string>>();
            foreach (Group grp in availableGroups)
            {
                PickerItem<string> pickerItem = new PickerItem<string>(grp.Name, grp.ID);
                collectionPickerItems.Add(pickerItem);
            }
            shareGroups = collectionPickerItems;

            //BoundPicker ownedPicker = new BoundPicker
            //{
            //    Title = "Share with",
            //    VerticalOptions = LayoutOptions.CenterAndExpand,
            //    BindingContext = this
            //};
            //ownedPicker.SetBinding(BoundPicker.ItemsSourceProperty, "shareGroups", BindingMode.TwoWay);
            //ownedPicker.SetBinding(BoundPicker.SelectedItemProperty, "OwnedBy", BindingMode.TwoWay);


            var parentLabel = new Label { Text = "Parent" };

            BindablePicker parentPicker = new BindablePicker
            {
                Title = "Parent",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };



            Dictionary<string, string> items = new Dictionary<string, string>();

            items[Todo.App.selectedDomainPage.selectedDomain.Name] = Todo.App.selectedDomainPage.selectedDomain.ID;
            parentPicker.Items.Add(Todo.App.selectedDomainPage.selectedDomain.Name);


            //this.BindingContextChanged += ((o, e) => {
            //    item = (Item)BindingContext;

            //    parentPicker.Items.Clear();
            //    items.Clear();

            //    foreach (Item item in availableItems)
            //    {
            //        if (item != null && item.ID != item.ID)
            //        {
            //            parentPicker.Items.Add(item.Name);
            //            items[item.Name] = item.ID;
            //        }
            //    }
            //});

            foreach (Item it in availableItems)
            {
                parentPicker.Items.Add(it.Name);
                items[it.Name] = it.ID;
            }


            //ownedPicker.ItemsSource = groupNameIDIENumerable;
            //ownedPicker2.ItemsSource = groupNameIDList;

            parentPicker.SetBinding(BindablePicker.SelectedItemProperty, "Parent", BindingMode.TwoWay);

            //var createdLabel = new Label { Text = "Created By" };
            //var createdEntry = new Entry();

            //createdEntry.SetBinding(Entry.TextProperty, "CreatedBy");

            //var doneLabel = new Label { Text = "Complete" };
            //var doneEntry = new Xamarin.Forms.Switch ();
            //doneEntry.SetBinding (Xamarin.Forms.Switch.IsToggledProperty, "Complete");



            BindablePicker parentPickerWConverter = new BindablePicker
            {
                Title = "Parent",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            //parentPickerWConverter.BindingContext = Todo.App.selectedDomainPage.viewModels[selectedDomain];
            //parentPickerWConverter.SetBinding(BindablePicker.ItemsSourceProperty, "Reports", BindingMode.TwoWay, new ItemToIDConverter());


            var scrollview = new ScrollView
            {
                Content = new StackLayout
                {
                    Padding = new Thickness(20),
                },
            };

            ToolbarItem save = new ToolbarItem("Save", "save.png", async () =>
            {
            //var selected = ownedPicker.SelectedItem;
            item = (Item)BindingContext;
            Boolean itemIsNew = item.Version == null;


            if (item.OwnedBy != null)
            {
                if (!groups.ContainsValue(item.OwnedBy) && groups.ContainsKey(item.OwnedBy))
                    item.OwnedBy = groups[item.OwnedBy];

                if (item.Parent != null)
                {
                    if (!items.ContainsValue(item.Parent) && items.ContainsKey(item.Parent))
                    {
                            //Debug.WriteLine(item.Parent);
                            //Debug.WriteLine(items[item.Parent]);
                        string test;
                        if(items.TryGetValue(item.Parent, out test))
                            {
                                item.Parent = test;
                            }
                            //item.Parent = items[item.Parent];
                            //Debug.WriteLine(item.Parent);
                    }
                            
                        
                    }

                    var parentItem = await Todo.App.Database.GetItem(item.Parent);
                    if (parentItem != null)
                    {
                        item.Type = parentItem.Type + 1;
                    }
                    else
                        item.Type = 2;

                    //ownedPicker.OnSelectedItemChanged(BoundPicker.SelectedItemProperty, item.OwnedBy, item.OwnedBy);
                    //var old_value = item.OwnedBy;

                    //ownedPicker.SelectedItem = 
                    //var ownedByGroup = await Todo.App.Database.getGroup(item.OwnedBy);
                    //item.OwnedBy = ownedByGroup.ID;


                    var prevItem = item;
                    await App.Database.SaveItem(item); // add to DB
                    item = await App.Database.GetItem(item.ID); // DB added (new) version so need to change the item
                                                                //item.OwnedBy = old_value;



                    var domains = Todo.App.selectedDomainPage.domains;

                    //Debug.WriteLine(Todo.App.selectedDomainPage.selectedDomain);

                    //foreach (var dom in domains)
                    //{
                    //    if (dom.ID == item.Parent)
                    //    {
                    //        domainName = dom.Name;
                    //        selectedDomain = dom;
                    //        break;
                    //    }
                    //}

                    if (selectedDomain != null)
                    {
                        if (itemIsNew) // new item
                        {
                            Todo.App.selectedDomainPage.viewModels[selectedDomain].Reports.Add(item);
                            Todo.App.selectedDomainPage.viewModels[selectedDomain].FilterAndSort(Todo.App.selectedDomainPage.domainPageType);
                        }
                        else // update item
                        {
                            var observableCollectionItem = Todo.App.selectedDomainPage.viewModels[selectedDomain].Reports.FirstOrDefault(i => i.ID == item.ID);
                            if (observableCollectionItem != null)
                            {
                                observableCollectionItem = item;
                            }

                            //Todo.App.selectedDomainPage.viewModels[selectedDomain].Reports.Remove(prevItem);
                            //Todo.App.selectedDomainPage.viewModels[selectedDomain].Reports.Add(item);

                        }
                    }
                    await this.Navigation.PopAsync();
                }
            }, 0, 0);

            ToolbarItem delete = new ToolbarItem("Delete", "delete.png", async () =>
            {
                item = (Item)BindingContext;
                //Item selectedDomain = Todo.App.selectedDomainPage.selectedDomain;
                int numberOfDirectChildren = Todo.App.selectedDomainPage.viewModels[selectedDomain].Reports.Where(x => x.Parent == item.ID).Count();
                if (item.ID == null) // item was new, so nothing to do except cancel the adding of a new item
                    await this.Navigation.PopAsync();
                else if (numberOfDirectChildren > 0) // still items under this item!
                {
                    Debug.WriteLine("this item still has children!");
                    var answer = await DisplayAlert("This item has links to other items", "Are you sure you want to delete this item AND the items linking to it?", "Yes", "No");
                    Debug.WriteLine("Answer: " + answer); // writes true or false to the console

                    if(answer)
                    {
                        //await App.Database.DeleteItem(item);

                        //if (selectedDomain != null)
                        //{
                        //    Todo.App.selectedDomainPage.viewModels[selectedDomain].Reports.Remove(item);
                        //}

                        //await this.Navigation.PopAsync();
                    }
                }
                else
                {
                    await App.Database.DeleteItem(item);

                    var domains = Todo.App.selectedDomainPage.domains;

                    //string domainName = null;

                    //foreach (var dom in domains)
                    //{
                    //    if (dom.ID == item.Parent)
                    //    {
                    //        domainName = dom.Name;
                    //        selectedDomain = dom;
                    //        break;
                    //    }
                    //}

                    if (selectedDomain != null)
                    {
                        Todo.App.selectedDomainPage.viewModels[selectedDomain].Reports.Remove(item);
                    }

                    await this.Navigation.PopAsync();
                }


            }, 0, 0);


            ToolbarItem speak = new ToolbarItem("Speak", "speak.png", () =>
            {
                item = (Item)BindingContext;
                var statusStringConverter = new StatusToStringConverter();
                string spokenText = item.Name + "\n" + "the status is: " + statusStringConverter.Convert(item.Status, null, null, null);
                DependencyService.Get<ITextToSpeech>().Speak(spokenText);
            }, 0, 0);



            //ToolbarItems.Clear();
            //ToolbarItems.Add(save);

            this.Appearing += (o, e) =>
            {
                ToolbarItems.Clear();
                ToolbarItems.Add(save);
                ToolbarItems.Add(delete);
                ToolbarItems.Add(speak);
            };


            Content = new StackLayout
            {
                //VerticalOptions = LayoutOptions.StartAndExpand,
                Padding = new Thickness(10),

                Children =
                {
                    new ScrollView
                    {
				        //VerticalOptions = LayoutOptions.StartAndExpand,
				        //Padding = new Thickness(20),
				        Content = new StackLayout
                        {
                            VerticalOptions = LayoutOptions.End,

                            Children =
                            {
                                nameLabel, nameEntry, 
					            //notesLabel, notesEntry,
                                
					            statusLabel, //slider, sliderImages,
                                sliderImageAndText, slider,
                                //,

                                //ownedLabel, ownedEntry,
                                ownedLabel, ownedPicker,
                                parentLabel, parentPicker//,

                                //share
                            }
                        }

                    }

                    //new StackLayout
                    //{
                    //    Children = {saveButton, deleteButton, cancelButton, speakButton}, Orientation = StackOrientation.Horizontal, HorizontalOptions = LayoutOptions.StartAndExpand
                    //}
                }
            };
        }
    }
}