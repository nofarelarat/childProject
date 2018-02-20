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

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string[] emails = new string[7];
            emails[0] = "child";
            emails[1] = Common.who_am_i;
            emails[2] = TextBoxMother.Text;
            emails[3] = TextBoxFather.Text;
            emails[4] = TextBoxFriend.Text;
            emails[5] = TextBoxSister.Text;

            //Common.AddUsersToDB(emails);
        }
    }
}
