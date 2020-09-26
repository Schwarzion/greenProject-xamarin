using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace GreenProjectMobile.Models
{

    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    internal sealed class OptionalAttribute : Attribute
    {
    }
    public class Quest
    {
        public int id { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
        public int expAmount { get; set; }
        public int minLevel { get; set; }
        public int timeForQuest { get; set; }
        public string endDate { get; set; }
        public int questStatus { get; set; }

        [Optional]
        public Pivot pivot { get; set; }
    }

    public class QuestsResult
    {
        public int status { get; set; }
        public List<Quest> quests { get; set; }
        public string msg { get; set; }
    }

    public class Pivot
    {
        public int userId { get; set; }
        public int questId { get; set; }
        public DateTime expire { get; set; }
    }
}
