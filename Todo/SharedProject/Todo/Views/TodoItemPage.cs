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
//using SolTech.Forms;

namespace Todo
{
	public class TodoItemPage : ContentPage
	{
		public TodoItemPage ()
		{
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





            var ownedLabel = new Label { Text = "Owned By" };





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




            //Style pickerStyle = new Style(ownedPicker.GetType());
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


            BindablePicker ownedPicker = new BindablePicker
            {
                Title = "Owned By",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            Dictionary<string, string> groups = new Dictionary<string, string>();
            foreach (Group group in availableGroups)
            {
                ownedPicker.Items.Add(group.Name);
                ownedPicker.Items.Add(group.ID);
                groups[group.Name] = group.ID;
            }
            //ownedPicker.ItemsSource = groupNameIDIENumerable;
            //ownedPicker2.ItemsSource = groupNameIDList;

            ownedPicker.SetBinding(BindablePicker.SelectedItemProperty, "OwnedBy", BindingMode.TwoWay);



            var typeLabel = new Label { Text = "Type" };
            var typeEntry = new Entry();

            typeEntry.SetBinding(Entry.TextProperty, "Type");

            //var createdLabel = new Label { Text = "Created By" };
            //var createdEntry = new Entry();

            //createdEntry.SetBinding(Entry.TextProperty, "CreatedBy");

            //var doneLabel = new Label { Text = "Complete" };
            //var doneEntry = new Xamarin.Forms.Switch ();
            //doneEntry.SetBinding (Xamarin.Forms.Switch.IsToggledProperty, "Complete");

			var saveButton = new Button { Text = "Save" };
			saveButton.Clicked += async (sender, e) => {
                var selected = ownedPicker.SelectedItem;
				Item Item = (Item)BindingContext;
                if (Item.OwnedBy != null)
                {
                    if (!groups.ContainsValue(Item.OwnedBy))
                        Item.OwnedBy = groups[Item.OwnedBy];
                    //ownedPicker.OnSelectedItemChanged(BoundPicker.SelectedItemProperty, Item.OwnedBy, Item.OwnedBy);
                    //var old_value = Item.OwnedBy;
                        
                    //ownedPicker.SelectedItem = 
                    //var ownedByGroup = await Todo.App.Database.getGroup(Item.OwnedBy);
                    //Item.OwnedBy = ownedByGroup.ID;
                    await App.Database.SaveItem(Item);
                    //Item.OwnedBy = old_value;

                    await this.Navigation.PopAsync();
                }
			};

			var deleteButton = new Button { Text = "Delete" };
			deleteButton.Clicked += async (sender, e) => {
				Item Item = (Item)BindingContext;
				await App.Database.DeleteItem(Item);
                await this.Navigation.PopAsync();
			};
							
			var cancelButton = new Button { Text = "Cancel" };
			cancelButton.Clicked += (sender, e) => {
				Item Item = (Item)BindingContext;
                this.Navigation.PopAsync();
			};


			var speakButton = new Button { Text = "Speak" };
			speakButton.Clicked += (sender, e) => {
				Item Item = (Item)BindingContext;
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
                                typeLabel, typeEntry
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