using AutoMapper;
using LevantamientoDeRed.Dto;
using LevantamientoDeRed.Entities;

namespace LevantamientoDeRed.Perfiles
{
    public class MufaPerfil:Profile
    {
        public MufaPerfil()
        {
            CreateMap<Mufa, MufaDto>()
                .ForMember(d => d.GponId, options => options.MapFrom(s => s.GponId))
                .ForMember(d => d.PosteId, options => options.MapFrom(s => s.PosteId))
                .ForMember(d => d.Latitud, options => options.MapFrom(s => s.Poste.Coordenadas.X))
                .ForMember(d => d.Longitud, options => options.MapFrom(s => s.Poste.Coordenadas.Y));
                
            CreateMap<CrearMufaDTO, Mufa>();
        }
    }
}
