using Application.Contracts;
using Application.DTOs.Response.Provincia;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Infraestructure.Repository
{
    public class ProvinciaRepository : IProvinciaRepository
    {
        private readonly ClinicContext _context;
        public ProvinciaRepository(ClinicContext context)
        {
            _context = context;

        }
        public async Task<List<ProvinciaDTO>> GetAllProvincias(string? provincia = null)
        {
            var query = _context.Provincias.Where(c => c.Activo).AsQueryable();

            if (!string.IsNullOrEmpty(provincia))
            {
                query = query.Where(u => u.Nombre.Contains(provincia));
            }
            var dTOs =  query.Select(c => new ProvinciaDTO 
                        { 
                         Id = c.Id,
                         Poblacion = c.Poblacion,
                         Nombre = c.Nombre
                     }).OrderBy(c=>c.Nombre).ToListAsync();

       
            return dTOs.Result;

            
         
        }
    }
}
