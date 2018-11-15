using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Punto_de_ventas.Connection
{
    public class Conexion : DataConnection
    {
        public Conexion() : base("dbVentas") { }
    }
}
