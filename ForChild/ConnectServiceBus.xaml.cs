using System;
using System.IO;
using Windows.UI.Xaml.Controls;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Net.Http;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ForChild
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ConnectServiceBus : Page
    {
        
        public async Task<int> sendMSG()
        {
            string completeUri = "http://localhost:49875/api/chat";
            Uri requestUri = new Uri(completeUri);

            Windows.Web.Http.HttpClient httpClient = new Windows.Web.Http.HttpClient();

            //Send the GET request asynchronously and retrieve the response as a string.
            Windows.Web.Http.HttpResponseMessage httpResponse = new Windows.Web.Http.HttpResponseMessage();
            string httpResponseBody = "";

            try
            {
                //Send the GET request
                httpResponse = await httpClient.GetAsync(requestUri);
                httpResponse.EnsureSuccessStatusCode();
                httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
              //  ReadToObject(httpResponseBody);
                return (int)httpResponse.StatusCode;
            }

            catch (Exception ex)
            {
                httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
            }

            return 0;
        }
        public async Task<int> ReciveMSG()
        {
            string completeUri = "http://localhost:49875/api/chat";
            Uri requestUri = new Uri(completeUri);

            Windows.Web.Http.HttpClient httpClient = new Windows.Web.Http.HttpClient();

            //Send the GET request asynchronously and retrieve the response as a string.
            Windows.Web.Http.HttpResponseMessage httpResponse = new Windows.Web.Http.HttpResponseMessage();
            string httpResponseBody = "";

            try
            {
                //Send the GET request
                httpResponse = await httpClient.GetAsync(requestUri);
                httpResponse.EnsureSuccessStatusCode();
                httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                //  ReadToObject(httpResponseBody);
                return (int)httpResponse.StatusCode;
            }

            catch (Exception ex)
            {
                httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
            }

            return 0;
        }
    }
}
