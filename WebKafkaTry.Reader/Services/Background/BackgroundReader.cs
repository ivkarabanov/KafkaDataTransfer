using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace WebKafkaTry.Reader.Services.Background
{
    public sealed class BackgroundReader : BackgroundService
    {
        public BackgroundReader()
        {

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (true)
            {
                await Task.Delay(750);
                Debug.WriteLine("Test back message");
                Console.WriteLine("Test back message");
            }
        }
    }
}
