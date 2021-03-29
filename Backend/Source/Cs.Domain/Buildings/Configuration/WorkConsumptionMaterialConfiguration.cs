using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cs.Domain.Buildings.Configuration
{
    public class WorkConsumptionMaterialConfiguration : IEntityTypeConfiguration<WorkConsumptionMaterial>
    {
        public void Configure(EntityTypeBuilder<WorkConsumptionMaterial> builder)
        {
            builder
                .HasKey(t => new {t.WorkId, t.MaterialId});

            builder
                .HasOne(it => it.Work)
                .WithMany(it => it.ConsumptionMaterials)
                .HasForeignKey(it => it.WorkId);

            builder
                .HasOne(it => it.Material)
                .WithMany(it => it.Works)
                .HasForeignKey(it => it.MaterialId);
        }
    }
}