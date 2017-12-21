using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
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

            return null;
        }


        public async Task<bool> CreateUser(user user)
        {
            string completeUri = "http://localhost:49875/api/users?user=user";
            Uri requestUri = new Uri(completeUri);
            string json = WriteFromObject(user);
            Windows.Web.Http.HttpClient httpClient = new Windows.Web.Http.HttpClient();

            //Send the POST request asynchronously and retrieve the response as a string.
            Windows.Web.Http.HttpResponseMessage httpResponse = new Windows.Web.Http.HttpResponseMessage();
            string httpResponseBody = "";

            try
            {
                //Send the POSR request
                Windows.Web.Http.HttpStringContent stringContent = new Windows.Web.Http.HttpStringContent(json.ToString());
                httpResponse = await httpClient.PostAsync(requestUri, stringContent);
                httpResponse.EnsureSuccessStatusCode();
                //httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                //user getUser = ReadToObject(httpResponseBody);
                return true;
            }

            catch (Exception ex)
            {
                httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
            }

            return false;
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

        public static string WriteFromObject(user user)
        {
            //Create a stream to serialize the object to.
            MemoryStream ms = new MemoryStream();

            // Serializer the User object to the stream.
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(user));
            ser.WriteObject(ms, user);
            byte[] json = ms.ToArray();
            //ms.Close();
            return Encoding.UTF8.GetString(json, 0, json.Length);
        }

        public bool ValidateUser(string email, string firstName, string lastName, DateTime birthDate,
           string childAge, bool childCheckBox, string gardenName, string password, out string msg)
        {
            msg = "success";
            bool isPass = true;
            if (email.IndexOf("a") == -1)
            {
                isPass = false;
                msg = "Non valid email";
            }
                
            if (email=="" || firstName == "" || lastName == "" 
           || childAge == "" || gardenName == "" || password == "")
            {
                isPass = false;
                msg = "Invalid input";
            }
            if (email.IndexOf("a") == -1)
                isPass = false;
            else
            {
                if(GetUserByMailAsync(email)!=null)
                {
                    isPass = false;
                    msg = "User already exist";
                }
            }
            
            if (birthDate == DateTime.MinValue)
            {
                isPass = false;
                msg = "Invalid birth date";
            }
            
            return isPass;
        }
    }
}
