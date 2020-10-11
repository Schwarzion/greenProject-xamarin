using System;
using System.Collections.Generic;
using System.Text;

namespace GreenProjectMobile.Models.AuthenticationModels
{
    public class UpdateModel
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string alias { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string postalCode { get; set; }
        public string phone { get; set; }
        public int sexe { get; set; }
    }
}
