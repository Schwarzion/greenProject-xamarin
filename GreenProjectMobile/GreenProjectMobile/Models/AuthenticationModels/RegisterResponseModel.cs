using Java.Util.Functions;
using System.Collections.Generic;

namespace GreenProjectMobile.Models.AuthenticationModels
{
    public class RegisterResponseModel
    {
        public int status { get; set; }
        public Msg msg { get; set; }
    }

    public class Msg
    {
        public List<string> firstName { get; set; }
        public List<string> lastName { get; set; }
        public List<string> alias { get; set; }
        public List<string> email { get; set; }
        public List<string> password { get; set; }
        public List<string> confirmPassword { get; set; }
        public List<string> address { get; set; }
        public List<string> city { get; set; }
        public List<string> postalCode { get; set; }
        public List<string> birthday { get; set; }
        public List<string> sexe { get; set; }
        public List<string> phone { get; set; }

        public List<List<string>> getAllErrors()
        {
            List<List<string>> errorsList = new List<List<string>>();
            errorsList.Add(firstName) ;
            errorsList.Add(lastName);
            errorsList.Add(alias);
            errorsList.Add(email);
            errorsList.Add(password);
            errorsList.Add(confirmPassword);
            errorsList.Add(address);
            errorsList.Add(city);
            errorsList.Add(postalCode);
            errorsList.Add(birthday);
            errorsList.Add(sexe);
            errorsList.Add(phone);

            return errorsList;
        }
    }
}
