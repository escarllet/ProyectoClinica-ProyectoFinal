using Application.Contracts;
using Domain.Entities;


namespace Application.Services
{
    public class ProvinciaService
    {
        private readonly IProvinciaRepository _provincia;
        public List<Provincia> GetAllProvincias()
        {
            return _provincia.GetAllProvincias();
        }
    }
}
