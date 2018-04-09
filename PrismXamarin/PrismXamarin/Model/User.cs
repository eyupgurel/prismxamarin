using Newtonsoft.Json;


namespace PrismXamarin.Model
{
   public class User
    {
        [JsonProperty(PropertyName = "login")]
        public string userName { get; set; }

        public override string ToString()
        {
            return userName;
        }
    }
}