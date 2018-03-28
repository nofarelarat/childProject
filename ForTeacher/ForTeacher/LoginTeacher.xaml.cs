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

namespace ForTeacher
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginTeacher : Page
    {
        public LoginTeacher()
        {
            this.InitializeComponent();
            loading.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

        }
        private async void EnterAppAsync(object sender, RoutedEventArgs e)
        {
            loading.Visibility = Windows.UI.Xaml.Visibility.Visible;
            result.Text = "loading...";
            ConnectDB db = new ConnectDB();
            user user = await db.GetUserByMailAsync(email.Text);
            if (user == null)
            {
                loading.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                result.Text = "user doesn't exists";

            }
            else
            {
                if (user.password.Equals(password.Text) && user.type.Equals("Teacher"))
                {
                    Common.who_am_i = email.Text;
                    Common.isConectet = true;
                    try
                    {
                        Windows.Storage.StorageFolder storageFolder =
                        Windows.Storage.ApplicationData.Current.LocalFolder;
                        Windows.Storage.StorageFile userFile =
                            await storageFolder.CreateFileAsync("userTeacher.txt",
                                Windows.Storage.CreationCollisionOption.ReplaceExisting);
                        await Windows.Storage.FileIO.WriteTextAsync(userFile, email.Text
                            + "+" + password.Text);
                    }
                    catch
                    {
                        //cant access file
                    }
                        loading.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                        result.Text = "welcome " + user.firstname + "!";
                    
                }
                else if(!user.type.Equals("Teacher"))
                {
                    loading.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    result.Text = "Type is nt a teacher";

                }
                else
                {
                    loading.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    result.Text = "incorrect password";
        
                }
            }
        }
        private void Button_Click_back(object sender, RoutedEventArgs e)
        {
            Frame toHome = Window.Current.Content as Frame;
            toHome.Navigate(typeof(MainPage));
        }

        
    }
}
