using Application.Contracts;
using Application.Services;
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
    public class VacacionesRepository : IVacacionesRepository
    {

        private readonly ClinicContext _context;
        private readonly IAuthService _authService;

        public VacacionesRepository(ClinicContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }
        public async Task<List<Vacaciones>> GetAllVacacionesAsync()
        {
            return await _context.Vacaciones
                .Include(s => s.Employee)
                .ToListAsync();
        }
        public async Task<bool> AprobarSolicitudAsync(int solicitudId)
        {
            var solicitud = await _context.Vacaciones.FindAsync(solicitudId);
            if (solicitud == null || solicitud.Activo == false || solicitud.Estado != "Pendiente")
                throw new Exception("La Solicitud no existe o es diferente de 'Pendiente'. ");

            solicitud.Estado = "Aprobado";
            solicitud.AprobadaPor = _authService.ObtenerUserIdActual() ?? "N/A";
            solicitud.IdUsuarioModificacion = _authService.ObtenerUserIdActual()??"N/A";
            solicitud.FechaModificacion = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DenegarSolicitudAsync(int solicitudId)
        {
            var solicitud = await _context.Vacaciones.FindAsync(solicitudId);
            if (solicitud == null || solicitud.Activo == false || solicitud.Estado != "Pendiente")
                throw new Exception ("La Solicitud no existe o es diferente de 'Pendiente'. ");

            solicitud.Estado = "Denegado";
            solicitud.IdUsuarioModificacion = _authService.ObtenerUserIdActual() ?? "N/A";
            solicitud.FechaModificacion = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<List<Vacaciones>> ObtenerHistorialVacacionesAsync(string? NombreEmpleado, string? estado)
        {
            var query = _context.Vacaciones
                .Include(s => s.Employee).Where(c => c.Activo)
                .AsQueryable();

            if (!string.IsNullOrEmpty(NombreEmpleado))
            {
                query = query.Where(s => s.Employee.Name.Contains(NombreEmpleado) && s.Employee.Activo);
            }

            if (!string.IsNullOrEmpty(estado))
            {
                query = query.Where(s => s.Estado == estado);
            }

            return query.ToList();
        }
    }
}
