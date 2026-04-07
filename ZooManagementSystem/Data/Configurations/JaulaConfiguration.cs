using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZooManagementSystem.Models.Entities;

namespace ZooManagementSystem.Data.Configurations
{
    public class JaulaConfiguration : IEntityTypeConfiguration<Jaula>
    {
        public void Configure(EntityTypeBuilder<Jaula> entity)
        {
            entity.ToTable("Jaula");

            entity.HasKey(e => e.Id);

            // PROPIEDADES

            entity.Property(e => e.Codigo)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.Property(e => e.ZonaId)
                  .IsRequired();

            // RELACIONES

            // Zona -> Jaula (1:N)
            entity.HasOne(e => e.Zona)
                  .WithMany(z => z.Jaulas)
                  .HasForeignKey(e => e.ZonaId)
                  .OnDelete(DeleteBehavior.Cascade)
                  .HasConstraintName("FK_Jaula_Zona");

            // Jaula -> Animal (1:N)
            entity.HasMany(e => e.Animales)
                  .WithOne(a => a.Jaula)
                  .HasForeignKey(a => a.JaulaId)
                  .HasConstraintName("FK_Animal_Jaula");

            // Jaula -> Empleado (1:N)
            entity.HasMany(e => e.Empleados)
                  .WithOne(e => e.Jaula)
                  .HasForeignKey(e => e.JaulaId)
                  .HasConstraintName("FK_Empleado_Jaula");

            // INDICES

            entity.HasIndex(e => e.Codigo).IsUnique();
            entity.HasIndex(e => e.ZonaId);
        }
    }
}
