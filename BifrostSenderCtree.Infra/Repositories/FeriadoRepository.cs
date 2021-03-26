using System.Collections.Generic;
using BifrostSenderCtree.Domain.Entities.Feriados.Models;
using BifrostSenderCtree.Domain.Interfaces.Infrastructure;

namespace BifrostSenderCtree.Infra.Repositories
{
    public class FeriadoRepository : GenericRepository<FeriadosPromaxModel>
    {
        public FeriadoRepository(IDatabase database) : base(database, "admin.efd122", new List<string>()
        {
            "f12200_cd_empresa",
            "f12200_cd_filial",
            "f12200_dt_feriado",
            "f12200_nm_pais",
            "f12200_cd_unidade_federacao",
            "f12200_cd_localidade"
        })
        {
        }
    }
}