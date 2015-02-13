using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SQLite;
using System.IO;

// Azure additions
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Xamarin.Forms;

namespace Todo
{
	public class TodoItemDatabase 
	{
        //Mobile Service Client reference
        public MobileServiceClient client;

        //Mobile Service sync table used to access data
        private IMobileServiceSyncTable<TodoItem> toDoTable;
        private IMobileServiceSyncTable<Item> itemTable;
        private IMobileServiceSyncTable<User> userTable;
        private IMobileServiceSyncTable<Group> groupTable;
        private IMobileServiceSyncTable<UserGroupMembership> userGroupMembershipTable;
        private IMobileServiceSyncTable<GroupGroupMembership> groupGroupMembershipTable;


        const string applicationURL = @"https://mindunlimited.azure-mobile.net/";
        const string applicationKey = @"RMFULNJBBVHwffaZeDYYhndAjEQzoT88";

        const string localDbFilename = "localstore.db";

		//static object locker = new object ();

		//SQLiteConnection database;

		string DatabasePath {
			get { 
				var sqliteFilename = "TodoSQLite.db3";
				#if __IOS__
				string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
				string libraryPath = Path.Combine (documentsPath, "..", "Library"); // Library folder
				var path = Path.Combine(libraryPath, sqliteFilename);
				#else
				#if __ANDROID__
				string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
				var path = Path.Combine(documentsPath, sqliteFilename);
				#else
				// WinPhone
				//var path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, sqliteFilename);;
			    var path = sqliteFilename;
				#endif
				#endif
				return path;
			}
		}


	    public TodoItemDatabase()
	    {
            try
            {
                //CurrentPlatform.Init();

                // Create the Mobile Service Client instance, using the provided
                // Mobile Service URL and key
                client = new MobileServiceClient(applicationURL, applicationKey);
                InitLocalStoreAsync();
                //Task.Run(async () => { await InitLocalStoreAsync(); }); //task.run part is necessary, behaves as await


                // Get the Mobile Service sync table instance to use
                toDoTable = client.GetSyncTable<TodoItem>();
                itemTable = client.GetSyncTable<Item>();
                userTable = client.GetSyncTable<User>();
                userGroupMembershipTable = client.GetSyncTable<UserGroupMembership>();
                groupTable = client.GetSyncTable<Group>();
                groupGroupMembershipTable = client.GetSyncTable<GroupGroupMembership>();

                //textNewToDo = FindViewById<EditText>(Resource.Id.textNewToDo);

                //// Create an adapter to bind the items with the view
                //adapter = new ToDoItemAdapter(this, Resource.Layout.Row_List_To_Do);
                //var listViewToDo = FindViewById<ListView>(Resource.Id.listViewToDo);
                //listViewToDo.Adapter = adapter;

                // Load the items from the Mobile Service
                OnRefreshItemsSelected();
            }
            //catch (Java.Net.MalformedURLException)
            //{
            //    CreateAndShowDialog(new Exception("There was an error creating the Mobile Service. Verify the URL"), "Error");
            //}
            catch (Exception e)
            {
                CreateAndShowDialog(e, "Error");
            }

            //database = new SQLiteConnection (DatabasePath);

            //// create the tables
            //database.CreateTable<TodoItem>();

            // Create the Mobile Service Client instance, using the provided
            // Mobile Service URL and key
            
	    }

        private async Task InitLocalStoreAsync()
        {
            // new code to initialize the SQLite store
            string path = DatabasePath;

            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
            }

            var store = new MobileServiceSQLiteStore(path);
            store.DefineTable<TodoItem>();
            store.DefineTable<Item>();
            store.DefineTable<User>();
            store.DefineTable<Group>();
            store.DefineTable<UserGroupMembership>();
            store.DefineTable<GroupGroupMembership>();


            // Uses the default conflict handler, which fails on conflict
            // To use a different conflict handler, pass a parameter to InitializeAsync. For more details, see http://go.microsoft.com/fwlink/?LinkId=521416
            await client.SyncContext.InitializeAsync(store);
        }

        //private async Task SyncAsync()
        //{
        //    await client.SyncContext.PushAsync();
        //    // All items should be synced since other clients might mark an item as complete
        //    // The first parameter is a query ID that uniquely identifies the query.
        //    // This is used in incremental sync to get only newer items the next time PullAsync is called
        //    await toDoTable.PullAsync("allTodoItems", toDoTable.CreateQuery()); // query ID is used for incremental sync
        //}

        private async Task SyncAsync()
        {
            String errorString = null;

            try
            {
                await client.SyncContext.PushAsync();
                await toDoTable.PullAsync("allTodoItems", toDoTable.CreateQuery()); // first param is query ID, used for incremental sync
            }

            catch (MobileServicePushFailedException ex)
            {
                errorString = "Push failed because of sync errors: " +
                  ex.PushResult.Errors.Count + " errors, message: " + ex.Message;
            }
            catch (Exception ex)
            {
                errorString = "Pull failed: " + ex.Message +
                  "\n\nIf you are still in an offline scenario, " +
                  "you can try your Pull again when connected with your Mobile Serice.";
            }

            if (errorString != null)
            {
                //MessageDialog d = new MessageDialog(errorString);
                //await d.ShowAsync();
            }
        }

        // Called when the refresh menu option is selected
        private async void OnRefreshItemsSelected()
        {
            await SyncAsync(); // get changes from the mobile service
            await RefreshItemsFromTableAsync(); // refresh view using local database
        }

        //Refresh the list with the items in the local database
        private async Task RefreshItemsFromTableAsync()
        {
            try
            {
                // Get the items that weren't marked as completed and add them in the adapter
                var list = await toDoTable.Where(item => item.Complete == false).ToListAsync();



                //adapter.Clear();

                //foreach (ToDoItem current in list)
                //    adapter.Add(current);

            }
            catch (Exception e)
            {
                CreateAndShowDialog(e, "Error");
            }
        }

        public async Task CheckItem(TodoItem item)
        {
            if (client == null)
            {
                return;
            }

            // Set the item as completed and update it in the table
            item.Complete = true;
            try
            {
                await toDoTable.UpdateAsync(item); // update the new item in the local database
                await SyncAsync(); // send changes to the mobile service

                //if (item.Done)
                //    adapter.Remove(item);

            }
            catch (Exception e)
            {
                CreateAndShowDialog(e, "Error");
            }
        }



        public async void AddItem(View view)
        {
            if (client == null)// || string.IsNullOrWhiteSpace(textNewToDo.Text))
            {
                return;
            }

            // Create a new item
            var item = new TodoItem
            {
                
                //Name = textNewToDo.Text,
                Complete = false
            };

            try
            {
                await toDoTable.InsertAsync(item); // insert the new item into the local database
                await SyncAsync(); // send changes to the mobile service


                //if (!item.Done)
                //{
                //    adapter.Add(item);
                //}
            }
            catch (Exception e)
            {
                CreateAndShowDialog(e, "Error: " + e.Message);
            }

            //textNewToDo.Text = "";
        }

	    private void CreateAndShowDialog(Exception exception, String title)
	    {
            Debug.WriteLine(exception.InnerException.Message);
	    }

        //private void CreateAndShowDialog(Exception exception, String title)
        //{
        //    CreateAndShowDialog(exception.Message, title);
        //}

        //private void CreateAndShowDialog(string message, string title)
        //{
        //    AlertDialog.Builder builder = new AlertDialog.Builder(this);

        //    builder.SetMessage(message);
        //    builder.SetTitle(title);
        //    builder.Create().Show();
        //}


        public async Task<IEnumerable<TodoItem>> GetItems()
        {
            //lock (locker)
            //{
            //    return (from i in database.Table<TodoItem>() select i).ToList();
            //}
            IEnumerable<TodoItem> items = null;

            try
            {
                // Get the items that weren't marked as completed and add them in the adapter
                //await SyncAsync(); // offline sync
                items = await toDoTable.ToListAsync();



                //adapter.Clear();

                //foreach (ToDoItem current in list)
                //    adapter.Add(current);

            }
            catch (Exception e)
            {
                CreateAndShowDialog(e, "Error");
            }
            return items;
        }


        public async Task<IEnumerable<TodoItem>> GetItemsNotDone()
        {
            //lock (locker)
            //{
            //    return database.Query<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
            //}

            IEnumerable<TodoItem> list = null;
            try
            {
                // Get the items that weren't marked as completed and add them in the adapter
                //await SyncAsync(); // offline sync
                list = await toDoTable.Where(item => item.Complete == false).ToListAsync();



                //adapter.Clear();

                //foreach (ToDoItem current in list)
                //    adapter.Add(current);

            }
            catch (Exception e)
            {
                CreateAndShowDialog(e, "Error");
            }

            return list;
        }

        public async Task<TodoItem> GetItem(string id)
        {
            //lock (locker)
            //{
            //    return database.Table<TodoItem>().FirstOrDefault(x => x.ID == id);
            //}

            try
            {
                //await SyncAsync();
                return await toDoTable.LookupAsync(id);
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"INVALID {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"ERROR {0}", e.Message);
            }
            return null;

            //try
            //{
            //    // Get the items that weren't marked as completed and add them in the adapter
            //    var list = await toDoTable.Where(item => item.Id == id).FirstOrDefaultAsync();



            //    //adapter.Clear();

            //    //foreach (ToDoItem current in list)
            //    //    adapter.Add(current);

            //}
            //catch (Exception e)
            //{
            //    CreateAndShowDialog(e, "Error");
            //}
        }

        public async Task newUser(string microsoftID)
        {
            try
            {
                // does the user already exist?
                var existing_user = await userTable.Where(u => u.MicrosoftID == microsoftID).ToListAsync();

                if (existing_user.Count == 0)
                {
                    User user = new User
                    {
                        MicrosoftID = microsoftID
                    };

                    // insert new user
                    await userTable.InsertAsync(user);


                    Group group = new Group
                    {
                        Name = user.ID
                    };

                    // add default group voor user
                    await groupTable.InsertAsync(group);

                    UserGroupMembership ugm = new UserGroupMembership
                    {
                        ID = user.ID,
                        MembershipID = group.ID
                    };

                    await userGroupMembershipTable.InsertAsync(ugm);

                }
                else if (existing_user.Count == 1)
                {
                    Debug.WriteLine("user exists, exactly one ID found: " + existing_user.FirstOrDefault<User>().ID);
                }
                else
                {
                    Debug.WriteLine("something weird happened, more than one user with the same ID found");
                }


                //Debug.WriteLine(
                //userGroupMembershipTable.Where(ugm => ugm.ID == microsofID).ToListAsync().Result
                //);

                //await userGroupMembershipTable.LookupAsync

                //var tmp = await groupTable.ToListAsync();



                //await MobileService.GetSyncTable<User>().InsertAsync(user);

                await SyncAsync(); // offline sync
                //adapter.Clear();
            }
            catch (Exception e)
            {
                CreateAndShowDialog(e, "Error: " + e.Message);
            }
        }

        public async Task SaveItem(TodoItem item)
        {
            //lock (locker)
            //{
            //    if (item.ID != 0)
            //    {
            //        database.Update(item);
            //        return item.ID;
            //    }
            //    else
            //    {
            //        return database.Insert(item);
            //    }
            //}

            try
            {
                // Get the items that weren't marked as completed and add them in the adapter
                await toDoTable.InsertAsync(item);

                await SyncAsync(); // offline sync
                //adapter.Clear();

                //foreach (ToDoItem current in list)
                //    adapter.Add(current);

            }
            catch (Exception e)
            {
                CreateAndShowDialog(e, "Error");
            }
        }

        public async Task DeleteItem(string id)
        {
            //lock (locker)
            //{
            //    return database.Delete<TodoItem>(id);
            //}

            try 
            {
                TodoItem to_be_deleted = GetItem(id).Result;
                await toDoTable.DeleteAsync(to_be_deleted);
                await SyncAsync(); // offline sync
            } 
            catch (MobileServiceInvalidOperationException msioe)
            {
               CreateAndShowDialog(msioe, msioe.Message);
            }
            catch (Exception e)
            {
                CreateAndShowDialog(e, "Error");
            }
        }

        //public async Task DeleteTaskAsync(TodoItem item)
        //{
        //    try
        //    {
        //        await todoTable.DeleteAsync(item);
        //        //await SyncAsync ();
        //    }
        //    catch (MobileServiceInvalidOperationException msioe)
        //    {
        //        Debug.WriteLine(@"INVALID {0}", msioe.Message);
        //    }
        //    catch (Exception e)
        //    {
        //        Debug.WriteLine(@"ERROR {0}", e.Message);
        //    }
        //}
	}
}

