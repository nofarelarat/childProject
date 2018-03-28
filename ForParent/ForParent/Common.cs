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
        public static bool isConectet = false;

        public static async Task<bool> GetUserFromFileAsync()
        {
            try
            {
                Windows.Storage.StorageFolder storageFolder =
                Windows.Storage.ApplicationData.Current.LocalFolder;
                if(storageFolder == null)
                {
                    return false;
                }
                Windows.Storage.StorageFile userFile =
                    await storageFolder.GetFileAsync("userParent.txt");
                if (userFile == null)
                {
                    return false;
                }
                string text = await Windows.Storage.FileIO.ReadTextAsync(userFile);
                if (String.IsNullOrEmpty(text) ||String.IsNullOrWhiteSpace(text) || text.Equals(""))
                {
                    return false;
                }
                else
                {
                    string[] fileResult = text.Split('+');
                    who_am_i = fileResult[0];
                    if (fileResult.Length >= 3)
                    {
                        myChild = fileResult[2];
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

        public static async Task<bool> DeleteFileAsync()
        {
            try
            {
                Windows.Storage.StorageFolder storageFolder =
                        Windows.Storage.ApplicationData.Current.LocalFolder;
                if (storageFolder == null)
                {
                    return false;
                }
                Windows.Storage.StorageFile userFile =
                    await storageFolder.CreateFileAsync("userParent.txt",
                        Windows.Storage.CreationCollisionOption.ReplaceExisting);
                if (userFile == null)
                {
                    return false;
                }
            }

            catch
            {
                return false;
            }

            who_am_i = "";
            myChild = "";
            return true;
        }

        //in the sentence allreay see who send the message
        public static async Task<bool> WriteConversation(string sentence)
        {
            try
            {
                 Windows.Storage.StorageFolder storageFolder =
                       Windows.Storage.ApplicationData.Current.LocalFolder;
                 if (storageFolder == null)
                 {
                     return false;
                 }
                 Windows.Storage.StorageFile userFile =
                     await storageFolder.GetFileAsync("chatWithChild.txt");
                 if (userFile == null)
                 {
                     return false;
                 }
                 IEnumerable<string> toAdd = new List<string>() { sentence };
                 await Windows.Storage.FileIO.AppendLinesAsync(userFile, toAdd);

            }
            //trying to create the file
            catch
            {
                try
                {
                    Windows.Storage.StorageFolder storageFolder =
                            Windows.Storage.ApplicationData.Current.LocalFolder;
                    Windows.Storage.StorageFile userFile =
                        await storageFolder.CreateFileAsync("chatWithChild.txt",
                            Windows.Storage.CreationCollisionOption.ReplaceExisting);
                    IEnumerable<string> toAdd = new List<string>() { sentence };
                    await Windows.Storage.FileIO.AppendLinesAsync(userFile, toAdd);
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        public static async Task<string> ReadConversation(string filename)
        {
            try
            {
                Windows.Storage.StorageFolder storageFolder =
                    Windows.Storage.ApplicationData.Current.LocalFolder;
                Windows.Storage.StorageFile userFile =
                    await storageFolder.GetFileAsync(filename);

                string text = await Windows.Storage.FileIO.ReadTextAsync(userFile);

                return text;    
            }

            catch
            {
                return "";
            }
        }
        public static async Task<symbol[]> GetUserCounterAsync(string symbolName)
        {
            string child_email = myChild;
           // string email = "rami@gmail.com";
            symbol[] res = null;
            ConnectDB db = new ConnectDB();
            
            if (!who_am_i.Equals("") && !child_email.Equals(""))
            {
                symbol symbol = new symbol
                {
                    email = child_email,
                    symbolName = symbolName,
                    date = DateTime.Today
                };
                if (symbolName.Equals("allsymbols"))
                {
                    res = await db.GetUserAllCountersAsync(child_email);
                }
                else {
                    res = await db.GetUserCounterAsync(symbol);
                }
            }
            return res;
        }

        public static async Task<string> GetParentContactAsync()
        {
            string email = who_am_i;
            string res = "";
            ConnectDB db = new ConnectDB();
            
            if (!email.Equals(""))
            {
                res = await db.GetParentContactAsync(email);
                if(res == null)
                {
                    res = "The child don't have this parent as his contact";
                }
            }
            else
            {
                res = "Parent is not connected";
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
