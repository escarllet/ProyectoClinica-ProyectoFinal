using Application.Contracts;
using Application.DTOs.Request.Employee;
using Application.DTOs.Response.Employee;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class SustitucionesRepository : IMedicoSustitucionService
    {
        private readonly ClinicContext _context;
        public SustitucionesRepository(ClinicContext context)
        {
            _context = context;

        }
        public async Task<bool> AsignarSustitutoAsync(ObtenerSustituciones sustituciones)
        {
            
            var sustitucion = new Sustituciones
            {
                IdDoctorTitular = sustituciones.MedicoTitularId,
                IdDoctorSustituto = sustituciones.MedicoSustitutoId,
                FechadDeAlta = sustituciones.FechaInicio,
                FechaDeBaja = sustituciones.FechaFin,
                FechaCreacion = DateTime.Now,
                Activo = true,
                Version = 1         
                
            };

            await _context.Sustituciones.AddAsync(sustitucion);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Sustituciones>> GetAllReplacementsAsync()
        {
            return await _context.Sustituciones
              .Include(s => s.DoctorSustituto) // Incluye el médico titular
              .Include(s => s.DoctorTitular) // Incluye el médico sustituto
              .ToListAsync();
        }

        public Task<List<MedicoSustitucion>> ObtenerSustitucionesAsync(string titularId)
        {
            throw new NotImplementedException();
        }
    }
}
