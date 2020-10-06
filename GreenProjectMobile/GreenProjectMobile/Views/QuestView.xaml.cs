using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreenProjectMobile.Models;
using GreenProjectMobile.ViewsModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GreenProjectMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Quests : ContentPage
    {
        public Quests()
        {
            BindingContext = new QuestViewModel();
            InitializeComponent();
        }

        async void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var questDetailPage = new QuestDetailView(e.SelectedItem as Quest);
            await Navigation.PushAsync(questDetailPage);
        }
    }
}