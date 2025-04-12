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

        public async Task<bool> CancelarVacaciones(int VacacionesId,string IdUser)
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
                vaca.IdUsuarioModificacion = IdUser;

                _context.Vacaciones.Update(vaca);
                await _context.SaveChangesAsync();
               
            }
            return true;
        }
        private async Task<bool> ExisteVacacionesAsync(int IdEmployee, DateTime HoraInicio, DateTime HoraFin)
        {
            return await _context.Vacaciones.AnyAsync(h =>
                h.EmployeeId == IdEmployee && h.Activo && (h.Estado == "Pendiente" || h.Estado == "Aprobado") &&
                ((HoraInicio >= h.FechaInicio && HoraInicio < h.FechaFinal) ||
                 (HoraFin > h.FechaInicio && HoraFin <= h.FechaFinal) ||
                 (HoraInicio <= h.FechaInicio && HoraFin >= h.FechaFinal)));
        }
        public async Task<bool> SolicitarVacaciones(InsertVacaciones insertVacaciones)
        {
            var a = _context.Employees.FirstOrDefault(c=>c.UserId == insertVacaciones.EmployeeUserId && c.Activo);
            if (a == null)
            {
                throw new Exception("El empleado no existe");
            }
            if (_context.DoctoresSustitutos.Where(c=>c.UserId == insertVacaciones.EmployeeUserId).Any())
            {
                throw new Exception("Los Doctores Sustitutos no pueden solicitar vacaciones");
            }
            if (insertVacaciones.FechaInicio <= DateTime.Now)
            {
                throw new Exception("No puede solicitar Vacaciones para hoy o a una fecha ya pasada");
            }
            if (insertVacaciones.FechaInicio >= insertVacaciones.FechaFinal)
            {
                throw new Exception("Las vacaciones no pueden finalizar antes de iniciar");
            }
            if (ExisteVacacionesAsync(a.Id,insertVacaciones.FechaInicio,insertVacaciones.FechaFinal).Result)
            {
                throw new Exception("Usted ya ha solicitado vacaciones para ese rango de fechas");
            }
            Vacaciones vacaciones = new Vacaciones
            {
                FechaInicio = insertVacaciones.FechaInicio,
                FechaFinal = insertVacaciones.FechaFinal,
                EmployeeId = a.Id,
                IdUsuarioCreacion = insertVacaciones.EmployeeUserId,
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

        public async Task<bool> AprobarSolicitudAsync(int solicitudId, string IdUser)
        {
            var solicitud = await _context.Vacaciones.FindAsync(solicitudId);
            if (solicitud == null || solicitud.Activo == false || solicitud.Estado != "Pendiente")
                throw new Exception("La Solicitud no existe o es diferente de 'Pendiente'. ");
            var aprobadoPor = _context.Administrativos.FirstOrDefault(c=>c.UserId == IdUser && c.Activo);

            solicitud.Estado = "Aprobado";
            solicitud.AprobadaPor = aprobadoPor.Name?? "N/A";
            solicitud.IdUsuarioModificacion = IdUser;
            solicitud.FechaModificacion = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DenegarSolicitudAsync(int solicitudId, string IdUser)
        {
            var solicitud = await _context.Vacaciones.FindAsync(solicitudId);
            if (solicitud == null || solicitud.Activo == false || solicitud.Estado != "Pendiente")
                throw new Exception ("La Solicitud no existe o es diferente de 'Pendiente'. ");
            var aprobadoPor = _context.Administrativos.FirstOrDefault(c => c.UserId == IdUser && c.Activo);

            solicitud.Estado = "Denegado";
            solicitud.AprobadaPor= aprobadoPor.Name ?? "N/A";
            solicitud.IdUsuarioModificacion = IdUser;
            solicitud.FechaModificacion = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<List<VacacionesDTO>> GetAllVacacionesAsync(string? NombreEmpleado = null, string? estado = null)
        {

            var query = _context.Vacaciones
                .Include(s => s.Employee).ThenInclude(c=>c.User).Where(c => c.Activo && c.Estado != "Cancelado")               
                .AsQueryable();
           

            if (!string.IsNullOrEmpty(NombreEmpleado))
            {
                query = query.Where(s => s.Employee.Name.Contains(NombreEmpleado) && s.Employee.Activo);
            }

            if (!string.IsNullOrEmpty(estado))
            {
                query = query.Where(s => s.Estado == estado);
            }
            return await (from a in query
                     join ur in _context.UserRoles on a.Employee.User.Id equals ur.UserId into userRoles
                     from ur in userRoles.DefaultIfEmpty()
                     join r in _context.Roles on ur.RoleId equals r.Id into roles
                     from r in roles.DefaultIfEmpty()
                     select new VacacionesDTO
                      {
                        Id = a.Id,
                        AprobadaPor = a.AprobadaPor,
                        EmployeeId = a.EmployeeId,
                        NombreEmployee = a.Employee.Name,
                        Role = r.Name,
                        EmailEmployee = a.Employee.User.Email,
                        FechaInicio = a.FechaInicio,
                        FechaFinal = a.FechaFinal,
                        FechaPlanificacion = a.FechaPlanificacion,
                        Estado = a.Estado


                        } ).OrderByDescending(c=>c.Id).ToListAsync();
        }
        public async Task<List<VacacionesDTO>> GetMisVacacionesAsync(string UserId , string? estado = null)
        {

            var query = _context.Vacaciones
                .Include(s => s.Employee).Where(c => c.Activo && c.Employee.UserId == UserId)               
                .AsQueryable();

            if (!string.IsNullOrEmpty(estado))
            {
                query = query.Where(s => s.Estado == estado);
            }

            return await query.Select(c => new VacacionesDTO
            {
                Id = c.Id,
                AprobadaPor = c.AprobadaPor,
                FechaInicio = c.FechaInicio,
                FechaFinal = c.FechaFinal,
                FechaPlanificacion = c.FechaPlanificacion,
                Estado = c.Estado


            } ).OrderByDescending(c=>c.Id).ToListAsync();
        }
    }
}
