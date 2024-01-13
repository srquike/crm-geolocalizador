using AutoMapper;
using LevantamientoDeRed.Dto;
using LevantamientoDeRed.Entities;

namespace LevantamientoDeRed.Perfiles
{
    public class UsuarioPerfil : Profile
    {
        public UsuarioPerfil()
        {
            // Entidad a DTO
            CreateMap<Usuario, UsuarioDto>()
                .ForMember(d => d.Rol, opciones => opciones.MapFrom(s => s.UsuarioRoles.FirstOrDefault().Rol.Name))
                .ForMember(d => d.Nombre, opciones => opciones.MapFrom(s => string.Concat(s.Nombre, " ", s.Apellido)));

            CreateMap<Usuario, EditarUsuarioDto>();

            // DTO a Entidad
            CreateMap<CrearUsuarioDto, Usuario>();
            CreateMap<EditarUsuarioDto, Usuario>();
        }
    }
}
