using LinqToDB;
using Punto_de_ventas.Connection;
using Punto_de_ventas.Models;
using System.Collections.Generic;
using System.Linq;

namespace Punto_de_ventas.ModelClass
{
    public class Caja : Conexion
    {
        public List<Cajas> getCaja()
        {
            return TablaCajas.Where(c => c.Estado == true).ToList();
        }

        public void UpdateCaja(int idCaja, bool estado)
        {
            TablaCajasRegistros.Where(c => c.IdCaja == idCaja)
                .Set(c => c.Estado, estado)
                .Update();
        }

        public void InsertarCajasTemporal(int idUsuario, string nombre, string apellido, string usuario, string rol, int idCaja, int caja, bool estado, string hora, string fecha)
        {
            TablaCajasRegistros.Value(c => c.IdUsuario, idUsuario)
                .Value(c => c.Nombre, nombre)
                .Value(c => c.Apellido, apellido)
                .Value(c => c.Usuario, usuario)
                .Value(c => c.Rol, rol)
                .Value(c => c.IdCaja, idCaja)
                .Value(c => c.Caja, caja)
                .Value(c => c.Estado, estado)
                .Value(c => c.Hora, hora)
                .Value(c => c.Fecha, fecha)
                .Insert();
        }
    
    }
}
