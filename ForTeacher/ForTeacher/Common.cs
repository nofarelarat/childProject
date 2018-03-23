using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForTeacher
{
    class Common
    {
        public static string who_am_i = "";
        public static bool isConectet = false;

        public static async Task<bool> GetUserFromFileAsync()
        {
            try
            {
                Windows.Storage.StorageFolder storageFolder =
                Windows.Storage.ApplicationData.Current.LocalFolder;
                Windows.Storage.StorageFile userFile =
                    await storageFolder.GetFileAsync("userTeacher.txt");
                string text = await Windows.Storage.FileIO.ReadTextAsync(userFile);
                if (text.Equals(""))
                {
                    return false;
                }
                else
                {
                    string[] fileResult = text.Split('+');
                    who_am_i = fileResult[0];
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
