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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ForTeacher
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
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
                Frame toLogin = Window.Current.Content as Frame;
                toLogin.Navigate(typeof(LoginTeacher));
            }

        }

        private void forRegister_Click(object sender, RoutedEventArgs e)
        {
            Frame toRegister = Window.Current.Content as Frame;
            toRegister.Navigate(typeof(Registration));
        }

        private void forLogin_Click(object sender, RoutedEventArgs e)
        {
            Frame toLogin = Window.Current.Content as Frame;
            toLogin.Navigate(typeof(LoginTeacher));
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
                Frame toAnalysis = Window.Current.Content as Frame;
                toAnalysis.Navigate(typeof(Statistics));
            }
        }

    }

}