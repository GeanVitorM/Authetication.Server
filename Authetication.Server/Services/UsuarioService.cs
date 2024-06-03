using Authetication.Server.DTOs;
using Authetication.Server.Models;
using Authetication.Server.Repository;
using AutoMapper;

namespace Authetication.Server.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IMapper  _mapper;
    private readonly IUsuarioRepository _repository;

    public UsuarioService(IMapper mapper , IUsuarioRepository repository)
    {
        _mapper = mapper;
        repository = repository;
    }

    public async  Task CreateUsuario(UsuarioDto usuarioDto)
    {
        try
        {
            var usuarioEntity = _mapper.Map<Usuario>(usuarioDto);
            await _repository.CreateNewUsuario(usuarioEntity);
            usuarioDto.IdUser = usuarioEntity.IdUser;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task DeleteUsuario(int id)
    {
        try
        {
            var usuarioEntity = await _repository.GetById(id);
            await _repository.DeleteUsuario(usuarioEntity.IdUser);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<IEnumerable<UsuarioDto>> GetAllUsers()
    {
        try
        {
            var usuarioEntity = await _repository.GetAll();
            return _mapper.Map<IEnumerable<UsuarioDto>>(usuarioEntity);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<UsuarioDto> GetUsuarioById(int id)
    {
        try
        {
            var usuarioEntity = await _repository.GetById(id);
            return _mapper.Map<UsuarioDto>(usuarioEntity);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task UpdateUsuario(UsuarioDto usuarioDto)
    {
        try
        {
            var usuarioEntity = _mapper.Map<Usuario>(usuarioDto);
            await _repository.UpdateUsuario(usuarioEntity);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
