using GreenProjectMobile.Views;
using System;
using Xamarin.Forms;
using GreenProjectMobile.ViewsModels;
using Xamarin.Essentials;
using System.Net.Http;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GreenProjectMobile
{
    public partial class App : Application
    {
        HttpClient client;
        public App()
        {
            Authorization();
            InitializeComponent();

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
