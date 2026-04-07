using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZooManagementSystem.Models.Entities;

namespace ZooManagementSystem.Data.Configurations
{
    public class EcosistemaConfiguration : IEntityTypeConfiguration<Ecosistema>
    {
        public void Configure(EntityTypeBuilder<Ecosistema> entity)
        {
            entity.ToTable("Ecosistema");

            entity.HasKey(e => e.Id);

            // PROPIEDADES

            entity.Property(e => e.Descripcion)
                  .HasMaxLength(200);

            entity.Property(e => e.Url)
                  .HasMaxLength(255);

            // RELACIONES

            // Zonas -> Ecosistema (1:N)
            entity.HasMany(e => e.Zonas)
                  .WithOne(z => z.Ecosistema)
                  .HasForeignKey(z => z.EcosistemaId)
                  .HasConstraintName("FK_Zona_Ecosistema");

            // Animales -> Ecosistema (1:N)
            entity.HasMany(e => e.Animales)
                  .WithOne(a => a.Ecosistema)
                  .HasForeignKey(a => a.EcosistemaId)
                  .HasConstraintName("FK_Animal_Ecosistema");

            // INDICES

            entity.HasIndex(e => e.Descripcion);
        }
    }
}
