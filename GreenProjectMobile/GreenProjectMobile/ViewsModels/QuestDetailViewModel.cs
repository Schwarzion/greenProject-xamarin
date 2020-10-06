using GreenProjectMobile.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreenProjectMobile.ViewsModels
{
    public class QuestDetailViewModel
    {
        private Quest _quest;
        public Quest Quest
        {
        get => _quest;
            set
            {
                _quest = value;
            }
        }
        public QuestDetailViewModel (Quest quest)
        {
            this.Quest = quest;
        }
    }
}
