using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZooManagementSystem.Models.Entities;

namespace ZooManagementSystem.Data.Configurations
{
    public class AlimentacionConfiguration : IEntityTypeConfiguration<Alimentacion>
    {
        public void Configure(EntityTypeBuilder<Alimentacion> entity)
        {
            entity.ToTable("Alimentacion");

            entity.HasKey(e => e.Id);

            // PROPIEDADES
            entity.Property(e => e.Nombre)
                  .IsRequired()
                  .HasMaxLength(50)
                  .IsUnicode(false);

            // RELACIONES

            // Alimentacion -> Animal (1:N)
            entity.HasMany(e => e.Animales)
                  .WithOne(a => a.Alimentacion)
                  .HasForeignKey(a => a.AlimentacionId)
                  .OnDelete(DeleteBehavior.SetNull)
                  .HasConstraintName("FK_Alimentacion_Animal");

            // INDICES

            entity.HasIndex(e => e.Nombre)
                  .IsUnique();
        }
    }
}
