using AutoMapper;
using LevantamientoDeRed.Dto;
using LevantamientoDeRed.Dto.Cliente;
using LevantamientoDeRed.Entities;
using NetTopologySuite.Geometries;

namespace LevantamientoDeRed.Perfiles
{
    public class ClientePerfil:Profile
    {
        public ClientePerfil(GeometryFactory geometry) 
        { 
            // Entity to DTO
            CreateMap<Cliente, ClienteDto>()
                .ForMember(d => d.Latitud, options => options.MapFrom(s => s.Coordenadas.X))
                .ForMember(d => d.Longitud, options => options.MapFrom(s => s.Coordenadas.Y))
                .ForMember(d => d.MufaId, options => options.MapFrom(s => s.MufaId))
                .ForMember(d => d.ContratoId, options => options.MapFrom(s => s.ContratoId));

            // DTO to Entity
            CreateMap<ClienteDto, Cliente>()
                .ForMember(d => d.Coordenadas, options => options.MapFrom(s => geometry.CreatePoint(new Coordinate(s.Latitud, s.Longitud))));

            // Crear ClienteDto
            CreateMap<CrearClienteDto, Cliente>()
                .ForMember(d => d.Coordenadas, options => options.MapFrom(s => geometry.CreatePoint(new Coordinate(s.Latitud, s.Longitud))));
        }
    }
}
