using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Entities
{
    public class TallasVenta
    {
        public int Cod_Prod { get; set; }
        public string Talla { get; set; }
        public int Cantidad { get; set; }
        public double Precio_Prod { get; set; }
        public Producto Producto { get; set; }

    }
}
