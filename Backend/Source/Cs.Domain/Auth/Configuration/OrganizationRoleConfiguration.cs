using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cs.Domain.Auth.Configuration
{
    public class OrganizationRoleConfiguration : IEntityTypeConfiguration<OrganizationRole>
    {
        public void Configure(EntityTypeBuilder<OrganizationRole> builder)
        {
            builder
                .HasKey(t => new {t.OrganizationId, t.RoleId});

            builder
                .HasOne(it => it.Role)
                .WithMany(it => it.OrganizationRoles)
                .HasForeignKey(it => it.RoleId);

            builder
                .HasOne(it => it.Organization)
                .WithMany(it => it.Roles)
                .HasForeignKey(it => it.OrganizationId);
        }
    }
}