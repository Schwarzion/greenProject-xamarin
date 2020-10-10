using GreenProjectMobile.Models.AuthenticationModels;
using GreenProjectMobile.Models.ProfileModels;
using GreenProjectMobile.Services;
using GreenProjectMobile.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace GreenProjectMobile.ViewsModels
{
    public class UpdateViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        readonly HttpClient client;
        private ProfileModel profile;
        public ProfileModel Profile
        {
            get { return profile; }
            set
            {
                profile = value;
                OnPropertyChanged();
            }
        }

        public ICommand UpdateCommand { protected set; get; }

        public UpdateViewModel(ProfileModel profile)
        {
            Profile = profile;
            CurrentDatas();
            client = HttpClientService.client;
            UpdateCommand = new Command(OnUpdate);
        }

        public UpdateModel Items { get; private set; }


        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        //Renseignement de la liste de sexes 
        static Dictionary<string, int> sexes { get; } = new Dictionary<string, int>
        {
            {"Homme", 0 },
            {"Femme", 1 },
            {"Autre", 2 }
        };

        public List<string> Sexes { get; } = sexes.Keys.ToList();

        public string SelectedSex { get; set; }

        public void CurrentDatas()
        {
            FirstName = Profile.firstName;
            LastName = Profile.lastName;
            Alias = Profile.alias;
            Email = Profile.email;
            Address = Profile.address;
            City = Profile.city;
            PostalCode = Profile.postalCode;
            Phone = Profile.phone;


            foreach (KeyValuePair<string, int> sexe in sexes)
            {
                if (sexe.Value == Profile.sexe)
                {
                    SelectedSex = sexe.Key;
                }
            }
        }
        public void OnSelectedSexChanged()
        {
            var selectedValue = sexes[SelectedSex];
            Sexe = selectedValue;
        }

        //Propriétés de l'objet Register
        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged();
            }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                OnPropertyChanged();
            }
        }

        private string alias;
        public string Alias
        {
            get { return alias; }
            set 
            { 
                alias = value;
                OnPropertyChanged();
            }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }

        private string address;
        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged();
            }
        }

        private string city;
        public string City
        {
            get { return city; }
            set
            { 
                city = value;
                OnPropertyChanged();
            }
        }

        private string postalCode;
        public string PostalCode
        {
            get { return postalCode; }
            set
            {
                postalCode = value;
                OnPropertyChanged();
            }
        }

        private int sexe;
        public int Sexe
        {
            get { return sexe; }
            set
            {
                if (sexe != value)
                {
                    sexe = value;
                    OnSelectedSexChanged();
                };
            }
        }

        private string phone;
        public string Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                OnPropertyChanged();
            }
        }

        //Méthodes liées à l'inscription
        public void OnUpdate()
        {
            OnSelectedSexChanged();
            update(FirstName, LastName, Alias, Email, Address, City, PostalCode, Phone, Sexe);
        }

        public async void update(string firstName, string lastName, string alias, string email, string address, string city, string postalCode, string phone, int sexe)
        {
            UpdateModel updateItems = new UpdateModel()
            {
                firstName = firstName,
                lastName = lastName,
                alias = alias,
                email = email,
                address = address,
                city = city,
                postalCode = postalCode,
                sexe = sexe,
                phone = phone,
            };
            string updateJson = JsonConvert.SerializeObject(updateItems);
            UpdateModel response = await UpdateRequest("editUser/" + Profile.id, updateJson);

            if (response != null)
            {
                await Application.Current.MainPage.DisplayAlert("Incription", "Votre compte est bien enregistré", "Ok");
                await Application.Current.MainPage.Navigation.PushModalAsync(new ProfileView());
            }
        }

        public async Task<UpdateModel> UpdateRequest(string path, string json)
        {
            string uri = Constants.Constants.BaseUrl + path;
            HttpResponseMessage response = null;
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                response = await client.PostAsync(path, content);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erreur connexion", "La connexion au serveur a échouée.", "Ok");
            }

            if (response != null && response.IsSuccessStatusCode == true)
            {
                var result = await response.Content.ReadAsStringAsync();
                Items = JsonConvert.DeserializeObject<UpdateModel>(result);
            }
            else if (response != null && response.IsSuccessStatusCode == false)
            {
                string errorMessage = "";
                var result = await response.Content.ReadAsStringAsync();
                UpdateResponseModel Errors = JsonConvert.DeserializeObject<UpdateResponseModel>(result);
                List<List<string>> errorsList = Errors.user.getAllErrors();

                foreach (List<string> error in errorsList)
                {
                    if (error != null)
                    {
                        errorMessage = errorMessage + error[0] + Environment.NewLine;
                    }
                }

                await Application.Current.MainPage.DisplayAlert("Erreur edition", errorMessage, "Ok");
            }
            return Items;
        }
    }
}