using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfitabilitaetBackend.Entities;

namespace ProfitabilitaetBackend.Connection;

public class ProjektEntityTypeConfiguration : IEntityTypeConfiguration<Projekt>
{
    public void Configure(EntityTypeBuilder<Projekt> modelBuilder)
    {
        modelBuilder.ToTable("projekt")
            .HasKey(x => x.Id);
        
        modelBuilder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new ProjektId(x))
            .IsRequired();
        
        modelBuilder.Property(x => x.Bezeichnung);
        
        modelBuilder.Property("LeiterId");
        modelBuilder.HasOne(x => x.Leiter)
            .WithMany()
            .HasForeignKey("LeiterId")
            .IsRequired();
        
        modelBuilder.Property(x => x.Auftragswert).IsRequired();
        modelBuilder.Property(x => x.AngezahlterBetrag).IsRequired();

        modelBuilder.Property(x => x.Beginn)
            .HasConversion(x => x.ToDateTime(),
            x => x.ToDateOnly())
            .IsRequired();

        modelBuilder.Property(x => x.Ende)
            .HasConversion(x => x.ToDateTime(),
            x => x.ToDateOnly())
            .IsRequired();

        modelBuilder.Property(x => x.IstStorniert).IsRequired();
    }
}