﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BOs;

public partial class EnglishPremierLeague2024DbContext : DbContext
{
    public EnglishPremierLeague2024DbContext()
    {
    }

    public EnglishPremierLeague2024DbContext(DbContextOptions<EnglishPremierLeague2024DbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<FootballClub> FootballClubs { get; set; }

    public virtual DbSet<FootballPlayer> FootballPlayers { get; set; }

    public virtual DbSet<PremierLeagueAccount> PremierLeagueAccounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(GetConnectionString());
        }
    }
    private string GetConnectionString()
    {
        IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true).Build();
        return configuration["ConnectionStrings:DefaultConnectionString"];
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FootballClub>(entity =>
        {
            entity.HasKey(e => e.FootballClubId).HasName("PK__Football__91279504683C22F9");

            entity.ToTable("FootballClub");

            entity.Property(e => e.FootballClubId)
                .HasMaxLength(30)
                .HasColumnName("FootballClubID");
            entity.Property(e => e.ClubName).HasMaxLength(100);
            entity.Property(e => e.ClubShortDescription).HasMaxLength(400);
            entity.Property(e => e.Mascos).HasMaxLength(100);
            entity.Property(e => e.SoccerPracticeField).HasMaxLength(250);
        });

        modelBuilder.Entity<FootballPlayer>(entity =>
        {
            entity.HasKey(e => e.FootballPlayerId).HasName("PK__Football__6D5466C3B06AEC54");

            entity.ToTable("FootballPlayer");

            entity.Property(e => e.FootballPlayerId)
                .HasMaxLength(30)
                .HasColumnName("FootballPlayerID");
            entity.Property(e => e.Achievements).HasMaxLength(400);
            entity.Property(e => e.Birthday).HasColumnType("datetime");
            entity.Property(e => e.FootballClubId)
                .HasMaxLength(30)
                .HasColumnName("FootballClubID");
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Nomination).HasMaxLength(400);
            entity.Property(e => e.PlayerExperiences).HasMaxLength(400);

            entity.HasOne(d => d.FootballClub).WithMany(p => p.FootballPlayers)
                .HasForeignKey(d => d.FootballClubId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__FootballP__Footb__3C69FB99");
        });

        modelBuilder.Entity<PremierLeagueAccount>(entity =>
        {
            entity.HasKey(e => e.AccId).HasName("PK__PremierL__91CBC398A0E181BC");

            entity.ToTable("PremierLeagueAccount");

            entity.HasIndex(e => e.EmailAddress, "UQ__PremierL__49A14740DF522471").IsUnique();

            entity.Property(e => e.AccId)
                .ValueGeneratedNever()
                .HasColumnName("AccID");
            entity.Property(e => e.Description).HasMaxLength(140);
            entity.Property(e => e.EmailAddress).HasMaxLength(90);
            entity.Property(e => e.Password).HasMaxLength(90);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
