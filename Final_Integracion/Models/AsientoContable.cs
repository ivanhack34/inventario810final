using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_Integracion.Models
{
    public class AsientoContable
    {
 
            public int IdentificadorAsiento { get; set; }
            public string Descripcion { get; set; }
            public int TipoInventarioId { get; set; }
            public string CuentaContable { get; set; }
            public int TipoMovimientoId { get; set; }
            public DateTime FechaAsiento { get; set; }
            public decimal MontoAsiento { get; set; }
            public bool Estado { get; set; }
       

    }
}