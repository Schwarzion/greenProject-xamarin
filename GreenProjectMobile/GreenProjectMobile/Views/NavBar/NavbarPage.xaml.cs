<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GreenProjectMobile.Models;
using Xamarin.Essentials;
using System.Net.Http;
using GreenProjectMobile.Services;

namespace GreenProjectMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavbarPage : ContentPage
    {
        public event EventHandler<PageType> PageSelected;

        public NavbarPage()
        {
            InitializeComponent();
            btnProfile.Clicked += (s, e) => PageSelected?.Invoke(this, PageType.ProfileView);
            btnMain.Clicked += (s, e) => PageSelected?.Invoke(this, PageType.MainPage);
            btnAbout.Clicked += (s, e) => PageSelected?.Invoke(this, PageType.AboutPage);
            btnTips.Clicked += (s, e) => PageSelected?.Invoke(this, PageType.Tips);
            btnLogout.Clicked += (s, e) =>
            {
                SecureStorage.Remove("Token");
                HttpClientService.logout();
                Navigation.PushModalAsync(new NavigationPage(new LoginView()));
            };
        }
    }
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GreenProjectMobile.Models;
using Xamarin.Essentials;
using System.Net.Http;
using GreenProjectMobile.Services;

namespace GreenProjectMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavbarPage : ContentPage
    {
        public event EventHandler<PageType> PageSelected;

        public NavbarPage()
        {
            InitializeComponent();
            btnProfile.Clicked += (s, e) => PageSelected?.Invoke(this, PageType.ProfileView);
            btnMain.Clicked += (s, e) => PageSelected?.Invoke(this, PageType.MainPage);
            btnAbout.Clicked += (s, e) => PageSelected?.Invoke(this, PageType.AboutPage);
            btnTips.Clicked += (s, e) => PageSelected?.Invoke(this, PageType.Tips);
            btnLogout.Clicked += (s, e) =>
            {
                SecureStorage.Remove("Token");
                HttpClientService.logout();
                Navigation.PushModalAsync(new NavigationPage(new LoginView()));
            };
        }
    }
>>>>>>> ac460c6... Logout on API
}