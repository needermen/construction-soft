using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cs.Domain.Buildings.Configuration
{
    public class WorkBrigadeConfiguration : IEntityTypeConfiguration<WorkBrigade>
    {
        public void Configure(EntityTypeBuilder<WorkBrigade> builder)
        {
            builder
                .HasKey(t => new {t.WorkId, t.BrigadeId});

            builder
                .HasOne(it => it.Work)
                .WithMany(it => it.Brigades)
                .HasForeignKey(it => it.WorkId);

            builder
                .HasOne(it => it.Brigade)
                .WithMany(it => it.Works)
                .HasForeignKey(it => it.BrigadeId);
        }
    }
}