using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Foxtrot.Models.ModelsConfiguration
{
    public class AppointmentStatusConfiguration : IEntityTypeConfiguration<AppointmentStatus
    >
    {
        public void Configure(EntityTypeBuilder<AppointmentStatus> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();
            
            builder.Property(x => x.IsDeleted)
                .HasDefaultValue(false);
        }
    }
}