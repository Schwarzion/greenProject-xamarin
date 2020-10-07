﻿using GreenProjectMobile.Views;
using System;
using Xamarin.Forms;
using GreenProjectMobile.ViewsModels;
using Xamarin.Essentials;
using System.Net.Http;
using System.Diagnostics;
using System.Threading.Tasks;
using GreenProjectMobile.Services;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace GreenProjectMobile
{
    public partial class App : Xamarin.Forms.Application
    {

        public App()
        {
            InitializeComponent();
            OnStart();
            
            MainPage = new NavbarDetailPage();

            Current.On<Xamarin.Forms.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
        }

        protected override void OnStart()
        {
            HttpClientService.initClient();
        }

        protected override void OnSleep()
        {

        }

        protected override void OnResume()
        {

        }
    }
}
