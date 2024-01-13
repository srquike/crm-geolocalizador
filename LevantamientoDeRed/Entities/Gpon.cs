namespace LevantamientoDeRed.Entities
{
    public class Gpon : Base
    {
        public string Nombre { get; set; }
        public string? PosteId { get; set; }
        public virtual Poste? Poste { get; set; }
        public ICollection<Mufa> Mufas { get; set; } = new List<Mufa>();
    }
}
