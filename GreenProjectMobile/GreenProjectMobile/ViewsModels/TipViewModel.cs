using GreenProjectMobile.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace GreenProjectMobile.ViewsModels
{
    class TipViewModel : INotifyCollectionChanged
    {
        readonly HttpClient client;

        public TipViewModel()
        {
            client = new HttpClient();
            GetTips();
        }

        public Tip Tips { get; private set; }

        public ObservableCollection<Tip> tipList { get; private set; }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public async void GetTips()
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await SecureStorage.GetAsync("Token"));
            string uri = Constants.Constants.BaseUrl + "allTips";
            HttpResponseMessage response = null;
            TipsResult tips = new TipsResult();
            
            try
            {
                response = await client.GetAsync(uri);
                if (response != null && response.IsSuccessStatusCode == true)
                {
                    var contents = response.Content.ReadAsStringAsync().Result;
                    tips = JsonConvert.DeserializeObject<TipsResult>(contents);
                    List<Tip> list = new List<Tip>(tips.tips);
                    tipList = new ObservableCollection<Tip>(list as List<Tip>);
                    foreach (Tip elem in tipList)
                    {
                        Debug.WriteLine(elem);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception Tips: {ex}");
            }
        }

        public async Task<ObservableCollection<Tip>> GetTipsRequest()
        {

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await SecureStorage.GetAsync("Token"));
            string uri = Constants.Constants.BaseUrl + "allTips";
            HttpResponseMessage response = null;
            TipsResult tips = new TipsResult();
            List<Tip> list = new List<Tip>();
            try
            {
                response = await client.GetAsync(uri);
                if (response != null && response.IsSuccessStatusCode == true)
                {
                    var contents = response.Content.ReadAsStringAsync().Result;
                    tips = JsonConvert.DeserializeObject<TipsResult>(contents);
                    list = tips.tips;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception Tips: {ex}");
            }
            ObservableCollection<Tip> obsList = new ObservableCollection<Tip>(list as List<Tip>);
            foreach (Tip elem in obsList)
            {
                Debug.WriteLine(elem.name);
            }
            return obsList;
        }
    }
}
