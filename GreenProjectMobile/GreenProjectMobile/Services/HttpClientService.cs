using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using Xamarin.Essentials;
using Java.Nio.Channels;

namespace GreenProjectMobile.Services
{
    class HttpClientService
    {
        private static HttpClient _client;
        private static string _token;

        public static HttpClient client
        {
            get
            {
                return _client;
            }
            set
            {
                _client = value;
            }
        }

        public static string token
        {
            get
            {
                return _token;
            }
            set
            {
                _token = value;
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", value);
            }
        }
        public static void initClient()
        {
            if (client == null)
                client = new HttpClient
                {
                    BaseAddress = new Uri(Constants.Constants.BaseUrl)
                };
            setToken();
        }

        public async static void setToken()
        {
            string authToken = await SecureStorage.GetAsync("Token");
            if (!String.IsNullOrEmpty(authToken))
                token = authToken;
        }

        public async static void logout()
        {
            HttpResponseMessage response = await client.PostAsync("logout", new StringContent("", Encoding.UTF8, "application/json"));
<<<<<<< HEAD
=======
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
>>>>>>> ac460c6... Logout on API
        }
    }
}
