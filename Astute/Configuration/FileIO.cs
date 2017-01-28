using System;
using System.IO;
using Newtonsoft.Json;

namespace Astute.Configuration
{
    // ReSharper disable once InconsistentNaming
    public static class FileIO
    {
        public static void SaveConfiguration(Configuration configuration)
        {
            using (var writer = File.CreateText(@"config.json"))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(writer, configuration);
            }
        }

        public static Configuration LoadConfiguration()
        {
            try
            {
                using (var reader = File.OpenText(@"config.json"))
                {
                    var serializer = new JsonSerializer();
                    return (Configuration) serializer.Deserialize(reader, typeof(Configuration));
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}