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
    public sealed partial class Registration : Page
    {
        public Registration()
        {
            this.InitializeComponent();
        }

        private async void RegisterAsync(object sender, RoutedEventArgs e)
        {
            //get data from textbox
            string email;
            string firstName;
            string lastName;
            string childAge;
            bool childCheckBox;
            string gardenName;
            string password;
            string birthDate;
            string msg = "";

            DateTime dt = Convert.ToDateTime(birthDate);

            ConnectDB db = new ConnectDB();
            bool isPass = db.ValidateUser(email,firstName,lastName,dt,childAge,childCheckBox, gardenName
                ,password,out msg);
            if (isPass == false)
            {
                //textbox = faild + msg
            }
            else
            {
                user newUser = new user
                {
                    email = email,
                    firstname = firstName,
                    lastname = lastName,
                    childage = childAge,
                    childcheckbox = childCheckBox,
                    gardenname = gardenName,
                    password = password,
                    birthdate = dt
                };

                isPass = await db.CreateUser(newUser);

                if (isPass == false)
                {
                    //textbox = faild + msg
                }
                if (isPass == false)
                {
                    //textbox = success
                }
            }

        }

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

    }
}
