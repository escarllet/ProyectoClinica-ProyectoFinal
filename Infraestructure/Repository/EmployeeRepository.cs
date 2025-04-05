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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infraestructure.Repository
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

        //public async Task<List<Employee>> GetAllEmployeeAsync()
        //{
        //    return await _context.Employees.Where(c => c.Activo).Include(c=>c.User).Include(v=>v.Provincia).ToListAsync();
        //}
        public async Task<List<Doctor>> GetAllDoctoresAsync()
        {
            return await _context.Doctores.Where(c => c.Activo).ToListAsync();
        }
        public async Task<Doctor?> GetIdDoctorByUserId(string userId)
        {
            return await _context.Doctores.FirstOrDefaultAsync(c => c.Activo && c.UserId == userId);
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
                        ProvinciaId = dto.IdProvincia,
                        fechaEntrada = dto.FechaEntradaEmpleado,
                        IdUsuarioCreacion = employee.IdUsuarioCreacion,
                        SocialSecurityNumber = dto.NumeroSeguridadSocial,
                        CodigoEmpleado = dto.CodigoEmpleado,
                        Activo = true,
                        Version = 1,
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
                        ProvinciaId = dto.IdProvincia,
                        fechaEntrada = dto.FechaEntradaEmpleado,
                        IdUsuarioCreacion = employee.IdUsuarioCreacion,
                        SocialSecurityNumber = dto.NumeroSeguridadSocial,
                        CodigoEmpleado = dto.CodigoEmpleado,
                        Activo = true,
                        Version = 1,
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
                        ProvinciaId = dto.IdProvincia,
                        fechaEntrada = dto.FechaEntradaEmpleado,
                        IdUsuarioCreacion = employee.IdUsuarioCreacion,
                        SocialSecurityNumber = dto.NumeroSeguridadSocial,
                        CodigoEmpleado = dto.CodigoEmpleado,
                        Activo = true,
                        Version = 1,
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
                        ProvinciaId = dto.IdProvincia,
                        fechaEntrada = dto.FechaEntradaEmpleado,
                        IdUsuarioCreacion = employee.IdUsuarioCreacion,
                        SocialSecurityNumber = dto.NumeroSeguridadSocial,
                        CodigoEmpleado = dto.CodigoEmpleado,
                        Activo = true,
                        Version = 1,
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
                        ProvinciaId = dto.IdProvincia,
                        fechaEntrada = dto.FechaEntradaEmpleado,
                        IdUsuarioCreacion = employee.IdUsuarioCreacion,
                        SocialSecurityNumber = dto.NumeroSeguridadSocial,
                        CodigoEmpleado = dto.CodigoEmpleado,
                        Activo = true,
                        Version = 1,
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
                        ProvinciaId = dto.IdProvincia,
                        fechaEntrada = dto.FechaEntradaEmpleado,
                        IdUsuarioCreacion = employee.IdUsuarioCreacion,
                        SocialSecurityNumber = dto.NumeroSeguridadSocial,
                        CodigoEmpleado = dto.CodigoEmpleado,
                        Activo = true,
                        Version = 1,
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
                        ProvinciaId = dto.IdProvincia,
                        fechaEntrada = dto.FechaEntradaEmpleado,
                        IdUsuarioCreacion = employee.IdUsuarioCreacion,
                        SocialSecurityNumber = dto.NumeroSeguridadSocial,
                        CodigoEmpleado = dto.CodigoEmpleado,
                        Activo = true,
                        Version = 1,
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
                        ProvinciaId = dto.IdProvincia,
                        fechaEntrada = dto.FechaEntradaEmpleado,
                        IdUsuarioCreacion = employee.IdUsuarioCreacion,
                        SocialSecurityNumber = dto.NumeroSeguridadSocial,
                        CodigoEmpleado = dto.CodigoEmpleado,
                        Activo = true,
                        Version = 1,
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
            //obtengo el rol/tipoEmpleado por el correo
            var user = (from u in _context.Users
                     join ur in _context.UserRoles on u.Id equals ur.UserId into userRoles
                     from ur in userRoles.DefaultIfEmpty()
                     join r in _context.Roles on ur.RoleId equals r.Id into roles
                     from r in roles.DefaultIfEmpty()
                     join e in _context.Employees on u.Id equals e.UserId into employees
                     from e in employees.DefaultIfEmpty()
                     where e.Id == dto.Id
                     select new{ r.Name,u.Id }).FirstOrDefault();

            if (user == null || user.Name.Trim() == "") return false;

            var provincia = await _context.Provincias.FindAsync(dto.IdProvincia);
            if (provincia == null || provincia.Activo == false) throw new Exception("La provincia no existe");

            switch (user.Name)
            {
                case "AuxEnfermeria":
                    var empleado = await _context.AuxiliaresEnfermeria.FirstOrDefaultAsync(c => c.UserId == user.Id);
                    if (empleado == null || empleado.Activo == false) throw new Exception("El Aux.Enfermeria no existe"); 

                    empleado.Name = dto.NombreCompleto;
                    empleado.Address = dto.Direccion;
                    empleado.Phone = dto.Telefono;
                    empleado.PostalCode = dto.CodigoPostal;
                    empleado.NIF = dto.NIF;
                    empleado.ProvinciaId = dto.IdProvincia;
                    empleado.fechaEntrada = dto.FechaEntradaEmpleado;
                    empleado.FechaModificacion = DateTime.Now;
                    empleado.SocialSecurityNumber = dto.NumeroSeguridadSocial;
                    empleado.CodigoEmpleado = dto.CodigoEmpleado;
                    empleado.Version++;

                    _context.AuxiliaresEnfermeria.Update(empleado);
                    break;
                case "ATSZona":
                    var asistenteZona = await _context.AsistentesZona.FirstOrDefaultAsync(c => c.UserId == user.Id);
                    if (asistenteZona == null || asistenteZona.Activo == false) throw new Exception("El Asistente de zona no existe");

                    asistenteZona.Name = dto.NombreCompleto;
                    asistenteZona.Address = dto.Direccion;
                    asistenteZona.Phone = dto.Telefono;
                    asistenteZona.PostalCode = dto.CodigoPostal;
                    asistenteZona.NIF = dto.NIF;
                    asistenteZona.ProvinciaId = dto.IdProvincia;
                    asistenteZona.fechaEntrada = dto.FechaEntradaEmpleado;
                    asistenteZona.FechaModificacion = DateTime.Now;
                    asistenteZona.SocialSecurityNumber = dto.NumeroSeguridadSocial;
                    asistenteZona.CodigoEmpleado = dto.CodigoEmpleado;
                    asistenteZona.Version++;
                    asistenteZona.DescripcionZona = dto.DescripcionZona ?? "N/A";
                    
                    _context.AsistentesZona.Update(asistenteZona);

                    break;
                case "ATS":
                    var asistente = await _context.Asistentes.FirstOrDefaultAsync(c => c.UserId == user.Id);
                    if (asistente == null || asistente.Activo == false) throw new Exception("El Asistente no existe");

                    asistente.Name = dto.NombreCompleto;
                    asistente.Address = dto.Direccion;
                    asistente.Phone = dto.Telefono;
                    asistente.PostalCode = dto.CodigoPostal;
                    asistente.NIF = dto.NIF;
                    asistente.ProvinciaId = dto.IdProvincia;
                    asistente.fechaEntrada = dto.FechaEntradaEmpleado;
                    asistente.FechaModificacion = DateTime.Now;
                    asistente.SocialSecurityNumber = dto.NumeroSeguridadSocial;
                    asistente.CodigoEmpleado = dto.CodigoEmpleado;
                    asistente.Version++;
                    
                    _context.Asistentes.Update(asistente);
                    break;
                case "Celadores":
                    var celador = await _context.Celadores.FirstOrDefaultAsync(c => c.UserId == user.Id);
                    if (celador == null || celador.Activo == false) throw new Exception("El Celador no existe");

                    celador.Name = dto.NombreCompleto;
                    celador.Address = dto.Direccion;
                    celador.Phone = dto.Telefono;
                    celador.PostalCode = dto.CodigoPostal;
                    celador.NIF = dto.NIF;
                    celador.ProvinciaId = dto.IdProvincia;
                    celador.fechaEntrada = dto.FechaEntradaEmpleado;
                    celador.FechaModificacion = DateTime.Now;
                    celador.SocialSecurityNumber = dto.NumeroSeguridadSocial;
                    celador.CodigoEmpleado = dto.CodigoEmpleado;
                    celador.Version++;

                    _context.Celadores.Update(celador);
                    break;
                case "Admin":
                    var admi = await _context.Administrativos.FirstOrDefaultAsync(c => c.UserId == user.Id);
                    if (admi == null || admi.Activo == false) throw new Exception("El Administrador no existe");

                    admi.Name = dto.NombreCompleto;
                    admi.Address = dto.Direccion;
                    admi.Phone = dto.Telefono;
                    admi.PostalCode = dto.CodigoPostal;
                    admi.NIF = dto.NIF;
                    admi.AreaOficina = dto.AreaOficina ?? "N/A";
                    admi.ProvinciaId = dto.IdProvincia;
                    admi.fechaEntrada = dto.FechaEntradaEmpleado;
                    admi.FechaModificacion = DateTime.Now;
                    admi.SocialSecurityNumber = dto.NumeroSeguridadSocial;
                    admi.CodigoEmpleado = dto.CodigoEmpleado;
                    admi.Version++;
                        
                    _context.Administrativos.Update(admi);
                    break;
                case "DoctorSustituto":
                    var sus = await _context.DoctoresSustitutos.FirstOrDefaultAsync(c => c.UserId == user.Id);
                    if (sus == null || sus.Activo == false) throw new Exception("El Doctor Sustituto no existe");

                    sus.Name = dto.NombreCompleto;
                    sus.Address = dto.Direccion;
                    sus.Phone = dto.Telefono;
                    sus.PostalCode = dto.CodigoPostal;
                    sus.NIF = dto.NIF;
                    sus.NumeroColegiado = dto.NumeroColegiado ?? "N/A";
                    sus.ProvinciaId = dto.IdProvincia;
                    sus.fechaEntrada = dto.FechaEntradaEmpleado;
                    sus.FechaModificacion = DateTime.Now;
                    sus.SocialSecurityNumber = dto.NumeroSeguridadSocial;
                    sus.CodigoEmpleado = dto.CodigoEmpleado;
                    sus.Version++;  
      
                    _context.DoctoresSustitutos.Update(sus);
                    break;
                case "DoctorInterino":
                    var inter = await _context.DoctoresInterinos.FirstOrDefaultAsync(c => c.UserId == user.Id);
                    if (inter == null || inter.Activo == false) throw new Exception("El Doctor Interino no existe");

                    inter.Name = dto.NombreCompleto;
                    inter.Address = dto.Direccion;
                    inter.Phone = dto.Telefono;
                    inter.PostalCode = dto.CodigoPostal;
                    inter.NIF = dto.NIF;
                    inter.NumeroColegiado = dto.NumeroColegiado ?? "N/A";
                    inter.ProvinciaId = dto.IdProvincia;
                    inter.fechaEntrada = dto.FechaEntradaEmpleado;
                    inter.FechaModificacion = DateTime.Now;
                    inter.SocialSecurityNumber = dto.NumeroSeguridadSocial;
                    inter.CodigoEmpleado = dto.CodigoEmpleado;
                    inter.Version++;
        
                    _context.DoctoresInterinos.Update(inter);
                    break;
                case "DoctorTitular":
                    var titu = await _context.DoctoresTitulares.FirstOrDefaultAsync(c => c.UserId == user.Id);
                    if (titu == null || titu.Activo == false) throw new Exception("El Doctor Titular no existe");

                    titu.Name = dto.NombreCompleto;
                    titu.Address = dto.Direccion;
                    titu.Phone = dto.Telefono;
                    titu.PostalCode = dto.CodigoPostal;
                    titu.NIF = dto.NIF;
                    titu.NumeroColegiado = dto.NumeroColegiado ?? "N/A";
                    titu.ProvinciaId = dto.IdProvincia;
                    titu.fechaEntrada = dto.FechaEntradaEmpleado;
                    titu.FechaModificacion = DateTime.Now;
                    titu.SocialSecurityNumber = dto.NumeroSeguridadSocial;
                    titu.CodigoEmpleado = dto.CodigoEmpleado;
                    titu.Version++;

                    _context.DoctoresTitulares.Update(titu);
                    break;
                default:
                    throw new Exception("Tipo de empleado no existe");
                  
                   
            }
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> DeleteEmpleadoAsync(DeleteEmployeeDTO deleteEmployee)
        {
            var empleado = await _context.Employees.FindAsync(deleteEmployee.Id);
            if (empleado == null) throw new Exception("El empleado no existe");
            var user = await _context.Users.FindAsync(empleado.UserId);
            if (user == null || user.Activo == false) throw new Exception("El usuario no existe");

            empleado.Activo = false;
            empleado.fechaSalida = deleteEmployee.FechaSalida;
            empleado.Version++;
            empleado.FechaModificacion = DateTime.Now;
            user.Activo = false;
            user.FechaModificacion = DateTime.Now;
            user.Version++;

            _context.Users.Update(user);
            _context.Employees.Update(empleado);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> ActivarEmpleadoAsync(int EmployeeId)
        {
            var empleado = await _context.Employees.FindAsync(EmployeeId);
            if (empleado == null) throw new Exception("El empleado no existe");
            var user = await _context.Users.FindAsync(empleado.UserId);
            if (user == null || user.Activo == true) throw new Exception("El usuario no existe o ya esta activo");

            empleado.Activo = true;          
            empleado.Version++;
            empleado.FechaModificacion = DateTime.Now;
            user.Activo = true;
            user.FechaModificacion = DateTime.Now;
            user.Version++;

            _context.Users.Update(user);
            _context.Employees.Update(empleado);
            await _context.SaveChangesAsync();

            return true;
        }

        public List<UsuarioPerfilDto> GetAllEmployeeAsync(string? filtro = null)
        {
            var usuario =  (
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
                join p in _context.Provincias on e.ProvinciaId equals p.Id into provincias
                from p in provincias.DefaultIfEmpty()
                where u.Activo
                select new UsuarioPerfilDto
                {
                    Id = e.Id,
                    UserId = e.UserId,
                    Nombre = e.Name,
                    Correo = u.Email,
                    Telefono = e.Phone?? u.PhoneNumber,
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
                .AsQueryable();

            if (!string.IsNullOrEmpty(filtro))
            {
                
                 usuario = usuario.Where(u => u.Correo.Contains(filtro)|| u.UserId.Contains(filtro)|| u.Nombre.Contains(filtro)|| u.Rol.Contains(filtro)|| u.CodigoEmpleado.Contains(filtro));
            }
            var k = usuario.ToList();
            return k;
        }



    }
}
