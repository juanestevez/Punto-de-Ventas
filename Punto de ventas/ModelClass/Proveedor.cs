using LinqToDB;
using Punto_de_ventas.Connection;
using Punto_de_ventas.Models;
using System.Collections.Generic;
using System.Linq;

namespace Punto_de_ventas.ModelClass
{
    public class Proveedor : Conexion
    {
        private List<Proveedores> proveedores, proveedores1;

        public List<Proveedores> GetProveedores()
        {
            return TablaProveedores.ToList();
        }

        public List<Proveedores> AgregarProveedor(string nombre, string telefono, string email)
        {
            int pos, idProveedor;
            proveedores = TablaProveedores.Where(p => p.Telefono == telefono || p.Email == email).ToList();

            if (0 == proveedores.Count)
            {
                TablaProveedores.Value(p => p.Nombre, nombre)
                    .Value(p => p.Telefono, telefono)
                    .Value(p => p.Email, email)
                    .Insert();
                List<Proveedores> pd = GetProveedores();
                pos = pd.Count;
                pos--;
                idProveedor = pd[pos].IdProveedor;

                TablaReportesProveedores
                    .Value(r => r.IdProveedor, idProveedor)
                    .Value(r => r.SaldoActual, "$0.0")
                    .Value(r => r.FechaActual, "Sin fecha")
                    .Value(r => r.UltimoPago, "$0.0")
                    .Value(r => r.FechaPago, "No hay pagos")
                    .Insert();
            }
            return proveedores;
        }
    }
}
