using ExchangeAdapters.Core.Adapters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;

namespace ExchangesHealthCheck.Functions.Functions
{
    public class BinanceHealthCheck
    {
        private readonly IBinanceSpotAdapter _adapter;

        public BinanceHealthCheck(
            IBinanceSpotAdapter adapter
            )
        {
            _adapter = adapter;
        }

        [Function("BinanceHealthCheck")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
        {
            bool res = await _adapter.Ping();

            return new OkObjectResult(res);
        }
    }
}
