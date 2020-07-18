using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Foxtrot.Extensions;
using Foxtrot.Models;
using Foxtrot.Models.Contracts;
using Foxtrot.Models.ModelsConfiguration;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Foxtrot
{
    public class FoxtrotContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public FoxtrotContext(DbContextOptions<FoxtrotContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        #region Tables
        
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        
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

        #region Save Changes

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Get the entries that are auditable
            var auditableEntitySet = ChangeTracker.Entries<IAuditableModel>().ToList();

            if (auditableEntitySet.Any())
            {
                Guid userId = _httpContextAccessor.HttpContext.GetLoggedUserId();
                //Guid userId = new Guid("9F917B76-5B52-4DF2-9A9E-D99875777AC4");
                DateTime currentDate = DateTime.Now;

                // Set the audit information for each record
                foreach (var auditableEntity in auditableEntitySet.Where(x =>
                    x.State == EntityState.Added || x.State == EntityState.Modified))
                {
                    if (auditableEntity.State == EntityState.Added)
                    {
                        auditableEntity.Entity.CreatedDate = currentDate;
                        auditableEntity.Entity.CreatedBy = userId;
                    }

                    auditableEntity.Entity.UpdatedDate = currentDate;
                    auditableEntity.Entity.UpdatedBy = userId;
                }
            }
            
            // Try-catch for ConcurrencyException
            try
            {
                return await base.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                await ex.Entries.Single().ReloadAsync(cancellationToken);
                return await base.SaveChangesAsync(cancellationToken);
            }
        }

        #endregion
        
    }
}