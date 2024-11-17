using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ExchangeAdapters.Core.BinanceData.Responses
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class BinanceError
    {
        public int Code { get; set; }
        public string Msg { get; set; } = default!;
    }
}
