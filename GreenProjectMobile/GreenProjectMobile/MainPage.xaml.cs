using GreenProjectMobile.Views;
using GreenProjectMobile.ViewsModels;
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
            InitializeComponent();
            BindingContext = new MainViewModel();
        }
    }
}
