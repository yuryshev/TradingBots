namespace ExchangeAdapters.Core.BinanceData.Requests
{
    public class BinanceKlineRequest
    {
        public string Symbol { get; set; } = default!;
        public string Interval { get; set; } = default!;
        public long StartTime { get; set; }
        public long EndTime { get; set; }
        public int Limit { get; set; }
    }
}
