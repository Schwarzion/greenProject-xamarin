using GreenProjectMobile.Models;
using GreenProjectMobile.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.CompilerServices;

namespace GreenProjectMobile.ViewsModels
{
    class TipViewModel : INotifyPropertyChanged
    {
        readonly HttpClient client;
        public TipViewModel()
        {
            client = HttpClientService.client;
            GetTips();
        }

        public Tip Tips { get; private set; }

        private ObservableCollection<Tip> _tipList;
        public ObservableCollection<Tip> tipList
        {
            get
            {
                return _tipList;
            }
            set
            {
                _tipList = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public async void GetTips()
        {
            TipsResult tips = new TipsResult();
            try
            {
                HttpResponseMessage response;
                response = await client.GetAsync("allTips");
                if (response != null && response.IsSuccessStatusCode == true)
                {
                    var contents = response.Content.ReadAsStringAsync().Result;
                    tips = JsonConvert.DeserializeObject<TipsResult>(contents);
                    List<Tip> list = new List<Tip>(tips.tips);
                    tipList = new ObservableCollection<Tip>(list as List<Tip>);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception Tips: {ex}");
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
