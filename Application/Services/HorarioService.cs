﻿using Application.Contracts;
using Application.DTOs.Request.Horario;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class HorarioService
    {
        private readonly IHorario _horario;
        public HorarioService(IHorario horario)
        {
            _horario = horario;
        }
        public async Task<List<Horario>> ObtenerHorariosPorUsuarioAsync(string UserId)
        {
            return await _horario.ObtenerHorariosPorUsuarioAsync(UserId);
        } 
        public async Task AgregarHorarioAsync(HorarioDTO horario)
        {
             await _horario.AgregarHorarioAsync(horario);
        }
        public async Task EditarHorarioAsync(UpdateHorarioDTO horario)
        {
             await _horario.EditarHorarioAsync(horario);
        }        
        public async Task EliminarHorarioAsync(int horario, string IdUserDoctor)
        {
             await _horario.EliminarHorarioAsync(horario,IdUserDoctor);
        }
    }
}
