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
    public class Cliente : Conexion
    {
        List<ReportesClientes> reporte;

        public List<Clientes> GetClientes()
        {
            IQueryable<Clientes> query = from c in Cliente select c;
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
                    SaldoActual = "0.0",
                    FechaActual = "Sin fecha",
                    UltimoPago = "0.0",
                    FechaPago = "Sin fecha",
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
                query = from c in Cliente select c; // Selecciona todos los datos de la tabla.
            }
            else
            {
                query = from c in Cliente where c.Id.StartsWith(campo) || c.Nombre.StartsWith(campo) select c;
            }
            grid.DataSource = query.Skip(inicio).Take(regPorPagina).ToList();
            grid.Columns[0].Visible = false;
            grid.Columns[1].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            grid.Columns[3].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            grid.Columns[5].DefaultCellStyle.BackColor = Color.WhiteSmoke;
        }

        public void GetReporteCliente(DataGridView grid, int idCliente)
        {
            var query = from c in Cliente
                        join r in Reportes_Clientes on c.IdCliente equals r.IdCliente
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
            Cliente.Where(c => c.IdCliente == idCliente)
                .Set(c => c.Id, id)
                .Set(c => c.Nombre, nombre)
                .Set(c => c.Apellido, apellido)
                .Set(c => c.Direccion, direccion)
                .Set(c => c.Telefono, telefono)
                .Update();
            reporte = GetReporte(idCliente);
        }

        public List<ReportesClientes> GetReporte(int idCliente)
        {
            return Reportes_Clientes.Where(r => r.IdCliente == idCliente).ToList(); // Lambda
        }
    }
}
