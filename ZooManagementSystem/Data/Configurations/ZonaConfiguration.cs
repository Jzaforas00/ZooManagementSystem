using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZooManagementSystem.Models.Entities;

namespace ZooManagementSystem.Data.Configurations
{
    public class ZonaConfiguration : IEntityTypeConfiguration<Zona>
    {
        public void Configure(EntityTypeBuilder<Zona> entity)
        {
            entity.ToTable("Zona");

            entity.HasKey(e => e.ZonaId);

            // PROPIEDADES

            entity.Property(e => e.Descripcion)
                  .IsRequired()
                  .HasMaxLength(200);

            // RELACIONES

            // Ecosistema -> Zona (1:N)
            entity.HasOne(e => e.Ecosistema)
                  .WithMany(e => e.Zonas)
                  .HasForeignKey(e => e.EcosistemaId)
                  .OnDelete(DeleteBehavior.SetNull)
                  .HasConstraintName("FK_Zona_Ecosistema");

            // Zona -> Jaula (1:N)
            entity.HasMany(e => e.Jaulas)
                  .WithOne(j => j.Zona)
                  .HasForeignKey(j => j.ZonaId)
                  .HasConstraintName("FK_Jaula_Zona");

            // INDICES

            entity.HasIndex(e => e.EcosistemaId);
        }
    }
}
