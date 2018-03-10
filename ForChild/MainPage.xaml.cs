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
        const String StorageAccountName = "functionqueuecob099";
        const String StorageAccountKey = "0/gynCIsiLI5IxzrQUAgAUtPiAzVrfetdrrXCvHPWM972iT9fdA5JbBqgZkbJIY37AgzMiwMpdBdB/Jcvu+6aQ==";
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

        private async void test_ClickAsync(object sender, RoutedEventArgs e)
        {
            //ConnectDB db = new ConnectDB();
            //db.TestDelete("osnat@gmail.con");
            //db.GetGardenChildren("flowers");
            //Common.UpdateCounterAsync("iagree");
            //Common.GetUserCounterAsync("iagree");
            TableQuerySegment<OutTable> x = await Common.GetMsgAsync();
        }

        private void Button_Click_Plus(object sender, RoutedEventArgs e)
        {
            Frame toAddUsersForChat = Window.Current.Content as Frame;
            toAddUsersForChat.Navigate(typeof(AddUsersForChat));
        }
      

    }
}
