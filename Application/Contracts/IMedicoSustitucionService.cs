using Application.DTOs.Request.Employee;
using Application.DTOs.Response.Employee;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IMedicoSustitucionService
    {
        Task<bool> AsignarSustitutoAsync(ObtenerSustituciones sustituciones);
        Task<List<ObtenerSustituciones>> GetAllReplacementsAsync(bool OnlyActive, int? IdDoctor = null);
        Task<List<MedicoSustitucion>> ObtenerSustitucionesAsync(string titularId);
        Task<bool> UpdateSustitucionAsync(UpdateSustitucionDto dto);
        Task<bool> DeleteSustitucionAsync(int id);
    }
}
