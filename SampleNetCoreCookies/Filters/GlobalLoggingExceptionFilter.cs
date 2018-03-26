using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleNetCoreCookies.Filters
{
    public class GlobalLoggingExceptionFilter : IExceptionFilter
    {
        private readonly ILogger _logger;

        public GlobalLoggingExceptionFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("GlobalLoggingExceptionFilter.");
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogInformation("OnException");
        }
    }
}
