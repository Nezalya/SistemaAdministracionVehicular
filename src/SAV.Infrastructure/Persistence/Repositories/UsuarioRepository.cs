namespace SAV.Infrastructure.Persistence.Repositories
{
    public class UsuarioRepository:BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(SavDbContext context) : base(context)
        {
        }

        public async Task<Usuario?> ObtenerPorUsername(string username)
        {
            return await _entities.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<Usuario?> ObtenerUsuarioPorEmail(string email)
        {
            return await _entities.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
