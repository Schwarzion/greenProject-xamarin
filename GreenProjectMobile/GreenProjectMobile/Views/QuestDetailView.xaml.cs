using GreenProjectMobile.Models;
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
    public partial class QuestDetailView : ContentPage
    {
        QuestDetailViewModel questDetailViewModel;
        public QuestDetailView()
        {
            InitializeComponent();
        }

        public QuestDetailView(Quest quest)
        {
            InitializeComponent();
            questDetailViewModel = new QuestDetailViewModel(quest);
            this.BindingContext = questDetailViewModel;
        }
    }
}