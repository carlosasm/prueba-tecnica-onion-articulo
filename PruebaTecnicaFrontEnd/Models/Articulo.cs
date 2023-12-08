using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaTecnicaFrontEnd.Models
{
    public class Articulo
    {
        public int IdArticulo { get; set; }
        public string Descripcion { get; set; }
        public int Sku { get; set; }
        public Marca Marca { get; set; }
    }
}