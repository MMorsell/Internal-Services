using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WakeOnLan
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                //TODO: Merge email service and cmd-service to make this fully work from both DevOps and Locally

                var macAddress = Environment.GetEnvironmentVariable("MACADDRESS");
                //if (macAddress != null && macAddress != string.Empty)
                //{
                //    macAddress = apiArgs["macAddress"];
                //}
                await LanConnection.WakeOnLan(macAddress);

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}