using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cs.Domain.Files.Configuration
{
    public class BrigadeFileConfiguration : IEntityTypeConfiguration<BrigadeFile>
    {
        public void Configure(EntityTypeBuilder<BrigadeFile> builder)
        {
            builder
                .HasKey(t => new {t.FileId, t.BrigadeId});

            builder
                .HasOne(it => it.Brigade)
                .WithMany(it => it.Files)
                .HasForeignKey(it => it.BrigadeId);

            builder
                .HasOne(it => it.File)
                .WithMany(it => it.Brigades)
                .HasForeignKey(it => it.FileId);
        }
    }
}