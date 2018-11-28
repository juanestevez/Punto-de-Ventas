
using LinqToDB.Mapping;

namespace Punto_de_ventas.Models
{
    public class Proveedores
    {
        [PrimaryKey, Identity]
        public int IdProveedor { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
    }
}
