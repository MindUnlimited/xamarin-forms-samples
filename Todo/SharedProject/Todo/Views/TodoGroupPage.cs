using System;
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
    public class TodoGroupPage : ContentPage
    {
        public TodoGroupPage()
        {
            List<Group> availableGroups = new List<Group>();
            //Group defGroup = new Group();
            //Task.Run(async () => { defGroup = await Todo.App.Database.getDefaultGroup(Todo.App.Database.userID); });
            //Task.Run(async () => { availableGroups = await Todo.App.Database.getGroups(Todo.App.Database.userID); }); //task.run part is necessary, behaves as await

            var _groups = Todo.App.Database.getGroups(Todo.App.Database.userID);
            availableGroups.AddRange(_groups.Result);

            this.SetBinding(ContentPage.TitleProperty, "Name");

            NavigationPage.SetHasNavigationBar(this, true);
            var nameLabel = new Label { Text = "Name" };
            var nameEntry = new Entry();

            nameEntry.SetBinding(Entry.TextProperty, "Name");

            //var notesLabel = new Label { Text = "Notes" };
            //var notesEntry = new Entry ();
            //notesEntry.SetBinding (Entry.TextProperty, "Notes");

            var coachLabel = new Label { Text = "is Coach" };
            var coachEntry = new Entry();

            coachEntry.SetBinding(Entry.TextProperty, "isCoach");

            //var doneLabel = new Label { Text = "Complete" };
            //var doneEntry = new Xamarin.Forms.Switch ();
            //doneEntry.SetBinding (Xamarin.Forms.Switch.IsToggledProperty, "Complete");

            var saveButton = new Button { Text = "Save" };
            saveButton.Clicked += async (sender, e) =>
            {
                var Group = (Group)BindingContext;
                await App.Database.SaveItem(Group);
                await this.Navigation.PopAsync();
            };

            var deleteButton = new Button { Text = "Delete" };
            deleteButton.Clicked += async (sender, e) =>
            {
                var Group = (Group)BindingContext;
                await App.Database.DeleteItem(Group);
                await this.Navigation.PopAsync();
            };

            var cancelButton = new Button { Text = "Cancel" };
            cancelButton.Clicked += (sender, e) =>
            {
                var Group = (Group)BindingContext;
                this.Navigation.PopAsync();
            };


            var speakButton = new Button { Text = "Speak" };
            speakButton.Clicked += (sender, e) =>
            {
                var Group = (Group)BindingContext;
                string spokenText;
                if (Group.isCoach == true)
                    spokenText = Group.Name;// + ", this item is completed";
                else
                    spokenText = Group.Name;// +", this item is not yet completed";
                DependencyService.Get<ITextToSpeech>().Speak(spokenText);
            };

            var scrollview = new ScrollView
            {
                Content = new StackLayout
                {
                    Padding = new Thickness(20),
                },
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
					            coachLabel, coachEntry
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