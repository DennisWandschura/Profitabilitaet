using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Profitabilitaet.Database;

public class AbteilungEntityTypeConfiguration : IEntityTypeConfiguration<Abteilung>
{
    public void Configure(EntityTypeBuilder<Abteilung> modelBuilder)
    {
        modelBuilder.ToTable("abteilung")
            .HasKey(x => x.Id);
        
        modelBuilder.Property(x => x.Id)
            .HasConversion(x => x.Id, x => new AbteilungsId(x))
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