namespace SAV.Infrastructure.Persistence.Repositories
{
    public class MarcaRepository : BaseRepository<Marca>, IMarcaRepository
    {
        public MarcaRepository(SavDbContext context) : base(context) { } 
        public async Task<Marca?> GetByNombre(string nombre)
        {
            return await _context.Marcas.FirstOrDefaultAsync(m => m.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));

            /*Si StringComparison.OrdinalIgnoreCase nos da problemas, usar: return await _context.Marcas .FirstOrDefaultAsync(m => m.Nombre == nombre);*/
        }
    }
}
