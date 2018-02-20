using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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
            
            string Email = email.Text;
            string firstName = firstname.Text;
            string lastName = lastname.Text;
            string childYear = childyear.Text;
            bool childCheckBox = (bool)Ischild.IsChecked;
            string gardenName = gardenname.Text;
            string Password = password.Text;
            string type = "Parent";
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
                if (childCheckBox == true)
                {
                    type = "Child";
                }

                user newUser = new user
                {
                    email = Email,
                    firstname = firstName,
                    lastname = lastName,
                    childyear = int.Parse(childYear),
                    type = type,
                    gardenname = gardenName,
                    password = Password,
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
