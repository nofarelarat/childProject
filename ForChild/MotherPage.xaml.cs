using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ForChild
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MotherPage : Page
    {
        static int symbolsForSend_curr = 0;
        static string[] symbolsForSend = new string[5];
        public MotherPage()
        {
            this.InitializeComponent();
        }
        private void Button_Click_back(object sender, RoutedEventArgs e)
        {
            symbolsForSend_curr = 0;
            Frame toHome = Window.Current.Content as Frame;
            toHome.Navigate(typeof(MainPage));
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            if (symbolsForSend_curr != 0)
            {
                symbolsForSend_curr = 0;
                afterSend1.Source = forSend1.Source;
                afterSend2.Source = forSend2.Source;
                afterSend3.Source = forSend3.Source;
                afterSend4.Source = forSend4.Source;
                afterSend5.Source = forSend5.Source;
                forSend1.Source = blank.Source;
                forSend2.Source = blank.Source;
                forSend3.Source = blank.Source;
                forSend4.Source = blank.Source;
                forSend5.Source = blank.Source;
                Common.UpdateCounterAsync(forSend1.Tag.ToString());
                Common.UpdateCounterAsync(forSend2.Tag.ToString());
                Common.UpdateCounterAsync(forSend3.Tag.ToString());
                Common.UpdateCounterAsync(forSend4.Tag.ToString());
                Common.UpdateCounterAsync(forSend5.Tag.ToString());
                forSend1.Tag = blank.Tag;
                forSend2.Tag = blank.Tag;
                forSend3.Tag = blank.Tag;
                forSend4.Tag = blank.Tag;
                forSend5.Tag = blank.Tag;
            }
        }
        private void Symbol_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string button_name = button.Name;
            if (symbolsForSend_curr < 5)
            {
                AddToForSendGrid(button_name);
            }
        }
        private void AddToForSendGrid(string button_name)
        {
            Uri requestUri = new Uri(base.BaseUri, "/symbols/"+button_name+".png");
            
            switch (symbolsForSend_curr)
            {
                case (0):
                        forSend1.Source = new BitmapImage(requestUri);
                        forSend1.Tag = button_name;
                        symbolsForSend_curr++;
                        break;
                case (1):
                        forSend2.Source = new BitmapImage(requestUri);
                        forSend2.Tag = button_name;
                        symbolsForSend_curr++;
                        break;
                case (2):
                        forSend3.Source = new BitmapImage(requestUri);
                        forSend3.Tag = button_name;
                        symbolsForSend_curr++;
                        break;
                case (3):
                        forSend4.Source = new BitmapImage(requestUri);
                        forSend1.Tag = button_name;
                        symbolsForSend_curr++;
                        break;
                case (4):
                        forSend5.Source = new BitmapImage(requestUri);
                        forSend1.Tag = button_name;
                        symbolsForSend_curr++;
                        break;
            }
            

        }
    }
}
