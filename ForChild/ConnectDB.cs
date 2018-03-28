using System;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using System.Runtime.Serialization.Json;
using Windows.Web.Http;
using Newtonsoft.Json;



namespace ForChild
{
    class ConnectDB
    {
        public async Task<user> GetUserByMailAsync(string email)
        {
            string completeUri = "http://childappapiservice.azurewebsites.net/api/users?email=" + email;
            //string completeUri = "http://localhost:49876/api/users?email=" + email;
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
                user getUser = ReadToObjectUser(httpResponseBody);
                return getUser;
            }

            catch (Exception ex)
            {
                httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
            }

            return null;
        }

        public async Task<bool> CreateUserAsync(user user)
        {
            string completeUri = "http://childappapiservice.azurewebsites.net/api/users";
            string json = WriteFromObject(user);

            try
            {
                //Send the POST request
                HttpStringContent stringContent = new HttpStringContent(json.ToString());

                System.Net.Http.HttpRequestMessage request = new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Post, completeUri);
                request.Content = new StringContent(json,
                                                    Encoding.UTF8,
                                                    "application/json");//CONTENT-TYPE header

                System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
                System.Net.Http.HttpResponseMessage response = await client.SendAsync(request);  //I know I should have used async/await here!
                return true;
            }

            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateUserCounterAsync(symbol userSymbol)
        {
            string completeUri = "http://childappapiservice.azurewebsites.net/api/counters";
            //string completeUri = "http://localhost:49876/api/counters";

            string json = WriteFromObject(userSymbol);

            try
            {
                //Send the POSR request
                HttpStringContent stringContent = new HttpStringContent(json.ToString());

                System.Net.Http.HttpRequestMessage request = new System.Net.Http.HttpRequestMessage(new System.Net.Http.HttpMethod("PATCH"), completeUri);
                request.Content = new StringContent(json,
                                                    Encoding.UTF8,
                                                    "application/json");//CONTENT-TYPE header

                System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
                System.Net.Http.HttpResponseMessage response = await client.SendAsync(request);  //I know I should have used async/await here!
                return true;
            }

            catch (Exception ex)
            {
                return false;
            }
        }
       
        public async Task<symbol[]> GetUserCounterAsync(symbol userSymbol)
        {
            string completeUri = "http://childappapiservice.azurewebsites.net/api/counters?email=" 
                + userSymbol.email + "&symbolName=" + userSymbol.symbolName;
            //string completeUri = "http://localhost:49876/api/counters?email=" 
            //    + userSymbol.email+"&symbolName="+ userSymbol.symbolName;

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
                
                symbol[] userSymbolUsage = JsonConvert.DeserializeObject<symbol[]>(httpResponseBody);

                return userSymbolUsage;
            }

            catch (Exception ex)
            {
                httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
            }

            return null;
        }

        public async Task<symbol[]> GetUserAllCountersAsync(string email)
        {
            string completeUri = "http://childappapiservice.azurewebsites.net/api/counters?email="
                + email;
            //string completeUri = "http://localhost:49876/api/counters?email=" 
            //    + email;

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

                symbol[] userSymbolUsage = JsonConvert.DeserializeObject<symbol[]>(httpResponseBody);

                return userSymbolUsage;
            }

            catch (Exception ex)
            {
                httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
            }

            return null;
        }

        public async void TestDelete(string email)
        {
            string completeUri = "http://childappapiservice.azurewebsites.net/api/users?email=" + email;
            //string completeUri = "http://localhost:49876/api/users?email=" + email;
            Uri requestUri = new Uri(completeUri);

            Windows.Web.Http.HttpClient httpClient = new Windows.Web.Http.HttpClient();

            //Send the GET request asynchronously and retrieve the response as a string.
            Windows.Web.Http.HttpResponseMessage httpResponse = new Windows.Web.Http.HttpResponseMessage();
            string httpResponseBody = "";

            try
            {
                //Send the GET request
                httpResponse = await httpClient.DeleteAsync(requestUri);
                httpResponse.EnsureSuccessStatusCode();
                httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
            }

            catch (Exception ex)
            {
            }
        }

        //position 0 for user type : Parent or Child
        //position 1 will be for the user email itself
        public async Task<bool> AddUserContactsAsync(string[] emails)
        {
            string completeUri = "http://childappapiservice.azurewebsites.net/api/contacts";
            //string completeUri = "http://localhost:49876/api/contacts";
            string json = JsonConvert.SerializeObject(emails);
            try
            {
                //Send the POST request
                HttpStringContent stringContent = new HttpStringContent(json.ToString());
                System.Net.Http.HttpRequestMessage request = new System.Net.Http.HttpRequestMessage(new System.Net.Http.HttpMethod("POST"), completeUri);
                request.Content = new StringContent(json,
                Encoding.UTF8,
                "application/json");//CONTENT-TYPE header
                System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
                System.Net.Http.HttpResponseMessage response = await client.SendAsync(request);  //I know I should have used async/await here!
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<userContacts> GetUserContactsAsync(string email)
        {
            string completeUri = "http://childappapiservice.azurewebsites.net/api/contacts?email=" + email;
            //string completeUri = "http://localhost:49876/api/contacts?email=" + email;
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
                userContacts userContacts = JsonConvert.DeserializeObject<userContacts>(httpResponseBody);
                return userContacts;
            }

            catch (Exception ex)
            {
                httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
            }

            return null;
        }

        public async Task<user[]> GetGardenChildren(string garden)
        {
            string completeUri = "http://childappapiservice.azurewebsites.net/api/users?garden=" + garden;
            //string completeUri = "http://localhost:49876/api/users?garden=" + garden;
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
                user[] gardenUsers = JsonConvert.DeserializeObject<user[]>(httpResponseBody);
                
                return gardenUsers;
            }
            catch (Exception ex)
            {
                httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
            }
            return null;
        }

        public static user ReadToObjectUser(string json)
        {
            user deserializedUser = new user();
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(deserializedUser.GetType());
            deserializedUser = ser.ReadObject(ms) as user;
            //ms.Close();
            return deserializedUser;
        }

        public static symbol[] ReadToSymbolObject(string json)
        {
            symbol[] deserializedUser=new symbol[2];
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(deserializedUser.GetType());
            deserializedUser = ser.ReadObject(ms) as symbol[];
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

        public static string WriteFromObject(symbol userSymbol)
        {
            //Create a stream to serialize the object to.
            MemoryStream ms = new MemoryStream();

            // Serializer the User object to the stream.
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(symbol));
            ser.WriteObject(ms, userSymbol);
            byte[] json = ms.ToArray();
            //ms.Close();
            return Encoding.UTF8.GetString(json, 0, json.Length);
        }

        public async Task<string> ValidateUserAsync(string email, string firstName, string lastName, string childYear,
           bool childCheckBox, string gardenName, string password)
        {
            string msg = "success";

            if (email == "" || firstName == "" || lastName == ""
           || childYear == "" || gardenName == "" || password == "")
            {
                msg = "Invalid input";
            }
            if (email.IndexOf("@") == -1)
            {
                msg = "Non valid email";
            }
            else
            {
                user res = await GetUserByMailAsync(email);
                if (res != null)
                {
                    msg = "User already exist";
                }
            }
            //check if year is int 
            var isNumeric = int.TryParse(childYear, out var n);
            if (isNumeric == false)
            {
                msg = "Invalid child birth year";
            }
            return msg;
        }
    }
}
