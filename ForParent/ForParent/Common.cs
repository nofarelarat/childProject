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

namespace ForParent
{
     class Common
    {
        public static string who_am_i = "";
        public static string myChild = "";

        public static async void GetUserFromFileAsync()
        {
            try
            {
                Windows.Storage.StorageFolder storageFolder =
                Windows.Storage.ApplicationData.Current.LocalFolder;
                Windows.Storage.StorageFile userFile =
                    await storageFolder.GetFileAsync("user.txt");
                string text = await Windows.Storage.FileIO.ReadTextAsync(userFile);
                if (text.Equals(""))
                {
                    Frame toLogin = Window.Current.Content as Frame;
                    toLogin.Navigate(typeof(LoginParent));
                }
                else
                {
                    string[] fileResult = text.Split('+');
                    who_am_i = fileResult[0];
                    myChild = fileResult[2];
                }
            }
            
            catch
            {
                Frame toLogin = Window.Current.Content as Frame;
                toLogin.Navigate(typeof(LoginParent));
            }
        }
       
        public static async Task<symbol[]> GetUserCounterAsync(string symbolName)
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
                symbol symbol = new symbol
                {
                    email = user.email,
                    symbolName = symbolName,
                    date = DateTime.Today
                };
                if (symbolName.Equals("allsymbols"))
                {
                    res = await db.GetUserAllCountersAsync(email);
                }
                else {
                    res = await db.GetUserCounterAsync(symbol);
                }
            }
            return res;
        }

        public static async Task<string> GetParentContactAsync()
        {
            //string email = who_am_i;
            string email = "rami@gmail.com";
            string res = "";
            ConnectDB db = new ConnectDB();
            user user = await db.GetUserByMailAsync(email);

            //dont need to get user from db because 
            //when login there is a check is user exisit 
            //just check if who_am_i not empty - saving time

            if (user != null)
            {
                res = await db.GetParentContactAsync(email);
            }
            else
            {
                res = "Can't find the parent in the Database";
            }
            return res;
        }
        public static async Task<OutTable[]> GetMsgAsync(string contact)
        {
            string completeUri = "https://function-queue-connect.azurewebsites.net/api/HttpGET-outTable-CSharp1?code=smvhBz/DBsmNUDqf7/TIhjZ1IMBSo77LwpSbhG2I9CsGCw1D6sNLkg==&&name="
                + who_am_i + "$" + contact;

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
        public static async void UpdateCounterAsync(string symbolName)
        {
            //string email = who_am_i;
            string email = "rami@gmail.com";
            ConnectDB db = new ConnectDB();
            user user = await db.GetUserByMailAsync(email);
            //dont need to get user from db because 
            //when login there is a check is user exisit 
            //just check if who_am_i not empty - saving time

            if (user != null && symbolName != "")
            {
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

        public static async void sendMsg(string message, string addressee)
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
    }
}
