using Microsoft.EntityFrameworkCore;

namespace ZooManagementSystem.Data
{
    public class ZooDbContext : DbContext
    {
        public ZooDbContext()
        {
        }

        public ZooDbContext(DbContextOptions<ZooDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var dbPath = Path.Combine(AppContext.BaseDirectory, "Data", "Zoo.mdf");

                optionsBuilder.UseSqlServer(
                    $@"Server=(LocalDB)\MSSQLLocalDB;
               AttachDbFilename={dbPath};
               Integrated Security=True;
               Connect Timeout=30;
               TrustServerCertificate=True;
               MultipleActiveResultSets=True;");
            }
            
        }

        //public DbSet<Alimento> Alimentos => Set<Alimento>();
        //public DbSet<Animal> Animales => Set<Animal>();
        //public DbSet<CategoriaLaboral> CategoriasLaborales => Set<CategoriaLaboral>();
        //public DbSet<Dosis> Dosis => Set<Dosis>();
        //public DbSet<Ecosistema> Ecosistemas => Set<Ecosistema>();
        //public DbSet<Empleado> Empleados => Set<Empleado>();
        //public DbSet<Jaula> Jaulas => Set<Jaula>();
        //public DbSet<Proveedor> Proveedores => Set<Proveedor>();
        //public DbSet<Zona> Zonas => Set<Zona>();

        }
}
