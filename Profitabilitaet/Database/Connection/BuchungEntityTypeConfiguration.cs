using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Profitabilitaet.Database.Entities;

namespace Profitabilitaet.Database.Connection;

public class BuchungEntityTypeConfiguration : IEntityTypeConfiguration<Buchung>
{
    public void Configure(EntityTypeBuilder<Buchung> modelBuilder)
    {
        modelBuilder.ToTable("buchung")
            .HasKey(x => x.Id);

        modelBuilder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new BuchungId(x))
            .IsRequired();

        modelBuilder.Property(x => x.Anteil).IsRequired();
        modelBuilder.Property(x => x.Jahr).IsRequired();
        modelBuilder.Property(x => x.Woche).IsRequired();

        modelBuilder.Property("MitarbeiterId");
        modelBuilder.HasOne(x => x.Mitarbeiter)
            .WithMany()
            .HasForeignKey("MitarbeiterId")
            .IsRequired();

        modelBuilder.Property("ProjektId");
        modelBuilder.HasOne(x => x.Projekt)
            .WithMany(y => y.Buchungen)
            .HasForeignKey("ProjektId")
            .IsRequired();
    }
}