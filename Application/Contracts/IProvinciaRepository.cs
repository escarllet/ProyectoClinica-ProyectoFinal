using Application.DTOs.Response.Provincia;
using Domain.Entities;


namespace Application.Contracts
{
    public interface IProvinciaRepository
    {
       Task<List<ProvinciaDTO>> GetAllProvincias(string? provincia = null);

    }
}
