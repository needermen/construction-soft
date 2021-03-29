using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cs.Domain.Auth.Configuration
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder
                .HasKey(t => new {t.UserId, t.RoleId});

            builder
                .HasOne(it => it.Role)
                .WithMany(it => it.Users)
                .HasForeignKey(it => it.RoleId);

            builder
                .HasOne(it => it.User)
                .WithMany(it => it.Roles)
                .HasForeignKey(it => it.UserId);
        }
    }
}