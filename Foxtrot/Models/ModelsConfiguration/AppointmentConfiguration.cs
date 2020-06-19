using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Foxtrot.Models.ModelsConfiguration
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.StartDate)
                .IsRequired();

            builder.Property(x => x.EndDate)
                .IsRequired();

            builder.HasOne(x => x.Service)
                .WithMany(x => x.Appointments);

            builder.HasOne(x => x.Creator)
                .WithMany(x => x.Appointments);

            builder.HasOne(x => x.Provider)
                .WithMany(x => x.Appointments);
        }
    }
}