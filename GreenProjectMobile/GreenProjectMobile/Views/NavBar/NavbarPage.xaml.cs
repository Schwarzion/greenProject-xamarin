using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GreenProjectMobile.Models;

namespace GreenProjectMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavbarPage : ContentPage
    {
        public event EventHandler<PageType> PageSelected;
        public NavbarPage()
        {
            InitializeComponent();
            btnLogin.Clicked += (s, e) => PageSelected?.Invoke(this, PageType.LoginView);
            btnProfile.Clicked += (s, e) => PageSelected?.Invoke(this, PageType.ProfileView);
            btnAbout.Clicked += (s, e) => PageSelected?.Invoke(this, PageType.AboutPage);
        }
    }
}