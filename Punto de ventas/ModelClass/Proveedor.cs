using LinqToDB;
using Punto_de_ventas.Connection;
using Punto_de_ventas.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Punto_de_ventas.ModelClass
{
    public class Proveedor : Conexion
    {
        private List<Proveedores> proveedores, proveedores1;
        private List<ReportesProveedores> reporte;

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

        public void BuscarProveedor(DataGridView grid, string campo, int numPagina, int regPorPagina)
        {
            IEnumerable<Proveedores> query;
            int inicio = (numPagina - 1) * regPorPagina;
            if (campo == "")
            {
                query = TablaProveedores.ToList();
            }
            else
            {
                query = TablaProveedores.Where(p => p.Nombre.StartsWith(campo) || p.Email.StartsWith(campo) || p.Telefono.StartsWith(campo));
            }
            grid.DataSource = query.Skip(inicio).Take(regPorPagina).ToList();
            grid.Columns[0].Visible = false;
            grid.Columns[1].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            grid.Columns[3].DefaultCellStyle.BackColor = Color.WhiteSmoke;
        }

        public List<Proveedores> ActualizarProveedor(string nombre, string telefono, string email, int idProveedor)
        {
            proveedores = TablaProveedores.Where(p => p.Telefono == telefono).ToList();
            proveedores1 = TablaProveedores.Where(p => p.Email == email).ToList();
            List<Proveedores> list = proveedores.Union(proveedores1).ToList();
            if (2 == list.Count)
            {
                if (idProveedor == proveedores[0].IdProveedor && idProveedor == proveedores1[0].IdProveedor)
                {
                    ActualizarDb();
                }
            }
            else
            {
                if (0 == list.Count) // El teléfono y el email no están registrados
                {
                    ActualizarDb();
                }
                else
                {
                    if (0 != proveedores.Count)
                    {
                        if (idProveedor == proveedores[0].IdProveedor)
                        {
                            ActualizarDb();
                        }
                    }
                    if (0 != proveedores1.Count)
                    {
                        if (idProveedor == proveedores1[0].IdProveedor)
                        {
                            ActualizarDb();
                        }
                    }
                }
            }

            void ActualizarDb()
            {
                TablaProveedores.Where(p => p.IdProveedor == idProveedor)
                            .Set(p => p.Nombre, nombre)
                            .Set(p => p.Telefono, telefono)
                            .Set(p => p.Email, email)
                            .Update();
                list.Clear();
            }
            return list;
        }

        public void BorrarProveedor(int idP, int idR)
        {
            TablaReportesProveedores.Where(p => p.IdRegistro == idR).Delete();
            TablaProveedores.Where(p => p.IdProveedor == idP).Delete();
        }

        public void ObtenerReporte(DataGridView grid, int id)
        {
            var query = from p in TablaProveedores
                        join r in TablaReportesProveedores on p.IdProveedor equals r.IdProveedor
                        where p.IdProveedor == id
                        select new
                        {
                            r.IdRegistro,
                            p.Nombre,
                            r.SaldoActual,
                            r.FechaActual,
                            r.UltimoPago,
                            r.FechaPago
                        };
            grid.DataSource = query.ToList();
            grid.Columns[0].Visible = false;
        }

        public void ActualizarReporte(string deudaActual, string ultimoPago, int idProveedor)
        {
            string fechaActual = DateTime.Now.ToString("dd/MM/yyyy");
            reporte = GetReporte(idProveedor);
            TablaReportesProveedores.Where(r => r.IdRegistro == reporte[0].IdRegistro)
                .Set(r => r.IdProveedor, idProveedor)
                .Set(r => r.SaldoActual, "$" + deudaActual)
                .Set(r => r.FechaActual, fechaActual)
                .Set(r => r.UltimoPago, "$" + ultimoPago)
                .Set(r => r.FechaPago, fechaActual)
                .Update();
        }

        private List<ReportesProveedores> GetReporte(int idProveedor)
        {
            return TablaReportesProveedores.Where(r => r.IdProveedor == idProveedor).ToList();
        }
    }
}
