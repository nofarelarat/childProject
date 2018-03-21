using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage.Auth;


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

        private async void Button_Click_FriendAsync(object sender, RoutedEventArgs e)
        {
            bool isSimilar = false;
            if (Common.who_am_i.Equals(""))
            {
                Frame toMather = Window.Current.Content as Frame;
                toMather.Navigate(typeof(loginChild));
            }
            else
            {
                isSimilar = await Common.CheckForSimilarFriendAsync();
                if (isSimilar == true)
                {
                    Frame toFriend = Window.Current.Content as Frame;
                    toFriend.Navigate(typeof(FriendPage));
                }
            }
        }

        private void Button_Click_father(object sender, RoutedEventArgs e)
        {
            if (Common.who_am_i.Equals(""))
            {
                Frame toMather = Window.Current.Content as Frame;
                toMather.Navigate(typeof(loginChild));
            }
            else
            {
                if (!Common.myFather.Equals(""))
                {
                    Frame toFather = Window.Current.Content as Frame;
                    toFather.Navigate(typeof(FatherPage));
                }
            }
        }

        private void Button_Click_Sister(object sender, RoutedEventArgs e)
        {
            if (Common.who_am_i.Equals(""))
            {
                Frame toMather = Window.Current.Content as Frame;
                toMather.Navigate(typeof(loginChild));
            }
            else
            {
                if (!Common.mySister.Equals(""))
                {
                    Frame toFather = Window.Current.Content as Frame;
                    toFather.Navigate(typeof(SisterPage));
                }
            }
        }

        private void Button_Click_mother(object sender, RoutedEventArgs e)
        {
            if (Common.who_am_i.Equals(""))
            {
                Frame toMather = Window.Current.Content as Frame;
                toMather.Navigate(typeof(loginChild));
            }
            else
            {
                if (!Common.myMother.Equals(""))
                {
                    Frame toMather = Window.Current.Content as Frame;
                    toMather.Navigate(typeof(MotherPage));
                }
            }
        }

        private void forLogin_Click(object sender, RoutedEventArgs e)
        {
            Frame toMather = Window.Current.Content as Frame;
            toMather.Navigate(typeof(loginChild));
        }

        private async void test_ClickAsync(object sender, RoutedEventArgs e)
        {
            //ConnectDB db = new ConnectDB();
            //db.TestDelete("osnat@gmail.con");
            //db.GetGardenChildren("flowers");
            //Common.UpdateCounterAsync("iagree");
            Common.GetMsgAsync();
            //Common.GetUserContactsAsync("rami@gmail.com");
        }

        private void Button_Click_Plus(object sender, RoutedEventArgs e)
        {
            if (Common.who_am_i.Equals(""))
            {
                Frame toMather = Window.Current.Content as Frame;
                toMather.Navigate(typeof(loginChild));
            }
            else
            { 
                Frame toAddUsersForChat = Window.Current.Content as Frame;
                toAddUsersForChat.Navigate(typeof(AddUsersForChat));
            }
        }
      

    }
}
