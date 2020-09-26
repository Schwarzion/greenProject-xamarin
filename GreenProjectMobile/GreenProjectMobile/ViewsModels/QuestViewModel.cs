using GreenProjectMobile.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using GreenProjectMobile.Views;

namespace GreenProjectMobile.ViewsModels
{
    class QuestViewModel
    {
        readonly HttpClient client;

        public Command SubmitCommand { get; set; }

        public QuestViewModel()
        {
            client = new HttpClient();
            SubmitCommand = new Command(OnSubmit);

        }
        private void OnSubmit(object obj)
        {
            throw new NotImplementedException();
        }

        public string Name
        {
            get
            {
                return Name;
            }
            set
            {
                Name = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }
        public Quests Quests { get; private set; }

        public async void GetQuests()
        {
            Quests response = await GetQuestsRequest();
        }
        public async Task<Quests> GetQuestsRequest()
        {

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await SecureStorage.GetAsync("Token"));
            string uri = Constants.Constants.BaseUrl + "allQuests";
            HttpResponseMessage response = null;
            var quests = new QuestsResult();
            var Quests = new Quests();
            try
            {
                response = await client.GetAsync(uri);
                Debug.WriteLine(response.IsSuccessStatusCode);
                var contents = response.Content.ReadAsStringAsync().Result;
                quests = JsonConvert.DeserializeObject<QuestsResult>(contents);
                Quests.quests.ForEach(Console.WriteLine(Name));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return Quests;
        }
        private void PropertyChanged(QuestViewModel questViewModel, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            throw new NotImplementedException();
        }

    }
}
