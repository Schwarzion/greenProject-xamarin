using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreenProjectMobile.Models.ProfileModels
{
    public class ProfileItems
    {
        [JsonProperty("Profile")]
        public List<ProfileModel> Profile { get; set; }
    }
    public class ProfileModel
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string alias { get; set; }
        public int level { get; set; }
        public int userStatus { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string postalCode { get; set; }
        public string email { get; set; }
        public int sexe { get; set; }
        public string phone { get; set; }
        public string birthday { get; set; }
        public int exp { get; set; }
        public int avatar { get; set; }
        public object temparyPasswordValid { get; set; }
    }
}
