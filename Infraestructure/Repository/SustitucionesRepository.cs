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
        private readonly IAuthService _authService;
        public SustitucionesRepository(ClinicContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }
        public async Task<bool> AsignarSustitutoAsync(ObtenerSustituciones sustituciones)
        {
            
            var sustitucion = new Sustituciones
            {
                IdDoctorTitular = sustituciones.MedicoTitularId,
                IdDoctorSustituto = sustituciones.MedicoSustitutoId,
                FechadDeAlta = sustituciones.FechaInicio,
                FechaDeBaja = sustituciones.FechaFin,
                IdUsuarioCreacion = _authService.ObtenerUserIdActual()??"N/A",
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
        // las activas son las que las fecha de baja son mayores o iguales a la fecha actual
        public async Task<List<Sustituciones>> GetActiveReplacementsAsync()
        {
            return await _context.Sustituciones
              .Include(s => s.DoctorSustituto) // Incluye el médico titular
              .Include(s => s.DoctorTitular)// Incluye el médico sustituto
              .Where(c => c.FechaDeBaja >= DateTime.Now ) 
              .ToListAsync();
        }
        public Task<List<MedicoSustitucion>> ObtenerSustitucionesAsync(string titularId)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> UpdateSustitucionAsync(UpdateSustitucionDto dto)
        {
            var sustitucion = await _context.Sustituciones.FirstOrDefaultAsync(c => c.Id == dto.Id);
            if (sustitucion == null) return false;

            // Actualizar valores
            sustitucion.FechadDeAlta = dto.FechaInicio;
            sustitucion.FechaDeBaja = dto.FechaFin.Value;
            sustitucion.IdDoctorSustituto = dto.IdEmpleadoSustituto;
            sustitucion.FechaModificacion = DateTime.Now;

            _context.Sustituciones.Update(sustitucion);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> DeleteSustitucionAsync(int id)
        {
            var sustitucion = await _context.Sustituciones.FindAsync(id);
            if (sustitucion == null) return false;

            sustitucion.Activo = false;
            sustitucion.FechaModificacion = DateTime.UtcNow;

            _context.Sustituciones.Update(sustitucion);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
