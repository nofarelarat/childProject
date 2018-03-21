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

namespace ForChild
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddUsersForChat : Page
    {
        public AddUsersForChat()
        {
            this.InitializeComponent();
        }

        private async void Add_ClickAsync(object sender, RoutedEventArgs e)
        {
            string[] emails = new string[6];
            emails[0] = "Child";
            emails[1] = Common.who_am_i;
            emails[2] = TextBoxMother.Text;
            emails[3] = TextBoxFather.Text;
            emails[4] = TextBoxFriend.Text;
            emails[5] = TextBoxSister.Text;

            bool res = await Common.AddUserChatContact(emails);
            Frame toHome = Window.Current.Content as Frame;
            toHome.Navigate(typeof(MainPage));
        }
        private void Button_Click_back(object sender, RoutedEventArgs e)
        {
            Frame toHome = Window.Current.Content as Frame;
            toHome.Navigate(typeof(MainPage));
        }

    }
}
