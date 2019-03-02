using Newtonsoft.Json;

namespace WeddingWebsite.BusinessLogic.Instagram
{
    public class Config
    {
        public Config(string csrf_token)
        {
            this.CsrfToken = csrf_token;
        }
        [JsonProperty("csrf_token")]
        public string CsrfToken { get; }
    }
}