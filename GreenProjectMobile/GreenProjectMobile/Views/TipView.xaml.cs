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
            BindingContext = new TipViewModel();
            InitializeComponent();
        }
    }
}