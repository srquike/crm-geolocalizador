namespace LevantamientoDeRed.Entities
{
    public class Servicio: Base
    {
        public string Nombre { get; set; }
        public virtual ICollection<Contrato> Contratos { get; set; }
    }
}
