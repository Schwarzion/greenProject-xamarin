using GreenProjectMobile.Models;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;

namespace GreenProjectMobile.ViewsModels
{
    public class TestViewModel : INotifyPropertyChanged
    {
        private TestModel _message;

        public TestModel Message
        {
            get
            {
                return _message;
            }
            set
            {
                if(_message != value)
                {
                    _message = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public TestViewModel()
        {
            GetTest();
        }
        public async void GetTest()
        {

            HttpClient client = new HttpClient();
            string uri = "http://192.168.0.47:8000/test";
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                TestModel result = JsonConvert.DeserializeObject<TestModel>(content);
                Message = result;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (propertyName != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
