using GreenProjectMobile.Models.ProfileModels;
using GreenProjectMobile.Views;
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
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GreenProjectMobile.ViewsModels
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        readonly HttpClient client;
        public ICommand SubmitCommand { protected set; get; }

        public ProfileViewModel()
        {
            client = new HttpClient();
            SubmitCommand = new Command(UpdateProfile);
            getProfile();
        }

        private ProfileModel _profile;
        private string _sexe;
        private string _avatar;


        public string Avatar
        {
            get
            { 
                return _avatar;
            }

            private set
            {
                if (_avatar != value)
                {
                    _avatar = value;
                    NotifyPropertyChanged();

                }
            }
        }

        public ProfileModel Profile
        {
            get
            {
                return _profile;
            }

            private set
            {
                if (_profile != value)
                {
                    _profile = value;
                    getProfilSex(_profile.sexe);
                    getProfileAvatar(_profile.avatar);
                    NotifyPropertyChanged();

                }
            }
        }

        private void getProfileAvatar(object avatar)
        {
            switch(avatar)
            {
                case 2:
                    Avatar = "picture2.jpg";
                    break;
                case 3:
                    Avatar = "picture3.jpg";
                    break;
                case 4:
                    Avatar = "picture4.jpg";
                    break;
                default:
                    Avatar = "picture1.jpg";
                    break;
            }
        }

        public string Sexe
        {
            get
            {
                return _sexe;
            }

            private set
            {
                if (_sexe != value)
                {
                    _sexe = value;
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
        public void UpdateProfile()
        {
            Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new UpdateView(Profile)));
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
