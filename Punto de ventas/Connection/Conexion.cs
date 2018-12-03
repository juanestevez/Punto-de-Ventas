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

        public ITable<ReportesProveedores> TablaReportesProveedores { get { return GetTable<ReportesProveedores>(); } }

        public ITable<Usuarios> TablaUsuarios { get { return GetTable<Usuarios>(); } }

        public ITable<Cajas> TablaCajas { get { return GetTable<Cajas>(); } }

        public ITable<CajasRegistros> TablaCajasRegistros { get { return GetTable<CajasRegistros>(); } }
    }
}
