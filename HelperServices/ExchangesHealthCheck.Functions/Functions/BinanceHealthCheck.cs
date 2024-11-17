using ExchangeAdapters.Core.Endpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;

namespace ExchangesHealthCheck.Functions.Functions
{
    public class BinanceHealthCheck
    {
        private readonly IBinanceSpotEndpoints _adapter;

        public BinanceHealthCheck(
            IBinanceSpotEndpoints adapter
            )
        {
            _adapter = adapter;
        }

        [Function("BinanceHealthCheck")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
        {
            bool res = await _adapter.PingAsync();

            return new OkObjectResult(res);
        }
    }
}
