using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Models;

namespace WebApplication1.Configurations
{
    public class PortfoiloConfiguration : IEntityTypeConfiguration<Portfoilo>
    {
        public void Configure(EntityTypeBuilder<Portfoilo> builder)
        {
            builder.Property(p=>p.Image).IsRequired();
            builder.Property(p=>p.AlternativeText).IsRequired().HasMaxLength(25);

        }
    }
}
