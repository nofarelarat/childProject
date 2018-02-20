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
            if(user != null){
                symbol symbol = new symbol
                {
                    email = user.email,
                    symbolName = symbolName,
                    date = DateTime.Today
                };
                await db.UpdateUserCounterAsync(symbol);
            }      
        }
        public static async void GetUserCounterAsync(string symbolName)
        {
            //string email = who_am_i;
            string email = "rami@gmail.com";
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
                await db.GetUserCounterAsync(symbol);
            }
        }
    }
}
