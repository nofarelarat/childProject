using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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
using Windows.UI.Xaml.Media.Imaging;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ForChild
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TeacherPage : Page
    {
        static bool flag = true;
        static Image[] symbolsForSend1 = new Image[5];
        static Image[] symbolsForSend2 = new Image[5];
        static Image[] symbolsForSend3 = new Image[5];

        static Image[] symbolsSentFromOther1 = new Image[5];
        static Image[] symbolsSentFromOther2 = new Image[5];
        static Image[] symbolsSentFromOther3 = new Image[5];

        static int symbolsSentFromOther_full1 = 0;
        static int symbolsSentFromOther_full2 = 0;
        static int symbolsSentFromOther_full3 = 0;

        public TeacherPage()
        {
            this.InitializeComponent();

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
            flag = true;
            GetMsgFromTeacher();

        }
        private void Button_Click_back(object sender, RoutedEventArgs e)
        {
            flag = false;
            Frame toHome = Window.Current.Content as Frame;
            toHome.Navigate(typeof(MainPage));
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            flag = false;
            for (int i = 0; i < symbolsForSend1.Length; i++)
            {
                symbolsSentFromOther1[i].Source = null;
                symbolsSentFromOther2[i].Source = null;
                symbolsSentFromOther3[i].Source = null;
            }
            flag = true;
            GetMsgFromTeacher();
        }

        private void GetMessageImg(Image[] symbolsSentFromOther)
        {
            if (symbolsSentFromOther_full1 == 0)
            {
                for (int i = 0; i < symbolsSentFromOther1.Length; i++)
                {
                    symbolsSentFromOther1[i].Source = symbolsSentFromOther[i].Source;
                }
                symbolsSentFromOther_full1 = 1;

            }
            else if (symbolsSentFromOther_full2 == 0)
            {
                for (int i = 0; i < symbolsSentFromOther2.Length; i++)
                {
                    symbolsSentFromOther2[i].Source = symbolsSentFromOther[i].Source;
                }
                symbolsSentFromOther_full2 = 1;


            }
            else if (symbolsSentFromOther_full3 == 0)
            {
                for (int i = 0; i < symbolsSentFromOther3.Length; i++)
                {
                    symbolsSentFromOther3[i].Source = symbolsSentFromOther[i].Source;
                }
                symbolsSentFromOther_full3 = 1;
            }

        }
        private async void GetMessageAsync(OutTable[] message)
        {
            Image[] images = new Image[5];

            int numofmsg = 0; //the number of messages cant be more than 3.
                              //TODO : add to the if 
            if (message.Length > 0)
            {
                for (int x = 0; x < message.Length; x++)
                {
                    int i = 0;
                    numofmsg++;
                    string msg = message[x].Message;
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

        private async void GetMsgFromTeacher()
        {
            while (flag)
            {
                OutTable[] table = await Common.GetMsgAsync(Common.myTeacher);
                GetMessageAsync(table);
            }

        }
    }
}

