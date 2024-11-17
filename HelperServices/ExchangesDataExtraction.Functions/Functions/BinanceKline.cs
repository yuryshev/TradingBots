using ExchangeAdapters.Core.BinanceData.Requests;
using ExchangeAdapters.Core.Endpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;

namespace ExchangesDataExtraction.Functions.Functions
{
    public class BinanceKline
    {
        private readonly IBinanceSpotEndpoints _binanceSpotEndpoints;

        public BinanceKline(
            IBinanceSpotEndpoints binanceSpotEndpoints
            )
        {
            _binanceSpotEndpoints = binanceSpotEndpoints;
        }

        [Function("BinanceKline")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
        {
            
            long startTime = new DateTimeOffset(new DateTime(2024, 11, 17, 0, 0, 0, DateTimeKind.Utc)).ToUnixTimeMilliseconds();
            Console.WriteLine(startTime);

            try
            {
                var response = await _binanceSpotEndpoints.GetHistoricalKlinesAsync(new BinanceKlineRequest
                {
                    Symbol = "BTCUSDT",
                    Interval = "1h",
                    Limit = 10
                });

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
                

           
        }
    }
}
