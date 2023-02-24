using Loto3000.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Loto3000.Infrastructure.EntityFrameWork.EntityTypeConfiguration
{
    public class DrawTypeConfiguration : IEntityTypeConfiguration<Draw>
    {
        public void Configure(EntityTypeBuilder<Draw> builder)
        {
            builder.Property(p => p.StartGame)
                              .IsRequired();
            builder.Property(p => p.EndGame)
                              .IsRequired();
            builder.Property(p => p.IsActive)
                           .IsRequired();
            builder.Property(p => p.TotalCredits)
                           .IsRequired();

            builder.HasOne(x => x.Admin)
               .WithMany(p => p.Draw)
               .IsRequired()
               .OnDelete(DeleteBehavior.ClientNoAction);


        }
    }
}
