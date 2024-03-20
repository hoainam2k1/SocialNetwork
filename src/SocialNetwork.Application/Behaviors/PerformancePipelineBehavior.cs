﻿using MediatR;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
namespace SocialNetwork.Application.Behaviors
{
    public class PerformancePipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger<TRequest> _logger;

        public PerformancePipelineBehavior(ILogger<TRequest> logger)
        {
            _timer = new Stopwatch();
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _timer.Start();
            var response = await next();
            _timer.Stop();

            var elapsedMilliseconds = _timer.ElapsedMilliseconds;

            if(elapsedMilliseconds <= 5000) return response;

            var requestName = typeof(TRequest).Name;
            _logger.LogWarning("Long Time Running - Request Detail: {Name} ({ElapsedMilliseconds} ,illiseconds) {@Request}", requestName, elapsedMilliseconds, request);
            return response;
        }
    }
}