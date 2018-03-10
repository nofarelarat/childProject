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
    public sealed partial class Registration : Page
    {
        public Registration()
        {
            this.InitializeComponent();
        }

        private async void RegisterAsync(object sender, RoutedEventArgs e)
        {
            //get data from textbox

            string Email = email.Text;
            string firstName = firstname.Text;
            string lastName = lastname.Text;
            string childYear = childyear.Text;
            string gardenName = gardenname.Text;
            string Password = password.Text;
            string Teacher = "Teacher";

            string msg = "";

            ConnectDB db = new ConnectDB();
            msg = await db.ValidateUserAsync(Email, firstName, lastName,
                childYear, gardenName, Password);
            if (!msg.Equals("success"))
            {
                result.Text = "faild: " + msg;
            }
            
            else
            {
                user newUser = new user
                {
                    email = Email,
                    firstname = firstName,
                    lastname = lastName,
                    childyear = int.Parse(childYear),
                    type = Teacher,
                    gardenname = gardenName,
                    password = Password,
                    count_month = 0,
                    count_year = ""
                };

                bool isPass = await db.CreateUserAsync(newUser);

                if (isPass == false)
                {
                    msg = "Can't connect database";
                    result.Text = "faild: " + msg;
                }

                if (isPass == true)
                {
                    result.Text = "success";
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
