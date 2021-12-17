using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OWLNEXT.Business.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OWLNEXT.Workers
{
    public class RatePairWorker : TimedHostedService
    {
        private readonly IRatePairsService _ratePairsService;

        public RatePairWorker(IServiceProvider services) : base(services)
        {
            //using var scope = services.CreateScope();

            _ratePairsService =  services.GetRequiredService<IRatePairsService>();
        }

        protected override TimeSpan Interval => new(0, 10, 0);

        protected override TimeSpan FirstRunAfter => new(0);

        protected override async Task RunJobAsync(IServiceProvider serviceProvider, CancellationToken stoppingToken)
        {
            await _ratePairsService.SaveMoneyRateAsync();
        }
    }
}
