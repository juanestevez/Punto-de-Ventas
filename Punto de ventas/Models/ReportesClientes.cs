using LinqToDB.Mapping;

namespace Punto_de_ventas.Models
{
    public class ReportesClientes
    {
        [PrimaryKey, Identity]
        public int IdRegistro { get; set; }
        public int IdCliente { get; set; }
        public string SaldoActual { get; set; }
        public string FechaActual { get; set; }
        public string UltimoPago { get; set; }
        public string FechaPago { get; set; }
        public string Id { get; set; }
    }
}
