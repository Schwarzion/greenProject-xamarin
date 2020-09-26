
using GreenProjectMobile.Models.AuthenticationModels;
using System;
using System.Text;
using System.Net.Http;
using System.ComponentModel;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using GreenProjectMobile.Views;

namespace GreenProjectMobile.ViewsModels
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        readonly HttpClient client;

        public ICommand RegisterCommand { protected set; get; }

        public RegisterViewModel()
        {
            client = new HttpClient();
            RegisterCommand = new Command(OnRegister);
        }

        public RegisterModel Items { get; private set; }


        //Renseignement de la liste de sexes 
        static Dictionary<string, int> sexes { get; } = new Dictionary<string, int>
        {
            {"Homme", 0 },
            {"Femme", 1 },
            {"Autre", 2 }
        };

        public List<string> Sexes { get; } = sexes.Keys.ToList();

        public string SelectedSex { get; set; } = "Homme";

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
            set { firstName = value; PropertyChanged(this, new PropertyChangedEventArgs("FirstName")); }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; PropertyChanged(this, new PropertyChangedEventArgs("LastName")); }
        }

        private string alias;
        public string Alias
        {
            get { return alias; }
            set { alias = value; PropertyChanged(this, new PropertyChangedEventArgs("Alias")); }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; PropertyChanged(this, new PropertyChangedEventArgs("Email")); }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; PropertyChanged(this, new PropertyChangedEventArgs("Password")); }
        }

        private string confirmPassword;
        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set { confirmPassword = value; PropertyChanged(this, new PropertyChangedEventArgs("ConfirmPassword")); }
        }

        private string address;
        public string Address
        {
            get { return address; }
            set { address = value; PropertyChanged(this, new PropertyChangedEventArgs("Address")); }
        }

        private string city;
        public string City
        {
            get { return city; }
            set { city = value; PropertyChanged(this, new PropertyChangedEventArgs("City")); }
        }

        private string postalCode;
        public string PostalCode
        {
            get { return postalCode; }
            set { postalCode = value; PropertyChanged(this, new PropertyChangedEventArgs("PostalCode")); }
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
                    PropertyChanged(this, new PropertyChangedEventArgs("Sexe"));
                    OnSelectedSexChanged();
                }; 
            }
        }

        private string phone;
        public string Phone
        {
            get { return phone; }
            set { phone = value; PropertyChanged(this, new PropertyChangedEventArgs("Phone")); }
        }

        private DateTime birthday;
        public DateTime Birthday
        {
            get { return birthday; }
            set { birthday = value; PropertyChanged(this, new PropertyChangedEventArgs("Birthday")); }
        }

        //Méthodes liées à l'inscription
        public void OnRegister()
        {
            OnSelectedSexChanged();
            Register(FirstName, LastName, Alias, Email, Password, ConfirmPassword, Address, City, PostalCode, Phone, Sexe, Birthday);
        }

        public async void Register(string firstName, string lastName, string alias, string email, string password, string confirmPassword, string address, string city, string postalCode, string phone, int sexe, DateTime birthday)
        {
            RegisterModel registerItems = new RegisterModel()
            {
                firstName = firstName,
                lastName = lastName,
                alias = alias,
                email = email,
                password = password,
                confirmPassword = confirmPassword,
                address = address,
                city = city,
                postalCode = postalCode,
                sexe = sexe,
                phone = phone,
                birthday = birthday
            };
            string registerJson = JsonConvert.SerializeObject(registerItems);
            RegisterModel response = await RegisterRequest("register", registerJson);

            if (response != null)
            {
                await Application.Current.MainPage.DisplayAlert("Incription", "Votre compte est bien enregistré", "Ok");
                await Application.Current.MainPage.Navigation.PushModalAsync(new LoginView());
            }
        }

        public async Task<RegisterModel> RegisterRequest(string path, string json)
        {
            string uri = Constants.Constants.BaseUrl + path;
            HttpResponseMessage response = null;
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                response = await client.PostAsync(uri, content);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erreur connexion", "La connexion au serveur a échouée.", "Ok");
            }

            if (response != null && response.IsSuccessStatusCode == true)
            {
                var result = await response.Content.ReadAsStringAsync();
                Items = JsonConvert.DeserializeObject<RegisterModel>(result);
            }
            else if(response != null && response.IsSuccessStatusCode == false)
            {
                string errorMessage = "";
                var result = await response.Content.ReadAsStringAsync();
                RegisterResponseModel Errors = JsonConvert.DeserializeObject<RegisterResponseModel>(result);
                List<List<string>>errorsList = Errors.msg.getAllErrors();

                foreach(List<string> error in errorsList)
                {
                    if (error != null )
                    {
                        errorMessage = errorMessage + error[0] + Environment.NewLine;
                    }
                }

                await Application.Current.MainPage.DisplayAlert("Erreur inscription", errorMessage, "Ok");
            }
            return Items;
        }
    }
}
