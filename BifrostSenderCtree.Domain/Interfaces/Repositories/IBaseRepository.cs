using System.Collections.Generic;
using System.Threading.Tasks;
using BifrostSenderCtree.Domain.Interfaces.Models;

namespace BifrostSenderCtree.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T: IBaseModel, new()
    {
        IEnumerable<T> GetBatch();

        Task<IEnumerable<T>> Find(string filters);
        
        Task<T> FindById(string id);

        Task<bool> DeleteById(string id);
    }
}