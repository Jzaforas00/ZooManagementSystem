using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZooManagementSystem.Models.Entities;

namespace ZooManagementSystem.Data.Configurations
{
    public class ProveedorConfiguration : IEntityTypeConfiguration<Proveedor>
    {
        public void Configure(EntityTypeBuilder<Proveedor> entity)
        {
            entity.ToTable("Proveedor");

            entity.HasKey(e => e.Id);

            // PROPIEDADES

            entity.Property(e => e.Nombre)
                  .HasMaxLength(100);

            entity.Property(e => e.Direccion)
                  .HasMaxLength(200);

            // RELACIONES

            // Alimentos -> Proveedor (1:N)
            entity.HasMany(e => e.Alimentos)
                  .WithOne(a => a.Proveedor)
                  .HasForeignKey(a => a.ProveedorId)
                  .HasConstraintName("FK_Alimento_Proveedor");

            // INDICES

            entity.HasIndex(e => e.Nombre);
        }
    }
}
