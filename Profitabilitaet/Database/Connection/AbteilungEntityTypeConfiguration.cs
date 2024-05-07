using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Profitabilitaet.Database.Connection;

public class AbteilungEntityTypeConfiguration : IEntityTypeConfiguration<Entities.Abteilung>
{
    public void Configure(EntityTypeBuilder<Entities.Abteilung> modelBuilder)
    {
        modelBuilder.ToTable("abteilung")
            .HasKey(x => x.Id);
        
        modelBuilder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new Entities.AbteilungsId(x))
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
