using Application.Contracts;
using Domain.Entities;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class ProvinciaRepository : IProvinciaRepository
    {
        private readonly ClinicContext _context;
        public ProvinciaRepository(ClinicContext context)
        {
            _context = context;

        }
        public List<Provincia> GetAllProvincias()
        {
            return _context.Provincias.Where(c => c.Activo).ToList();
        }
    }
}
