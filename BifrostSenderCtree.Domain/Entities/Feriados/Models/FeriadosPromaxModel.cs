using System.Text.Json.Serialization;
using BifrostSenderCtree.Domain.Interfaces.Models;

namespace BifrostSenderCtree.Domain.Entities.Feriados.Models
{
    public class FeriadosPromaxModel : IBaseModel
    {
        [JsonPropertyName("f12200_cd_empresa")]
        public decimal CodigoEmpresa { get; set; }

        [JsonPropertyName("f12200_cd_filial")] 
        public decimal Filial { get; set; }
        
        [JsonPropertyName("f12200_dt_feriado")]
        public decimal DataFeriado { get; set; }

        [JsonPropertyName("f12200_nm_pais")]
        public string NomePais { get; set; }

        [JsonPropertyName("f12200_cd_unidade_federacao")]
        public string CodigoUnidadeFederacao { get; set; }

        [JsonPropertyName("f12200_cd_localidade")]
        public decimal CodigoLocalidade { get; set; }

        [JsonPropertyName("f12200_nm_feriado")]
        public string NomeFeriado { get; set; }
        
        [JsonPropertyName("f12200_dt_alteracao")]
        public decimal DataAlteracao { get; set; }

        [JsonPropertyName("f12200_dt_inclusao")]
        public decimal DataInclusao { get; set; }
        
        [JsonPropertyName("f12200_dt_exclusao")]
        public decimal DataExclusao { get; set; }

        [JsonPropertyName("f12200_filler")]
         public string Filler { get; set; }
        
        public void FromString(string payloadModel)
        {
            throw new System.NotImplementedException();
        }
    }
}