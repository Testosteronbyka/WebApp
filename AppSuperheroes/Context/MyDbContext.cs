using System;
using System.Collections.Generic;
using AppSuperheroes.Entities;
using Microsoft.EntityFrameworkCore;
using Attribute = AppSuperheroes.Entities.Attribute;

namespace AppSuperheroes.Context;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alignment> Alignments { get; set; }

    public virtual DbSet<Attribute> Attributes { get; set; }

    public virtual DbSet<Colour> Colours { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<HeroAttribute> HeroAttributes { get; set; }

    public virtual DbSet<HeroPower> HeroPowers { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

    public virtual DbSet<Race> Races { get; set; }

    public virtual DbSet<Superhero> Superheroes { get; set; }

    public virtual DbSet<Superpower> Superpowers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
                
            optionsBuilder.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alignment>(entity =>
        {
            entity.ToTable("alignment");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Alignment1)
                .HasDefaultValueSql("NULL")
                .HasColumnName("alignment");
        });

        modelBuilder.Entity<Attribute>(entity =>
        {
            entity.ToTable("attribute");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AttributeName)
                .HasDefaultValueSql("NULL")
                .HasColumnName("attribute_name");
        });

        modelBuilder.Entity<Colour>(entity =>
        {
            entity.ToTable("colour");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Colour1)
                .HasDefaultValueSql("NULL")
                .HasColumnName("colour");
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.ToTable("gender");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Gender1)
                .HasDefaultValueSql("NULL")
                .HasColumnName("gender");
        });

        modelBuilder.Entity<HeroAttribute>(entity =>
        {
            entity.HasKey(e => new { e.HeroId, e.AttributeId }); // Klucz złożony

            entity.ToTable("hero_attribute");

            entity.Property(e => e.HeroId).HasColumnName("hero_id");
            entity.Property(e => e.AttributeId).HasColumnName("attribute_id");

            entity.HasOne(d => d.Hero)
                .WithMany()
                .HasForeignKey(d => d.HeroId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Attribute)
                .WithMany()
                .HasForeignKey(d => d.AttributeId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<HeroPower>(entity =>
        {
            entity.HasKey(e => new { e.HeroId, e.PowerId }); // Ustaw klucz złożony

            entity.ToTable("hero_power");

            entity.Property(e => e.HeroId).HasColumnName("hero_id");
            entity.Property(e => e.PowerId).HasColumnName("power_id");

            entity.HasOne(d => d.Hero)
                .WithMany(p => p.HeroPowers)
                .HasForeignKey(d => d.HeroId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(d => d.Power)
                .WithMany(p => p.HeroPowers)
                .HasForeignKey(d => d.PowerId)
                .OnDelete(DeleteBehavior.Cascade);
        });


        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.ToTable("publisher");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.PublisherName)
                .HasDefaultValueSql("NULL")
                .HasColumnName("publisher_name");
        });

        modelBuilder.Entity<Race>(entity =>
        {
            entity.ToTable("race");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Race1)
                .HasDefaultValueSql("NULL")
                .HasColumnName("race");
        });

        modelBuilder.Entity<Superhero>(entity =>
        {
            entity.ToTable("superhero");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AlignmentId)
                .HasDefaultValueSql("NULL")
                .HasColumnName("alignment_id");
            entity.Property(e => e.EyeColourId)
                .HasDefaultValueSql("NULL")
                .HasColumnName("eye_colour_id");
            entity.Property(e => e.FullName)
                .HasDefaultValueSql("NULL")
                .HasColumnName("full_name");
            entity.Property(e => e.GenderId)
                .HasDefaultValueSql("NULL")
                .HasColumnName("gender_id");
            entity.Property(e => e.HairColourId)
                .HasDefaultValueSql("NULL")
                .HasColumnName("hair_colour_id");
            entity.Property(e => e.HeightCm)
                .HasDefaultValueSql("NULL")
                .HasColumnName("height_cm");
            entity.Property(e => e.PublisherId)
                .HasDefaultValueSql("NULL")
                .HasColumnName("publisher_id");
            entity.Property(e => e.RaceId)
                .HasDefaultValueSql("NULL")
                .HasColumnName("race_id");
            entity.Property(e => e.SkinColourId)
                .HasDefaultValueSql("NULL")
                .HasColumnName("skin_colour_id");
            entity.Property(e => e.SuperheroName)
                .HasDefaultValueSql("NULL")
                .HasColumnName("superhero_name");
            entity.Property(e => e.WeightKg)
                .HasDefaultValueSql("NULL")
                .HasColumnName("weight_kg");

            entity.HasOne(d => d.Alignment).WithMany(p => p.Superheroes).HasForeignKey(d => d.AlignmentId);

            entity.HasOne(d => d.EyeColour).WithMany(p => p.SuperheroEyeColours).HasForeignKey(d => d.EyeColourId);

            entity.HasOne(d => d.Gender).WithMany(p => p.Superheroes).HasForeignKey(d => d.GenderId);

            entity.HasOne(d => d.HairColour).WithMany(p => p.SuperheroHairColours).HasForeignKey(d => d.HairColourId);

            entity.HasOne(d => d.Publisher).WithMany(p => p.Superheroes).HasForeignKey(d => d.PublisherId);

            entity.HasOne(d => d.Race).WithMany(p => p.Superheroes).HasForeignKey(d => d.RaceId);

            entity.HasOne(d => d.SkinColour).WithMany(p => p.SuperheroSkinColours).HasForeignKey(d => d.SkinColourId);
        });

        modelBuilder.Entity<Superpower>(entity =>
        {
            entity.ToTable("superpower");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.PowerName)
                .HasDefaultValueSql("NULL")
                .HasColumnName("power_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
