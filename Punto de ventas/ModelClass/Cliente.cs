using LinqToDB;
using Punto_de_ventas.Connection;
using Punto_de_ventas.Models;
using System.Collections.Generic;
using System.Linq;

namespace Punto_de_ventas.ModelClass
{
    public class Cliente : Conexion
    {
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

        public IEnumerable<Clientes> BuscarCliente(string campo, int numPagina, int regPorPagina)
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
            return query.Skip(inicio).Take(regPorPagina).ToList();
        }
    }
}
