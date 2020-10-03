namespace GreenProjectMobile.Models.AuthenticationModels
{
    public class TokenModel
    {

        //public string message { get; set; }
        public string token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
    }
}
