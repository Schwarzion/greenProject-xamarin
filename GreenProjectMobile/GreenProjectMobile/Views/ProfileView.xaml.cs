using GreenProjectMobile.ViewsModels;
using GreenProjectMobile.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GreenProjectMobile.Models.ProfileModels;

namespace GreenProjectMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileView : ContentPage
    {
        public ProfileView()
        {
            ProfileViewModel vm = new ProfileViewModel();
            vm.getProfile();
            ProfileModel prifile = vm.Profile;
            BindingContext = vm;
            InitializeComponent();
        }
    }
}