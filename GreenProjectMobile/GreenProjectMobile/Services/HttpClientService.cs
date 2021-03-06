﻿using Android.OS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Java.Util.Functions;
using Org.Apache.Http.Protocol;
using System.Net.Http.Headers;
using Xamarin.Essentials;

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
        }

        public async static void setToken()
        {
            string authToken = await SecureStorage.GetAsync("Token");
            if (!String.IsNullOrEmpty(authToken))
                token = authToken;
        }
    }
}
