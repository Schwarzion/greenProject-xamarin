using GreenProjectMobile.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GreenProjectMobile.ViewsModels;
using Xamarin.Essentials;

namespace GreenProjectMobile
{
    public partial class App : Application
    {
        public App()
        {
            Authorization();
            InitializeComponent();

            //MainPage = new NavigationPage(new MainPage());
            MainPage = new NavbarDetailPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public async void Authorization()
        {
            string oauthToken = await SecureStorage.GetAsync("Token");
            if (String.IsNullOrEmpty(oauthToken))
            {
                MainPage = new LoginView();
            }
        }
    }
}
