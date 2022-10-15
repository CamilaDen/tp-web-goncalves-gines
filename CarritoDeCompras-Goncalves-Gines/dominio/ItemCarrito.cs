using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class ItemCarrito
    {
        public Articulo articulo { get; set; }     
        public int cantidad { get; set; }
        public decimal precioTotal { get; set; }
    }
}
