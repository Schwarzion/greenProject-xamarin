using GreenProjectMobile.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using GreenProjectMobile.Models;
using System.IdentityModel.Tokens.Jwt;
using Xamarin.Essentials;

namespace GreenProjectMobile.ViewsModels
{
    class NavbarDetailPage : MasterDetailPage
    {
        public NavbarDetailPage()
        {
            Authorization();
            var master = new NavbarPage();

            this.Master = master;

            this.Detail = new NavigationPage(new AboutPage());

            this.MasterBehavior = MasterBehavior.Popover;

            master.PageSelected += MasterPageSelected;
        }

        void MasterPageSelected(object sender, PageType e)
        {
            PresentDetailPage(e);
        }

        void PresentDetailPage(PageType pageType)
        {
            Page page;

            switch (pageType)
            {
                case PageType.Tips:
                    page = new Tips();
                    break;
                case PageType.LoginView:
                    page = new LoginView();
                    break;
                case PageType.MainPage:
                    page = new MainPage();
                    break;
                case PageType.ProfileView:
                    page = new ProfileView();
                    break;
                case PageType.AboutPage:
                    page = new AboutPage();
                    break;
                default:
                    page = new MainPage();
                    break;
            }

            Detail = new NavigationPage(page);

            IsPresented = false;
        }

        public async void Authorization()
        {
            string oauthToken = await SecureStorage.GetAsync("Token");
            if (String.IsNullOrEmpty(oauthToken))
            {
                await Navigation.PushModalAsync(new NavigationPage(new LoginView()));
            }
            else
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(oauthToken);
                DateTime utcDate = DateTime.UtcNow;
                if (utcDate > jsonToken.ValidTo)
                {
                    SecureStorage.Remove("Token");
                    await Navigation.PushModalAsync(new NavigationPage(new LoginView()));
                }
            }
        }
    }
}