using GreenProjectMobile.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using Xamarin.Essentials;
using System.Runtime.CompilerServices;
using System.Linq;

namespace GreenProjectMobile.ViewsModels
{
    class QuestViewModel : INotifyPropertyChanged
    {
        readonly HttpClient client;
        public QuestViewModel()
        {
            client = new HttpClient();
            GetQuests();

        }
        public Quest Quests { get; private set; }

        private ObservableCollection<Quest> _questList;

        public ObservableCollection<Quest> questList
        {
            get
            {
                return _questList;
            }
            set
            {
                _questList = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged ;

        public async void GetQuests()
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await SecureStorage.GetAsync("Token"));
            string uri = Constants.Constants.BaseUrl + "allQuests";
            QuestsResult quests = new QuestsResult();

            try
            {
                HttpResponseMessage response;
                response = await client.GetAsync(uri);
                if (response != null && response.IsSuccessStatusCode == true)
                {
                    var contents = response.Content.ReadAsStringAsync().Result;
                    quests = JsonConvert.DeserializeObject<QuestsResult>(contents);
                    List<Quest> list = new List<Quest>(quests.quests).Where(w => w.questStatus.Equals(1)).ToList();

                    questList = new ObservableCollection<Quest>(list);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception Quests in GetQuests: {ex}");
            }
        }

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public Quest selectedQuest
        {
            get;
            set;
        }
    }
}
