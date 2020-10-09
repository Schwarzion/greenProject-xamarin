using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using GreenProjectMobile.Models;
using GreenProjectMobile.Services;
using Newtonsoft.Json;

namespace GreenProjectMobile.ViewsModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        readonly HttpClient client;
        public MainViewModel()
        {
            client = HttpClientService.client;
            GetUserquests();
        }

        public Tip Tips { get; private set; }

        private ObservableCollection<Quest> _userQuestList;
        public ObservableCollection<Quest> userQuestList
        {
            get
            {
                return _userQuestList;
            }
            set
            {
                _userQuestList = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public async void GetUserquests()
        {
            QuestsResult quest = new QuestsResult();
            try
            {
                HttpResponseMessage response;
                response = await client.GetAsync("userQuests");
                if (response != null && response.IsSuccessStatusCode == true)
                {
                    var contents = response.Content.ReadAsStringAsync().Result;
                    quest = JsonConvert.DeserializeObject<QuestsResult>(contents);
                    List<Quest> list = new List<Quest>(quest.quests);
                    userQuestList = new ObservableCollection<Quest>(list as List<Quest>);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception quests: {ex}");
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
    }
}

