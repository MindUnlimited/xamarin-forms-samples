﻿using Android.Content;
using Android.Preferences;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Todo.Android;
using Xamarin.Auth;
using Xamarin.Forms;
using System.Net;
using System.IO;
using System.Xml;


[assembly: Xamarin.Forms.Dependency(typeof(Authenticate_Android))]

namespace Todo.Android
{
    public class RetrieveFriends
    {
        public static string retrieveFacebookFriends(string token, string name)
        {
            string sURL = String.Format("https://graph.facebook.com/me/friends&access_token={0}", token);

            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            //HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            //Stream resStream = response.GetResponseStream();

            //return resStream.ToString();


            //string sURL;
            //sURL = "http://www.microsoft.com";



            WebRequest wrGETURL;
            wrGETURL = WebRequest.Create(sURL);

            WebProxy myProxy = new WebProxy("myproxy", 80);
            myProxy.BypassProxyOnLocal = true;

            wrGETURL.Proxy = WebProxy.GetDefaultProxy();

            Stream objStream;
            objStream = wrGETURL.GetResponse().GetResponseStream();

            StreamReader objReader = new StreamReader(objStream);

            string sLine = "";
            int i = 0;

            return objReader.ReadToEnd();

            //while (sLine != null)
            //{
            //    i++;
            //    sLine = objReader.ReadLine();
            //    if (sLine != null)
            //        Console.WriteLine("{0}:{1}", i, sLine);
            //}
            //Console.ReadLine();
        }
    }

    public class tst
    {
        public string Message { get; set; }
    }



    public class Authenticate_Android : IAuthenticate
    {
        public async Task Authenticate(MobileServiceAuthenticationProvider provider)
        {
            string message;
            string providerName = provider.ToString();

            // Authorization credential.
            MobileServiceUser user = null;

            var accountStore = AccountStore.Create(Forms.Context); // Xamarin.Android

            // Sharedpreferences (local storage) for storing the last used oauth provider
            var preferences = PreferenceManager.GetDefaultSharedPreferences(Forms.Context);
            var editor = preferences.Edit();

            while (user == null)
            {
                Account accountFound = accountStore.FindAccountsForService(providerName).FirstOrDefault();
                // Try to get an existing encrypted credential from isolated storage.        
                if (accountFound != null)
                {
                    user = new MobileServiceUser(accountFound.Username);
                    user.MobileServiceAuthenticationToken = accountFound.Properties["token"];
                }
                if (user != null)
                {
                    // Set the user from the stored credentials.
                    Todo.App.Database.client.CurrentUser = user;

                    try
                    {
                        // Try to return an item now to determine if the cached credential has expired.
                        await App.Database.client.GetTable<Item>().Take(1).ToListAsync();
                        JToken userInfo = await App.Database.client.InvokeApiAsync("userInfo", HttpMethod.Get, null); // also gather extra user information
                        JToken response = await App.Database.client.InvokeApiAsync("getcontacts", HttpMethod.Get, null);
                        var test = response["feed"];

                        

                        //if (provider == MobileServiceAuthenticationProvider.Google)
                        //{
                        //    JToken identity = await App.Database.client.InvokeApiAsync("getIdentities", HttpMethod.Get, null);
                        //    string accessToken = identity["google"]["accessToken"].ToString();
                        //    var contactsUrl = "https://www.google.com/m8/feeds/contacts/default/full?access_token=" + accessToken;

                        //    XmlDocument myXMLDocument = new XmlDocument();
                        //    myXMLDocument.Load(contactsUrl);
                        //    var test = myXMLDocument.ToString();

                        //    var json = JsonConvert.SerializeObject(
                        //}


                        string var = "stop";


                        //if (provider == MobileServiceAuthenticationProvider.Google)
                        //{
                        //    //WebRequest request = WebRequest.Create("");
                        //    //StringContent xml = new StringContent("", Encoding.UTF8, "application/xml");
                        //    //JToken response = await App.Database.client.InvokeApiAsync("getcontacts", HttpMethod.Get, null);
                        //    //var response = await App.Database.client.InvokeApiAsync<string>("getcontacts", HttpMethod.Get, null);
                        //    //HttpResponseMessage response = await App.Database.client.InvokeApiAsync("getcontacts", null, HttpMethod.Get, null, null);
                        //    //string xmlResponse =  await response.Content.ReadAsStringAsync();
                        //    //Debug.WriteLine(response);
                        //    //string json = Newtonsoft.Json.JsonConvert.SerializeObject(response);
                        //}
                        //else
                        //{
                        //    JToken contacts = await App.Database.client.InvokeApiAsync("getcontacts", HttpMethod.Get, null); // also gather contacts information
                        //    Debug.WriteLine(contacts.ToString());
                        //}
                    }
                    catch (MobileServiceInvalidOperationException ex)
                    {
                        if (ex.Response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                        {
                            // Remove the credential with the expired token.
                            accountStore.Delete(accountFound, providerName);
                            user = null;
                            continue;
                        }
                    }
                }
                else
                {
                    try
                    {
                        // Login with the identity provider.
                        user = await Todo.App.Database.client
                            .LoginAsync(Forms.Context, provider);

                        // Store the encrypted user credentials in local settings.
                        Account currentAccount = new Account(user.UserId, new Dictionary<string, string> { { "token", user.MobileServiceAuthenticationToken } });
                        accountStore.Save(currentAccount, providerName);
                    }
                    catch (MobileServiceInvalidOperationException ex)
                    {
                        message = "You must log in. Login Required";
                    }
                }

                // add last user provider, so that you don't have to click on which login provider you want to use
                editor.PutString("LastUsedProvider", providerName);
                editor.Apply();

                Todo.App.Database.mobileServiceUser = user;
                Todo.App.Database.userID = user.UserId;

                //JToken contacts2 = await App.Database.client.InvokeApiAsync("getContacts", HttpMethod.Get, null); // also gather extra user information
                //Debug.WriteLine(contacts2.ToString());


                //RetrieveFriends.retrieveFacebookFriends(user.MobileServiceAuthenticationToken, (string)userInfo2["name"]);

                await Todo.App.Database.InitLocalStoreAsync();
                await Todo.App.Database.newUser(Todo.App.Database.mobileServiceUser.UserId, provider);
                await Todo.App.Database.OnRefreshItemsSelected(); // pull database tables

                await Todo.App.Database.getContactsThatUseApp();

                message = string.Format("You are now logged in - {0}", user.UserId);
                Debug.WriteLine(message);
                //MessageBox.Show(message);
            }
        }
    }
}