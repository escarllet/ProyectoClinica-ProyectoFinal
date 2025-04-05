using Application.Contracts;
using Application.DTOs.Request.Vacaciones;
using Application.DTOs.Response.Vacaciones;
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
   //falta solititud vacaciones y cancelar vacaciones

        public async Task<bool> CancelarVacaciones(int VacacionesId)
        {
            var vaca = _context.Vacaciones.FirstOrDefault(c => c.Id == VacacionesId && c.Activo);
            if (vaca == null)
            {
                throw new Exception("La solicitud de Vacaciones que intenta cancelar no existe");

            }else if (vaca.Estado != "Pendiente") 
            {
                throw new Exception("La solicitud de Vacaciones que intenta cancelar tiene un estado diferente a 'Pendiente'");
            }
            else{
                vaca.Estado = "Cancelado";
                vaca.FechaModificacion = DateTime.Now;
                vaca.Version++;
                vaca.IdUsuarioModificacion = "N/A"; //falta esto

                _context.Vacaciones.Update(vaca);
                await _context.SaveChangesAsync();
               
            }
            return true;
        }
        public async Task<bool> SolicitarVacaciones(InsertVacaciones insertVacaciones)
        {
            var a = _context.Employees.FirstOrDefault(c=>c.Id == insertVacaciones.EmployeeId && c.Activo);
            if (a == null)
            {
                throw new Exception("El empleado no existe");
            }
            if (_context.DoctoresSustitutos.Where(c=>c.Id == insertVacaciones.EmployeeId).Any())
            {
                throw new Exception("Los Doctores Sustitutos no pueden solicitar vacaciones");
            }
            Vacaciones vacaciones = new Vacaciones
            {
                FechaInicio = insertVacaciones.FechaInicio,
                FechaFinal = insertVacaciones.FechaFinal,
                IdUsuarioCreacion = "N/A",//falta esto
                FechaPlanificacion = DateTime.Now,
                Version = 1,
                Estado = "Pendiente",
                FechaCreacion = DateTime.Now,
                Activo = true

            };

            _context.Vacaciones.Add(vacaciones);
            await _context.SaveChangesAsync();
            return true;
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
        public async Task<List<VacacionesDTO>> GetAllVacacionesAsync(string? NombreEmpleado = null, int? EmployeId = null, string? estado = null)
        {

            var query = _context.Vacaciones
                .Include(s => s.Employee).Where(c => c.Activo)               
                .AsQueryable();

            if (!string.IsNullOrEmpty(NombreEmpleado))
            {
                query = query.Where(s => s.Employee.Name.Contains(NombreEmpleado) || s.Employee.Id == EmployeId && s.Employee.Activo);
            }

            if (!string.IsNullOrEmpty(estado))
            {
                query = query.Where(s => s.Estado == estado);
            }

            return await query.Select(c => new VacacionesDTO
            {
                Id = c.Id,
                AprobadaPor = c.AprobadaPor,
                EmployeeId = c.EmployeeId,
                FechaInicio = c.FechaInicio,
                FechaFinal = c.FechaFinal,
                FechaPlanificacion = c.FechaPlanificacion,
                Estado = c.Estado


            } ).ToListAsync();
        }
    }
}
