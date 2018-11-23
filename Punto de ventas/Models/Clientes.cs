using LinqToDB.Mapping;

namespace Punto_de_ventas.Models
{
    public class Clientes
    {
        [PrimaryKey, Identity] // Llave primaria autoincrementable
        public int IdCliente { get; set; }
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
    }
}
