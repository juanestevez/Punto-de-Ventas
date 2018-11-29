using LinqToDB.Mapping;

namespace Punto_de_ventas.Models
{
    public class ReportesProveedores
    {
        [PrimaryKey, Identity]
        public int IdRegistro { get; set; }
        public int IdProveedor { get; set; }
        public string SaldoActual { get; set; }
        public string FechaActual { get; set; }
        public string UltimoPago { get; set; }
        public string FechaPago { get; set; }
    }
}
