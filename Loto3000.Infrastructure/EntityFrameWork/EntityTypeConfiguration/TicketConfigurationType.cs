using Loto3000.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;

namespace Loto3000.Infrastructure.EntityFrameWork.EntityTypeConfiguration
{
    public class TicketConfigurationType : IEntityTypeConfiguration<Ticket> 
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.Property(p => p.CombinationNumbers)
                             .HasMaxLength(7)
                             .IsRequired();
            builder.Property(p => p.TicketOwner)
                            .HasMaxLength(100)
                            .IsRequired();


        }
    }
}
