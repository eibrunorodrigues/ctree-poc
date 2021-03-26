using System;
using System.Threading;
using System.Threading.Tasks;
using BifrostSenderCtree.Domain.Entities.Feriados.Models;
using BifrostSenderCtree.Domain.Interfaces.Services;
using BifrostSenderCtree.Domain.Process.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BifrostSenderCtree
{
    public class Worker : IHostedService
    {
        private readonly ILogger<Worker> _logger;
        
        private IBaseService<FeriadosPromaxModel> ServiceFeriado { get; }
        
        private IBaseService<ProcessTableModel> ServiceProcess { get; }

        public Worker(ILogger<Worker> logger, IHostApplicationLifetime appLifetime,
            IBaseService<ProcessTableModel> serviceProcess, IBaseService<FeriadosPromaxModel> serviceFeriado)
        {
            ServiceFeriado = serviceFeriado;
            ServiceProcess = serviceProcess;
            _logger = logger;

            appLifetime.ApplicationStarted.Register(OnStarted);
            appLifetime.ApplicationStopping.Register(OnStopping);
            appLifetime.ApplicationStopped.Register(OnStopped);
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var cursor = await ServiceProcess.GetCursor();
            foreach (var item in cursor)
            {
                var result = await ServiceFeriado.FindById(item.Id);
                Console.WriteLine(result);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private void OnStarted()
        {
            _logger.LogInformation("2. OnStarted has been called");
        }

        private void OnStopping()
        {
            _logger.LogInformation("3. OnStopping has been called");
        }

        private void OnStopped()
        {
            _logger.LogInformation("5. OnStopped has been called");
        }
    }
}