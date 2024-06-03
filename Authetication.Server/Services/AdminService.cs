using Authetication.Server.DTOs;
using Authetication.Server.Models;
using Authetication.Server.Repository;
using AutoMapper;

namespace Authetication.Server.Services;

public class AdminService : IAdminService
{
    private readonly IMapper _mapper;
    private readonly IAdminRepository _repository;

    public AdminService(IMapper mapper, IAdminRepository repository)
    {
        _mapper = mapper;
        repository = repository;
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
            throw new Exception(ex.Message);
        }
    }

    public async Task DeleteAdmin(int id)
    {
        try
        {
            var adminEntity = await _repository.GetById(id);
            await _repository.DeleteAdmin(adminEntity.IdAdmin);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<AdminDto> GetAdminById(int id)
    {
        try
        {
            var adminEntity = await _repository.GetById(id);
            return _mapper.Map<AdminDto>(adminEntity);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<IEnumerable<AdminDto>> GetAllAdmins()
    {
        try
        {
            var adminEntity = await _repository.GetAll();
            return _mapper.Map<IEnumerable<AdminDto>>(adminEntity);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
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
            throw new Exception(ex.Message);
        }
    }
}
