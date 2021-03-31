using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BifrostSenderCtree.Domain.Entities.Feriados.DTOs;
using BifrostSenderCtree.Domain.Interfaces.Repositories;
using BifrostSenderCtree.Domain.Interfaces.Services;
using BifrostSenderCtree.Domain.Entities.Feriados.Models;
using BifrostSenderCtree.Domain.Interfaces.Models;

namespace BifrostSenderCtree.Domain.Entities.Feriados.Services
{
    public class FeriadoService : IGenericService, IBaseService<FeriadosPromaxModel>
    {
        public IBaseRepository<FeriadosPromaxModel> Repository { get; }

        
        public FeriadoService(IBaseRepository<FeriadosPromaxModel> repository)
        {
            Repository = repository;
        }
        
        public IEnumerable<FeriadosPromaxModel> GetBatch()
        {
            var result = Repository.GetBatch();
            List<FeriadosBifrostModel> listOfFeriados = new List<FeriadosBifrostModel>();
            
            foreach (var entityModel in result)
            {
                var model = FeriadosDTO.TransferData(entityModel);
                yield return (FeriadosPromaxModel)model;
            }
        }

        public Task<IEnumerable<FeriadosPromaxModel>> Find(string filters)
        {
            throw new NotImplementedException();
        }

        public async Task<FeriadosPromaxModel> FindById(string id)
        {
            return await Repository.FindById(id);
        }

        public Task<bool> DeleteById(string id)
        {
            throw new NotImplementedException();
        }
    }
}