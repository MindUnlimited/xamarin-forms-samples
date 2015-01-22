using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;

namespace Todo
{
	public class TodoItemPage : ContentPage
	{
		public TodoItemPage ()
		{
			this.SetBinding (ContentPage.TitleProperty, "Text");

			NavigationPage.SetHasNavigationBar (this, true);
			var nameLabel = new Label { Text = "Text" };
			var nameEntry = new Entry ();
			
			nameEntry.SetBinding (Entry.TextProperty, "Text");

            //var notesLabel = new Label { Text = "Notes" };
            //var notesEntry = new Entry ();
            //notesEntry.SetBinding (Entry.TextProperty, "Notes");

			var doneLabel = new Label { Text = "Complete" };
			var doneEntry = new Xamarin.Forms.Switch ();
			doneEntry.SetBinding (Xamarin.Forms.Switch.IsToggledProperty, "Complete");

			var saveButton = new Button { Text = "Save" };
			saveButton.Clicked += async (sender, e) => {
				var todoItem = (TodoItem)BindingContext;
				await App.Database.SaveItem(todoItem);
				await this.Navigation.PopAsync();
			};

			var deleteButton = new Button { Text = "Delete" };
			deleteButton.Clicked += async (sender, e) => {
				var todoItem = (TodoItem)BindingContext;
				await App.Database.DeleteItem(todoItem.Id);
                await this.Navigation.PopAsync();
			};
							
			var cancelButton = new Button { Text = "Cancel" };
			cancelButton.Clicked += (sender, e) => {
				var todoItem = (TodoItem)BindingContext;
                this.Navigation.PopAsync();
			};


			var speakButton = new Button { Text = "Speak" };
			speakButton.Clicked += (sender, e) => {
				var todoItem = (TodoItem)BindingContext;
                string spokenText;
                if (todoItem.Complete)
                    spokenText = todoItem.Text + ", dit item is voltooid.";
                else
                    spokenText = todoItem.Text + ", dit item is nog niet klaar";
				DependencyService.Get<ITextToSpeech>().Speak(spokenText);
			};

			Content = new StackLayout {
				VerticalOptions = LayoutOptions.StartAndExpand,
				Padding = new Thickness(20),
				Children = {
					nameLabel, nameEntry, 
					//notesLabel, notesEntry,
					doneLabel, doneEntry,
					saveButton, deleteButton, cancelButton,
					speakButton
				}
			};
		}
	}
}