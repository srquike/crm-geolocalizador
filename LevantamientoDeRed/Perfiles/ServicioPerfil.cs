using AutoMapper;
using LevantamientoDeRed.Dto;
using LevantamientoDeRed.Entities;

namespace LevantamientoDeRed.Perfiles
{
    public class ServicioPerfil: Profile
    {
        public ServicioPerfil()
        {
            CreateMap<Servicio, ServicioDto>();
            CreateMap<CrearServicioDto, Servicio>();
            CreateMap<Servicio, CrearServicioDto>();

        }
    }
}
