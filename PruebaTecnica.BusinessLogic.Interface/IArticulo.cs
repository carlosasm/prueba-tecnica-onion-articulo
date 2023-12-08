using System.Collections.Generic;

namespace PruebaTecnica.BusinessLogic.Interface
{
    public interface IArticulo
    {
        IEnumerable<Model.Articulo> Listar();
    }
}
