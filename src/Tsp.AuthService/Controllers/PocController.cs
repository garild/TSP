using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Tsp.AuthService.Controllers
{
    [Route("[controller]/[action]")]
    public class PocController
    {
        private readonly ILogger logger;
        public PocController(ILoggerFactory loggerFactory)
        {
            logger = loggerFactory.CreateLogger<PocController>();
        }

        [HttpGet]
        public void Info()
        {
            logger.LogInformation($"{DateTime.Now} - Testing fluentd aggregator log");
        }

        [HttpGet]
        public void Error()
        {
            logger.LogError($"{DateTime.Now} - Testing fluentd aggregator log");
        }

        [HttpGet]
        public void Throw()
        {
            throw new Exception($"{DateTime.Now} - Testing fluentd aggregator log");
        }
    }
}
