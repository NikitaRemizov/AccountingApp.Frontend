using Newtonsoft.Json;

namespace AccountingApp.Frontend.DataAccess.Models
{
    public class AccessToken
    {

        [JsonProperty("AccessToken")]
        public string Value { get; set; }
    }
}
