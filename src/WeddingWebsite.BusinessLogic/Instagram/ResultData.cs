using Newtonsoft.Json;

namespace WeddingWebsite.BusinessLogic.Instagram
{
    public class ResultData
    {
        public ResultData(Config config, EntryData entryData)
        {
            Config = config;
            EntryData = entryData;
        }

        public Config Config { get; }

        [JsonProperty("entry_data")]
        public EntryData EntryData { get; }
    }
}