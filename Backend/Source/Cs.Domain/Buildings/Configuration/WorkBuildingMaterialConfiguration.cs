using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cs.Domain.Buildings.Configuration
{
    public class WorkBuildingMaterialConfiguration : IEntityTypeConfiguration<WorkBuildingMaterial>
    {
        public void Configure(EntityTypeBuilder<WorkBuildingMaterial> builder)
        {
            builder
                .HasKey(t => new {t.WorkId, t.MaterialId});

            builder
                .HasOne(it => it.Work)
                .WithMany(it => it.BuildingMaterials)
                .HasForeignKey(it => it.WorkId);

            builder
                .HasOne(it => it.BuildingMaterial)
                .WithMany(it => it.Works)
                .HasForeignKey(it => it.MaterialId);
        }
    }
}