using System;
using System.Text.Json.Serialization;
using BifrostSenderCtree.Domain.Interfaces.Models;

namespace BifrostSenderCtree.Domain.Process.Models
{
    public class ProcessTableModel : IBaseModel
    {
        [JsonPropertyName("ts")] public DateTime Timestamp { get; set; }

        private String _id { get; set; }

        [JsonPropertyName("chaveprimaria")]
        public string Id
        {
            get => _id;
            set => _id = value.Trim();
        }

        private string _operator { get; set; }

        [JsonPropertyName("operacao")]
        public string Operator
        {
            get => _operator;
            set => _operator = value.Trim();
        }

        private string _previousData { get; set; }

        [JsonPropertyName("recantes")]
        public string PreviousData
        {
            get => _previousData;
            set => _previousData = value.Trim();
        }

        private string _newData { get; set; }

        [JsonPropertyName("recdepois")]
        public String NewData
        {
            get => _newData;
            set => _newData = value.Trim();
        }

        public void FromString(string payloadModel)
        {
            throw new NotImplementedException();
        }
    }
}