﻿using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ForChild
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class loginChild : Page
    {
        public loginChild()
        {

            this.InitializeComponent();
            loading.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

        }

        private async void EnterAppAsync(object sender, RoutedEventArgs e)
        {
            loading.Visibility = Windows.UI.Xaml.Visibility.Visible;
            string[] lines = System.IO.File.ReadAllLines(@"config.txt");
            ConnectDB db = new ConnectDB();
            user user = await db.GetUserByMailAsync(email.Text);
            if (user == null)
            {
                loading.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                result.Text = "user doesn't exists";
            }
            else
            {
                if (user.password.Equals(password.Password.ToString()) && user.type.Equals("Child"))
                {
                    Common.who_am_i = email.Text;
                    Common.isConectet = true;
                    await Common.GetUserContactsAsync();

                    // Create sample file; replace if exists.
                    try
                    {
                        Windows.Storage.StorageFolder storageFolder =
                        Windows.Storage.ApplicationData.Current.LocalFolder;
                        Windows.Storage.StorageFile userFile =
                            await storageFolder.CreateFileAsync("userChild.txt",
                                Windows.Storage.CreationCollisionOption.ReplaceExisting);
                        await Windows.Storage.FileIO.WriteTextAsync(userFile, "email:" +
                            Common.who_am_i + "+password:" + password.Password.ToString()
                            + "+father:" + Common.myFather + "+mother:" + Common.myMother
                            + "+friend:" + Common.myFriend);
                    }
                    catch
                    {
                        //cant access file
                    }
                    loading.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    result.Text = "welcome " + user.firstname + "!";
                }
                else if (!user.type.Equals("Child"))
                {
                    loading.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    result.Text = "The type is not child";
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
