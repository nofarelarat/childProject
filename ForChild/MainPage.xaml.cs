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

        //noy added need to check
        private async void get_msg()
        {
            //Create an HTTP client object
            HttpClient httpClient = new HttpClient();
            Uri requestUri = new Uri("https://functionqueuecob099.table.core.windows.net/outTable?$filter=Message_Send%20eq%" + loginChild.who_am_i);

            ////Send the GET request asynchronously and retrieve the response as a string.
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            string httpResponseBody = "";

            try
            {
                //Send the GET request
                httpResponse = await httpClient.GetAsync(requestUri);
                httpResponse.EnsureSuccessStatusCode();
                httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                string[] userMsg = JsonConvert.DeserializeObject<string[]>(httpResponseBody);

            }

            catch (Exception ex)
            {
                httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
            }

        }
        private void test_Click(object sender, RoutedEventArgs e)
        {
            //ConnectDB db = new ConnectDB();
            //db.TestDelete("osnat@gmail.con");
            //db.GetGardenChildren("flowers");
            //Common.UpdateCounterAsync("iagree");
            //Common.GetUserCounterAsync("iagree");
        }

        private void Button_Click_Plus(object sender, RoutedEventArgs e)
        {
            Frame toAddUsersForChat = Window.Current.Content as Frame;
            toAddUsersForChat.Navigate(typeof(AddUsersForChat));
        }       

    }
}
