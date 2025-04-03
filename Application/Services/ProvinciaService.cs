using Application.Contracts;
using Application.DTOs.Response.Provincia;
using Domain.Entities;


namespace Application.Services
{
    public class ProvinciaService
    {
        private readonly IProvinciaRepository _provincia;
        public ProvinciaService(IProvinciaRepository provincia)
        {
            _provincia = provincia;
        }

        public async Task<List<ProvinciaDTO>> GetAllProvincias(string? provincia = null)

        {
            return await _provincia.GetAllProvincias(provincia);
        }
    }
}
