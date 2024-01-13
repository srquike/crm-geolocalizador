using AutoMapper;
using LevantamientoDeRed.Dto;
using LevantamientoDeRed.Entities;

namespace LevantamientoDeRed.Perfiles
{
    public class CablePerfil:Profile
    {
        public CablePerfil()
        {
            CreateMap<AgregarCableDto, Cable>();
            CreateMap<Cable, CableDto>();
        }
    }
}
