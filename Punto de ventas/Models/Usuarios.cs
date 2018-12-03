using LinqToDB.Mapping;

namespace Punto_de_ventas.Models
{
    public class Usuarios
    {
        [PrimaryKey, Identity]
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public string Rol { get; set; }
    }
}
