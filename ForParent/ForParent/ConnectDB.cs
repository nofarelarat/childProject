using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using Windows.Data.Json;

namespace ForParent
{
    class ConnectDB
    {
        public async Task<user> GetUserByMailAsync(string email)
        {
            string completeUri = "http://localhost:49875/api/users?email=" + email;
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
                user getUser = ReadToObject(httpResponseBody);
                return getUser;
            }

            catch (Exception ex)
            {
                httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
            }
            //JsonObject js;
            //bool ispass= JsonObject.TryParse(httpResponseBody,out js);
            //if (ispass)
            //{

            //}

            return null;
        }
        public static user ReadToObject(string json)
        {
            user deserializedUser = new user();
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(deserializedUser.GetType());
            deserializedUser = ser.ReadObject(ms) as user;
            //ms.Close();
            return deserializedUser;
        }

    }
}
