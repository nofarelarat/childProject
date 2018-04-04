using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;


namespace ForChild
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FriendPage : Page
    {
        static Image[] symbolsForSend1 = new Image[5];
        static Image[] symbolsForSend2 = new Image[5];
        static Image[] symbolsForSend3 = new Image[5];
        //index in massege
        static int symbolsForSend_curr1 = 0;
        static int symbolsForSend_curr2 = 0;
        static int symbolsForSend_curr3 = 0;
        // full/open flag
        static int[] symbolsForSend_full = new int[3];

        static Image[] symbolsSentFromOther1 = new Image[5];
        static Image[] symbolsSentFromOther2 = new Image[5];
        static Image[] symbolsSentFromOther3 = new Image[5];

        static int[] symbolsSentFromOther_full = new int[3];
        static bool flag = true;

        public FriendPage()
        {
            this.InitializeComponent();
            InitializeArrays();
            flag = true;
            GetMsgFromFileAsync();
        }
        private void Button_Click_back(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < symbolsForSend1.Length; i++)
            {
                symbolsForSend1[i].Source = null;
                symbolsForSend2[i].Source = null;
                symbolsForSend3[i].Source = null;
                symbolsSentFromOther1[i].Source = null;
                symbolsSentFromOther2[i].Source = null;
                symbolsSentFromOther3[i].Source = null;
            }
            send.Visibility = Windows.UI.Xaml.Visibility.Visible;

            symbolsForSend_curr1 = 0;
            symbolsForSend_curr2 = 0;
            symbolsForSend_curr3 = 0;

            symbolsForSend_full[0] = 0;
            symbolsForSend_full[1] = 0;
            symbolsForSend_full[2] = 0;

            symbolsSentFromOther_full[0] = 0;
            symbolsSentFromOther_full[1] = 0;
            symbolsSentFromOther_full[2] = 0;

            flag = false;
            Frame toHome = Window.Current.Content as Frame;
            toHome.Navigate(typeof(MainPage));
        }
        private void delete_Click(object sender, RoutedEventArgs e)
        {

            for (int i = 0; i < symbolsForSend1.Length; i++)
            {
                symbolsForSend1[i].Source = null;
                symbolsForSend2[i].Source = null;
                symbolsForSend3[i].Source = null;
                symbolsSentFromOther1[i].Source = null;
                symbolsSentFromOther2[i].Source = null;
                symbolsSentFromOther3[i].Source = null;
            }
            send.Visibility = Windows.UI.Xaml.Visibility.Visible;

            symbolsForSend_curr1 = 0;
            symbolsForSend_curr2 = 0;
            symbolsForSend_curr3 = 0;

            symbolsForSend_full[0] = 0;
            symbolsForSend_full[1] = 0;
            symbolsForSend_full[2] = 0;
            Common.DeleteFileAsync("chatWithFriend.txt");
        }
        private async void FriendSendClick(object sender, RoutedEventArgs e)
        {
            int message_num = 0;
            string sentence = "";
            Image[] symbolsForSend = symbolsForSend1;
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
                        Common.UpdateCounterAsync(symbolsForSend1[i].Tag.ToString());
                    }
                    else if (message_num == 2)
                    {
                        sentence = sentence + symbolsForSend2[i].Tag.ToString() + "+";
                        Common.UpdateCounterAsync(symbolsForSend2[i].Tag.ToString());
                    }
                    else if (message_num == 3)
                    {
                        sentence = sentence + symbolsForSend3[i].Tag.ToString() + "+";
                        Common.UpdateCounterAsync(symbolsForSend3[i].Tag.ToString());
                    }
                }
            }
            //To do : sent to is hard coded!!
            Common.sendMsg(sentence, Common.myMother);
            send.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            int message_num = 0;
            string sentence = "";
            Image[] symbolsForSend = symbolsForSend1;
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
                        Common.UpdateCounterAsync(symbolsForSend1[i].Tag.ToString());
                    }
                    else if (message_num == 2)
                    {
                        sentence = sentence + symbolsForSend2[i].Tag.ToString() + "+";
                        Common.UpdateCounterAsync(symbolsForSend2[i].Tag.ToString());
                    }
                    else if (message_num == 3)
                    {
                        sentence = sentence + symbolsForSend3[i].Tag.ToString() + "+";
                        Common.UpdateCounterAsync(symbolsForSend3[i].Tag.ToString());
                    }
                }
            }
            //To do : sent to is hard coded!!
            Common.sendMsg(sentence, Common.myFriend);
            send.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            Common.WriteConversation("child:" + sentence, "chatWithFriend.txt");
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

        private void GetMessageImg(Image[] symbolsSentFromOther)
        {
            if (symbolsSentFromOther_full[0] == 0)
            {
                for (int i = 0; i < symbolsSentFromOther1.Length; i++)
                {
                    symbolsSentFromOther1[i].Source = symbolsSentFromOther[i].Source;
                }
                symbolsSentFromOther_full[0] = 1;
                send.Visibility = Windows.UI.Xaml.Visibility.Visible;

            }
            else if (symbolsSentFromOther_full[1] == 0)
            {
                for (int i = 0; i < symbolsSentFromOther2.Length; i++)
                {
                    symbolsSentFromOther2[i].Source = symbolsSentFromOther[i].Source;
                }
                symbolsSentFromOther_full[1] = 1;
                send.Visibility = Windows.UI.Xaml.Visibility.Visible;


            }
            else if (symbolsSentFromOther_full[2] == 0)
            {
                for (int i = 0; i < symbolsSentFromOther3.Length; i++)
                {
                    symbolsSentFromOther3[i].Source = symbolsSentFromOther[i].Source;
                }
                symbolsSentFromOther_full[2] = 1;
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

        private async Task GetMessageAsync(OutTable[] message)
        {
            Image[] images = new Image[5];

            int numofmsg = 0; //the number of messages cant be more than 3.
                              //TODO : add to the if 
            if (message != null && message.Length > 0)
            {
                for (int x = 0; x < message.Length; x++)
                {
                    int i = 0;
                    numofmsg++;
                    string msg = message[x].Message;
                    await Common.WriteConversation("friend:" + msg, "chatWithFriend.txt");
                    string[] tmp = msg.Split('-');
                    foreach (string source in tmp)
                    {
                        if (i >= 5)
                            break;
                        Uri requestUri = new Uri(base.BaseUri, "/symbols/" + source + ".png");
                        images[i] = new Image();
                        images[i].Source = new BitmapImage(requestUri);
                        i++;
                    }
                    GetMessageImg(images);
                    if (numofmsg == 3)
                    {
                        return;
                    }//if
                }
                for (int x = 0; x < message.Length; x++)
                {
                    await Common.markAsDeleteMsg(message[x]);
                }
            }
        }

        private async Task GetMsgFromFriend()
        {
            while (flag)
            {
                OutTable[] table = await Common.GetMsgAsync(Common.myFriend);
                await GetMessageAsync(table);
            }

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

            symbolsSentFromOther1[0] = afterSend11;
            symbolsSentFromOther1[1] = afterSend12;
            symbolsSentFromOther1[2] = afterSend13;
            symbolsSentFromOther1[3] = afterSend14;
            symbolsSentFromOther1[4] = afterSend15;

            symbolsSentFromOther2[0] = afterSend21;
            symbolsSentFromOther2[1] = afterSend22;
            symbolsSentFromOther2[2] = afterSend23;
            symbolsSentFromOther2[3] = afterSend24;
            symbolsSentFromOther2[4] = afterSend25;

            symbolsSentFromOther3[0] = afterSend31;
            symbolsSentFromOther3[1] = afterSend32;
            symbolsSentFromOther3[2] = afterSend33;
            symbolsSentFromOther3[3] = afterSend34;
            symbolsSentFromOther3[4] = afterSend35;
        }

        private async void GetMsgFromFileAsync()
        {
            GetMsgFromFriend();
            string res = await Common.ReadConversation("chatWithFriend.txt");
            if (!res.Equals(""))
            {
                res = res + "-";
                string[] messages = res.Split('\r', '\n');
                for (int i = 0; i < messages.Length; i++)
                {
                    string[] message = messages[i].Split(':', '-');
                    Image[] images = new Image[5];

                    for (int j = 0; j < images.Length; j++)
                    {
                        Uri requestUri = new Uri(base.BaseUri, "/symbols/" + ".png");

                        if (j + 1 < message.Length)
                        {
                            requestUri = new Uri(base.BaseUri, "/symbols/" + message[j + 1] + ".png");
                        }
                        images[j] = new Image();
                        images[j].Source = new BitmapImage(requestUri);

                    }
                    if (message[0].Equals("friend"))
                    {
                        GetMessageImg(images);
                    }
                    if (message[0].Equals("child"))
                    {
                        GetSentMessage(images);
                    }

                    if (symbolsForSend_full.Sum() > symbolsSentFromOther_full.Sum())
                    {
                        send.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                    }
                }
            }
        }

    }
}

