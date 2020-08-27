using GreenProjectMobile.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;

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
            string uri = "http://192.168.1.90:8000/test";
            var response = await client.GetAsync(uri);
            System.Diagnostics.Debug.WriteLine(response);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine("Content" + content);
                TestModel result = JsonConvert.DeserializeObject<TestModel>(content);
                System.Diagnostics.Debug.WriteLine("Result" + result.msg);
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
