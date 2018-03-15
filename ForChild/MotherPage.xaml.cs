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
        static Image[] symbolsForSend = new Image[5];
        static Image[] symbolsAfterSend = new Image[5];
        public MotherPage()
        {
            this.InitializeComponent();
            symbolsForSend[0] = forSend1;
            symbolsForSend[1] = forSend2;
            symbolsForSend[2] = forSend3;
            symbolsForSend[3] = forSend4;
            symbolsForSend[4] = forSend5;
            symbolsAfterSend[0] = afterSend1;
            symbolsAfterSend[1] = afterSend2;
            symbolsAfterSend[2] = afterSend3;
            symbolsAfterSend[3] = afterSend4;
            symbolsAfterSend[4] = afterSend5;

        }
        private void Button_Click_back(object sender, RoutedEventArgs e)
        {
            symbolsForSend_curr = 0;
            Frame toHome = Window.Current.Content as Frame;
            toHome.Navigate(typeof(MainPage));
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            string sentence = "";
            if (symbolsForSend_curr != 0)
            {
                symbolsForSend_curr = 0;
                for (int i=0; i < symbolsAfterSend.Length; i++)
                {
                    symbolsAfterSend[i].Source = symbolsForSend[i].Source;
                    sentence = sentence + symbolsForSend[i].Tag.ToString() + "+";
                    Common.UpdateCounterAsync(symbolsForSend[i].Tag.ToString());
                    symbolsForSend[i].Source = blank.Source;
                    symbolsForSend[i].Tag = blank.Tag;
                }
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
                        symbolsForSend[0].Source = new BitmapImage(requestUri);
                        symbolsForSend[0].Tag = button_name;
                        symbolsForSend_curr++;
                        break;
                case (1):
                        symbolsForSend[1].Source = new BitmapImage(requestUri);
                        symbolsForSend[1].Tag = button_name;
                        symbolsForSend_curr++;
                        break;
                case (2):
                        symbolsForSend[2].Source = new BitmapImage(requestUri);
                        symbolsForSend[2].Tag = button_name;
                        symbolsForSend_curr++;
                        break;
                case (3):
                        symbolsForSend[3].Source = new BitmapImage(requestUri);
                        symbolsForSend[3].Tag = button_name;
                        symbolsForSend_curr++;
                        break;
                case (4):
                        symbolsForSend[4].Source = new BitmapImage(requestUri);
                        symbolsForSend[4].Tag = button_name;
                        symbolsForSend_curr++;
                        break;
            }
        }
    }
}
