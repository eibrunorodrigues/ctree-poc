using System.Collections.Generic;
using System.Threading.Tasks;
using BifrostSenderCtree.Domain.Interfaces.Infrastructure;
using BifrostSenderCtree.Domain.Process.Models;

namespace BifrostSenderCtree.Infra.Repositories
{
    public class ProcessRepository : GenericRepository<ProcessTableModel>
    {
        public ProcessRepository(IDatabase database) : base(database, "admin.efd122_processar", "chaveprimaria")
        {
        }
    }
}