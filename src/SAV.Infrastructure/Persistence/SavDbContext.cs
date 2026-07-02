namespace SAV.Infrastructure.Persistence;

public class SavDbContext : DbContext
{
    //DbContextOptions<SavDbContext> options es la conexión a la base de datos
    public SavDbContext(DbContextOptions<SavDbContext> options) : base(options) { }

    // Catálogo
    public DbSet<Marca> Marcas => Set<Marca>();
    public DbSet<Modelo> Modelos => Set<Modelo>();
    public DbSet<Caracteristica> Caracteristicas => Set<Caracteristica>();
    public DbSet<ModeloSerie> ModelosSerie => Set<ModeloSerie>();
    public DbSet<ModeloExtra> ModelosExtra => Set<ModeloExtra>();

    
    
    // Inventario
    public DbSet<Automovil> Automoviles => Set<Automovil>();

    
    
    
    // Red de Distribución y Personal
    public DbSet<Concesionario> Concesionarios => Set<Concesionario>();
    public DbSet<ServicioOficial> ServiciosOficiales => Set<ServicioOficial>();
    public DbSet<Vendedor> Vendedores => Set<Vendedor>();
    public DbSet<Cliente> Clientes => Set<Cliente>();

    
    
    // Ventas
    public DbSet<Venta> Ventas => Set<Venta>();
    public DbSet<VentaExtra> VentasExtra => Set<VentaExtra>();

    
    
    // Pedidos a Fábrica
    public DbSet<PedidoFabrica> PedidosFabrica => Set<PedidoFabrica>();
    public DbSet<ArticuloPedido> ArticulosPedido => Set<ArticuloPedido>();

    
    
    // Seguridad
    public DbSet<Usuario> Usuarios => Set<Usuario>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SavDbContext).Assembly);
    }
}