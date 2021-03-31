using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BifrostSenderCtree.Domain.Interfaces.Repositories;
using BifrostSenderCtree.Domain.Interfaces.Services;
using BifrostSenderCtree.Domain.Process.Models;

namespace BifrostSenderCtree.Domain.Process.Services
{
    public class ProcessService : IBaseService<ProcessTableModel>
    {
        public IBaseRepository<ProcessTableModel> Repository { get; }

        
        public ProcessService(IBaseRepository<ProcessTableModel> repository)
        {
            Repository = repository;
        }
        
        public IEnumerable<ProcessTableModel> GetBatch()
        {
            return Repository.GetBatch();
        }

        public Task<IEnumerable<ProcessTableModel>> Find(string filters)
        {
            throw new NotImplementedException();
        }

        public Task<ProcessTableModel> FindById(string id)
        {
            return Repository.FindById(id);
        }

        public Task<bool> DeleteById(string id)
        {
            return Repository.DeleteById(id);
        }
    }
}