using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfitabilitaetBackend.Entities;

namespace ProfitabilitaetBackend.Connection;

public class AbteilungEntityTypeConfiguration : IEntityTypeConfiguration<ProfitabilitaetBackend.Entities.Abteilung>
{
    public void Configure(EntityTypeBuilder<ProfitabilitaetBackend.Entities.Abteilung> modelBuilder)
    {
        modelBuilder.ToTable("abteilung")
            .HasKey(x => x.Id);
        
        modelBuilder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new AbteilungsId(x))
            .IsRequired();

        modelBuilder.Property("LeiterId");
        
        modelBuilder.HasOne(x => x.Leiter)
            .WithMany()
            .HasForeignKey("LeiterId")
            .IsRequired();
        
        modelBuilder.Property(x => x.Bezeichnung).IsRequired();
        
        modelBuilder.Property(x => x.Etat).IsRequired();
    }
}
