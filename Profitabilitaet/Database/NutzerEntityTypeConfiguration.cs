using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Profitabilitaet.Database;

public class NutzerEntityTypeConfiguration : IEntityTypeConfiguration<Nutzer>
{
    public void Configure(EntityTypeBuilder<Nutzer> modelBuilder)
    {
        modelBuilder.ToTable("nutzer")
            .HasKey(x => x.Id);
                
        modelBuilder.Property(x => x.Id)
            .HasConversion(x => x.Id, x => new NutzerId(x))
            .IsRequired();

        modelBuilder.Property(x => x.Rolle)
            .HasConversion<string>();
        
        modelBuilder.Property(x => x.Vorname).IsRequired();
        modelBuilder.Property(x => x.Nachname).IsRequired();
        modelBuilder.Property(x => x.Plz);
        modelBuilder.Property(x => x.Ort);
        modelBuilder.Property(x => x.Strasse);
        modelBuilder.Property(x => x.Hausnummer);
        modelBuilder.Property(x => x.Geschlecht).HasConversion<string>();
        modelBuilder.Property(x => x.Telefonnummer);
        modelBuilder.Property(x => x.Einstellungsdatum).IsRequired();
        modelBuilder.Property(x => x.Loginname).IsRequired();
        modelBuilder.Property(x => x.Passwort).IsRequired();
    }
}