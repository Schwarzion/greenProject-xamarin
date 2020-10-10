﻿using GreenProjectMobile.Models.ProfileModels;
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
    public partial class UpdateView : ContentPage
    {
        public UpdateView(ProfileModel profile)
        {
            BindingContext = new UpdateViewModel(profile);
            InitializeComponent();
        }


    }
}