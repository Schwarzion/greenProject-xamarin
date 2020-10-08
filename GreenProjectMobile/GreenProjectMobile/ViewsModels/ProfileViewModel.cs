using GreenProjectMobile.Models.ProfileModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GreenProjectMobile.ViewsModels
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        readonly HttpClient client;

        public ProfileViewModel()
        {
            client = new HttpClient();
            getProfile();
        }

        private ProfileModel profile;
        private string sexe;


        public ProfileModel Profile
        {
            get
            {
                return profile;
            }

            private set
            {
                if (profile != value)
                {
                    profile = value;
                    getProfilSex(profile.sexe);
                    NotifyPropertyChanged();

                }
            }
        }
        public string Sexe
        {
            get
            {
                return sexe;
            }

            private set
            {
                if (sexe != value)
                {
                    sexe = value;
                    NotifyPropertyChanged();

                }
            }
        }

        private void getProfilSex(int sexe)
        {
            if (sexe == 0)
            {
                Sexe = "H";
            }
            if (sexe == 1)
            {
                Sexe = "F";
            }
            if (sexe == 2)
            {
                Sexe = "A";
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        public async void getProfile()
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await SecureStorage.GetAsync("Token"));
            string uri = Constants.Constants.BaseUrl + "profile";
            HttpResponseMessage response = null;

            try
            {
                response = await client.GetAsync(uri);
                if (response != null && response.IsSuccessStatusCode == true)
                {
                    var contents = response.Content.ReadAsStringAsync().Result;
                    Profile = JsonConvert.DeserializeObject<ProfileModel>(contents);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception Profile: {ex}");
            }
        }

        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (propertyName != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
