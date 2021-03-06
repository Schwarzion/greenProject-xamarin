﻿using GreenProjectMobile.Views;
using System;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GreenProjectMobile
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            Authorization();
            InitializeComponent();
        }

        public async void Authorization()
        {
            string oauthToken = await SecureStorage.GetAsync("Token");
            Console.WriteLine(oauthToken);
            if (String.IsNullOrEmpty(oauthToken))
            {
                await Navigation.PushModalAsync(new NavigationPage(new LoginView()));
            }
            else
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(oauthToken);
                DateTime utcDate = DateTime.UtcNow;
                if (utcDate < jsonToken.ValidTo)
                {
                    SecureStorage.Remove("Token");
                    await Navigation.PushModalAsync(new NavigationPage(new LoginView()));
                }
            }
        }
    }
}
