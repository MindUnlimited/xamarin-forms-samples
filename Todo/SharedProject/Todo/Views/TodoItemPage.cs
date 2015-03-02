﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;
using Todo.Views;
using Todo.Views.Controls;

namespace Todo
{
	public class TodoItemPage : ContentPage
	{
		public TodoItemPage ()
		{
            List<Group> availableGroups = new List<Group>();
            //Group defGroup = new Group();
            //Task.Run(async () => { defGroup = await Todo.App.Database.getDefaultGroup(Todo.App.Database.userID); });
            //Task.Run(async () => { availableGroups = await Todo.App.Database.getGroups(Todo.App.Database.userID); }); //task.run part is necessary, behaves as await

            if (Todo.App.Database.userID != null)
            {
                var _groups = Todo.App.Database.getGroups(Todo.App.Database.userID);
                availableGroups.AddRange(_groups.Result);
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

            BindablePicker ownedPicker = new BindablePicker
            {
                Title = "Owned By",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            foreach (Group group in availableGroups)
            {
                ownedPicker.Items.Add(group.ID);
            }

            ownedPicker.SetBinding(BindablePicker.SelectedItemProperty, "OwnedBy", BindingMode.TwoWay);          
            //picker.SetBinding((BindableProperty)(picker.SelectedItem), "OwnedBy");

            //BindableProperty bind = BindablePicker.SelectedItemProperty;

            //var pickerLabel = new Label { Text = "OwnedBy" };
            //this.SetBinding(picker., "OwnedBy");
            

            //var tmp = picker.SelectedItemProperty;
            //picker.SetBinding(picker.ItemsSourceProperty, "OwnedBy");

            //picker.SelectedIndexChanged += (sender, args) =>
            //{
                
            //    //if (picker.SelectedIndex == -1)
            //    //{
            //    //    boxView.Color = Color.Default;
            //    //}
            //    //else
            //    //{
            //    //    string colorName = picker.Items[picker.SelectedIndex];
            //    //    boxView.Color = nameToColor[colorName];
            //    //}
            //};

            var typeLabel = new Label { Text = "Type" };
            var typeEntry = new Entry();

            typeEntry.SetBinding(Entry.TextProperty, "Type");

            var createdLabel = new Label { Text = "Created By" };
            var createdEntry = new Entry();

            createdEntry.SetBinding(Entry.TextProperty, "CreatedBy");

            //var doneLabel = new Label { Text = "Complete" };
            //var doneEntry = new Xamarin.Forms.Switch ();
            //doneEntry.SetBinding (Xamarin.Forms.Switch.IsToggledProperty, "Complete");

			var saveButton = new Button { Text = "Save" };
			saveButton.Clicked += async (sender, e) => {
				var Item = (Item)BindingContext;
				await App.Database.SaveItem(Item);
				await this.Navigation.PopAsync();
			};

			var deleteButton = new Button { Text = "Delete" };
			deleteButton.Clicked += async (sender, e) => {
				var Item = (Item)BindingContext;
				await App.Database.DeleteItem(Item);
                await this.Navigation.PopAsync();
			};
							
			var cancelButton = new Button { Text = "Cancel" };
			cancelButton.Clicked += (sender, e) => {
				var Item = (Item)BindingContext;
                this.Navigation.PopAsync();
			};


			var speakButton = new Button { Text = "Speak" };
			speakButton.Clicked += (sender, e) => {
				var Item = (Item)BindingContext;
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
                                typeLabel, typeEntry,
                                createdLabel, createdEntry
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