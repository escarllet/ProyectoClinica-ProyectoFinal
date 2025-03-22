using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

using System.IO;

namespace Infrastructure.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ClinicContext>
    {
        public ClinicContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ClinicContext>();
            optionsBuilder.UseSqlServer("Server=localhost;Database=clinicDatabase;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");

            return new ClinicContext(optionsBuilder.Options);

        }
    }
}
