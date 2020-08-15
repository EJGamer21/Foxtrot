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

            builder.Property(x => x.Note)
                .HasMaxLength(200);

            builder.HasOne(x => x.Service)
                .WithMany(x => x.Appointments);

            builder.HasOne(x => x.Creator)
                .WithMany(x => x.MyAppointments);

            builder.HasOne(x => x.Provider)
                .WithMany(x => x.Appointments);

            builder.HasOne(x => x.Status)
                .WithMany(x => x.Appointments);
            
            builder.Property(x => x.IsDeleted)
                .HasDefaultValue(false);
            
            /*builder.Property(x => x.RowVersion)
                .HasColumnType("datetime2")
                .IsRowVersion()
                .IsRequired();*/
        }
    }
}