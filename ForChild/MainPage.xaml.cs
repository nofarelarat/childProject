using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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
            CountForAnalyzeAsync();
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
        
        private async void CountForAnalyzeAsync()
        {
            if (loginChild.who_am_i == "")
            {
                Frame toMather = Window.Current.Content as Frame;
                toMather.Navigate(typeof(loginChild));
            }
            int month;
            if (DateTime.Now.Month == 1)
                month = 12;
            else
                month = DateTime.Now.Month - 1;
            if (DateTime.Now.Day == 1)
            {
                //change to static email - noy
                string email = loginChild.who_am_i;
                ConnectDB db = new ConnectDB();
                user user = await db.GetUserByMailAsync(email);
                int countMonth = (int)user.count_month;
                user.count_year = user.count_year + "_" + countMonth + "-" + (DateTime.Now.Month - 1);
                user.count_month = 0;
                Counters counts = new Counters
                {
                    email = user.email,
                    countUpdate = countMonth,
                    countYearUpdate = user.count_year
                };
                db.UpdateUserCountersAsync(counts);
            }
        }
    }
}
