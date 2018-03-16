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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ForParent
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            //start this function in srart world
            //if not working go to login page
            Common.GetUserFromFileAsync();
        }

        private void forChat_Click(object sender, RoutedEventArgs e)
        {
            if (Common.who_am_i == "")
            {
                Frame toLogin = Window.Current.Content as Frame;
                toLogin.Navigate(typeof(LoginParent));
            }
            else
            {
                //if there is no chat email in the local xml
                //file goto add child
                //else check in db the child has the parent
                //in his contact list and then open chat with the child
                Frame toAdd = Window.Current.Content as Frame;
                toAdd.Navigate(typeof(AddChild));
            }
        }

        private void forRegister_Click(object sender, RoutedEventArgs e)
        {
            Frame toRegister = Window.Current.Content as Frame;
            toRegister.Navigate(typeof(Registration));
        }

        private void forLogin_Click(object sender, RoutedEventArgs e)
        {
            Frame toLogin = Window.Current.Content as Frame;
            toLogin.Navigate(typeof(LoginParent));
        }

        private void forAnalysis_Click(object sender, RoutedEventArgs e)
        {
            Frame toAnalysis = Window.Current.Content as Frame;
            toAnalysis.Navigate(typeof(Analysis));
        }
    }
}
