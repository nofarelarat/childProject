using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ForParent
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginParent : Page
    {
        public LoginParent()
        {
            this.InitializeComponent();
        }

        private async void EnterAppAsync(object sender, RoutedEventArgs e)
        {
            string Email = email.Text;
            string Password = password.Text;
            ConnectDB db = new ConnectDB();
            user user = await db.GetUserByMailAsync(Email);
            if (user == null)
            {
                result.Text = "user doesn't exists";
            }
            else
            {
                if (user.password.Equals(Password) && user.type.Equals("Parent"))
                {
                    Common.who_am_i = Email;

                    //write email & pass to local file
                    //write contact to local file
                    //add a message for get contact you need to login
                    //add check if string child is not email do something
                    string child = await Common.GetParentContactAsync();

                    //make the add child contacts page in the parent app
                    // Create sample file; replace if exists.
                    if(child.IndexOf('@')> 0)
                    {
                        Windows.Storage.StorageFolder storageFolder =
                        Windows.Storage.ApplicationData.Current.LocalFolder;
                        Windows.Storage.StorageFile userFile =
                            await storageFolder.CreateFileAsync("user.txt",
                                Windows.Storage.CreationCollisionOption.ReplaceExisting);
                        await Windows.Storage.FileIO.WriteTextAsync(userFile, Email
                            + "+" + Password + "+" + child);
                        //await Windows.Storage.FileIO.WriteTextAsync(userFile, Password);
                        //await Windows.Storage.FileIO.WriteTextAsync(userFile, child);
                        result.Text = "welcome " + user.firstname + "!";
                    }
                }
                else if(!user.type.Equals("Parent"))
                {
                    result.Text = "The type is not Parent";
                }
                else
                {
                    result.Text = "incorrect password";
                }
            }
        }

        private void Register(object sender, RoutedEventArgs e)
        {
            Frame toRegister = Window.Current.Content as Frame;
            toRegister.Navigate(typeof(Registration));
        }

        private void Button_Click_back(object sender, RoutedEventArgs e)
        {
            Frame toHome = Window.Current.Content as Frame;
            toHome.Navigate(typeof(MainPage));
        }

    }
}
