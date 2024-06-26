﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Profitabilitaet.Database.Entities;

namespace Profitabilitaet.Database.Connection;

public class ProjektEntityTypeConfiguration : IEntityTypeConfiguration<Projekt>
{
    public void Configure(EntityTypeBuilder<Projekt> modelBuilder)
    {
        modelBuilder.ToTable("projekt")
            .HasKey(x => x.Id);
        
        modelBuilder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new ProjektId(x))
            .IsRequired();
        
        modelBuilder.Property(x => x.Bezeichnung).IsRequired();

        modelBuilder.Property("LeiterId");
        modelBuilder.HasOne(x => x.Leiter)
            .WithMany()
            .HasForeignKey("LeiterId");

        modelBuilder.Property(x => x.Auftragswert).IsRequired();
        modelBuilder.Property(x => x.AngezahlterBetrag).IsRequired();

        modelBuilder.Property(x => x.Beginn)
            .IsRequired();

        modelBuilder.Property(x => x.Ende)
            .IsRequired();

        modelBuilder.Property(x => x.IstStorniert).IsRequired();

        modelBuilder.HasMany(x => x.Buchungen)
            .WithOne(y => y.Projekt)
            .HasForeignKey("ProjektId");

    }
}