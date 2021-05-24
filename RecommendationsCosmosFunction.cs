using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace RecommendationsCosmosFunctionApp
{
    public static class RecommendationsCosmosFunction
    {
        [FunctionName("RecommendationsCosmosFunction")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "cosmos/recommendations")] HttpRequest req,
             [CosmosDB(
                databaseName: "sample",
                collectionName: "recommendations",
                ConnectionStringSetting = "CosmosDBConnection",
                SqlQuery = "SELECT  * FROM c ")]
                IEnumerable<Recommendation> recommendations,
            ILogger log)
        {
            return new OkObjectResult(recommendations);
        }

        public class Recommendation
        {
            public string Id { get; set; }
            public string Isbn { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public string Description { get; set; }
            public string ImageURL { get; set; }
        }
    }
}
