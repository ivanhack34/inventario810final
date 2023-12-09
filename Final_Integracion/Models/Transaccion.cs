using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_Integracion.Models
{
    public class Transaccion
    {
        public int TransaccionId { get; set; }
        public int TipoTransaccionId { get; set; }
        public int IdArticulo { get; set; }
        public DateTime Fecha { get; set; }
        public int Cantidad { get; set; }
        public decimal Monto { get; set; }
    }
}