using Authetication.Server.DTOs;
using Authetication.Server.Models;
using Authetication.Server.Repository;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Authetication.Server.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _repository;
        private readonly ILogger<UsuarioService> _logger; // Campo para ILogge

        public UsuarioService(IMapper mapper, IUsuarioRepository repository, ILogger<UsuarioService> logger)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
        }

        public async Task CreateUsuario(UsuarioDto usuarioDto)
        {
            try
            {
                var usuarioEntity = _mapper.Map<Usuario>(usuarioDto);
               // usuarioEntity.Password = BCrypt.Net.BCrypt.HashPassword(usuarioDto.Password);
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
                var usuarioEntities = await _repository.GetAll();
                return _mapper.Map<IEnumerable<UsuarioDto>>(usuarioEntities);
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

        public async Task<UsuarioDto> GetByUsernameAndPassword(string username, string password)
        {
            try
            {
                _logger.LogInformation($"Authenticating user: {username}");

                var usuarioEntity = await _repository.GetByUsername(username);

                if (usuarioEntity != null)
                {
                    _logger.LogInformation($"User found: {usuarioEntity.Username}");

                    bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, usuarioEntity.Password);

                    _logger.LogInformation($"Password valid: {isPasswordValid}");

                    if (isPasswordValid)
                    {
                        return _mapper.Map<UsuarioDto>(usuarioEntity);
                    }
                }
                else
                {
                    _logger.LogWarning($"User not found: {username}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while verifying the password for user: {username}");
                throw;
            }

            return null;
        }

    }
}
