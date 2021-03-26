using BifrostSenderCtree.Domain.Entities.Feriados.Models;
using BifrostSenderCtree.Domain.Entities.Feriados.Services;
using BifrostSenderCtree.Domain.Interfaces.Infrastructure;
using BifrostSenderCtree.Domain.Interfaces.Repositories;
using BifrostSenderCtree.Domain.Interfaces.Services;
using BifrostSenderCtree.Domain.Process.Models;
using BifrostSenderCtree.Domain.Process.Services;
using BifrostSenderCtree.Infra.DataContext;
using BifrostSenderCtree.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BifrostSenderCtree
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<IDatabase, CtreeSQL>();
                    
                    services.AddTransient<IBaseService<FeriadosPromaxModel>, FeriadoService>();
                    services.AddTransient<IBaseRepository<FeriadosPromaxModel>, FeriadoRepository>();
                    
                    services.AddTransient<IBaseService<ProcessTableModel>, ProcessService>();
                    services.AddTransient<IBaseRepository<ProcessTableModel>, ProcessRepository>();
                    
                    services.AddHostedService<Worker>();
                });
    }
}