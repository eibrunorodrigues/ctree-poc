using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using BifrostSenderCtree.Domain.Entities.Feriados.DTOs;
using BifrostSenderCtree.Domain.Entities.Feriados.Models;
using BifrostSenderCtree.Domain.Interfaces.Services;
using BifrostSenderCtree.Domain.Process.Enums;
using BifrostSenderCtree.Domain.Process.Models;
using CanaaBlueprint.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BifrostSenderCtree
{
    public class Worker : IHostedService
    {
        private readonly ILogger<Worker> _logger;

        private IBaseService<FeriadosPromaxModel> ServiceFeriado { get; }

        private IBaseService<ProcessTableModel> ServiceProcess { get; }

        private IBroker Broker { get; }


        public Worker(ILogger<Worker> logger, IHostApplicationLifetime appLifetime,
            IBaseService<ProcessTableModel> serviceProcess, IBaseService<FeriadosPromaxModel> serviceFeriado)
        {
            ServiceProcess = serviceProcess;
            _logger = logger;

            appLifetime.ApplicationStarted.Register(OnStarted);
            appLifetime.ApplicationStopping.Register(OnStopping);
            appLifetime.ApplicationStopped.Register(OnStopped);
            Broker = new CanaaInfraRabbitMQ.RabbitMq();
            ServiceFeriado = serviceFeriado;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            while (true)
            {

                foreach (var item in ServiceProcess.GetBatch())
                {
                    FeriadosPromaxModel result = await ServiceFeriado.FindById(item.Id);
                    if (result.NomeFeriado is not null && result.NomePais is not null)
                    {
                        var bifrostModel = FeriadosDTO.TransferData(result);
                        var output = OutputModel.Build(item.Timestamp, GetOperationEnum(item.Operator), "feriado", bifrostModel);
                        var resultPublish = Broker.PublishToQueue(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(output)), "PROMAX_VISITA_FERIADO");

                        if (resultPublish)
                        {
                            if (!await ServiceProcess.DeleteById(item.Id))
                            {
                                throw new Exception($"Id {item.Id} Not Found");
                            }
                        }
                    }
                }

                Task.Delay(5000).Wait();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private OperationEnum GetOperationEnum(string operation)
        {
            switch (operation)
            {
                case "A":
                    return OperationEnum.Insert;
                case "D":
                    return OperationEnum.Delete;
                case "U":
                    return OperationEnum.Update;
                default:
                    throw new Exception($"Unknown Operation Type received {operation}");
            }
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