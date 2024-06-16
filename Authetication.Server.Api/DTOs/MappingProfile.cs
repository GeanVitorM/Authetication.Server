using Authetication.Server.Api.Models;
using AutoMapper;

namespace Authetication.Server.Api.DTOs;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AdminDto, Admin>();
        CreateMap<Admin, AdminDto>();

        CreateMap<FisioterapeutaDto, Fisioterapeuta>();
        CreateMap<Fisioterapeuta, FisioterapeutaDto>();

        CreateMap<CoordenadorDto, Coordenador>();
        CreateMap<Coordenador, CoordenadorDto>();

        CreateMap<UsuarioDto, Usuario>();
        CreateMap<Usuario, UsuarioDto>();

        CreateMap<PacienteDto, Paciente>();
        CreateMap<Paciente, PacienteDto>();
    }
}