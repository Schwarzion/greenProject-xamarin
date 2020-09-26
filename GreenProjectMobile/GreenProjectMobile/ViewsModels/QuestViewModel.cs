using GreenProjectMobile.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using GreenProjectMobile.Views;
using System.Runtime.CompilerServices;

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
                    Debug.WriteLine("DEBUG contents : " + contents);
                    quests = JsonConvert.DeserializeObject<QuestsResult>(contents);
                    Debug.WriteLine("DEBUG quests : " + quests);
                    List<Quest> list = new List<Quest>(quests.quests);
                    Debug.WriteLine("DEBUG list : " + list);
                    questList = new ObservableCollection<Quest>(list as List<Quest>);
                    foreach (Quest elem in questList)
                    {
                        Debug.WriteLine("DEBUG elem " + elem);
                    }
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

        //public async Task<ObservableCollection<Quest>> GetQuestsRequest()
        //{

        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await SecureStorage.GetAsync("Token"));
        //    string uri = Constants.Constants.BaseUrl + "allQuests";
        //    HttpResponseMessage response = null;
        //    QuestsResult quests = new QuestsResult();
        //    List<Quest> list = new List<Quest>();
        //    try
        //    {
        //        response = await client.GetAsync(uri);
        //        if (response != null && response.IsSuccessStatusCode == true)
        //        {
        //            var contents = response.Content.ReadAsStringAsync().Result;
        //            quests = JsonConvert.DeserializeObject<QuestsResult>(contents);
        //            list = quests.quests;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine($"Exception Quests in GetQuestsRequest : {ex}");
        //    }
        //    ObservableCollection<Quest> obsList = new ObservableCollection<Quest>(list as List<Quest>);
        //    foreach (Quest elem in obsList)
        //    {
        //        Debug.WriteLine("DEBUG elem.name : " + elem.name);
        //    }
        //    return obsList;
        //}

    }
}
