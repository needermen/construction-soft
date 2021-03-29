using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cs.Domain.Files.Configuration
{
    public class WorkerFileConfiguration : IEntityTypeConfiguration<WorkerFile>
    {
        public void Configure(EntityTypeBuilder<WorkerFile> builder)
        {
            builder
                .HasKey(t => new {t.FileId, t.WorkerId});

            builder
                .HasOne(it => it.Worker)
                .WithMany(it => it.Files)
                .HasForeignKey(it => it.WorkerId);

            builder
                .HasOne(it => it.File)
                .WithMany(it => it.Workers)
                .HasForeignKey(it => it.FileId);
        }
    }
}