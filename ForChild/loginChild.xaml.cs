using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ForChild
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class loginChild : Page
    {
        public loginChild()
        {
            this.InitializeComponent();
        }
        
        private async void EnterAppAsync(object sender, RoutedEventArgs e)
        {
            string[] lines = System.IO.File.ReadAllLines(@"config.txt");
            
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
                     Common.who_am_i = user.email;
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
