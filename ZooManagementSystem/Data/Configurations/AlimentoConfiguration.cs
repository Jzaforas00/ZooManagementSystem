using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZooManagementSystem.Models.Entities;

namespace ZooManagementSystem.Data.Configurations
{
    public class AlimentoConfiguration : IEntityTypeConfiguration<Alimento>
    {
        public void Configure(EntityTypeBuilder<Alimento> entity)
        {
            entity.ToTable("Alimento");

            entity.HasKey(e => e.Id);

            // PROPIEDADES

            entity.Property(e => e.Nombre)
                  .IsRequired()
                  .HasMaxLength(100)
                  .IsUnicode(false);

            entity.Property(e => e.Precio)
                  .HasColumnType("decimal(10,2)")
                  .IsRequired();

            entity.Property(e => e.Stock)
                  .IsRequired();

            entity.Property(e => e.StockMinimo)
                  .IsRequired();

            entity.Property(e => e.Url)
                  .HasMaxLength(255);

            entity.Property(e => e.Calorias);

            // RELACIONES

            // Proveedor -> Alimentos (1:N)
            entity.HasOne(e => e.Proveedor)
                  .WithMany(p => p.Alimentos)
                  .HasForeignKey(e => e.ProveedorId)
                  .OnDelete(DeleteBehavior.SetNull)
                  .HasConstraintName("FK_Alimento_Proveedor");

            // Alimento -> Dosis (1:N)
            entity.HasMany(e => e.Dosis)
                  .WithOne(d => d.Alimento)
                  .HasForeignKey(d => d.AlimentoId)
                  .OnDelete(DeleteBehavior.Cascade)
                  .HasConstraintName("FK_Alimento_Dosis");

            // INDICES

            entity.HasIndex(e => e.Nombre).IsUnique();
            entity.HasIndex(e => e.ProveedorId);
        }
    }
}
