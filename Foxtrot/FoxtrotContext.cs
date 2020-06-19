using Foxtrot.Models;
using Foxtrot.Models.ModelsConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Foxtrot
{
    public class FoxtrotContext : DbContext
    {
        public FoxtrotContext(DbContextOptions<FoxtrotContext> options) : base(options)
        { }

        #region Tables
        
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        
        #endregion

        #region EntityConfigurations

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new AppointmentConfiguration());
            modelBuilder.ApplyConfiguration(new ServiceConfiguration());
            
            base.OnModelCreating(modelBuilder);
        }

        #endregion
        
    }
}