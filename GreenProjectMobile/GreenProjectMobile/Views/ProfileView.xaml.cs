using GreenProjectMobile.ViewsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenProjectMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileView : ContentPage
    {
        public ProfileView()
        {
            var vm = new ProfileViewModel();
            this.BindingContext = vm;
            InitializeComponent();
        }
        void UpdateProfile(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new NavbarDetailPage());
        }
    }
}