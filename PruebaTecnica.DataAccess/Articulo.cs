using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PruebaTecnica.DataAccess
{
    public class Articulo : Interface.IArticulo
    {
        #region Atributos
        private readonly Interface.IConnectionManager connectionManager;

        #endregion

        public Articulo(Interface.IConnectionManager connectionManager)
        {
            this.connectionManager = connectionManager;
        }

        public IEnumerable<Model.Articulo> Listar()
        {
            using (var _connection = connectionManager.CreateConnection(ConnectionManager.Prueba_Key))
            {
                var resultado = _connection.Query<Model.Articulo, 
                                                  Model.Marca,
                                                  Model.Articulo>(
                    "usp_ConsultaArticulos",
                    (a, b) =>
                    {
                        a.Marca = b;
                        return a;
                    },
                    splitOn: "IdMarca",
                    commandType: System.Data.CommandType.StoredProcedure);
                return resultado;
            }
        }
    }
}
