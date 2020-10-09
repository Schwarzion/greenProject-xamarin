using GreenProjectMobile.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using GreenProjectMobile.Models;

namespace GreenProjectMobile.ViewsModels
{
    class NavbarDetailPage : MasterDetailPage
    {
        public NavbarDetailPage()
        {
            var master = new NavbarPage();

            this.Master = master;

            this.Detail = new NavigationPage(new MainPage());

            this.MasterBehavior = MasterBehavior.Popover;

            master.PageSelected += MasterPageSelected;
            PresentDetailPage(PageType.AboutPage);
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
    }
}