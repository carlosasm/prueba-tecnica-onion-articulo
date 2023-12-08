using System;
using System.Collections.Generic;

namespace PruebaTecnica.BusinessLogic
{
    public class Articulo : Interface.IArticulo
    {
        #region Atributos
        private readonly DataAccess.Interface.IArticulo articuloDataAccess;

        #endregion

        public Articulo(DataAccess.Interface.IArticulo articuloDataAccess)
        {
            this.articuloDataAccess = articuloDataAccess;
        }

        public IEnumerable<Model.Articulo> Listar()
        {
            return articuloDataAccess.Listar();
        }
    }
}
