using GreenProjectMobile.Models;
using GreenProjectMobile.Views;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GreenProjectMobile.ViewsModels
{
    public class QuestDetailViewModel
    {

        readonly HttpClient client;

        private Quest _quest;
        public Quest Quest
        {
            get => _quest;
            set
            {
                _quest = value;
            }
        }

        public ICommand AddQuestCommand { protected set; get; }

        public QuestDetailViewModel (Quest quest)
        {
            this.Quest = quest;
            client = new HttpClient();
            AddQuestCommand = new Command(OnAcceptQuest);
        }


        public void OnAcceptQuest()
        {
            AcceptQuest("addQuest/" + _quest.id);
        }

        public async void  AcceptQuest(string path)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await SecureStorage.GetAsync("Token"));
            string uri = Constants.Constants.BaseUrl + path;
            HttpResponseMessage response = null;
            StringContent content = new StringContent("", Encoding.UTF8, "application/json");

            try
            {
                response = await client.PostAsync(uri, null);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erreur validation", "Une erreur serveur est survenue.", "Ok");
            }

            if (response != null && response.IsSuccessStatusCode == true)
            {
                Console.WriteLine("Response success");
                await Application.Current.MainPage.Navigation.PopModalAsync();

            }
            else if (response != null && response.IsSuccessStatusCode == false)
            {
                string errorMessage = "";
                var result = await response.Content.ReadAsStringAsync();
                AcceptQuestResult error = JsonConvert.DeserializeObject<AcceptQuestResult>(result);
                errorMessage = error.msg;

                await Application.Current.MainPage.DisplayAlert("Erreur validation", errorMessage, "Ok");
            }
        }
    }
}
