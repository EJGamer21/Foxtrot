using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Foxtrot.Models.ModelsConfiguration
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Duration)
                .IsRequired();
            
            builder.Property(x => x.IsDeleted)
                .HasDefaultValue(false);
            
            /*builder.Property(x => x.RowVersion)
                .HasColumnType("datetime2")
                .IsRowVersion()
                .IsRequired();*/
        }
    }
}