﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;


namespace ForChild
{
    public sealed partial class FriendPage : Page
    {
        static Image[] symbolsForSend1 = new Image[5];
        static Image[] symbolsForSend2 = new Image[5];
        static Image[] symbolsForSend3 = new Image[5];
        static Image[] symbolsForSend4 = new Image[5];

        //index in massege
        static int symbolsForSend_curr1 = 0;
        static int symbolsForSend_curr2 = 0;
        static int symbolsForSend_curr3 = 0;
        static int symbolsForSend_curr4 = 0;

        // full/open flag
        static int[] symbolsForSend_full = new int[4];

        static Image[] symbolsSentFromOther1 = new Image[5];
        static Image[] symbolsSentFromOther2 = new Image[5];
        static Image[] symbolsSentFromOther3 = new Image[5];

        static int[] symbolsSentFromOther_full = new int[3];
        static bool flag = true;
        public static bool iStarted = true;

        public FriendPage()
        {
            this.InitializeComponent();
            InitializeArrays();
            delete_all.Visibility = Visibility.Collapsed;
            flag = true;
            Common.my_num_of_msg = 0;
            GetMsgFromFileAsync();
        }

        private void Button_Click_back(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < symbolsForSend1.Length; i++)
            {
                symbolsForSend1[i].Source = null;
                symbolsForSend2[i].Source = null;
                symbolsForSend3[i].Source = null;
                symbolsForSend4[i].Source = null;

                symbolsSentFromOther1[i].Source = null;
                symbolsSentFromOther2[i].Source = null;
                symbolsSentFromOther3[i].Source = null;
            }
            send.Visibility = Visibility.Visible;

            symbolsForSend_curr1 = 0;
            symbolsForSend_curr2 = 0;
            symbolsForSend_curr3 = 0;
            symbolsForSend_curr4 = 0;


            symbolsForSend_full[0] = 0;
            symbolsForSend_full[1] = 0;
            symbolsForSend_full[2] = 0;
            symbolsForSend_full[3] = 0;

            symbolsSentFromOther_full[0] = 0;
            symbolsSentFromOther_full[1] = 0;
            symbolsSentFromOther_full[2] = 0;

            flag = false;
            Frame toHome = Window.Current.Content as Frame;
            toHome.Navigate(typeof(MainPage));
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            flag = false;
            Common.my_num_of_msg = 0;
            iStarted = true;

            for (int i = 0; i < symbolsForSend1.Length; i++)
            {
                symbolsForSend1[i].Source = null;
                symbolsForSend2[i].Source = null;
                symbolsForSend3[i].Source = null;
                symbolsForSend4[i].Source = null;

                symbolsSentFromOther1[i].Source = null;
                symbolsSentFromOther2[i].Source = null;
                symbolsSentFromOther3[i].Source = null;
                symbolsForSend1[i].Tag = "";
                symbolsForSend2[i].Tag = "";
                symbolsForSend3[i].Tag = "";
                symbolsForSend4[i].Tag = "";

                symbolsSentFromOther1[i].Tag = "";
                symbolsSentFromOther2[i].Tag = "";
                symbolsSentFromOther3[i].Tag = "";
            }
            send.Visibility = Visibility.Visible;

            symbolsForSend_curr1 = 0;
            symbolsForSend_curr2 = 0;
            symbolsForSend_curr3 = 0;
            symbolsForSend_curr4 = 0;

            symbolsForSend_full[0] = 0;
            symbolsForSend_full[1] = 0;
            symbolsForSend_full[2] = 0;
            symbolsForSend_full[3] = 0;

            symbolsSentFromOther_full[0] = 0;
            symbolsSentFromOther_full[1] = 0;
            symbolsSentFromOther_full[2] = 0;

            Common.DeleteFileAsync("chatWithFriend.txt");
            flag = true;
            GetMsgFromFriend();
            delete_all.Visibility = Visibility.Collapsed;
        }

        private async void Send_Click(object sender, RoutedEventArgs e)
        {
            Common.my_num_of_msg++;
            int index = Common.my_num_of_msg;
            string sentence = "";
            if (iStarted == false)
            {
                index += 1;
            }
            symbolsForSend_full[index-1] = 1;
            if (Common.my_num_of_msg <= 3)
            {
                for (int i = 0; i < symbolsForSend1.Length; i++)
                {
                    if (index == 1)
                    {
                        sentence = sentence + symbolsForSend1[i].Tag.ToString() + "-";
                        Common.UpdateCounterAsync(symbolsForSend1[i].Tag.ToString());
                    }
                    else if (index == 2)
                    {
                        sentence = sentence + symbolsForSend2[i].Tag.ToString() + "-";
                        Common.UpdateCounterAsync(symbolsForSend2[i].Tag.ToString());
                    }
                    else if (index == 3)
                    {
                        sentence = sentence + symbolsForSend3[i].Tag.ToString() + "-";
                        Common.UpdateCounterAsync(symbolsForSend3[i].Tag.ToString());
                    }
                    else if (index == 4)
                    {
                        sentence = sentence + symbolsForSend4[i].Tag.ToString() + "-";
                        Common.UpdateCounterAsync(symbolsForSend4[i].Tag.ToString());
                    }
                }

                if (iStarted == false)
                {
                    if (symbolsForSend_full[3] == 1 && symbolsSentFromOther_full[2] ==1) {
                        delete_all.Visibility = Visibility.Visible;
                        send.Visibility = Visibility.Collapsed;
                    }
                }
                else {
                    if (symbolsForSend_full[2] == 1 && symbolsSentFromOther_full[2] == 1) {
                        delete_all.Visibility = Visibility.Visible;
                        send.Visibility = Visibility.Collapsed;
                    }
                }
            }

            Common.sendMsg(sentence, Common.myFriend);
            send.Visibility = Visibility.Collapsed;
            await Common.WriteConversation("child:" + sentence, "chatWithFriend.txt");
            await Common.WriteConversation("my_num_of_msg:" + Common.my_num_of_msg, "chatWithFriend.txt");
            await Common.WriteConversation("iStarted:" + Common.iStarted, "chatWithFriend.txt");

        }

        private void Symbol_Click(object sender, RoutedEventArgs e)
        {
            int index = Common.my_num_of_msg + 1;
            if (iStarted == false)
            {
                index += 1;
            }
            Button button = (Button)sender;
            string button_name = button.Name;
            AddToForSendGrid(button_name, index);            
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
            if (symbolsForSend_curr == 4 && symbolsForSend_curr4 < 5)
            {
                symbolsForSend4[symbolsForSend_curr4].Source = new BitmapImage(requestUri);
                symbolsForSend4[symbolsForSend_curr4].Tag = button_name;
                symbolsForSend_curr4++;
            }
        }

        private void GetMessageImg(Image[] symbolsSentFromOther)
        {
            if (symbolsForSend_full[0] == 0) {
                symbolsForSend_full[0] = 1;
                iStarted = false;
            }
            if (symbolsSentFromOther_full[0] == 0)
            {
                for (int i = 0; i < symbolsSentFromOther1.Length; i++)
                {
                    symbolsSentFromOther1[i].Source = symbolsSentFromOther[i].Source;
                }
                symbolsSentFromOther_full[0] = 1;
                send.Visibility = Visibility.Visible;
            }

            else if (symbolsSentFromOther_full[1] == 0)
            {
                for (int i = 0; i < symbolsSentFromOther2.Length; i++)
                {
                    symbolsSentFromOther2[i].Source = symbolsSentFromOther[i].Source;
                }
                symbolsSentFromOther_full[1] = 1;
                send.Visibility = Visibility.Visible;
            }
            else if (symbolsSentFromOther_full[2] == 0)
            {
                for (int i = 0; i < symbolsSentFromOther3.Length; i++)
                {
                    symbolsSentFromOther3[i].Source = symbolsSentFromOther[i].Source;
                }
                symbolsSentFromOther_full[2] = 1;
                if (iStarted == false)
                {
                    send.Visibility = Visibility.Visible;
                }
                else {
                    send.Visibility = Visibility.Collapsed;
                    delete_all.Visibility = Visibility.Visible;
                }
            }
        }

        private void GetSentMessage(Image[] symbolsSent)
        {
            if (Common.iStarted == false) {
                symbolsForSend_full[0] = 1;
            }
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

        private async Task GetMessageAsync(OutTable[] messages)
        {
            Image[] images = new Image[5];
            int numofmsg = 0;             
            if (messages != null && messages.Length > 0)
            {

                if (symbolsForSend_full[0] == 0)
                {
                    symbolsForSend_full[0] = 1;
                    Common.iStarted = false;
                }
                await Common.WriteConversation("iStarted:" + Common.iStarted, "chatWithFriend.txt");
                for (int x = 0; x < messages.Length; x++)
                {
                    int i = 0;
                    numofmsg++;
                    string msg = messages[x].Message;
                    await Common.WriteConversation("friend:" + msg, "chatWithFriend.txt");
                    await Common.WriteConversation("iStarted:" + iStarted, "chatWithFriend.txt");

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
            }
        }

        private async Task GetMsgFromFriend()
        {
            while (flag && symbolsSentFromOther_full[2] != 1)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
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

            symbolsForSend4[0] = forSend41;
            symbolsForSend4[1] = forSend42;
            symbolsForSend4[2] = forSend43;
            symbolsForSend4[3] = forSend44;
            symbolsForSend4[4] = forSend45;

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
                    if (message[0].Equals("iStarted"))
                    {
                        if (message[1].Equals("true"))
                        {
                            Common.iStarted = true;
                        }
                        else {
                            Common.iStarted = false;
                        }
                    }
                    else if (message[0].Equals("my_num_of_msg"))
                    {
                        Common.my_num_of_msg = Int32.Parse(message[1]);
                    }
                    else
                    {
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
                            send.Visibility = Visibility.Collapsed;
                        }
                    }
                }
            }
        }
    }
}

