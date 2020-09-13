using System.Collections.Generic;

namespace GreenProjectMobile.Models
{
    class Tip
    {
        public int id { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
        public bool tipStatus { get; set; }

    }

    class TipsResult
    {
        public int status { get; set; }
        public List<Tip> tips { get; set; }
        public string msg { get; set; }

    }
}
