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
			saveButton.Clicked += (sender, e) => {
				var todoItem = (TodoItem)BindingContext;
				App.Database.SaveItem(todoItem);
				this.Navigation.PopAsync();
			};

			var deleteButton = new Button { Text = "Delete" };
			deleteButton.Clicked += (sender, e) => {
				var todoItem = (TodoItem)BindingContext;
				App.Database.DeleteItem(todoItem.Id);
                this.Navigation.PopAsync();
			};
							
			var cancelButton = new Button { Text = "Cancel" };
			cancelButton.Clicked += (sender, e) => {
				var todoItem = (TodoItem)BindingContext;
                this.Navigation.PopAsync();
			};


			var speakButton = new Button { Text = "Speak" };
			speakButton.Clicked += (sender, e) => {
				var todoItem = (TodoItem)BindingContext;
				DependencyService.Get<ITextToSpeech>().Speak(todoItem.Text + " " + todoItem.Id);
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