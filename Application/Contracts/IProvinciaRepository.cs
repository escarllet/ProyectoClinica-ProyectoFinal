using Domain.Entities;


namespace Application.Contracts
{
    public interface IProvinciaRepository
    {
        List<Provincia> GetAllProvincias();
    }
}
