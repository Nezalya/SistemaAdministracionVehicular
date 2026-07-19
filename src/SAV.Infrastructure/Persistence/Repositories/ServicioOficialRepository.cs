namespace SAV.Infrastructure.Persistence.Repositories
{
    public class ServicioOficialRepository:BaseRepository<ServicioOficial>, IServicioOficialRepository
    {
        public ServicioOficialRepository(SavDbContext context) : base(context)
        {
        }

        public async Task<ServicioOficial?> ObtenerServicioOficialPorNif(string nif)
        {
            return await _entities.FirstOrDefaultAsync(s => s.Nif == nif);
        }
    }
}
