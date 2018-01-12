namespace CurrencyModel
{
    using System;
    using System.Net;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public partial class CryptoModel
    {
        [JsonProperty("Meta Data")]
        public MetaData MetaData { get; set; }

        [JsonProperty("Time Series (Digital Currency Intraday)")]
        public Dictionary<string, TimeSeriesDigitalCurrencyIntraday> TimeSeriesDigitalCurrencyIntraday { get; set; }
    }

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

        [JsonProperty("6. Interval")]
        public string The6Interval { get; set; }

        [JsonProperty("7. Last Refreshed")]
        public string The7LastRefreshed { get; set; }

        [JsonProperty("8. Time Zone")]
        public string The8TimeZone { get; set; }
    }

    public partial class TimeSeriesDigitalCurrencyIntraday
    {
        [JsonProperty("1a. price (USD)")]
        public decimal The1APriceUsd { get; set; }

        [JsonProperty("1b. price (USD)")]
        public decimal The1BPriceUsd { get; set; }

        [JsonProperty("2. volume")]
        public decimal The2Volume { get; set; }

        [JsonProperty("3. market cap (USD)")]
        public decimal The3MarketCapUsd { get; set; }
    }

    public partial class Empty
    {
        public static Empty FromJson(string json) => JsonConvert.DeserializeObject<Empty>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Empty self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    public class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
    }
}