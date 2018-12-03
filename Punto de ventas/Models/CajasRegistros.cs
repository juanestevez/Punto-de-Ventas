using LinqToDB.Mapping;

namespace Punto_de_ventas.Models
{
    public class CajasRegistros
    {
        [PrimaryKey, Identity]
        public int IdCajaTempo { get; set; }
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Usuario { get; set; }
        public string Rol { get; set; }
        public int IdCaja { get; set; }
        public int Caja { get; set; }
        public bool Estado { get; set; }
        public string Hora { get; set; }
        public string Fecha { get; set; }
    }
}
