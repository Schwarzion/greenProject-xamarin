using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreenProjectMobile.ViewsModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenProjectMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Tips : ContentPage
    {
        public Tips()
        {
            InitializeComponent();
            var service = new TipViewModel();
            service.GetTips();
        }
    }
}