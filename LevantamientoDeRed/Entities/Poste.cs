using NetTopologySuite.Geometries;

namespace LevantamientoDeRed.Entities
{
    public class Poste : Base
    {
        public string Nombre { get; set; }
        public Point? Coordenadas { get; set; }

        public ICollection<Gpon> Gpons { get; set; } = new List<Gpon>();
        public ICollection<Mufa> Mufas { get; set; } = new List<Mufa>();
    }
}
