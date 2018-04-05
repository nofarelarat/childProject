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
        static Image[] symbolsForSend1 = new Image[5];
        static Image[] symbolsForSend2 = new Image[5];
        static Image[] symbolsForSend3 = new Image[5];
        //index in massege
        static int symbolsForSend_curr1 = 0;
        static int symbolsForSend_curr2 = 0;
        static int symbolsForSend_curr3 = 0;
        // full/open flag
        static int[] symbolsForSend_full = new int[3];


        public SendBrodcast()
        {
            this.InitializeComponent();
            InitializeArrays();


        }
        private void InitializeArrays()
        {
            symbolsForSend1[0] = forSend11;
            symbolsForSend1[1] = forSend12;
            symbolsForSend1[2] = forSend13;
            symbolsForSend1[3] = forSend14;
            symbolsForSend1[4] = forSend15;

            symbolsForSend2[0] = forSend21;
            symbolsForSend2[1] = forSend22;
            symbolsForSend2[2] = forSend23;
            symbolsForSend2[3] = forSend24;
            symbolsForSend2[4] = forSend25;

            symbolsForSend3[0] = forSend31;
            symbolsForSend3[1] = forSend32;
            symbolsForSend3[2] = forSend33;
            symbolsForSend3[3] = forSend34;
            symbolsForSend3[4] = forSend35;
        }

        private void Button_Click_back(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < symbolsForSend1.Length; i++)
            {
                symbolsForSend1[i].Source = null;
                symbolsForSend2[i].Source = null;
                symbolsForSend3[i].Source = null;
            }
            send.Visibility = Windows.UI.Xaml.Visibility.Visible;

            symbolsForSend_curr1 = 0;
            symbolsForSend_curr2 = 0;
            symbolsForSend_curr3 = 0;

            symbolsForSend_full[0] = 0;
            symbolsForSend_full[1] = 0;
            symbolsForSend_full[2] = 0;

            Frame toHome = Window.Current.Content as Frame;
            flag = false;
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
            int message_num = 0;
            string sentence = "";
            int fullFlag = 0;
            if (symbolsForSend_full[0] != 0)
            {
                if (symbolsForSend_full[1] == 0)
                {
                    message_num = 2;
                    symbolsForSend_full[1] = 1;
                }
                else if (symbolsForSend_full[2] == 0)
                {
                    message_num = 3;
                    symbolsForSend_full[2] = 1;
                }
                else
                {//all full
                    fullFlag = 1;
                }
            }
            else
            {
                message_num = 1;
                symbolsForSend_full[0] = 1;
            }
            if (fullFlag == 0)
            {
                for (int i = 0; i < symbolsForSend1.Length; i++)
                {
                    if (message_num == 1)
                    {
                        sentence = sentence + symbolsForSend1[i].Tag.ToString() + "+";
                    }
                    else if (message_num == 2)
                    {
                        sentence = sentence + symbolsForSend2[i].Tag.ToString() + "+";
                    }
                    else if (message_num == 3)
                    {
                        sentence = sentence + symbolsForSend3[i].Tag.ToString() + "+";
                    }
                }
            }
            return sentence;
        }

        private void Symbol_Click(object sender, RoutedEventArgs e)
        {
            int symbolsForSend_messege = -1;
            if (symbolsForSend_full[0] != 0)
            {
                if (symbolsForSend_full[1] != 0)
                {
                    if (symbolsForSend_full[2] == 0)
                    {
                        symbolsForSend_messege = 3;
                    }
                }
                else
                {
                    symbolsForSend_messege = 2;
                }
            }
            else
            {
                symbolsForSend_messege = 1;
            }
            Button button = (Button)sender;
            string button_name = button.Name;
            if (symbolsForSend_messege != -1)
            {
                AddToForSendGrid(button_name, symbolsForSend_messege);
            }
        }

        private void AddToForSendGrid(string button_name, int symbolsForSend_curr)
        {
            Uri requestUri = new Uri(base.BaseUri, "/symbols/" + button_name + ".png");
            if (symbolsForSend_curr == 1 && symbolsForSend_curr1 < 5)
            {
                symbolsForSend1[symbolsForSend_curr1].Source = new BitmapImage(requestUri);
                symbolsForSend1[symbolsForSend_curr1].Tag = button_name;
                symbolsForSend_curr1++;
            }
            if (symbolsForSend_curr == 2 && symbolsForSend_curr2 < 5)
            {
                symbolsForSend2[symbolsForSend_curr2].Source = new BitmapImage(requestUri);
                symbolsForSend2[symbolsForSend_curr2].Tag = button_name;
                symbolsForSend_curr2++;
            }
            if (symbolsForSend_curr == 3 && symbolsForSend_curr3 < 5)
            {
                symbolsForSend3[symbolsForSend_curr3].Source = new BitmapImage(requestUri);
                symbolsForSend3[symbolsForSend_curr3].Tag = button_name;
                symbolsForSend_curr3++;
            }
        }

        private void GetSentMessage(Image[] symbolsSent)
        {
            if (symbolsForSend_full[0] == 0)
            {
                for (int i = 0; i < symbolsForSend1.Length; i++)
                {
                    symbolsForSend1[i].Source = symbolsSent[i].Source;
                }
                symbolsForSend_full[0] = 1;
            }
            else if (symbolsForSend_full[1] == 0)
            {
                for (int i = 0; i < symbolsForSend2.Length; i++)
                {
                    symbolsForSend2[i].Source = symbolsSent[i].Source;
                }
                symbolsForSend_full[1] = 1;
            }
            else if (symbolsForSend_full[2] == 0)
            {
                for (int i = 0; i < symbolsForSend3.Length; i++)
                {
                    symbolsForSend3[i].Source = symbolsSent[i].Source;
                }
                symbolsForSend_full[2] = 1;
            }
        }
        private void delete_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < symbolsForSend1.Length; i++)
            {
                symbolsForSend1[i].Source = null;
                symbolsForSend2[i].Source = null;
                symbolsForSend3[i].Source = null;
            }
            send.Visibility = Windows.UI.Xaml.Visibility.Visible;

            symbolsForSend_curr1 = 0;
            symbolsForSend_curr2 = 0;
            symbolsForSend_curr3 = 0;

            symbolsForSend_full[0] = 0;
            symbolsForSend_full[1] = 0;
            symbolsForSend_full[2] = 0;

            Common.DeleteFileAsync("chatWithChild.txt");
        }

    }
}
