using LinqToDB;
using LinqToDB.Data;
using Punto_de_ventas.Models;

namespace Punto_de_ventas.Connection
{
    public class Conexion : DataConnection
    {
        public Conexion() : base("dbVentas") { }

        public ITable<Clientes> TablaClientes { get { return GetTable<Clientes>(); } }

        public ITable<ReportesClientes> TablaReportesClientes { get { return GetTable<ReportesClientes>(); } }

        public ITable<Proveedores> TablaProveedores { get { return GetTable<Proveedores>(); } }
    }
}
