using ExchangeAdapters.Core.BinanceData.Requests;
using ExchangeAdapters.Core.BinanceData.Responses;

namespace ExchangeAdapters.Core.Endpoints
{
    public interface IBinanceSpotEndpoints
    {
        public Task<bool> PingAsync();
        public Task<IEnumerable<BinanceKlineResponse>> GetHistoricalKlinesAsync(BinanceKlineRequest request);
    }
}
