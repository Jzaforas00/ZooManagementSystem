using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZooManagementSystem.Models.Entities;

namespace ZooManagementSystem.Data.Configurations
{
    public class EmpleadoConfiguration : IEntityTypeConfiguration<Empleado>
    {
        public void Configure(EntityTypeBuilder<Empleado> entity)
        {
            entity.ToTable("Empleado");

            entity.HasKey(e => e.Id);

            // PROPIEDADES

            entity.Property(e => e.Nombre)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(e => e.Apodo)
                  .HasMaxLength(50);

            entity.Property(e => e.Telefono)
                  .HasMaxLength(20);

            entity.Property(e => e.Cp)
                  .HasMaxLength(10);

            // RELACIONES

            // categoriaLaboral - Empleado (1:N)
            entity.HasOne(e => e.CategoriaLaboral)
                  .WithMany(c => c.Empleados)
                  .HasForeignKey(e => e.CategoriasId)
                  .HasConstraintName("FK_Empleado_CategoriaLaboral");

            // jaula - Empleado (1:N)
            entity.HasOne(e => e.Jaula)
                  .WithMany(j => j.Empleados)
                  .HasForeignKey(e => e.JaulaId)
                  .HasConstraintName("FK_Empleado_Jaula");

            // ÍNDICES

            entity.HasIndex(e => e.CategoriasId);
            entity.HasIndex(e => e.JaulaId);
        }
    }
}
