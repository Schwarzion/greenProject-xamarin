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

namespace GreenProjectMobile.ViewsModels
{
    class QuestViewModel : INotifyCollectionChanged
    {
        readonly HttpClient client;
        public QuestViewModel()
        {
            client = new HttpClient();
            GetQuests();

        }
        public Quest Quests { get; private set; }

        public ObservableCollection<Quest> questList { get; private set; }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public async void GetQuests()
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await SecureStorage.GetAsync("Token"));
            string uri = Constants.Constants.BaseUrl + "allQuests";
            HttpResponseMessage response = null;
            QuestsResult quests = new QuestsResult();

            try
            {
                response = await client.GetAsync(uri);
                if (response != null && response.IsSuccessStatusCode == true)
                {
                    var contents = response.Content.ReadAsStringAsync().Result;
                    quests = JsonConvert.DeserializeObject<QuestsResult>(contents);
                    List<Quest> list = new List<Quest>(quests.quests);
                    questList = new ObservableCollection<Quest>(list as List<Quest>);
                    foreach (Quest elem in questList)
                    {
                        Debug.WriteLine(elem);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception Quests: {ex}");
            }
        }

        public async Task<ObservableCollection<Quest>> GetQuestsRequest()
        {

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await SecureStorage.GetAsync("Token"));
            string uri = Constants.Constants.BaseUrl + "allQuests";
            HttpResponseMessage response = null;
            QuestsResult quests = new QuestsResult();
            List<Quest> list = new List<Quest>();
            try
            {
                response = await client.GetAsync(uri);
                if (response != null && response.IsSuccessStatusCode == true)
                {
                    var contents = response.Content.ReadAsStringAsync().Result;
                    quests = JsonConvert.DeserializeObject<QuestsResult>(contents);
                    list = quests.quests;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception Quests: {ex}");
            }
            ObservableCollection<Quest> obsList = new ObservableCollection<Quest>(list as List<Quest>);
            foreach (Quest elem in obsList)
            {
                Debug.WriteLine(elem.name);
            }
            return obsList;
        }

    }
}
