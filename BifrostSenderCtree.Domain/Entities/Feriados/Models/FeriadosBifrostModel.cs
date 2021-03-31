using BifrostSenderCtree.Domain.Interfaces.Models;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BifrostSenderCtree.Domain.Entities.Feriados.Models
{
    public class FeriadosBifrostModel : IBaseModel
    {
        [JsonPropertyName("codigo_empresa")] 
        public int CodigoEmpresa { get; set; }

        [JsonPropertyName("codigo_filial")] 
        public int CodigoFilial { get; set; }

        [JsonPropertyName("data_feriado")] 
        public DateTime DataFeriado { get; set; }

        [JsonPropertyName("nome_pais")] 
        public string NomePais { get; set; }

        [JsonPropertyName("codigo_unidade_federacao")]
        public string CodigoUnidadeFederacao { get; set; }

        [JsonPropertyName("codigo_localidade")]
        public int CodigoLocalidade { get; set; }

        [JsonPropertyName("nome_feriado")] 
        public string NomeFeriado { get; set; }

        [JsonPropertyName("data_inclusao")] 
        public DateTime DataInclusao { get; set; }

        [JsonPropertyName("data_alteracao")] 
        public DateTime DataAlteracao { get; set; }

        [JsonPropertyName("data_exclusao")] 
        public DateTime DataExclusao { get; set; }

        public void FromString(string payloadModel)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}