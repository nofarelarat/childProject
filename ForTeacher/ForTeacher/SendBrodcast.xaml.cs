using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Net.Http;
using Windows.UI.Xaml.Media.Imaging;

namespace ForTeacher
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SendBrodcast : Page
    {
        static bool flag = true;
        static int symbolsForSend_curr = 0;
        static Image[] symbolsForSend = new Image[5];

        public SendBrodcast()
        {
            this.InitializeComponent();
            symbolsForSend[0] = forSend11;
            symbolsForSend[1] = forSend12;
            symbolsForSend[2] = forSend13;
            symbolsForSend[3] = forSend14;
            symbolsForSend[4] = forSend15;

        }
        private void Button_Click_back(object sender, RoutedEventArgs e)
        {
            Frame toHome = Window.Current.Content as Frame;
            toHome.Navigate(typeof(MainPage));
        }
        private async void SendClick(object sender, RoutedEventArgs e)
        {

            // Create final message
            string sendMsg = GetMsg();
            if (sendMsg != "")
            {
                String final_msg = sendMsg + "$" + Common.who_am_i + "$";
                // Get Garden children
                user[] kids = Common.gardenChildren;
                for (int i = 0; i < kids.Length; i++)
                {
                    if (kids[i].type != "Teacher" && kids[i].type != "Parent")
                    {
                        //Create an HTTP client object
                        HttpClient httpClient = new HttpClient();
                        Uri requestUri = new Uri("https://function-queue-connect.azurewebsites.net/api/HttpTriggerCSharp1-send?code=c4TP96qDiVU6X5Zd6HNmAOCOIp35R52MB0MZnL6GRjY8ldfF2GqZ3A==&&name=" + final_msg + kids[i].email);
                        //Send the GET request asynchronously and retrieve the response as a string.
                        HttpResponseMessage httpResponse = new HttpResponseMessage();
                        try
                        {
                            //Send the GET request
                            httpResponse = await httpClient.GetAsync(requestUri);
                            httpResponse.EnsureSuccessStatusCode();
                            // errormessage.Text = await response.Content.ReadAsStringAsync();
                        }
                        catch (Exception ex)
                        {

                            //  errormessage.Text = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
                        }
                    }
                }
            }
        }

        private string GetMsg()
        {
            string sentence = "";
            if (symbolsForSend_curr != 0)
            {
                symbolsForSend_curr = 0;
                for (int i = 0; i < symbolsForSend.Length; i++)
                {
                    sentence = sentence + symbolsForSend[i].Tag.ToString() + "-";
                    symbolsForSend[i].Source = blank.Source;
                    symbolsForSend[i].Tag = blank.Tag;
                }
            }
            return sentence;
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
            Uri requestUri = new Uri(base.BaseUri, "/symbols/" + button_name + ".png");

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