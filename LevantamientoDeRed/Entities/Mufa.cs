namespace LevantamientoDeRed.Entities
{
    public class Mufa : Base
    {
        public string Tipo { get; set; }
        public string Nombre { get; set; }
        public string? GponId { get; set; }
        public string? PosteId { get; set; }

        public virtual ICollection<Cliente>? Clientes { get; set; }
        public virtual Gpon? Gpon { get; set; }
        public virtual Poste? Poste { get; set; }
    }
}
