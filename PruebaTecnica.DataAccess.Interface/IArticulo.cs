using System;
using System.Collections.Generic;
using System.Text;

namespace PruebaTecnica.DataAccess.Interface
{
    public interface IArticulo
    {
        IEnumerable<Model.Articulo> Listar();
    }
}
