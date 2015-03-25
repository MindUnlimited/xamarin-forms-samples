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

namespace Todo
{
    public class TodoGroupPage : ContentPage
    {
        public TodoGroupPage()
        {
            List<Group> availableGroups = new List<Group>();
            var _groups = Todo.App.Database.getGroups(Todo.App.Database.userID);
            availableGroups.AddRange(_groups.Result);

            var parentLabel = new Label { Text = "Parent of Group" };
            BoundPicker parentPicker = new BoundPicker
            {
                Title = "Parent of Group",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            List<PickerItem<Group>> groupNameIDList = new List<PickerItem<Group>>();
            Dictionary<string, string> groups = new Dictionary<string, string>();

            foreach (Group group in availableGroups)
            {
                PickerItem<Group> it = new PickerItem<Group>(group.Name, group);
                groupNameIDList.Add(it);

                groups[group.Name] = group.ID;
            }

            //IEnumerable<PickerItem<String>> groupNameIDIENumerable = (IEnumerable<PickerItem<String>>)groupNameIDList;
            parentPicker.ItemsSource = groupNameIDList;





            var userLabel = new Label { Text = "Add users" };
            BoundPicker userPicker = new BoundPicker
            {
                Title = "Parent of Group",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            List<PickerItem<User>> userNameUserList = new List<PickerItem<User>>();
            Dictionary<string, User> users = new Dictionary<string, User>();

            foreach (User user in Todo.App.Database.contacts)
            {
                PickerItem<User> us = new PickerItem<User>(user.Name, user);
                userNameUserList.Add(us);

                users[user.Name] = user;
            }

            //IEnumerable<PickerItem<String>> groupNameIDIENumerable = (IEnumerable<PickerItem<String>>)groupNameIDList;
            userPicker.ItemsSource = userNameUserList;




            this.SetBinding(ContentPage.TitleProperty, "Name");

            NavigationPage.SetHasNavigationBar(this, true);
            var nameLabel = new Label { Text = "Name" };
            var nameEntry = new Entry();

            nameEntry.SetBinding(Entry.TextProperty, "Name");


            var coachLabel = new Label { Text = "is Coach" };
            var coachEntry = new Entry();

            coachEntry.SetBinding(Entry.TextProperty, "isCoach");


            var saveButton = new Button { Text = "Save" };
            saveButton.Clicked += async (sender, e) =>
            {
                PickerItem<Group> parentGroup = (PickerItem<Group>) parentPicker.SelectedItem;
                string parentID = parentGroup.Item.ID;
                GroupGroupMembership ggm = new GroupGroupMembership();
                ggm.MemberID = parentID;

                var group = (Group)BindingContext;
                //await App.Database.SaveItem(group, ggm);

                User pickedUser = ((PickerItem<User>) userPicker.SelectedItem).Item;

                List<User> pickerUserList = new List<User>();
                pickerUserList.Add(pickedUser);

                await App.Database.SaveItem(group, pickerUserList, ggm);

                //if (Todo.App.Database.existingUser(pickedUser) != null)
                //{
                //    Group userDefGroup = await Todo.App.Database.getDefaultGroup(pickedUser.ID);
                //    GroupGroupMembership userGGM = new GroupGroupMembership();

                //    userGGM.MemberID = userDefGroup.ID;
                //    userGGM.MembershipID = group.ID;
                //}




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
                                parentLabel, parentPicker,
                                userLabel, userPicker,
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