using System;

namespace GreenProjectMobile.Models.AuthenticationModels
{
    public class RegisterModel
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string alias { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string postalCode { get; set; }
        public string phone { get; set; }
        public int sexe { get; set; }
        public DateTime birthday { get; set; }
    }
}
