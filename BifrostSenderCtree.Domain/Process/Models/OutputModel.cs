using BifrostSenderCtree.Domain.Interfaces.Models;
using BifrostSenderCtree.Domain.Process.Enums;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace BifrostSenderCtree.Domain.Process.Models
{
    public static class OutputModel
    {
        public static IDictionary<string, object> Build(DateTime operationTime, OperationEnum operationType, string entity, IBaseModel document)
        {
            return new Dictionary<string, object>() {
                {"timestamp", operationTime },
                {"operation", operationType.ToString().ToLower() },
                {entity, document},
            };
        }

    }
}
