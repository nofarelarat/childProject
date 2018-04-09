using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace ForTeacher
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            forLogout.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
         //   for_login.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            if (Common.isConectet == false)
            {
                CheckUserExistAsync();
            }
            else
            {
                forLogout.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
        }
  

        private async void forChat_Click(object sender, RoutedEventArgs e)
        {
            if (Common.who_am_i == "")
            {
                Frame toLogin = Window.Current.Content as Frame;
                toLogin.Navigate(typeof(LoginTeacher));
            }            
            else if (Common.counter_child == 0)
            {
                Frame toChat = Window.Current.Content as Frame;
                toChat.Navigate(typeof(MainPage));
            }
            else
            {
                Frame toChat = Window.Current.Content as Frame;
                toChat.Navigate(typeof(SendBrodcast));
            }
        }

        private async void CheckUserExistAsync()
        {

            bool success = await Common.GetUserFromFileAsync();
            if (success == false)
            {
                forLogout.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                Frame toLogin = Window.Current.Content as Frame;
                toLogin.Navigate(typeof(LoginTeacher));
            }
            else
            {
                forLogout.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }

        }

        private async void forLogOut_ClickAsync(object sender, RoutedEventArgs e)
        {
            await Common.DeleteFileAsync("userTeacher.txt");
            Common.who_am_i = "";
            Common.garden = "";
            Common.isConectet = false;

            Frame toLogin = Window.Current.Content as Frame;
            toLogin.Navigate(typeof(LoginTeacher));
        }


        private void forRegister_Click(object sender, RoutedEventArgs e)
        {
            Frame toRegister = Window.Current.Content as Frame;
            toRegister.Navigate(typeof(Registration));
        }


        private async void Login_ClickAsync(object sender, RoutedEventArgs e)
        {
            if (Common.who_am_i.Equals(""))
            {
                Frame toLogin = Window.Current.Content as Frame;
                toLogin.Navigate(typeof(LoginTeacher));
            }
        }

        private void forAnalysis_Click(object sender, RoutedEventArgs e)
        {
            if (Common.who_am_i == "")
            {
                Frame toLogin = Window.Current.Content as Frame;
                toLogin.Navigate(typeof(LoginTeacher));
            }
            else
            { 
                Frame toSendBrodcast = Window.Current.Content as Frame;
                toSendBrodcast.Navigate(typeof(Statistics));
            }
        }

    }

}