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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ForParent
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginParent : Page
    {
        static string saved_email = "";
        public LoginParent()
        {
            this.InitializeComponent();
        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private async void EnterAppAsync(object sender, RoutedEventArgs e)
        {
            ConnectDB db = new ConnectDB();
            user user = await db.GetUserByMailAsync(email.Text);
            saved_email = user.email;
            if (user == null)
            {
                result.Text = "faild get user";
            }
            else
            {
                if (user.password.Equals(password.Text))
                {
                    result.Text = user.firstname;
                }
                else
                {
                   result.Text = "wrong password";
                }
            }
        }

        private void Register(object sender, RoutedEventArgs e)
        {
            Frame toRegister = Window.Current.Content as Frame;
            toRegister.Navigate(typeof(Registration));
        }

        private void Button_Click_back(object sender, RoutedEventArgs e)
        {
            Frame toHome = Window.Current.Content as Frame;
            toHome.Navigate(typeof(MainPage));
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
