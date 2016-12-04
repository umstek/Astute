using Newtonsoft.Json;

namespace Astute.Configuration
{
    [JsonObject]
    public class Configuration
    {
        [JsonProperty]
        [JsonRequired]
        public int GridSize { get; set; }

        [JsonProperty]
        [JsonRequired]
        public string ListenOnIP { get; set; }

        [JsonProperty]
        [JsonRequired]
        public int ListenOnPort { get; set; }

        [JsonProperty]
        [JsonRequired]
        public string SendToIP { get; set; }

        [JsonProperty]
        [JsonRequired]
        public int SendToPort { get; set; }

        #region Hacks

        [JsonIgnore]
        public bool EnableBurstFire { get; set; }

        #endregion
    }
}