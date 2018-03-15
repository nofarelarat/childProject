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

namespace ForTeacher
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginTeacher : Page
    {
        public static string who_am_i = "";

        public LoginTeacher()
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
                if (user.password.Equals(password.Text) && user.type.Equals("Teacher"))
                {
                    result.Text = "welcome " + user.firstname + "!";
                    who_am_i = email.Text;
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
