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

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Frame toLogin = Window.Current.Content as Frame;
            toLogin.Navigate(typeof(LoginTeacher));
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
    }
}
