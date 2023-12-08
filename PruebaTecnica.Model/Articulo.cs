using System;

namespace PruebaTecnica.Model
{
    public class Articulo
    {
        public int IdArticulo { get; set; }

        public string Descripcion { get; set; }

        public int IdMarca { get; set; }

        public int Sku { get; set; }

        public Marca Marca { get; set; }
    }
}
