using Microsoft.EntityFrameworkCore;
using ServicesProvider.Data.Entities;
using ServicesProvider.Enums;
using ServicesProvider.Models;
using System.Diagnostics.Contracts;

namespace ServicesProvider.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base (options)
        {
        }

        public DbSet<ContractEntity> Contracts { get; set; }
        public DbSet<ContractStatusEntity> ContractStatuses { get; set; }
        public DbSet<RequestEntity> Requests { get; set; }
        public DbSet<RequestStatusEntity> RequestStatuses { get; set; }
        public DbSet<ServiceEntity> Services { get; set; }
        public DbSet<ServiceCategoryEntity> ServiceCategories { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UserRoleEntity> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var roles = Enum.GetValues<UserRole>().Select(r => new UserRoleEntity
            {
                Id = (int)r,
                Name = r.ToString()
            });

            modelBuilder.Entity<Entities.UserRoleEntity>()
                .HasData(roles);
        }
    }

    //private void UpdateStructure (ModelBuilder modelBuilder)
    //{
    //    modelBuilder.Entity<Entities.Contract>()
    //        .HasKey(c => c.Id);

    //    modelBuilder.Entity<Entities.Contract>()
    //        .HasOne(c => c.User)
    //        .WithMany(u => u.Contracts)
    //        .HasForeignKey(c => c.UserId);

    //    modelBuilder.Entity<Entities.Contract>()
    //        .HasOne(c => c.Status)
    //        .WithMany(s => s.Contracts)
    //        .HasForeignKey(c => c.StatusId);

    //    modelBuilder.Entity<Entities.Contract>()
    //        .HasOne(c => c.Service)
    //        .WithMany(ser => ser.Contracts)
    //        .HasForeignKey(c => c.ServiceId);

    //    //
    //    modelBuilder.Entity<ContractStatus>()
    //        .HasKey(cs => cs.Id);

    //    modelBuilder.Entity<ContractStatus>()
    //        .HasMany(cs => cs.Contracts)
    //        .WithOne(s => s.Status)
    //        .HasForeignKey(c => c.StatusId);

    //}
}
