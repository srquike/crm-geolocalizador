namespace LevantamientoDeRed.Entities
{
    public class Cable : Base
    {
        public string? Tipo { get; set; }
        public string? Estado { get; set; }

        public virtual ICollection<Punto>? Puntos { get; set; }
    }
}
