using Domain.Entities.People;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Application.Contracts;
using Application.DTOs.Request.Employee;
using Domain.Entities.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using Domain.Entities;
using System.Net;
using System;
using Application.DTOs.Request.User;

namespace Infrastructure.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ClinicContext _context;
        private readonly IAuthService _authService;
        public EmployeeRepository(ClinicContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        public async Task<List<Employee>> GetAllEmployeeAsync()
        {
            return await _context.Employees.Where(c => c.Activo).ToListAsync();
        }
        public async Task<List<Doctor>> GetAllDoctoresAsync()
        {
            return await _context.Doctores.Where(c => c.Activo).ToListAsync();
        }
        public async Task<List<DoctorSustituto>> GetAllDoctoresSustitutosAsync()
        {
            return await _context.DoctoresSustitutos.Where(c => c.Activo).ToListAsync();
        }

        public async Task<string> RegisterEmployeeAsync(RegisterEmployeeDto dto)
        {
            var employee = await _authService.RegisterUserEmployeAsync(dto);
            switch (dto.TipoEmpleado)
            {
                case "AuxEnfermeria":
                    var enf = new AuxEnfermeria
                    {
                        Name = dto.NombreCompleto,
                        UserId = employee.Id,
                        Address = dto.Direccion,
                        Phone = dto.Telefono,
                        PostalCode = dto.CodigoPostal,
                        NIF = dto.NIF,
                        IdProvincia = dto.IdProvincia,
                        fechaEntrada = dto.FechaEntradaEmpleado,
                        IdUsuarioCreacion = employee.IdUsuarioCreacion,
                        SocialSecurityNumber = dto.NumeroSeguridadSocial,
                        CodigoEmpleado = dto.CodigoEmpleado,
                        Activo = true,
                        FechaCreacion = DateTime.Now,
                    };
                    _context.AuxiliaresEnfermeria.Add(enf);
                    break;
                case "ATSZona":
                    var asistenteZona = new AsistenteZona
                    {
                        Name = dto.NombreCompleto,
                        UserId = employee.Id,
                        Address = dto.Direccion,
                        Phone = dto.Telefono,
                        PostalCode = dto.CodigoPostal,
                        NIF = dto.NIF,
                        DescripcionZona = dto.DescripcionZona ?? "N/A",
                        IdProvincia = dto.IdProvincia,
                        fechaEntrada = dto.FechaEntradaEmpleado,
                        IdUsuarioCreacion = employee.IdUsuarioCreacion,
                        SocialSecurityNumber = dto.NumeroSeguridadSocial,
                        CodigoEmpleado = dto.CodigoEmpleado,
                        Activo = true,
                        FechaCreacion = DateTime.Now,
                    };
                    _context.AsistentesZona.Add(asistenteZona);
                   
                    break;
                case "ATS":
                    var asistente = new Asistente
                    {
                        Name = dto.NombreCompleto,
                        UserId = employee.Id,
                        Address = dto.Direccion,
                        Phone = dto.Telefono,
                        PostalCode = dto.CodigoPostal,
                        NIF = dto.NIF,
                        IdProvincia = dto.IdProvincia,
                        fechaEntrada = dto.FechaEntradaEmpleado,
                        IdUsuarioCreacion = employee.IdUsuarioCreacion,
                        SocialSecurityNumber = dto.NumeroSeguridadSocial,
                        CodigoEmpleado = dto.CodigoEmpleado,
                        Activo = true,
                        FechaCreacion = DateTime.Now,
                    };
                    _context.Asistentes.Add(asistente);
                    break;
                case "Celadores":
                    var celador = new Celador
                    {
                        Name = dto.NombreCompleto,
                        UserId = employee.Id,
                        Address = dto.Direccion,
                        Phone = dto.Telefono,
                        PostalCode = dto.CodigoPostal,
                        NIF = dto.NIF,
                        IdProvincia = dto.IdProvincia,
                        fechaEntrada = dto.FechaEntradaEmpleado,
                        IdUsuarioCreacion = employee.IdUsuarioCreacion,
                        SocialSecurityNumber = dto.NumeroSeguridadSocial,
                        CodigoEmpleado = dto.CodigoEmpleado,
                        Activo = true,
                        FechaCreacion = DateTime.Now,
                    };
                    _context.Celadores.Add(celador);
                    break;
                case "Admin":
                    var admi = new Administrativo
                    {
                        Name = dto.NombreCompleto,
                        Address = dto.Direccion,
                        UserId = employee.Id,
                        Phone = dto.Telefono,
                        PostalCode = dto.CodigoPostal,
                        NIF = dto.NIF,
                        AreaOficina = dto.AreaOficina ?? "N/A",
                        IdProvincia = dto.IdProvincia,
                        fechaEntrada = dto.FechaEntradaEmpleado,
                        IdUsuarioCreacion = employee.IdUsuarioCreacion,
                        SocialSecurityNumber = dto.NumeroSeguridadSocial,
                        CodigoEmpleado = dto.CodigoEmpleado,
                        Activo = true,
                        FechaCreacion = DateTime.Now,
                    };
                    _context.Administrativos.Add(admi);
                    break;
                case "DoctorSustituto":
                    var sus = new DoctorSustituto
                    {
                        Name = dto.NombreCompleto,
                        Address = dto.Direccion,
                        Phone = dto.Telefono,
                        UserId = employee.Id,
                        PostalCode = dto.CodigoPostal,
                        NIF = dto.NIF,
                        NumeroColegiado = dto.NumeroColegiado ?? "N/A",
                        IdProvincia = dto.IdProvincia,
                        fechaEntrada = dto.FechaEntradaEmpleado,
                        IdUsuarioCreacion = employee.IdUsuarioCreacion,
                        SocialSecurityNumber = dto.NumeroSeguridadSocial,
                        CodigoEmpleado = dto.CodigoEmpleado,
                        Activo = true,
                        FechaCreacion = DateTime.Now,
                    };
                    _context.DoctoresSustitutos.Add(sus);
                    break;
                case "DoctorInterino":
                    var inter = new DoctorInterino
                    {
                        Name = dto.NombreCompleto,
                        Address = dto.Direccion,
                        Phone = dto.Telefono,
                        UserId = employee.Id,
                        PostalCode = dto.CodigoPostal,
                        NIF = dto.NIF,
                        NumeroColegiado = dto.NumeroColegiado ?? "N/A",
                        IdProvincia = dto.IdProvincia,
                        fechaEntrada = dto.FechaEntradaEmpleado,
                        IdUsuarioCreacion = employee.IdUsuarioCreacion,
                        SocialSecurityNumber = dto.NumeroSeguridadSocial,
                        CodigoEmpleado = dto.CodigoEmpleado,
                        Activo = true,
                        FechaCreacion = DateTime.Now,
                    };
                    _context.DoctoresInterinos.Add(inter);
                    break;
                case "DoctorTitular":
                    var titu = new DoctorTitular
                    {
                        Name = dto.NombreCompleto,
                        Address = dto.Direccion,
                        Phone = dto.Telefono,
                        UserId = employee.Id,
                        PostalCode = dto.CodigoPostal,
                        NIF = dto.NIF,
                        NumeroColegiado = dto.NumeroColegiado ?? "N/A",
                        IdProvincia = dto.IdProvincia,
                        fechaEntrada = dto.FechaEntradaEmpleado,
                        IdUsuarioCreacion = employee.IdUsuarioCreacion,
                        SocialSecurityNumber = dto.NumeroSeguridadSocial,
                        CodigoEmpleado = dto.CodigoEmpleado,
                        Activo = true,
                        FechaCreacion = DateTime.Now,
                    };
                    _context.DoctoresTitulares.Add(titu);
                    break;
                default:
                    throw new Exception("Tipo de empleado no existe");

            };

          

            await _context.SaveChangesAsync();
            return "Usuario y empleado creados correctamente.";
        }
        public async Task<bool> UpdateEmpleadoAsync(UpdateEmployeeDto dto)
        {
            var empleado = await _context.Employees.FindAsync(dto.Id);
            if (empleado == null ||empleado.Activo == false) return false;

            empleado.Name = dto.NombreCompleto;

            empleado.Address = dto.Direccion;
            empleado.Phone = dto.Telefono;
            empleado.PostalCode = dto.CodigoPostal;
            empleado.NIF = dto.NIF;
            empleado.IdProvincia = dto.IdProvincia;
            empleado.fechaEntrada = dto.FechaEntradaEmpleado;
            empleado.SocialSecurityNumber = dto.NumeroSeguridadSocial;
            empleado.CodigoEmpleado = dto.CodigoEmpleado;
            empleado.Version++;
            empleado.FechaModificacion = DateTime.Now;

            

            _context.Employees.Update(empleado);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> DeleteEmpleadoAsync(int id,DateTime fechaSalida)
        {
            var empleado = await _context.Employees.FindAsync(id);
            if (empleado == null) return false;

            empleado.Activo = false;
            empleado.fechaSalida = fechaSalida;
            empleado.Version++;
            empleado.FechaModificacion = DateTime.Now;



            _context.Employees.Update(empleado);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<UsuarioPerfilDto?> ObtenerMiPerfilAsync(string userId)
        {
            var usuario = await (
                from u in _context.Users
                join ur in _context.UserRoles on u.Id equals ur.UserId into userRoles
                from ur in userRoles.DefaultIfEmpty()
                join r in _context.Roles on ur.RoleId equals r.Id into roles
                from r in roles.DefaultIfEmpty()
                join e in _context.Employees on u.Id equals e.UserId into employees
                from e in employees.DefaultIfEmpty()
                join d in _context.Doctores on e.Id equals d.Id into doctores
                from d in doctores.DefaultIfEmpty()
                join a in _context.Administrativos on e.Id equals a.Id into administrativos
                from a in administrativos.DefaultIfEmpty()
                join az in _context.AsistentesZona on e.Id equals az.Id into asistentesZona
                from az in asistentesZona.DefaultIfEmpty()
                join p in _context.Provincias on e.IdProvincia equals p.Id into provincias
                from p in provincias.DefaultIfEmpty()
                select new UsuarioPerfilDto
                {
                    Id = u.Id,
                    Nombre = e.Name,
                    Correo = u.Email,
                    Telefono = u.PhoneNumber,
                    Rol = r.Name,

                    // Datos del empleado
                    CodigoEmpleado = e.CodigoEmpleado,
                    FechaEntrada = e.fechaEntrada,
                    FechaSalida = e.fechaSalida,
                    Direccion = e.Address,
                    CodigoPostal = e.PostalCode,
                    NIF = e.NIF,
                    SeguridadSocial = e.SocialSecurityNumber,
                    Provincia = p.Nombre,

                    // Datos específicos de cada tipo de empleado
                    NumeroColegiado = d.NumeroColegiado,  // Solo para doctores
                    AreaOficina = a.AreaOficina,          // Solo para administrativos
                    DescripcionZona = az.DescripcionZona // Solo para asistentes de zona
                })
                .FirstOrDefaultAsync();

            return usuario;
        }



    }
}
