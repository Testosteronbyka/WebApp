using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebApp.Models;

public class AppDbContext: DbContext
{
    public DbSet<ContactEntity> Contacts { get; set; }
    public DbSet<OrganizationEntity> Organizations { get; set; }
    private string DbPath { get; set; }

    public AppDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "contacts.db");
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data source={DbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ContactEntity>()
            .HasOne<OrganizationEntity>(c => c.Organization)
            .WithMany(o => o.Contacts)
            .HasForeignKey(c => c.OrganizationId);

        modelBuilder.Entity<OrganizationEntity>()
            .ToTable("organizations")
            .HasData(
                    new OrganizationEntity()
                    {
                        Id= 101,
                        Name = "WSEI",
                        NIP = "098482899",
                        REGON = "09786179841"
                    },
                    new OrganizationEntity()
                    {
                        Id= 102,
                        Name = "PKP",
                        NIP = "893894618",
                        REGON = "6894386829"
                    }
                );
        modelBuilder.Entity<OrganizationEntity>()
            .OwnsOne(o => o.Address)
            .HasData(
                new 
                {
                    City = "Kraków",
                    Street = "Św. filipa 17",
                    OrganizationEntityId = 101
                },
                new 
                {
                    City = "Warszawa",
                    Street = "Dworcowa 8",
                    OrganizationEntityId = 102
                }
            );
        
        modelBuilder.Entity<ContactEntity>()
            .HasData(
                new ContactEntity()
                {
                    Id = 1,
                    FirstName = "ewa",
                    LastName = "bak",
                    BirthDate = new DateOnly(2000, 10, 10),
                    Email = "ewabak@gmail.com",
                    PhoneNumber = "497290407",
                    Created = DateTime.Now,
                    OrganizationId = 101


                }, 
                new ContactEntity()
                {
                    Id = 2,
                    FirstName = "flf",
                    LastName = "bhfyuk",
                    BirthDate = new DateOnly(1997, 10,10),
                    Email = "vbsruihbuiwhak@gmail.com",
                    PhoneNumber = "497085668",
                    Created = DateTime.Now,
                    OrganizationId = 102
                            
                            
                }


            );
    }
}