using LinqToDB;
using Punto_de_ventas.Connection;
using Punto_de_ventas.Models;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Punto_de_ventas.ModelClass
{
    public class Cliente : Conexion
    {
        List<ReportesClientes> reporte;

        public List<Clientes> GetClientes()
        {
            IQueryable<Clientes> query = from c in TablaClientes select c;
            return query.ToList();
        }

        public void InsertCliente(string id, string nombre, string apellido, string direccion, string telefono)
        {
            int pos, idCliente;
            using (var db = new Conexion())
            {
                db.Insert(new Clientes()
                {
                    Id = id,
                    Nombre = nombre,
                    Apellido = apellido,
                    Direccion = direccion,
                    Telefono = telefono
                });

                List<Clientes> cliente = GetClientes();
                pos = cliente.Count; // Cantidad de registros almcenados en la lista cliente.
                pos--;
                idCliente = cliente[pos].IdCliente; // Obtiene la id del registro insertado

                db.Insert(new ReportesClientes()
                {
                    IdCliente = idCliente,
                    SaldoActual = "$0.0",
                    FechaActual = "Sin fecha",
                    UltimoPago = "$0.0",
                    FechaPago = "No hay pagos",
                    Id = id
                });
            }
        }

        /// <summary>
        /// Hace una consulta a la tabla clientes de la base de datos.
        /// </summary>
        /// <param name="grid">DataGridView para mostrar los datos.</param>
        /// <param name="campo">Término a buscar. Un string vacío mostrará toda la tabla.</param>
        /// <param name="numPagina">Número de página a mostrar.</param>
        /// <param name="regPorPagina">Número de registros a mostrar por cada página</param>
        public void BuscarCliente(DataGridView grid, string campo, int numPagina, int regPorPagina)
        {
            IEnumerable<Clientes> query;
            int inicio = (numPagina - 1) * regPorPagina;
            if (campo == "") 
            {
                query = from c in TablaClientes select c; // Selecciona todos los datos de la tabla.
            }
            else
            {
                query = from c in TablaClientes where c.Id.StartsWith(campo) || c.Nombre.StartsWith(campo) || 
                        c.Apellido.StartsWith(campo) select c;
            }
            grid.DataSource = query.Skip(inicio).Take(regPorPagina).ToList();
            grid.Columns[0].Visible = false;
            grid.Columns[1].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            grid.Columns[3].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            grid.Columns[5].DefaultCellStyle.BackColor = Color.WhiteSmoke;
        }

        public void GetReporteCliente(DataGridView grid, int idCliente)
        {
            var query = from c in TablaClientes
                        join r in TablaReportesClientes on c.IdCliente equals r.IdCliente
                        where c.IdCliente == idCliente
                        select new
                        {
                            r.IdRegistro,
                            c.Nombre,
                            c.Apellido,
                            r.SaldoActual,
                            r.FechaActual,
                            r.UltimoPago,
                            r.FechaPago
                        };
            grid.DataSource = query.ToList();
            grid.Columns[0].Visible = false;
        }

        public void ActualizarCliente(string id, string nombre, string apellido, string direccion, 
            string telefono, int idCliente)
        {
            TablaClientes.Where(c => c.IdCliente == idCliente)
                .Set(c => c.Id, id)
                .Set(c => c.Nombre, nombre)
                .Set(c => c.Apellido, apellido)
                .Set(c => c.Direccion, direccion)
                .Set(c => c.Telefono, telefono)
                .Update();
            reporte = GetReporte(idCliente);
            TablaReportesClientes.Where(r => r.IdRegistro == reporte[0].IdRegistro)
                .Set(r => r.IdCliente, reporte[0].IdCliente)
                .Set(r => r.SaldoActual, reporte[0].SaldoActual)
                .Set(r => r.FechaActual, reporte[0].FechaActual)
                .Set(r => r.UltimoPago, reporte[0].UltimoPago)
                .Set(r => r.FechaPago, reporte[0].FechaPago)
                .Set(r => r.Id, id)
                .Update();
        }

        public List<ReportesClientes> GetReporte(int idCliente)
        {
            return TablaReportesClientes.Where(r => r.IdCliente == idCliente).ToList(); // Lambda
        }

        public void EliminarCliente(int idCLiente, int idRegistro)
        {
            TablaReportesClientes.Where(r => r.IdRegistro == idRegistro).Delete();
            TablaClientes.Where(c => c.IdCliente == idCLiente).Delete();
        }

        public void ActualizarReporte(string deudaActual, string ultimoPago, int idCliente)
        {
            string fecha = System.DateTime.Now.ToString("dd/MM/yyyy"); // Ej. 26/11/2018
            reporte = GetReporte(idCliente);
            TablaReportesClientes.Where(r => r.IdRegistro == reporte[0].IdRegistro)
                .Set(r => r.IdCliente, reporte[0].IdCliente)
                .Set(r => r.SaldoActual, "$" + deudaActual)
                .Set(r => r.FechaActual, fecha)
                .Set(r => r.UltimoPago, "$" + ultimoPago)
                .Set(r => r.FechaPago, fecha)
                .Set(r => r.Id, reporte[0].Id)
                .Update();
        }
    }
}
