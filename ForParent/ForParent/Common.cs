using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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
                Windows.Storage.StorageFile userFile =
                    await storageFolder.GetFileAsync("userParent.txt");
                string text = await Windows.Storage.FileIO.ReadTextAsync(userFile);
                if (text.Equals(""))
                {
                    //    Frame toLogin = Window.Current.Content as Frame;
                    //    toLogin.Navigate(typeof(LoginParent));
                    return false;
                }
                else
                {
                    string[] fileResult = text.Split('+');
                    who_am_i = fileResult[0];
                    myChild = fileResult[2];
                    isConectet = true;
                    return true;
                }
            }
            
            catch
            {
                //Frame toLogin = Window.Current.Content as Frame;
                //toLogin.Navigate(typeof(LoginParent));
                return false;
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
    }
}
