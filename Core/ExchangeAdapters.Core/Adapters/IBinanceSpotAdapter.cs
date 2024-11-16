namespace ExchangeAdapters.Core.Adapters
{
    public interface IBinanceSpotAdapter
    {
        public Task<bool> Ping();
    }
}
