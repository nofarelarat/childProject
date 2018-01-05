using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ForChild
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class loginChild : Page
    {
        public static string who_am_i = "";
        public loginChild()
        {
            this.InitializeComponent();
        }
        
        private async void EnterAppAsync(object sender, RoutedEventArgs e)
        {
            ConnectDB db = new ConnectDB();
            user user = await db.GetUserByMailAsync(email.Text);
            if (user == null)
            {
                result.Text = "user doesn't exists";
            }
            else
            {
                if (user.password.Equals(password.Text))
                {
                    result.Text ="welcome "+ user.firstname+ "!";
                     who_am_i = user.email;
                }
                else
                {
                    result.Text = "incorrect password";
                }
            }
        }

        private void Button_Click_back(object sender, RoutedEventArgs e)
        {
            Frame toHome = Window.Current.Content as Frame;
            toHome.Navigate(typeof(MainPage));
        }
        
    }
}
