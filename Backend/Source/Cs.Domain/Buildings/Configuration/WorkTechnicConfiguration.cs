using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cs.Domain.Buildings.Configuration
{
    public class WorkTechnicConfiguration : IEntityTypeConfiguration<WorkTechnic>
    {
        public void Configure(EntityTypeBuilder<WorkTechnic> builder)
        {
            builder
                .HasKey(t => new {t.WorkId, t.TechnicId});

            builder
                .HasOne(it => it.Work)
                .WithMany(it => it.Technics)
                .HasForeignKey(it => it.WorkId);

            builder
                .HasOne(it => it.Technic)
                .WithMany(it => it.Works)
                .HasForeignKey(it => it.TechnicId);
        }
    }
}