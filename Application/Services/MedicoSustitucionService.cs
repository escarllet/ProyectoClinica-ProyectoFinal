﻿using Application.Contracts;
using Application.DTOs.Request.Employee;
using Application.DTOs.Response.Employee;
using Application.DTOs.Response.Sustituciones;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class MedicoSustitucionService
    {
        private readonly IMedicoSustitucionService _sustitucionService;
        public MedicoSustitucionService(IMedicoSustitucionService sustitucionService)
        {
            _sustitucionService = sustitucionService;
        }

        public async Task<bool> AsignarSustitutoAsync(ObtenerSustituciones sustituciones)
        {
            return await _sustitucionService.AsignarSustitutoAsync(sustituciones);
        } 

        public async Task<List<GetSustituciones>> GetAllReplacementsAsync(bool OnlyActive , int? idReplacement = null)
        {
            return await _sustitucionService.GetAllReplacementsAsync(OnlyActive, idReplacement);
        }
        public async Task<bool> UpdateSustitucionAsync(UpdateSustitucionDto dto)
        {
            return await _sustitucionService.UpdateSustitucionAsync(dto);
        }
        public async Task<bool> DeleteSustitucionAsync(int id,string UserId)
        {
            return await _sustitucionService.DeleteSustitucionAsync(id,UserId);
        }
    }
}
