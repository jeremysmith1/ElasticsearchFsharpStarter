namespace CurrencyModel
{
    using Newtonsoft.Json;
    using System;
     using System.Net;
using System.Collections.Generic;

    public partial class CryptoDailyModel
     {
         [JsonProperty("Meta Data")]
         public MetaData MetaData { get; set; }
 
         [JsonProperty("Time Series (Digital Currency Daily)")]
         public Dictionary<string, TimeSeriesDigitalCurrencyDaily> TimeSeriesDigitalCurrencyDaily { get; set; }
     }
 
     public partial class TimeSeriesDigitalCurrencyDaily
     {
         [JsonProperty("1a. open (USD)")]
         public decimal The1AOpenUsd { get; set; }
 
         [JsonProperty("1b. open (USD)")]
         public decimal The1BOpenUsd { get; set; }
 
         [JsonProperty("2a. high (USD)")]
         public decimal The2AHighUsd { get; set; }
 
         [JsonProperty("2b. high (USD)")]
         public decimal The2BHighUsd { get; set; }
 
         [JsonProperty("3a. low (USD)")]
         public decimal The3ALowUsd { get; set; }
 
         [JsonProperty("3b. low (USD)")]
         public decimal The3BLowUsd { get; set; }
 
         [JsonProperty("4a. close (USD)")]
         public decimal The4ACloseUsd { get; set; }
 
         [JsonProperty("4b. close (USD)")]
         public decimal The4BCloseUsd { get; set; }
 
         [JsonProperty("5. volume")]
         public decimal The5Volume { get; set; }
 
         [JsonProperty("6. market cap (USD)")]
         public decimal The6MarketCapUsd { get; set; }
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
