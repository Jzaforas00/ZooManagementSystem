using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZooManagementSystem.Models.Entities;

namespace ZooManagementSystem.Data.Configurations
{
    public class CategoriaLaboralConfiguration : IEntityTypeConfiguration<CategoriaLaboral>
    {
        public void Configure(EntityTypeBuilder<CategoriaLaboral> entity)
        {
            entity.ToTable("CategoriaLaboral");

            entity.HasKey(e => e.Id);

            // PROPIEDADES

            entity.Property(e => e.Descripcion)
                  .IsRequired()
                  .HasMaxLength(100)
                  .IsUnicode(false);

            entity.Property(e => e.Sueldo)
                  .HasColumnType("decimal(10,2)")
                  .IsRequired();

            // RELACIONES

            // Empleados -> CategoriaLaboral (1:N)
            entity.HasMany(e => e.Empleados)
                  .WithOne(e => e.CategoriaLaboral)
                  .HasForeignKey(e => e.CategoriasId)
                  .OnDelete(DeleteBehavior.Restrict)
                  .HasConstraintName("FK_Empleado_CategoriaLaboral");

            // ÍNDICES

            entity.HasIndex(e => e.Descripcion).IsUnique();
        }
    }
}
