using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BifrostSenderCtree.Domain.Entities.Feriados.Models;
using BifrostSenderCtree.Domain.Interfaces.Infrastructure;

namespace BifrostSenderCtree.Infra.Repositories
{
    public class FeriadoRepository : GenericRepository<FeriadosPromaxModel>
    {
        public FeriadoRepository(IDatabase database) : base(database, "admin.efd122", new Dictionary<string, IList<int>>()
        {
            { "f12200_cd_empresa" , new List<int>() {0,3 } },
            { "f12200_cd_filial", new List<int>() {3,4 } },
            { "f12200_dt_feriado", new List<int>() { 7,8}},
            { "f12200_nm_pais", new List<int>() {15,25 }},
            { "f12200_cd_unidade_federacao", new List<int>() {40,2 }},
            { "f12200_cd_localidade", new List<int>() {42,8 }},
        })
        {
        }

        public override Task<bool> DeleteById(string id)
        {
            throw new Exception("Not allowed to Delete in FeriadoService");
        }
    }
}