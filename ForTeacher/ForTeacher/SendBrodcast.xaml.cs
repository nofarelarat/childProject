﻿using System;
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
using System.Net.Http;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ForTeacher
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SendBrodcast : Page
    {
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        public SendBrodcast()
        {
            this.InitializeComponent();
        }
        private void Button_Click_back(object sender, RoutedEventArgs e)
        {
            Frame toHome = Window.Current.Content as Frame;
            toHome.Navigate(typeof(MainPage));
        }
        private async void SendClick(object sender, RoutedEventArgs e)
        {

            // Create final message
            String final_msg = sendMsg.Text + "$" + LoginTeacher.who_am_i + "$";
            // Get Garden children
            user[] kids = await ConnectDB.GetGardenChildren("flowers");
            for (int i = 0; i < kids.Length; i++)
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
