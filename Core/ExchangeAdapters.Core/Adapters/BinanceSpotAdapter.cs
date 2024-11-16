namespace ExchangeAdapters.Core.Adapters
{
    public class BinanceSpotAdapter : IBinanceSpotAdapter
    {
        private readonly HttpClient _httpClient;

        public BinanceSpotAdapter(
            HttpClient httpClient
            )
        {
            _httpClient = httpClient;
        }

        public async Task<bool> Ping()
        {
            _httpClient.Timeout = TimeSpan.FromSeconds(1);

            var response = await _httpClient.GetAsync("https://api.binance.com/api/v3/ping");

            return response.IsSuccessStatusCode;
        }
    }
}
