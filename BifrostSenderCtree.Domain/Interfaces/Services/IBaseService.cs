using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BifrostSenderCtree.Domain.Interfaces.Services
{
    public interface IBaseService<T> where T : class
    {
        Task<IEnumerable<T>> GetCursor();

        Task<IEnumerable<T>> Find(string filters);

        Task<T> FindById(string id);

        Task<bool> DeleteById(string id);
    }
}