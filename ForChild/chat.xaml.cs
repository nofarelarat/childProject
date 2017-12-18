using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Threading.Tasks;
//using System.Windows.Documents;




// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ForChild
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class chat : Page
    {
        public chat()
        {
            this.InitializeComponent();
        }

        private void TextBlock_SelectionChanged(System.Object sender, RoutedEventArgs e)
        {

        }

        private async void Button_Click(System.Object sender, RoutedEventArgs e) //HERE???
        {
            // TextRange MyChatMyText = new TextRange(You.Document.ContentEnd, You.Document.ContenEnd);
            ConnectServiceBus obj = new ConnectServiceBus();
            int status = await obj.sendMSG("testmsg");
            if (status != 200 & status !=204)
            {
                //ERROR MSG!!
            }
            
        }
             
    }
}
