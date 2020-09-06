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

            this.Detail = new NavigationPage(new AboutPage());

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
                case PageType.LoginView:
                    page = new LoginView();
                    break;
/*                case PageType.ProfileView:
                    page = new ProfileView();
                    break;*/
                default:
                    page = new AboutPage();
                    break;
            }

            Detail = new NavigationPage(page);

            IsPresented = false;
        }
    }
}
