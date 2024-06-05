using Authetication.Server.DTOs;
using Authetication.Server.Models;
using Authetication.Server.Repository;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authetication.Server.Services;

public class AdminService : IAdminService
{
    private readonly IMapper _mapper;
    private readonly IAdminRepository _repository;
    private readonly ILogger<AdminService> _logger;

    public AdminService(IMapper mapper, IAdminRepository repository, ILogger<AdminService> logger)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task CreateAdmin(AdminDto adminDto)
    {
        try
        {
            var adminEntity = _mapper.Map<Admin>(adminDto);
            await _repository.CreateNewAdmin(adminEntity);
            adminDto.IdAdmin = adminEntity.IdAdmin;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar administrador.");
            throw new Exception(ex.Message, ex);
        }
    }

    public async Task DeleteAdmin(int id)
    {
        try
        {
            var adminEntity = await _repository.GetById(id);
            if (adminEntity == null)
            {
                throw new Exception("Administrador não encontrado.");
            }
            await _repository.DeleteAdmin(adminEntity.IdAdmin);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao deletar administrador.");
            throw new Exception(ex.Message, ex);
        }
    }

    public async Task<AdminDto> GetAdminById(int id)
    {
        try
        {
            var adminEntity = await _repository.GetById(id);
            if (adminEntity == null)
            {
                throw new Exception("Administrador não encontrado.");
            }
            return _mapper.Map<AdminDto>(adminEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao recuperar administrador por ID.");
            throw new Exception(ex.Message, ex);
        }
    }

    public async Task<IEnumerable<AdminDto>> GetAllAdmins()
    {
        try
        {
            var adminEntities = await _repository.GetAll();
            if (adminEntities == null || !adminEntities.Any())
            {
                throw new Exception("Nenhum administrador encontrado.");
            }
            return _mapper.Map<IEnumerable<AdminDto>>(adminEntities);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao recuperar administradores.");
            throw new Exception($"Erro ao recuperar administradores: {ex.Message}", ex);
        }
    }

    public async Task UpdateAdmin(AdminDto adminDto)
    {
        try
        {
            var adminEntity = _mapper.Map<Admin>(adminDto);
            await _repository.UpdateAdmin(adminEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar administrador.");
            throw new Exception(ex.Message, ex);
        }
    }
}
