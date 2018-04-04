using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ForTeacher
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            if (Common.isConectet == false)
            {
                CheckUserExistAsync();
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            Frame toRegister = Window.Current.Content as Frame;
            toRegister.Navigate(typeof(Registration));
        }

        private void Statistics_Click(object sender, RoutedEventArgs e)
        {
            if (Common.who_am_i.Equals(""))
            {
                Frame toLogin = Window.Current.Content as Frame;
                toLogin.Navigate(typeof(LoginTeacher));
            }
            else
            { 
                Frame toRegister = Window.Current.Content as Frame;
                toRegister.Navigate(typeof(Statistics));
            }
    }

        private async void Login_ClickAsync(object sender, RoutedEventArgs e)
        {
            if (Common.who_am_i.Equals(""))
            {
                Frame toLogin = Window.Current.Content as Frame;
                toLogin.Navigate(typeof(LoginTeacher));
            }
            else
            {
                await Common.DeleteFileAsync("userTeacher.txt");
                Common.who_am_i = "";
                Common.garden = "";
                Common.isConectet = false;

                Frame toLogin = Window.Current.Content as Frame;
                toLogin.Navigate(typeof(LoginTeacher));
            }
        }

        private void Broadcast_Click(object sender, RoutedEventArgs e)
        {
            if(Common.who_am_i.Equals(""))
            {
                Frame toLogin = Window.Current.Content as Frame;
                toLogin.Navigate(typeof(LoginTeacher));
            }
            else
            { 
                Frame toSendBrodcast = Window.Current.Content as Frame;
                toSendBrodcast.Navigate(typeof(SendBrodcast));
            }
        }

        private async void CheckUserExistAsync()
        {
            bool success = await Common.GetUserFromFileAsync();
            if (success == false)
            {
                Frame toLogin = Window.Current.Content as Frame;
                toLogin.Navigate(typeof(LoginTeacher));
            }
        }
    }
}
