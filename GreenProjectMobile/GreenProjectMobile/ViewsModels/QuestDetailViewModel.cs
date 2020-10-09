using GreenProjectMobile.Models;
using System;
using System.Net.Http;
using System.Windows.Input;
using Xamarin.Forms;

namespace GreenProjectMobile.ViewsModels
{
    public class QuestDetailViewModel
    {

        readonly HttpClient client;

        private Quest _quest;
        public Quest Quest
        {
            get => _quest;
            set
            {
                _quest = value;
            }
        }

        public ICommand AddQuestCommand { protected set; get; }

        public QuestDetailViewModel (Quest quest)
        {
            this.Quest = quest;
            AddQuestCommand = new Command(OnAcceptQuest);
        }


        public void OnAcceptQuest()
        {
            AcceptQuest("addQuest/{id}", _quest.id);
        }

        public async void  AcceptQuest(string path, int questId)
        {
            string uri = Constants.Constants.BaseUrl + path;
            HttpResponseMessage response = null;

            try
            {
                response = await client.PostAsync(uri, content);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erreur connexion", "La connexion au serveur a échouée.", "Ok");
            }
        }
    }
}
