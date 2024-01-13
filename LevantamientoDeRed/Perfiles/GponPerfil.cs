using AutoMapper;
using LevantamientoDeRed.Dto;
using LevantamientoDeRed.Dto.Gpon;
using LevantamientoDeRed.Entities;

namespace LevantamientoDeRed.Perfiles
{
    public class GponPerfil:Profile
    {
        public GponPerfil()
        {
            CreateMap<Gpon, GponDto>()
                .ForMember(d => d.Latitud, options => options.MapFrom(s => s.Poste.Coordenadas.X))
                .ForMember(d => d.Longitud, options => options.MapFrom(s => s.Poste.Coordenadas.Y));

            CreateMap<CrearGponDto, Gpon>();
        }
    }
}
