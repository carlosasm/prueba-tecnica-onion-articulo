using System;
using System.Data;

namespace PruebaTecnica.DataAccess.Interface
{
    public interface IConnectionManager
    {
        IDbConnection CreateConnection(string keyName);
    }
}
