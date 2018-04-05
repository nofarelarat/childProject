﻿using System;
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

namespace ForParent
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            forLogout.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            for_login.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            if (Common.isConectet == false)
            {
                CheckUserExistAsync();
            }
            {
                forLogout.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }

        }

        private async void forChat_Click(object sender, RoutedEventArgs e)
        {
            if (Common.who_am_i == "")
            {
                Frame toLogin = Window.Current.Content as Frame;
                toLogin.Navigate(typeof(LoginParent));
            }
            else if (Common.myChild =="")
            {
                Common.myChild = await Common.GetParentContactAsync();
                if(Common.myChild == "")
                {
                    //the child didnt insert the parent has is contact
                    //add alert
                }
                else
                {
                    //login again for update from db
                    Frame toLogin = Window.Current.Content as Frame;
                    toLogin.Navigate(typeof(LoginParent));
                }
            }
            else
            {
                Frame toChat = Window.Current.Content as Frame;
                toChat.Navigate(typeof(Chat));
            }
        }

        private async void CheckUserExistAsync()
        {
            bool success = await Common.GetUserFromFileAsync();
            if(success == false)
            {
                forLogout.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                for_login.Visibility = Windows.UI.Xaml.Visibility.Visible;
                Frame toLogin = Window.Current.Content as Frame;
                toLogin.Navigate(typeof(LoginParent));
            }
            else
            {
                forLogout.Visibility = Windows.UI.Xaml.Visibility.Visible;
                for_login.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
        }

        private void forRegister_Click(object sender, RoutedEventArgs e)
        {
            Frame toRegister = Window.Current.Content as Frame;
            toRegister.Navigate(typeof(Registration));
        }

        private async void forLogin_ClickAsync(object sender, RoutedEventArgs e)
        {
            if (Common.who_am_i.Equals(""))
            {
                Frame toLogin = Window.Current.Content as Frame;
                toLogin.Navigate(typeof(LoginParent));
            }
        }

        private async void forLogOut_ClickAsync(object sender, RoutedEventArgs e)
        {
            await Common.DeleteFileAsync("userParent.txt");
            await Common.DeleteFileAsync("chatWithChild.txt");
            Common.who_am_i = "";
            Common.isConectet = false;
            Common.myChild = "";
            Frame toLogin = Window.Current.Content as Frame;
            toLogin.Navigate(typeof(LoginParent));
        }
        private void forAnalysis_Click(object sender, RoutedEventArgs e)
        {
            if (Common.who_am_i == "")
            {
                Frame toLogin = Window.Current.Content as Frame;
                toLogin.Navigate(typeof(LoginParent));
            }
            else
            {
                Frame toAnalysis = Window.Current.Content as Frame;
                toAnalysis.Navigate(typeof(Analysis));
            }
        }

    }
}
