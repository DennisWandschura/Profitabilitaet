using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
        
        modelBuilder.Property(x => x.Name).IsRequired();
        modelBuilder.Property(x => x.Bezeichnung);
        
        modelBuilder.Property("LeiterId");
        modelBuilder.HasOne(x => x.Leiter)
            .WithMany()
            .HasForeignKey("LeiterId")
            .IsRequired();
        
        modelBuilder.Property(x => x.Auftragswert);
        modelBuilder.Property(x => x.AngezahlterBetrag);
        modelBuilder.Property(x => x.Beginn).IsRequired();
        modelBuilder.Property(x => x.Ende).IsRequired();
        modelBuilder.Property(x => x.IstStorniert).IsRequired();
    }
}