using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if(user != null && symbolName!=""){
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
            if (user != null)
            {
                await db.GetUserContactsAsync(email);
            }
        }
        public static async Task<bool> AddUserChatContact(string[] emails)
        {
            ConnectDB db = new ConnectDB();
            for (int i=1;i<emails.Length;i++)
            {
                user user = await db.GetUserByMailAsync(emails[i]);
                if (user == null)
                {
                    return false;
                }
            }
            bool x = await db.AddUserContactsAsync(emails);
            return x;
        }
    }
}
