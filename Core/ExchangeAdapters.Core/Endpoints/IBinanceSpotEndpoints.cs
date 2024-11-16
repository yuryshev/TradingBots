namespace ExchangeAdapters.Core.Endpoints
{
    public interface IBinanceSpotEndpoints
    {
        public Task<bool> Ping();
    }
}
