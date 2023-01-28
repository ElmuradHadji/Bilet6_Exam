using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Configurations
{
    public class SettingConfiguration: IEntityTypeConfiguration<Setting>
    {
        public void Configure(EntityTypeBuilder<Setting> builder)
        {
            builder.Property(p => p.Logo).IsRequired().HasMaxLength(25);
            builder.Property(p => p.Adress).IsRequired();
            builder.Property(p => p.Year).IsRequired();

        }
    
    }
}
