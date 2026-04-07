using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZooManagementSystem.Models.Entities;

namespace ZooManagementSystem.Data.Configurations
{
    public class DosisConfiguration : IEntityTypeConfiguration<Dosis>
    {
        public void Configure(EntityTypeBuilder<Dosis> entity)
        {
            entity.ToTable("Dosis");

            entity.HasKey(e => e.DosisId);

            // PROPIEDADES

            entity.Property(e => e.Cantidad)
                  .HasColumnType("decimal(10,2)")
                  .IsRequired();

            // RELACIONES

            // Animal -> Dosis (N:1)
            entity.HasOne(e => e.Animal)
                  .WithMany(a => a.Dosis)
                  .HasForeignKey(e => e.AnimalId)
                  .HasConstraintName("FK_Dosis_Animal");

            // Alimento -> Dosis (N:1)
            entity.HasOne(e => e.Alimento)
                  .WithMany(a => a.Dosis)
                  .HasForeignKey(e => e.AlimentoId)
                  .HasConstraintName("FK_Dosis_Alimento");
        }
    }
}
