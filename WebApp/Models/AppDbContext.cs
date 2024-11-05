using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebApp.Models;

public class AppDbContext: DbContext
{
    public DbSet<ContactEntity> Contacts { get; set; }
    
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
            .HasData(
                new ContactEntity()
                {
                    Id = 1,
                    FirstName = "ewa",
                    LastName = "bak",
                    BirthDate = new DateOnly(2000, 10, 10),
                    Email = "ewabak@gmail.com",
                    PhoneNumber = "497290407",
                    Created = DateTime.Now


                }, 
                new ContactEntity()
                {
                    Id = 2,
                    FirstName = "flf",
                    LastName = "bhfyuk",
                    BirthDate = new DateOnly(2000, 10,10),
                    Email = "vbsruihbuiwhak@gmail.com",
                    PhoneNumber = "497085668",
                    Created = DateTime.Now
                            
                            
                }


            );
    }
}