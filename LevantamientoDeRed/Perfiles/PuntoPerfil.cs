using AutoMapper;
using LevantamientoDeRed.Dto;
using LevantamientoDeRed.Entities;
using NetTopologySuite.Geometries;

namespace LevantamientoDeRed.Perfiles
{
    public class PuntoPerfil : Profile
    {
        public PuntoPerfil(GeometryFactory geometry)
        {
            // DTO to Entity
            CreateMap<CrearPuntoDto, Punto>()
                .ForMember(d => d.Coordenadas, options => options.MapFrom(s => geometry.CreatePoint(new Coordinate(s.Latitud, s.Longitud))));

            // Entity to DTO
            CreateMap<Punto, PuntoDto>()
                .ForMember(d => d.Latitud, options => options.MapFrom(s => s.Coordenadas.X))
                .ForMember(d => d.Longitud, options => options.MapFrom(s => s.Coordenadas.Y));
        }
    }
}
