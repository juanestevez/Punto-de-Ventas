using LinqToDB;
using LinqToDB.Data;
using Punto_de_ventas.Models;

namespace Punto_de_ventas.Connection
{
    public class Conexion : DataConnection
    {
        public Conexion() : base("dbVentas") { }

        public ITable<Clientes> Cliente { get { return GetTable<Clientes>(); } }

        public ITable<ReportesClientes> Reportes_Clientes { get { return GetTable<ReportesClientes>(); } }
    }
}
