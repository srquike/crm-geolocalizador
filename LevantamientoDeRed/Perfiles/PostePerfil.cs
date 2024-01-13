using AutoMapper;
using LevantamientoDeRed.Dto;
using LevantamientoDeRed.Entities;
using NetTopologySuite.Geometries;

namespace LevantamientoDeRed.Perfiles
{
    public class PostePerfil : Profile
    {
        public PostePerfil(GeometryFactory geometry)
        {
            // DTO a Entity
            CreateMap<CrearPosteDto, Poste>()
                .ForMember(d => d.Coordenadas, options => options.MapFrom(s => geometry.CreatePoint(new Coordinate(s.Latitud, s.Longitud))));

            // Entity a DTO
            CreateMap<Poste, PosteDto>()
                .ForMember(d => d.Latitud, options => options.MapFrom(s => s.Coordenadas.X))
                .ForMember(d => d.Longitud, options => options.MapFrom(s => s.Coordenadas.Y))
                .ForMember(d => d.GponId, options => options.MapFrom(s => s.Gpons.FirstOrDefault().Id))
                .ForMember(d => d.MufaId, options => options.MapFrom(s => s.Mufas.FirstOrDefault().Id));
        }
    }
}
