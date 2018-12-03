using LinqToDB.Mapping;

namespace Punto_de_ventas.Models
{
    public class Cajas
    {
        [PrimaryKey, Identity]
        public int IdCaja { get; set; }
        public int Caja { get; set; }
        public bool Estado { get; set; }
}
}
