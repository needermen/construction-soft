using System.Linq;
using Cs.Domain.Auth;
using Cs.Domain.Auth.Configuration;
using Cs.Domain.Buildings;
using Cs.Domain.Buildings.Configuration;
using Cs.Domain.Files;
using Cs.Domain.Files.Configuration;
using Cs.Domain.Hr;
using Cs.Domain.Materials;
using Cs.Domain.Technics;
using Microsoft.EntityFrameworkCore;

namespace Cs.Persistence
{
    public sealed class CsDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<BuildingMaterial> BuildingMaterials { get; set; }
        public DbSet<ConsumptionMaterial> ConsumptionMaterials { get; set; }
        public DbSet<MainMaterial> MainMaterials { get; set; }

        public DbSet<Technic> Technics { get; set; }

        public DbSet<Worker> Workers { get; set; }
        public DbSet<Brigade> Brigades { get; set; }

        public DbSet<Organization> Organizations { get; set; }

        public DbSet<Building> Buildings { get; set; }

        public DbSet<File> Files { get; set; }

        public CsDbContext(DbContextOptions<CsDbContext> options) : base(options)
        {
            if (Database.GetPendingMigrations().Any())
            {
                Database.Migrate();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.Relational().ColumnType = "decimal(18, 6)";
            }

            modelBuilder.Entity<Worker>()
                .Property(b => b.Coefficient)
                .HasDefaultValue(1);

            modelBuilder.Entity<BuildingMaterial>()
                .Property(b => b.Coefficient)
                .HasDefaultValue(1);

            modelBuilder.Entity<ConsumptionMaterial>()
                .Property(b => b.Coefficient)
                .HasDefaultValue(1);

            modelBuilder.Entity<MainMaterial>()
                .Property(b => b.Coefficient)
                .HasDefaultValue(1);

            modelBuilder.Entity<Technic>()
                .Property(b => b.Coefficient)
                .HasDefaultValue(1);

            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new OrganizationRoleConfiguration());

            modelBuilder.ApplyConfiguration(new WorkBuildingMaterialConfiguration());
            modelBuilder.ApplyConfiguration(new WorkConsumptionMaterialConfiguration());
            modelBuilder.ApplyConfiguration(new WorkMainMaterialConfiguration());
            modelBuilder.ApplyConfiguration(new WorkTechnicConfiguration());
            modelBuilder.ApplyConfiguration(new WorkWorkerConfiguration());
            modelBuilder.ApplyConfiguration(new WorkBrigadeConfiguration());

            modelBuilder.ApplyConfiguration(new WorkerFileConfiguration());
            modelBuilder.ApplyConfiguration(new BrigadeFileConfiguration());
        }
    }
}