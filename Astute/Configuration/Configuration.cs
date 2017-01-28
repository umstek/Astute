using Newtonsoft.Json;

namespace Astute.Configuration
{
    [JsonObject]
    public class Configuration
    {
        public static Configuration Config { get; } = FileIO.LoadConfiguration() ?? new Configuration();

        public int GridSize { get; set; } = 10;

        public string ListenOnIP { get; set; } = "127.0.0.1";

        public int ListenOnPort { get; set; } = 7000;

        public string SendToIP { get; set; } = "127.0.0.1";

        public int SendToPort { get; set; } = 6000;

        #region Hacks

        public bool EnableBurstFire { get; set; } = false;

        #endregion
    }
}