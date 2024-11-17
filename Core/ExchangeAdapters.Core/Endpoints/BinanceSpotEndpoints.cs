using ExchangeAdapters.Core.BinanceData.Requests;
using ExchangeAdapters.Core.BinanceData.Responses;
using Newtonsoft.Json;
using System.Text;

namespace ExchangeAdapters.Core.Endpoints
{
    public class BinanceSpotEndpoints : IBinanceSpotEndpoints
    {
        private readonly string BaseAddress = "https://api.binance.com/api/v3/";
        private readonly HttpClient _httpClient;

        public BinanceSpotEndpoints(
            HttpClient httpClient
            )
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(BaseAddress);
        }

        public async Task<bool> PingAsync()
        {
            _httpClient.Timeout = TimeSpan.FromSeconds(1);

            var response = await _httpClient.GetAsync("ping");

            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<BinanceKlineResponse>> GetHistoricalKlinesAsync(BinanceKlineRequest request)
        {
            var requestUriBuilder = new StringBuilder($"klines?symbol={request.Symbol}&interval={request.Interval}");

            if (request.StartTime != default)
            {
                requestUriBuilder.Append($"&startTime={request.StartTime}");
            }

            if (request.EndTime != default)
            {
                requestUriBuilder.Append($"&endTime={request.EndTime}");
            }

            if (request.Limit != default)
            {
                requestUriBuilder.Append($"&limit={request.Limit}");
            }

            var response = await _httpClient.GetAsync(requestUriBuilder.ToString());

            if (!response.IsSuccessStatusCode)
            {
                // handle binance error
                string errorContent = await response.Content.ReadAsStringAsync();
                var error = JsonConvert.DeserializeObject<BinanceError>(errorContent);

                throw new Exception($"Binance error {error.Code}:  {error.Msg}");


            }

            string content = await response.Content.ReadAsStringAsync();

            var klines = JsonConvert.DeserializeObject<List<object[]>>(content);

            var result = new List<BinanceKlineResponse>();

            foreach (var kline in klines)
            {
                result.Add(new BinanceKlineResponse
                {
                    OpenTime = Convert.ToInt64(kline[0]),
                    Open = Convert.ToDecimal(kline[1]),
                    High = Convert.ToDecimal(kline[2]),
                    Low = Convert.ToDecimal(kline[3]),
                    Close = Convert.ToDecimal(kline[4]),
                    Volume = Convert.ToDecimal(kline[5]),
                    CloseTime = Convert.ToInt64(kline[6]),
                    QuoteAssetVolume = Convert.ToDecimal(kline[7]),
                    NumberOfTrades = Convert.ToInt32(kline[8]),
                    TakerBuyBaseAssetVolume = Convert.ToDecimal(kline[9]),
                    TakerBuyQuoteAssetVolume = Convert.ToDecimal(kline[10])
                });
            }

            return result;
        }
    }
}
