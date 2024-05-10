using ClientManagement.Core.Domain.Common;
using ClientManagement.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClientManagement.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreatedBy = "DefaultAppUser";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        entry.Entity.LastModifiedBy = "DefaultAppUser";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region Tables

            modelBuilder.Entity<Client>().ToTable("Clients");
            modelBuilder.Entity<Address>().ToTable("Addresses");

            #endregion

            #region "Primary Keys"

            modelBuilder.Entity<Client>().HasKey(c => c.Id);
            modelBuilder.Entity<Address>().HasKey(a => a.Id);

            #endregion

            #region "Relationships"

            modelBuilder.Entity<Client>()
                .HasMany(ca => ca.Addresses)
                .WithOne(c => c.Client)
                .HasForeignKey(ca => ca.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region "Property configurations"

            #region "Client"
            modelBuilder.Entity<Client>()
                .Property(c => c.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Client>()
                .Property(c => c.LastName)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Client>()
                .Property(c => c.Email)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Client>()
                .Property(c => c.Phone)
                .HasMaxLength(15)
                .IsRequired();

            modelBuilder.Entity<Client>()
                .Property(c => c.IdentificationNumber)
                .HasMaxLength(20)
                .IsRequired();

            #endregion

            #region "Address"

            modelBuilder.Entity<Address>()
                .Property(a => a.ClientId)
                .IsRequired();

            modelBuilder.Entity<Address>()
                .Property(a => a.Street)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Address>()
                .Property(a => a.City)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Address>()
                .Property(a => a.State)
                .HasMaxLength(50);

            modelBuilder.Entity<Address>()
                .Property(a => a.PostalCode)
                .HasMaxLength(10)
                .IsRequired();

            modelBuilder.Entity<Address>()
                .Property(a => a.Country)
                .HasMaxLength(50)
                .IsRequired();

            #endregion

            #endregion

        }

    }
}
