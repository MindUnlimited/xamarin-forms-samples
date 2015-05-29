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
		public TodoItemPage ()
		{
            Item Item = null;
            ObservableCollection<Item> availableItems = new ObservableCollection<Item>();
            List<Group> availableGroups = new List<Group>();

            Group defGroup;
            //Task.Run(async () => { defGroup = await Todo.App.Database.getDefaultGroup(Todo.App.Database.userID); });
            //Task.Run(async () => { availableGroups = await Todo.App.Database.getGroups(Todo.App.Database.userID); }); //task.run part is necessary, behaves as await

            if (Todo.App.Database.userID != null)
            {
                var _groups = Todo.App.Database.getGroups(Todo.App.Database.userID);
                availableGroups.AddRange(_groups.Result);

                var _defGroup = Todo.App.Database.getDefaultGroup(Todo.App.Database.userID);
                defGroup = _defGroup.Result;

                var test1 = Todo.App.selectedDomainPage.selectedDomain;
                var keys = Todo.App.selectedDomainPage.viewModels.Keys;
                var test = Todo.App.selectedDomainPage.viewModels[Todo.App.selectedDomainPage.selectedDomain].Reports;
                availableItems = Todo.App.selectedDomainPage.viewModels[Todo.App.selectedDomainPage.selectedDomain].Reports;
            }


			this.SetBinding (ContentPage.TitleProperty, "Name");

			NavigationPage.SetHasNavigationBar (this, true);
			var nameLabel = new Label { Text = "Name" };
			var nameEntry = new Entry ();
			
			nameEntry.SetBinding (Entry.TextProperty, "Name");

            //var notesLabel = new Label { Text = "Notes" };
            //var notesEntry = new Entry ();
            //notesEntry.SetBinding (Entry.TextProperty, "Notes");

            var statusLabel = new Label { Text = "Status" };
            var statusEntry = new Entry();

            statusEntry.SetBinding(Entry.TextProperty, "Status");

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
                //ownedPicker.Items.Add(group.ID);
                groups[group.Name] = group.ID;
            }
            //ownedPicker.ItemsSource = groupNameIDIENumerable;
            //ownedPicker2.ItemsSource = groupNameIDList;

            ownedPicker.SetBinding(BindablePicker.SelectedItemProperty, "OwnedBy", BindingMode.TwoWay);



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
            //    Item = (Item)BindingContext;

            //    parentPicker.Items.Clear();
            //    items.Clear();

            //    foreach (Item item in availableItems)
            //    {
            //        if (Item != null && item.ID != Item.ID)
            //        {
            //            parentPicker.Items.Add(item.Name);
            //            items[item.Name] = item.ID;
            //        }
            //    }
            //});

            foreach (Item item in availableItems)
            {
                parentPicker.Items.Add(item.Name);
                items[item.Name] = item.ID;
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

			var saveButton = new Button { Text = "Save" };
			saveButton.Clicked += async (sender, e) => {
                var selected = ownedPicker.SelectedItem;
				Item = (Item)BindingContext;
                Boolean itemIsNew = Item.Version == null;
                if (Item.OwnedBy != null)
                {
                    if (!groups.ContainsValue(Item.OwnedBy) && groups.ContainsKey(Item.OwnedBy))
                        Item.OwnedBy = groups[Item.OwnedBy];

                    if (Item.Parent != null)
                    {
                        if (!items.ContainsValue(Item.Parent) && items.ContainsKey(Item.Parent))
                            Item.Parent = items[Item.Parent];
                    }

                    var parentItem = await Todo.App.Database.GetItem(Item.Parent);
                    Item.Type = parentItem.Type + 1;

                    //ownedPicker.OnSelectedItemChanged(BoundPicker.SelectedItemProperty, Item.OwnedBy, Item.OwnedBy);
                    //var old_value = Item.OwnedBy;
                        
                    //ownedPicker.SelectedItem = 
                    //var ownedByGroup = await Todo.App.Database.getGroup(Item.OwnedBy);
                    //Item.OwnedBy = ownedByGroup.ID;


                    var prevItem = Item;
                    await App.Database.SaveItem(Item); // add to DB
                    Item = await App.Database.GetItem(Item.ID); // DB added (new) version so need to change the Item
                    //Item.OwnedBy = old_value;

                    

                    var domains = Todo.App.selectedDomainPage.domains;

                    string domainName = null;
                    Item selectedDomain = null;

                    foreach (var dom in domains)
                    {
                        if (dom.ID == Item.Parent)
                        {
                            domainName = dom.Name;
                            selectedDomain = dom;
                            break;
                        }
                    }

                    if (selectedDomain != null)
                    {
                        if (itemIsNew) // new item
                        {
                            Todo.App.selectedDomainPage.viewModels[selectedDomain].Reports.Add(Item);
                            Todo.App.selectedDomainPage.viewModels[selectedDomain].FilterAndSort(Todo.App.selectedDomainPage.domainPageType);
                        }
                        else // update item
                        {
                            var observableCollectionItem = Todo.App.selectedDomainPage.viewModels[selectedDomain].Reports.FirstOrDefault(i => i.ID == Item.ID);
                            if (observableCollectionItem != null)
                            {
                                observableCollectionItem = Item;
                            }

                            //Todo.App.selectedDomainPage.viewModels[selectedDomain].Reports.Remove(prevItem);
                            //Todo.App.selectedDomainPage.viewModels[selectedDomain].Reports.Add(Item);

                        }
                    }
                    await this.Navigation.PopAsync();
                }
			};

			var deleteButton = new Button { Text = "Delete" };
			deleteButton.Clicked += async (sender, e) => {
				Item = (Item)BindingContext;
                Item selectedDomain = Todo.App.selectedDomainPage.selectedDomain;
                int numberOfDirectChildren = Todo.App.selectedDomainPage.viewModels[selectedDomain].Reports.Where(x => x.Parent == Item.ID).Count();

                if (numberOfDirectChildren > 0) // still items under this item!
                {
                    Debug.WriteLine("this item still has children!");
                    var answer = await DisplayAlert ("This item has links to other items", "Are you sure you want to delete this item AND the items linking to it?", "Yes", "No");
                    Debug.WriteLine("Answer: " + answer); // writes true or false to the console
                }
                else
                {
                    await App.Database.DeleteItem(Item);

                    var domains = Todo.App.selectedDomainPage.domains;

                    string domainName = null;

                    foreach (var dom in domains)
                    {
                        if (dom.ID == Item.Parent)
                        {
                            domainName = dom.Name;
                            selectedDomain = dom;
                            break;
                        }
                    }

                    if (domainName != null)
                    {
                        Todo.App.selectedDomainPage.viewModels[selectedDomain].Reports.Remove(Item);
                    }

                    await this.Navigation.PopAsync();
                }


			};
							
			var cancelButton = new Button { Text = "Cancel" };
			cancelButton.Clicked += async (sender, e) => {
				Item = (Item)BindingContext;
                await this.Navigation.PopAsync();
			};


			var speakButton = new Button { Text = "Speak" };
			speakButton.Clicked += (sender, e) => {
				Item = (Item)BindingContext;
                string spokenText;
                if (Item.Status == 7)
                    spokenText = Item.Name + ", this item is completed";
                else
                    spokenText = Item.Name + ", this item is not yet completed";
				DependencyService.Get<ITextToSpeech>().Speak(spokenText);
			};

            var scrollview = new ScrollView
            {
                Content = new StackLayout
                {
                    Padding = new Thickness(20),
                },
            };

			Content = new StackLayout {
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
					            statusLabel, statusEntry,
                                //ownedLabel, ownedEntry,
                                ownedLabel, ownedPicker,
                                parentLabel, parentPicker
				            }
                        }

                    },

                    new StackLayout
                    {
                        Children = {saveButton, deleteButton, cancelButton, speakButton}
                    }
                }
			};
		}
	}
}