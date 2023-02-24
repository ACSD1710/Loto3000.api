using Loto3000.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loto3000.Infrastructure.EntityFrameWork.EntityTypeConfiguration
{
    public class AdminTypeConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.Property(p => p.Name)
                            .HasMaxLength(100)
                            .IsRequired();
            builder.Property(p => p.Password)
                           .HasMaxLength(100)
                           .IsRequired();
            builder.Property(p => p.Username)
                           .HasMaxLength(100)
                           .IsRequired();
        }
    }
}
