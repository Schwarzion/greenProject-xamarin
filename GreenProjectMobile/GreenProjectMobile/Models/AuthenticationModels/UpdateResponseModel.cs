using System.Collections.Generic;

namespace GreenProjectMobile.Models.AuthenticationModels
{
    public class UpdateResponseModel
    {
        public int status { get; set; }
        public User user { get; set; }
        public string msg { get; set; }
    }
    public class User
    {
        public List<string> firstName { get; set; }
        public List<string> lastName { get; set; }
        public List<string> alias { get; set; }
        public List<string> address { get; set; }
        public List<string> city { get; set; }
        public List<string> postalCode { get; set; }
        public List<string> email { get; set; }
        public List<string> sexe { get; set; }
        public List<string> phone { get; set; }

        public List<List<string>> getAllErrors()
        {
            List<List<string>> errorsList = new List<List<string>>
            {
                firstName,
                lastName,
                alias,
                email,
                address,
                city,
                postalCode,
                sexe,
                phone
            };

            return errorsList;
        }
    }

}
