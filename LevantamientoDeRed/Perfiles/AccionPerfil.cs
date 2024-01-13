using AutoMapper;
using LevantamientoDeRed.Dto;
using LevantamientoDeRed.Entities;

namespace LevantamientoDeRed.Perfiles
{
    public class AccionPerfil:Profile
    {
        public AccionPerfil()
        {
            CreateMap<Accion,AccionDto>();
        }
    }
}
