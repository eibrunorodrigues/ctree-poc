using System;
using System.Globalization;
using BifrostSenderCtree.Domain.Entities.Feriados.Models;
using BifrostSenderCtree.Domain.Interfaces.Models;

namespace BifrostSenderCtree.Domain.Entities.Feriados.DTOs
{
    public static class FeriadosDTO
    {
        public static IBaseModel TransferData(FeriadosPromaxModel model)
        {
            FeriadosBifrostModel transformedModel = new FeriadosBifrostModel();
            return new FeriadosBifrostModel
            {
                CodigoEmpresa = (int) model.CodigoEmpresa,
                CodigoFilial = (int) model.Filial,
                DataFeriado = Utils.Types.StringToDateTime(model.DataFeriado, "yyyyMMdd"),
                NomePais = model.NomePais.Trim(),
                CodigoUnidadeFederacao = model.CodigoUnidadeFederacao.Trim(),
                CodigoLocalidade = (int) model.CodigoLocalidade,
                NomeFeriado = model.NomeFeriado.Trim(),
                DataInclusao = Utils.Types.StringToDateTime(model.DataInclusao, "ddMMyyyy"),
                DataAlteracao = Utils.Types.StringToDateTime(model.DataAlteracao, "ddMMyyyy"),
                DataExclusao = Utils.Types.StringToDateTime(model.DataExclusao, "ddMMyyyy"),
            };
        }
    }
}