using Newtonsoft.Json;

namespace CurrencyModel
{
    public partial class MetaData
    {
        [JsonProperty("1. Information")]
        public string The1Information { get; set; }

        [JsonProperty("2. Digital Currency Code")]
        public string The2DigitalCurrencyCode { get; set; }

        [JsonProperty("3. Digital Currency Name")]
        public string The3DigitalCurrencyName { get; set; }

        [JsonProperty("4. Market Code")]
        public string The4MarketCode { get; set; }

        [JsonProperty("5. Market Name")]
        public string The5MarketName { get; set; }

        [JsonProperty("6. Last Refreshed")]
        public string The6LastRefreshed { get; set; }

        [JsonProperty("7. Time Zone")]
        public string The7TimeZone { get; set; }
    }
}