using Application.Contracts;
using Application.DTOs.Request.Employee;
using Application.DTOs.Response.Employee;
using Application.DTOs.Response.Sustituciones;
using Domain.Entities;
using Domain.Entities.People;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
            if (_context.Sustituciones.Where(c => c.IdDoctorSustituto == sustituciones.DoctorSustitutoId && c.FechaDeBaja > sustituciones.FechaInicio && c.Activo).Any())
            {
                throw new Exception("No se puede Asignar el Doctor sustituto por que estara ocupado con otra sustitucion");
            }
            if (sustituciones.FechaInicio < DateTime.Now) throw new Exception("No es posible Agregar una sustitucion que inicie antes de la fecha actual");
            if (sustituciones.FechaInicio > sustituciones.FechaFin) throw new Exception("No es posible Agregar una sustitucion que inicie despues de la fecha fin");
            var sustituto = _context.DoctoresSustitutos.FirstOrDefault(c => c.Id == sustituciones.DoctorSustitutoId && c.Activo);
            if (sustituto == null) throw new Exception("Doctor Sustituto no existe o no es un empleado con ese rol ");
            var interino = _context.DoctoresInterinos.FirstOrDefault(c => c.Id == sustituciones.DoctorId && c.Activo);
            var titular = _context.DoctoresTitulares.FirstOrDefault(c => c.Id == sustituciones.DoctorId && c.Activo);
            if (titular == null && interino == null) throw new Exception("Doctor no existe o no es un empleado con rol de Titular o Interino");
                            
                var sustitucion = new Sustituciones
                {
                    DoctorTitularId = titular==null?null:titular.Id,
                    DoctorInterinoId = interino == null ? null : interino.Id,
                    IdDoctorSustituto = sustituto == null ? null : sustituto.Id,
                    FechadDeAlta = sustituciones.FechaInicio,
                    FechaDeBaja = sustituciones.FechaFin,
                    IdUsuarioCreacion = sustituciones.ModifyUserId,
                    FechaCreacion = DateTime.Now,
                    Activo = true,
                    Version = 1

                };

                await _context.Sustituciones.AddAsync(sustitucion);
                await _context.SaveChangesAsync();

                return true;
            
        }

        public async Task<List<GetSustituciones>> GetAllReplacementsAsync(bool OnlyActive, int? idReplacement = null)
        {
            var query =  _context.Sustituciones.Include(c=>c.DoctorInterino).Include(c=>c.DoctorTitular).Where(c => c.Activo).AsQueryable();
            if (query == null || query.Count()==0)
            {
                throw new Exception("No hay Sustituciones por mostrar");
            }
            if (idReplacement != null)
            {

                query = query.Where(u => u.Id == idReplacement);
            }
            if (OnlyActive)
            {
                query = query.Where(c=> c.FechadDeAlta <= DateTime.Now &&c.FechaDeBaja >= DateTime.Now);
            }
   
            var sustituciones = (from ab in query                      
                                 join sus in _context.DoctoresSustitutos on ab.IdDoctorSustituto equals sus.Id
                     where ab.Activo && sus.Activo
                     select new GetSustituciones
                     {
                         Id = ab.Id,
                         FechaInicio = ab.FechadDeAlta,
                         FechaFin = ab.FechaDeBaja,
                         EstaActiva = ab.FechaDeBaja >= DateTime.Now && ab.FechadDeAlta <= DateTime.Now ? "Si":"No",
                         DoctorName = ab.DoctorInterino == null? ab.DoctorTitular.Name: ab.DoctorInterino.Name,
                         DoctorType = ab.DoctorInterino == null ? "Titular" : "Interino",
                         DoctorSustitutoName = sus.Name

                     }).OrderByDescending(c=>c.Id).ToList();
            
            return sustituciones;

        }
     
        public async Task<bool> UpdateSustitucionAsync(UpdateSustitucionDto sustituciones)
        {
            var sustitucion = _context.Sustituciones.FirstOrDefault(c => c.Id == sustituciones.Id && c.Activo ==false);
            if (sustitucion == null) throw new Exception("La sustitucion id: "+sustituciones.Id+" no existe");
            if (_context.Sustituciones.Where(c => c.IdDoctorSustituto == sustituciones.DoctorSustitutoId && c.FechaDeBaja > sustituciones.FechaInicio && c.Activo).Any())
            {
                throw new Exception("No se puede Asignar el Doctor sustituto por que estara ocupado con otra sustitucion");
            }
            if (sustituciones.FechaInicio < DateTime.Now) throw new Exception("No es posible Agregar una sustitucion que inicie antes de la fecha actual");
            if (sustituciones.FechaInicio > sustituciones.FechaFin) throw new Exception("No es posible Agregar una sustitucion que inicie despues de la fecha fin");
            var sustituto = _context.DoctoresSustitutos.FirstOrDefault(c => c.Id == sustituciones.DoctorSustitutoId&&c.Activo);
            if (sustituto == null) throw new Exception("Doctor Sustituto no existe o no es un empleado con ese rol ");
            var interino = _context.DoctoresInterinos.FirstOrDefault(c => c.Id == sustituciones.DoctorId && c.Activo);
            var titular = _context.DoctoresTitulares.FirstOrDefault(c => c.Id == sustituciones.DoctorId && c.Activo);
            if (titular == null && interino == null) throw new Exception("Doctor no existe o no es un empleado con rol de Titular o Interino");

            
            sustitucion.DoctorTitularId = titular == null ? null : titular.Id;
            sustitucion.DoctorInterinoId = interino == null ? null : interino.Id;
            sustitucion.IdDoctorSustituto = sustituto == null ? null : sustituto.Id;
            sustitucion.FechadDeAlta = sustituciones.FechaInicio;
            sustitucion.FechaDeBaja = sustituciones.FechaFin;
            sustitucion.IdUsuarioModificacion = sustituciones.ModifyUserId;
            sustitucion.FechaModificacion = DateTime.Now;
            sustitucion.Version++;
  
            _context.Sustituciones.Update(sustitucion);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> DeleteSustitucionAsync(int id, string UserId)
        {
            var sustitucion = await _context.Sustituciones.FindAsync(id);
            if (sustitucion == null) return false;

            sustitucion.Activo = false;
            sustitucion.FechaModificacion = DateTime.Now;
            sustitucion.IdUsuarioModificacion = UserId;

            _context.Sustituciones.Update(sustitucion);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
