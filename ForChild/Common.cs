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
using System.Net.Http;
using Windows.UI.Xaml.Media.Imaging;


namespace ForChild
{
    static class Common
    {
        public static string who_am_i = "";
        public static string myFather = "";
        public static string myMother = "";
        public static string myFriend = "";
        public static string myTeacher = "";
        public static bool isConectet = false;
        // Input: message and addresse
        // Output: send the message to azure storage Queue 
        public static async void sendMsg(string message,string addressee)
        {
            String final_msg = message + "$" + Common.who_am_i + "$";
            //Create an HTTP client object
            HttpClient httpClient = new HttpClient();
            Uri requestUri = new Uri("https://function-queue-connect.azurewebsites.net/api/HttpTriggerCSharp1-send?code=c4TP96qDiVU6X5Zd6HNmAOCOIp35R52MB0MZnL6GRjY8ldfF2GqZ3A==&&name=" + final_msg + addressee);
            //Send the GET request asynchronously and retrieve the response as a string.
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                //Send the GET request
                httpResponse = await httpClient.GetAsync(requestUri);
                httpResponse.EnsureSuccessStatusCode();
                // errormessage.Text = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {

                //  errormessage.Text = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
            }
        }

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
                    myTeacher = "shosh@gmail.com"; //TODO: change it!!!
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
                    myTeacher = users[2].email;
                    myFriend = users[3].email;
                }
                return x;
            }
            return false;
        }

        public static async Task<OutTable[]> GetDedicatedMsgAsync(OutTable[] msg,string msg_sender)
        {
            int index = 0; //arr index
            OutTable[] dedicate_msg = null;
            for (int i=0; i < msg.Length; i++)
            {
                if (msg[i].Message_Send.Equals(msg_sender))
                {
                    dedicate_msg[index] = msg[i];
                    index++;
                    if (index ==3)
                    {
                        break;
                    }
                }
            }
            return dedicate_msg;
        }

        public static async Task<OutTable[]> GetMsgAsync(string contact)
        {
            string completeUri = "https://function-queue-connect.azurewebsites.net/api/HttpGET-outTable-CSharp1?code=smvhBz/DBsmNUDqf7/TIhjZ1IMBSo77LwpSbhG2I9CsGCw1D6sNLkg==&&name="
                + who_am_i +"$"+ contact;

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
        public static async Task<bool> GetUserFromFileAsync()
        {
            try
            {
                Windows.Storage.StorageFolder storageFolder =
                Windows.Storage.ApplicationData.Current.LocalFolder;
                Windows.Storage.StorageFile userFile =
                    await storageFolder.GetFileAsync("userChild.txt");
                string text = await Windows.Storage.FileIO.ReadTextAsync(userFile);
                if (text.Equals(""))
                {
                    return false;
                }
                else
                {
                    string[] fileResult = text.Split('+');
                    foreach (string per in fileResult)
                    {
                        string[] per_arr = per.Split(':');
                        switch (per_arr[0])
                        {
                            case "email":
                                who_am_i = per_arr[1];
                                break;
                            case "father":
                                myFather = per_arr[1];
                                break;
                            case "mother":
                                myMother = per_arr[1];
                                break;
                            case "Teacher":
                                myTeacher = per_arr[1];
                                break;
                            case "friend":
                                myFriend = per_arr[1];
                                break;
                        }
                    }

                    isConectet = true;
                    return true;
                }
            }

            catch
            {
                return false;
            }
        }

    }
}

