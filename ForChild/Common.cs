using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;

namespace ForChild
{
    static class Common
    {
        public static string who_am_i = "";

        public static async void UpdateCounterAsync(string symbolName)
        {
            //string email = who_am_i;
            string email = "rami@gmail.com";
            ConnectDB db = new ConnectDB();
            user user = await db.GetUserByMailAsync(email);
            //dont need to get user from db because 
            //when login there is a check is user exisit 
            //just check if who_am_i not empty - saving time

            if (user != null && symbolName!=""){
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
        public static async void GetUserContactsAsync(string email)
        {
            //string email = who_am_i;
            ConnectDB db = new ConnectDB();
            //await db.GetGardenChildren("flowers");
            user user = await db.GetUserByMailAsync(email);
            //dont need to get user from db because 
            //when login there is a check is user exisit 
            //just check if who_am_i not empty - saving time

            if (user != null)
            {
                await db.GetUserContactsAsync(email);
            }
        }

        //updation user contacts update all the for contacts
        public static async Task<bool> AddUserChatContact(string[] emails)
        {
            ConnectDB db = new ConnectDB();
            for (int i=1;i<emails.Length;i++)
            {
                user user = await db.GetUserByMailAsync(emails[i]);
                //dont need to get user from db because 
                //when login there is a check is user exisit 
                //just check if who_am_i not empty - saving time

                if (user == null)
                {
                    return false;
                }
            }
            bool x = await db.AddUserContactsAsync(emails);
            return x;
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
    }
}
