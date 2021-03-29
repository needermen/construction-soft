using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cs.Domain.Buildings.Configuration
{
    public class WorkWorkerConfiguration : IEntityTypeConfiguration<WorkWorker>
    {
        public void Configure(EntityTypeBuilder<WorkWorker> builder)
        {
            builder
                .HasKey(t => new {t.WorkId, t.WorkerId});

            builder
                .HasOne(it => it.Work)
                .WithMany(it => it.Workers)
                .HasForeignKey(it => it.WorkId);

            builder
                .HasOne(it => it.Worker)
                .WithMany(it => it.Works)
                .HasForeignKey(it => it.WorkerId);
        }
    }
}