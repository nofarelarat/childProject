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

namespace ForChild
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

        private void Button_Click_Friend(object sender, RoutedEventArgs e)
        {
            Frame toFriend = Window.Current.Content as Frame;
            toFriend.Navigate(typeof(FriendPage));
        }

        private void Button_Click_father(object sender, RoutedEventArgs e)
        {
            Frame toFather = Window.Current.Content as Frame;
            toFather.Navigate(typeof(FatherPage));
        }

        private void Button_Click_Sister(object sender, RoutedEventArgs e)
        {
            Frame toFather = Window.Current.Content as Frame;
            toFather.Navigate(typeof(SisterPage));
        }

        private void Button_Click_mother(object sender, RoutedEventArgs e)
        {
            Frame toMather = Window.Current.Content as Frame;
            toMather.Navigate(typeof(MotherPage));
        }

        private void forLogin_Click(object sender, RoutedEventArgs e)
        {
            Frame toMather = Window.Current.Content as Frame;
            toMather.Navigate(typeof(loginChild));
        }
    }
}
