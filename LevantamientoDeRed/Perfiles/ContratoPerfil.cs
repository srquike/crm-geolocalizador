using AutoMapper;
using LevantamientoDeRed.Dto;
using LevantamientoDeRed.Entities;
using Microsoft.Extensions.Options;

namespace LevantamientoDeRed.Perfiles
{
    public class ContratoPerfil:Profile
    {
        public ContratoPerfil()
        {
            CreateMap<Contrato, ContratoDto>()
                .ForMember(d=>d.Servicio,Options=>Options.MapFrom(s=>s.Servicio.Nombre));
            CreateMap<CrearContratoDto, Contrato>();
            CreateMap<Contrato, CrearContratoDto>();
            


        }
    }
}
