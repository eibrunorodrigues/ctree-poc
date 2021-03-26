using System;
using System.Data;

namespace BifrostSenderCtree.Domain.Interfaces.Infrastructure
{
    public interface IDatabase
    {
        IDbCommand  Command { get; set; }
    }
}