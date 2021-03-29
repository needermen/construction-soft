using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Cs.Persistence.Factory
{
    public class CsDbContetFactory : IDesignTimeDbContextFactory<CsDbContext>
    {
        public CsDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CsDbContext>();

            optionsBuilder.UseSqlServer($"Server=31.146.182.18,1433;Initial Catalog=ConstructionSoft_Dev;Persist Security Info=False;User ID=ConstructionSoft;Password=JD4sUY7hfx9vcWyp;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");

            return new CsDbContext(optionsBuilder.Options);
        }
    }
}