using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    internal class ItemCarrito
    {
        public int cantidad { get; set; }
        public Articulo articulo { get; set; }
        public decimal precioTotal { get; set; }
    }
}
