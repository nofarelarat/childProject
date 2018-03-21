using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ForChild
{
    static class Common
    {
        public static string who_am_i = "";
        public static string myFather = "";
        public static string myMother = "";
        public static string myFriend = "";
        public static string mySister = "";

        public static async void UpdateCounterAsync(string symbolName)
        {
            //string email = who_am_i;
            string email = "rami@gmail.com";
            ConnectDB db = new ConnectDB();
            user user = await db.GetUserByMailAsync(email);
            //dont need to get user from db because 
            //when login there is a check is user exisit 
            //just check if who_am_i not empty - saving time

            if (user != null && symbolName != "") {
                symbol symbol = new symbol
                {
                    email = user.email,
                    symbolName = symbolName,
                    date = DateTime.Today
                };
                await db.UpdateUserCounterAsync(symbol);
            }
            else
            {
                //user or symbol is empty
            }
        }

        public static async Task<symbol[]> GetUserCounterAsync(string symbolName)
        {
            symbol[] res = null;
            ConnectDB db = new ConnectDB();
            if (!who_am_i.Equals(""))
            {
                symbol symbol = new symbol
                {
                    email = who_am_i,
                    symbolName = symbolName,
                    date = DateTime.Today
                };
                res = await db.GetUserCounterAsync(symbol);
            }
            return res;
        }

        public static async Task<symbol[]> GetUserAllCounterAsync()
        {
            //string email = who_am_i;
            string email = "rami@gmail.com";
            symbol[] res = null;
            ConnectDB db = new ConnectDB();
            user user = await db.GetUserByMailAsync(email);
            //dont need to get user from db because 
            //when login there is a check is user exisit 
            //just check if who_am_i not empty - saving time

            if (user != null)
            {
                res = await db.GetUserAllCountersAsync(email);
            }
            return res;
        }
        public static async void GetUserContactsAsync()
        {
            string email = who_am_i;
            ConnectDB db = new ConnectDB();
            
            if (!who_am_i.Equals(""))
            {
                userContacts contacts = await db.GetUserContactsAsync(email);
                if (contacts != null)
                {
                    myFather = contacts.father;
                    myMother = contacts.mother;
                    myFriend = contacts.friend;
                    mySister = contacts.sister;
                }
                else
                {
                    Frame toAddUsersForChat = Window.Current.Content as Frame;
                    toAddUsersForChat.Navigate(typeof(AddUsersForChat));
                }
            }
        }

        //updation user contacts update all the for contacts
        public static async Task<bool> AddUserChatContact(string[] emails)
        {
            if (who_am_i.Equals(""))
            {
                return false;
            }
            user[] users = new user[4];
            ConnectDB db = new ConnectDB();
            for (int i = 0;i < users.Length;i++)
            {
                users[i] = await db.GetUserByMailAsync(emails[i+2]);
                if (users[i] == null)
                {
                    return false;
                }
            }
            if(users[0].type.Equals("Parent")&& users[1].type.Equals("Parent")
                && users[2].type.Equals("Parent") && users[3].type.Equals("Child"))
            {
                bool x = await db.AddUserContactsAsync(emails);
                if (x == true)
                {
                    myFather = users[0].email;
                    myMother = users[1].email;
                    mySister = users[2].email;
                    myFriend = users[3].email;
                }
                return x;
            }
            return false;
        }

        public static async Task<OutTable[]> GetMsgAsync()
        {
            string completeUri = "https://function-queue-connect.azurewebsites.net/api/HttpGET-outTable-CSharp1?code=smvhBz/DBsmNUDqf7/TIhjZ1IMBSo77LwpSbhG2I9CsGCw1D6sNLkg==&&name="
                + who_am_i;

            Uri requestUri = new Uri(completeUri);
            Windows.Web.Http.HttpClient httpClient = new Windows.Web.Http.HttpClient();

            //Send the GET request asynchronously and retrieve the response as a string.
            Windows.Web.Http.HttpResponseMessage httpResponse = new Windows.Web.Http.HttpResponseMessage();
            string httpResponseBody = "";

            try
            {
                //Send the GET request
                httpResponse = await httpClient.GetAsync(requestUri);
                httpResponse.EnsureSuccessStatusCode();
                httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                OutTable[] msg = JsonConvert.DeserializeObject<OutTable[]>(httpResponseBody);
                return msg;
            }

            catch (Exception ex)
            {
                httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
            }

            return null;
        }

        public static async Task<bool> CheckForSimilarFriendAsync()
        {
            ConnectDB db = new ConnectDB();

            if (!(who_am_i.Equals("") || myFriend.Equals("")))
            {
                userContacts contacts = await db.GetUserContactsAsync(myFriend);
                if (contacts != null)
                {
                    if (contacts.friend.Equals(who_am_i))
                    {
                        return true;
                    }
                }
            }
            return false;
                
        }
    }
}

