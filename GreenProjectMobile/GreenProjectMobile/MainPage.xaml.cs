using GreenProjectMobile.Views;
using System;
using System.ComponentModel;
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
            if (String.IsNullOrEmpty(oauthToken))
            {
                
                await Navigation.PushModalAsync(new NavigationPage(new LoginView()));
            }
        }
    }
}
