namespace SAV.Infrastructure.Persistence.Repositories
{
    public class VentaRepository:BaseRepository<Venta>, IVentaRepository
    {
        public VentaRepository(SavDbContext context) : base(context)
        {
        }
    }
}
