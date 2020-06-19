using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Foxtrot.Models.ModelsConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FullName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.Address)
                .HasMaxLength(200);

            builder.Property(x => x.Dni)
                .HasMaxLength(50)
                .IsRequired();
            
            builder.HasOne(x => x.Role)
                .WithMany(x => x.Users);
        }
    }
}