using Application.DTOs.Request.Employee;
using Application.DTOs.Response.Employee;
using Application.DTOs.Response.Sustituciones;
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
        Task<bool> UpdateSustitucionAsync(UpdateSustitucionDto dto);
        Task<bool> DeleteSustitucionAsync(int id,string UserId);
        Task<List<GetSustituciones>> GetAllReplacementsAsync(bool OnlyActive);
    }
}
