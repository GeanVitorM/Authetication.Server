﻿using Authetication.Server.DTOs;

namespace Authetication.Server.Services;

public interface IUsuarioService
{
    Task<IEnumerable<UsuarioDto>> GetAllUsers();
    Task<UsuarioDto> GetUsuarioById(int id);
    Task CreateUsuario(UsuarioDto usuarioDto);
    Task UpdateUsuario(UsuarioDto usuarioDto);
    Task DeleteUsuario(int id);
}
