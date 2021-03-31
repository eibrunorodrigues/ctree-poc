using System.Collections.Generic;

namespace BifrostSenderCtree.Domain.Interfaces.Models
{
    public interface IBaseModel
    {
        void FromString(string payloadModel);

        string ToString();
    }
}