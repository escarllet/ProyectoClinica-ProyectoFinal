﻿using Application.Contracts;
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
                    IdUsuarioCreacion = _authService.ObtenerUserIdActual() ?? "N/A",
                    FechaCreacion = DateTime.Now,
                    Activo = true,
                    Version = 1

                };

                await _context.Sustituciones.AddAsync(sustitucion);
                await _context.SaveChangesAsync();

                return true;
            
        }

        public async Task<List<ObtenerSustituciones>> GetAllReplacementsAsync(bool OnlyActive, int? IdDoctor = null)
        {
            var query =  _context.Sustituciones.Where(c => c.Activo).AsQueryable();
            if (OnlyActive)
            {
                query = query.Where(c=> c.FechaDeBaja >= DateTime.Now);
            }
            if (IdDoctor == null)
            {
                return await query.Select(c => new ObtenerSustituciones
                {
                    DoctorId = c.DoctorInterinoId ?? c.DoctorTitularId,
                    FechaInicio = c.FechadDeAlta,
                    DoctorSustitutoId = c.IdDoctorSustituto,
                    FechaFin = c.FechaDeBaja
                }).ToListAsync();
                 
            }


            return await query.Where(u => u.IdDoctorSustituto == IdDoctor
                           || u.DoctorInterinoId == IdDoctor || u.DoctorTitularId == IdDoctor)
                .Select(c => new ObtenerSustituciones {
                    DoctorId = c.DoctorInterinoId??c.DoctorTitularId,
                    FechaInicio = c.FechadDeAlta,
                    DoctorSustitutoId = c.IdDoctorSustituto,
                    FechaFin = c.FechaDeBaja
                }).ToListAsync();

        }
     
        public Task<List<MedicoSustitucion>> ObtenerSustitucionesAsync(string titularId)
        {
            throw new NotImplementedException();
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
            sustitucion.IdUsuarioModificacion = _authService.ObtenerUserIdActual() ?? "N/A";
            sustitucion.FechaModificacion = DateTime.Now;
            sustitucion.Version++;
  
            _context.Sustituciones.Update(sustitucion);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> DeleteSustitucionAsync(int id)
        {
            var sustitucion = await _context.Sustituciones.FindAsync(id);
            if (sustitucion == null) return false;

            sustitucion.Activo = false;
            sustitucion.FechaModificacion = DateTime.Now;

            _context.Sustituciones.Update(sustitucion);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
