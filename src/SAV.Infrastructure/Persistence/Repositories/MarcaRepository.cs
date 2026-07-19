namespace SAV.Infrastructure.Persistence.Repositories
{
    public class MarcaRepository : BaseRepository<Marca>, IMarcaRepository
    {
        public MarcaRepository(SavDbContext context) : base(context) { } 
        public async Task<Marca?> ObtenerMarcaPorNombre(string nombre)
        {
            return await _context.Marcas.FirstOrDefaultAsync(m => m.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
        }
    }
}
