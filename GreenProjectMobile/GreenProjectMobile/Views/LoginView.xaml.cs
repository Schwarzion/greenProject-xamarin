using GreenProjectMobile.ViewsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenProjectMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            var vm = new LoginViewModel();
            this.BindingContext = vm;
            InitializeComponent();

            Email.Completed += (object sender, EventArgs e) =>
            {
                Password.Focus();
            };

            Password.Completed += async (object sender, EventArgs e) =>
            {
                vm.SubmitCommand.Execute(null);
            };
        }
    }
}