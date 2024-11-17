using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using System.Text;

namespace ExchangesDataExtraction.Functions.Functions
{
    public class BinanceExtractDataCsv
    {
        public BinanceExtractDataCsv()
        {

        }

        [Function("BinanceExtractDataCsv")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
        {
            var csvData = new[]
            {
                new { Name = "Alice", Age = 30, Country = "USA" },
                new { Name = "Bob", Age = 25, Country = "UK" },
                new { Name = "Charlie", Age = 35, Country = "Canada" }
            };

            // Build CSV string
            var sb = new StringBuilder();
            sb.AppendLine("OpenTime,Open,High,Low,Close,Volume,CloseTime,QuoteAssetVolume,NumberOfTrades,TakerBuyBaseAssetVolume,TakerBuyQuoteAssetVolume");
            foreach (var item in csvData)
            {
                sb.AppendLine($"{item.Name},{item.Age},{item.Country}");
            }

            // Convert string to byte array
            byte[] csvBytes = Encoding.UTF8.GetBytes(sb.ToString());

            // Return CSV file as a response
            return new FileContentResult(csvBytes, "text/csv")
            {
                FileDownloadName = "sample.csv"
            };
        }
    }
}
