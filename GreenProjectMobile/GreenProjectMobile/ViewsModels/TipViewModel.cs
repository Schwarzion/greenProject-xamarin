using GreenProjectMobile.Models;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GreenProjectMobile.ViewsModels
{
    class TipViewModel
    {
        readonly HttpClient client;

        public Command SubmitCommand { get; set; }

        public TipViewModel()
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

        public Tips Tips { get; private set; }

        public async void GetTips()
        {
            Tips response = await GetTipsRequest();
        }

        public async Task<Tips>  GetTipsRequest()
        {
            
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await SecureStorage.GetAsync("Token"));
            string uri = Constants.Constants.BaseUrl + "allTips";
            HttpResponseMessage response = null;
            var tips = new TipsResult();
            var Tips = new Tips();
            try
            {
                response = await client.GetAsync(uri);
                Debug.WriteLine(response.IsSuccessStatusCode);
                var contents = response.Content.ReadAsStringAsync().Result;
                tips = JsonConvert.DeserializeObject<TipsResult>(contents);
                tips.tips.ForEach(Console.WriteLine(Name));

                //List<Tips> tips = JsonConvert.DeserializeObject<List<Tips>>(contents);
                //Debug.WriteLine(Tips);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return Tips;
        }
        private void PropertyChanged(TipViewModel tipViewModel, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            throw new NotImplementedException();
        }

    }
}
