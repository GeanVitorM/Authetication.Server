using Authetication.Server.Models;
using AutoMapper;

namespace Authetication.Server.DTOs;

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

        CreateMap<PacienteDto, Paciente>();
        CreateMap<PacienteDto, Paciente>();

        CreateMap<UsuarioDto, Usuario>();
        CreateMap<Usuario, UsuarioDto>();
    }
}