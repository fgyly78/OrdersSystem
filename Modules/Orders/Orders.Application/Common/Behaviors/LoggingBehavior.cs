using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Orders.Application.Common.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
        {
            var requestName = typeof(TRequest).Name;

            _logger.LogInformation("Executing {RequestName}", requestName);

            var sw = Stopwatch.StartNew();

            try
            {
                var response = await next();

                _logger.LogInformation(
                    "Completed {RequestName} in {ElapsedMs} ms",
                    requestName,
                    sw.ElapsedMilliseconds);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Failed {RequestName} in {ElapsedMs} ms",
                    requestName,
                    sw.ElapsedMilliseconds);

                throw;
            }
            finally
            {
                sw.Stop();
            }
        }

    }
}
