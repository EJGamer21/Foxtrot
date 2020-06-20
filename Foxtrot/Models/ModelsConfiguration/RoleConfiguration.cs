using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Foxtrot.Models.ModelsConfiguration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(200);
            
            builder.Property(x => x.IsDeleted)
                .HasDefaultValue(false);
            
            /*builder.Property(x => x.RowVersion)
                .HasColumnType("datetime2")
                .IsRowVersion()
                .IsRequired();*/
        }
    }
}