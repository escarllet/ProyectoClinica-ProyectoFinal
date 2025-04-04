using Microsoft.EntityFrameworkCore;
using Domain.Entities;

using Domain.Entities.Authentication;
using Domain.Entities.People;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ClinicContext : IdentityDbContext<ApplicationUser>
    {
        public ClinicContext(DbContextOptions<ClinicContext> options) : base(options) { }

        public DbSet<Person> Personas { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Doctor> Doctores { get; set; }
        public DbSet<DoctorInterino> DoctoresInterinos { get; set; }
        public DbSet<DoctorTitular> DoctoresTitulares { get; set; }
        public DbSet<DoctorSustituto> DoctoresSustitutos { get; set; }
        public DbSet<Administrativo> Administrativos { get; set; }
        public DbSet<Asistente> Asistentes { get; set; }
        public DbSet<AsistenteZona> AsistentesZona { get; set; }
        public DbSet<AuxEnfermeria> AuxiliaresEnfermeria { get; set; }
        public DbSet<Celador> Celadores { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<Sustituciones> Sustituciones { get; set; }
        public DbSet<Vacaciones> Vacaciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Employee>()
                .HasDiscriminator<string>("TipoEmpleado")
                .HasValue<Doctor>("Doctor")
                .HasValue<DoctorInterino>("DoctorInterino")
                .HasValue<DoctorTitular>("DoctorTitular")
                .HasValue<DoctorSustituto>("DoctorSustituto")
                .HasValue<Administrativo>("Administrativo")
                .HasValue<Asistente>("Asistente")
                .HasValue<AsistenteZona>("AsistenteZona")
                .HasValue<AuxEnfermeria>("AuxEnfermeria")
                .HasValue<Celador>("Celador");
           
        }
    }
}
