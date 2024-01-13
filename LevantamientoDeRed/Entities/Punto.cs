using NetTopologySuite.Geometries;

namespace LevantamientoDeRed.Entities
{
    public class Punto : Base
    {
        public string? CableId { get; set; }
        public int Order { get; set; }
        public Point? Coordenadas { get; set; }

        public Cable? Cable { get; set; }
    }
}
