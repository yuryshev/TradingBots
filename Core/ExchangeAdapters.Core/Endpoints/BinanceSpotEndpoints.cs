namespace ExchangeAdapters.Core.Endpoints
{
    public class BinanceSpotEndpoints : IBinanceSpotEndpoints
    {
        private readonly HttpClient _httpClient;

        public BinanceSpotEndpoints(
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
