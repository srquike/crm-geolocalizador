using NetTopologySuite.Geometries;

namespace LevantamientoDeRed.Entities
{
    public class Cliente : Base
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public bool Activo { get; set; }
        public Point Coordenadas { get; set; }
        public string? MufaId { get; set; }

        public string? ContratoId { get; set; }

        public Contrato? Contrato { get; set; }

        // Propiedades de navegacion
        public virtual Mufa? Mufa { get; set; }
    }
}
