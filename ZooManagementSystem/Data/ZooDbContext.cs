using Microsoft.EntityFrameworkCore;
using ZooManagementSystem.Models.Entities;

namespace ZooManagementSystem.Data
{
    public class ZooDbContext : DbContext
    {
        public ZooDbContext(){}

        public ZooDbContext(DbContextOptions<ZooDbContext> options) : base(options){}

        public DbSet<Alimentacion> Alimentaciones => Set<Alimentacion>();
        public DbSet<Alimento> Alimentos => Set<Alimento>();
        public DbSet<Animal> Animales => Set<Animal>();
        public DbSet<CategoriaLaboral> CategoriasLaborales => Set<CategoriaLaboral>();
        public DbSet<Dosis> Dosis => Set<Dosis>();
        public DbSet<Ecosistema> Ecosistemas => Set<Ecosistema>();
        public DbSet<Empleado> Empleados => Set<Empleado>();
        public DbSet<Jaula> Jaulas => Set<Jaula>();
        public DbSet<Proveedor> Proveedores => Set<Proveedor>();
        public DbSet<Zona> Zonas => Set<Zona>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ZooDbContext).Assembly);
        }

    }
}
