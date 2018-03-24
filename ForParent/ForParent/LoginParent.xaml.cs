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
            loading.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

        }

        private async void EnterAppAsync(object sender, RoutedEventArgs e)
        {
            loading.Visibility = Windows.UI.Xaml.Visibility.Visible;
            result.Text = "";
            string Email = email.Text;
            string Password = password.Password.ToString();
            ConnectDB db = new ConnectDB();
            user user = await db.GetUserByMailAsync(Email);
            if (user == null)
            {
                loading.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                result.Text = "user doesn't exists";
            }
            else
            {
                if (user.password.Equals(Password) && user.type.Equals("Parent"))
                {
                    Common.who_am_i = Email;
                    Common.isConectet = true;
                    string childResult = await Common.GetParentContactAsync();
                    
                    // Create sample file; replace if exists.
                    if(childResult.IndexOf('@')> 0)
                    {
                        Common.myChild = childResult;
                        try
                        {
                            Windows.Storage.StorageFolder storageFolder =
                            Windows.Storage.ApplicationData.Current.LocalFolder;
                            Windows.Storage.StorageFile userFile =
                                await storageFolder.CreateFileAsync("userParent.txt",
                                    Windows.Storage.CreationCollisionOption.ReplaceExisting);
                            await Windows.Storage.FileIO.WriteTextAsync(userFile, Email
                                + "+" + Password + "+" + childResult);
                        }
                        catch
                        {
                            //cant access file
                        }
                        loading.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                        result.Text = "welcome " + user.firstname + "!";
                        Frame toMainPage = Window.Current.Content as Frame;
                        toMainPage.Navigate(typeof(MainPage));
                    }
                    else
                    {
                        result.Text = childResult;
                    }
                }
                else if(!user.type.Equals("Parent"))
                {
                    loading.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    result.Text = "The type is not Parent";
                }
                else
                {
                    loading.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
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
