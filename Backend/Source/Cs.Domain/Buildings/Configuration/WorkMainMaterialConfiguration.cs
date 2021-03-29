using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cs.Domain.Buildings.Configuration
{
    public class WorkMainMaterialConfiguration : IEntityTypeConfiguration<WorkMainMaterial>
    {
        public void Configure(EntityTypeBuilder<WorkMainMaterial> builder)
        {
            builder
                .HasKey(t => new {t.WorkId, t.MaterialId});

            builder
                .HasOne(it => it.Work)
                .WithMany(it => it.MainMaterials)
                .HasForeignKey(it => it.WorkId);

            builder
                .HasOne(it => it.Material)
                .WithMany(it => it.Works)
                .HasForeignKey(it => it.MaterialId);
        }
    }
}