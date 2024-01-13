namespace LevantamientoDeRed.Entities
{
    public abstract class Base
    {
        public string Id { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaModificacion { get; set; }
    }
}
