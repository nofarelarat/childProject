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
            forLogout.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            forLogin.Visibility = Windows.UI.Xaml.Visibility.Visible;
            if (Common.isConectet == false)
            {
                CheckUserExistAsync();
                Frame toLogin = Window.Current.Content as Frame;
                toLogin.Navigate(typeof(loginChild));
            }
            else
            {
                forLogin.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                forLogout.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }

        }
        private async void Button_Click_FriendAsync(object sender, RoutedEventArgs e)
        {
            bool isSimilar = false;
            if (Common.who_am_i.Equals(""))
            {
                todo.Text= "please log-in";
                Frame toLogin = Window.Current.Content as Frame;
                toLogin.Navigate(typeof(loginChild));
            }
            else
            {
                if (Common.myFriend.Equals(""))
                {
                    todo.Text = "please add friend";
                }
                isSimilar = await Common.CheckForSimilarFriendAsync();
                if (isSimilar == true)
                {
                    Frame toFriend = Window.Current.Content as Frame;
                    toFriend.Navigate(typeof(FriendPage));
                }
                else {
                    todo.Text = "you have to be registerd at yout friend's app";

                }
            }
        }

        private void Button_Click_father(object sender, RoutedEventArgs e)
        {
            if (Common.who_am_i.Equals(""))
            {
                todo.Text = "please log-in";
                Frame toLogin = Window.Current.Content as Frame;
                toLogin.Navigate(typeof(loginChild));
            }
            else
            {
                if (!Common.myFather.Equals(""))
                {
                    Frame toFather = Window.Current.Content as Frame;
                    toFather.Navigate(typeof(FatherPage));
                }
                else {
                    todo.Text = "please add father";

                }
            }
        }

        private void Button_Click_mother(object sender, RoutedEventArgs e)
        {
            if (Common.who_am_i.Equals(""))
            {
                todo.Text = "please log-in";
                Frame toLogin = Window.Current.Content as Frame;
                toLogin.Navigate(typeof(loginChild));
            }
            else
            {
                if (!Common.myMother.Equals(""))
                {
                    Frame toMather = Window.Current.Content as Frame;
                    toMather.Navigate(typeof(MotherPage));
                }
                else {
                    todo.Text = "please add mother";
                }
            }
        }

        private void Button_Click_Teacher(object sender, RoutedEventArgs e)
        {
            if (Common.who_am_i.Equals(""))
            {
                todo.Text = "please log-in";
                Frame toLogin = Window.Current.Content as Frame;
                toLogin.Navigate(typeof(loginChild));
            }
            else
            {
                if (!Common.myTeacher.Equals(""))
                {
                    Frame toTeacher = Window.Current.Content as Frame;
                    toTeacher.Navigate(typeof(TeacherPage));
                }
            }
        }

        private async void forLogin_ClickAsync(object sender, RoutedEventArgs e)
        {
            if (Common.who_am_i.Equals(""))
            {
                Frame toMather = Window.Current.Content as Frame;
                toMather.Navigate(typeof(loginChild));
            }
        }

        private async void forLogOut_ClickAsync(object sender, RoutedEventArgs e)
        {
                await Common.DeleteFileAsync("userChild.txt");
                await Common.DeleteFileAsync("chatWithFriend.txt");
                await Common.DeleteFileAsync("chatWithMother.txt");
                await Common.DeleteFileAsync("chatWithFather.txt");
                Common.who_am_i = "";
                Common.myFather = "";
                Common.myMother = "";
                Common.myFriend = "";
                Common.myTeacher = "";
                Common.isConectet = false;
                // await Common.DeleteFileAsync(); //Need to add the Father file

                Frame toMather = Window.Current.Content as Frame;
                toMather.Navigate(typeof(loginChild));
        }

        private void Button_Click_Plus(object sender, RoutedEventArgs e)
        {
            if (Common.who_am_i.Equals(""))
            {
                Frame toLogin = Window.Current.Content as Frame;
                toLogin.Navigate(typeof(loginChild));
            }
            else
            { 
                Frame toAddUsersForChat = Window.Current.Content as Frame;
                toAddUsersForChat.Navigate(typeof(AddUsersForChat));
            }
        }
        private async void CheckUserExistAsync()
        {
            bool success = await Common.GetUserFromFileAsync();
            if (success == false)
            {
                forLogout.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                forLogin.Visibility = Windows.UI.Xaml.Visibility.Visible;

                Frame toLogin = Window.Current.Content as Frame;
                toLogin.Navigate(typeof(loginChild));
            }
            else
            {
                forLogout.Visibility = Windows.UI.Xaml.Visibility.Visible;
                forLogin.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            }
        }

    }
}
