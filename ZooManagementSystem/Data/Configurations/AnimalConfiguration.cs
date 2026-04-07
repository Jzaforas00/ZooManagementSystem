using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZooManagementSystem.Models.Entities;

namespace ZooManagementSystem.Data.Configurations
{
    public class AnimalConfiguration : IEntityTypeConfiguration<Animal>
    {
        public void Configure(EntityTypeBuilder<Animal> entity)
        {
            entity.ToTable("Animal");

            entity.HasKey(e => e.Id);

            // PROPIEDADES

            entity.Property(e => e.Especie)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.NombrePopular)
                .HasColumnName("NombrePopular")
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.UrlImagen)
                .HasColumnName("UrlImagen")
                .HasMaxLength(255);

            // RELATIONS

            // Animal -> Jaula (1:N)
            entity.HasOne(e => e.Jaula).WithMany(j => j.Animales)
                  .HasForeignKey(e => e.JaulaId)
                  .OnDelete(DeleteBehavior.Restrict)
                  .HasConstraintName("FK_Animal_Jaula");

            // Animal -> Ecosistema (1:N)
            entity.HasOne(e => e.Ecosistema)
                  .WithMany(ec => ec.Animales)
                  .HasForeignKey(e => e.EcosistemaId)
                  .OnDelete(DeleteBehavior.Restrict)
                  .HasConstraintName("FK_Animal_Ecosistema");

            // Animal -> Alimentacion (1:N)
            entity.HasOne(e => e.Alimentacion)
                  .WithMany(a => a.Animales)
                  .HasForeignKey(e => e.AlimentacionId)
                  .OnDelete(DeleteBehavior.SetNull)
                  .HasConstraintName("FK_Animal_Alimentacion");

            // Animal -> Dosis (1:N)
            entity.HasMany(e => e.Dosis)
                .WithOne(d => d.Animal)
                .HasForeignKey(d => d.AnimalId)
                .OnDelete(DeleteBehavior.Cascade);

            // INDICES
            entity.HasIndex(e => e.NombrePopular);
        }
    }
}
