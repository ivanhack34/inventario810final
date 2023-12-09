using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_Integracion.Models
{
    public class TipoInventario
    {
        public int TipoInventarioId { get; set; }
        public string Descripcion { get; set; }
        public string CuentaContable { get; set; }
        public bool Estado { get; set; }
    }
}