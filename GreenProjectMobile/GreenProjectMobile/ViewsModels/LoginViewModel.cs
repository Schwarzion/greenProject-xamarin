using GreenProjectMobile.Models;
using GreenProjectMobile.Models.AuthenticationModels;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using GreenProjectMobile.Views;

namespace GreenProjectMobile.ViewsModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        readonly HttpClient client;

        public LoginViewModel()
        {
            client = new HttpClient();
            SubmitCommand = new Command(OnSubmit);
            email = "test@mail.com";
            password = "password";
        }
        public ICommand SubmitCommand { protected set; get; }
        private string email;
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Email"));
            }
        }
        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Password"));
            }
        }

        public TokenModel Items { get; private set; }

        public void OnSubmit()
        {
            Login(Email, Password);
        }

        public async void Login(string email, string password)
        {
            LoginModel loginItems = new LoginModel()
            {
                email = email,
                password = password
            };
            string loginJson = JsonConvert.SerializeObject(loginItems);
            TokenModel response = await LoginRequest("login", loginJson);
            Debug.WriteLine(response != null && response.token != null);
            if (response != null && response.token != null)
            {
                await SecureStorage.SetAsync("Token", response.token);
                await Application.Current.MainPage.DisplayAlert("Connexion", "La connexion est un succès.", "Ok");
                await Application.Current.MainPage.Navigation.PushModalAsync(new NavbarDetailPage());
            }
        }


        public async Task<TokenModel> LoginRequest(string path, string json)
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
                await Application.Current.MainPage.DisplayAlert("Connexion", $"La connexion à échoué. Vérifiez votre connexion : {ex}", "Ok");
            }

            if (response != null && response.IsSuccessStatusCode == true)
            {
                var result = await response.Content.ReadAsStringAsync();
                Items = JsonConvert.DeserializeObject<TokenModel>(result);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Connexion", "La connexion à échoué. Vérifiez que vous avez entré les bons identifiants", "Ok");
            }
            return Items;
        }
    }
}
