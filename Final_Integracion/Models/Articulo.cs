using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_Integracion.Models
{
    public class Articulo
    {
        public int IdArticulo { get; set; }
        public string Descripcion { get; set; }
        public int Existencia { get; set; }
        public int TipoInventarioId { get; set; }
        public int AlmacenID { get; set; }
        public int TransaccionId { get; set; }
        public int AsientoId { get; set; }
        public decimal CostoUnitario { get; set; }
        public bool Estado { get; set; }
    }
}