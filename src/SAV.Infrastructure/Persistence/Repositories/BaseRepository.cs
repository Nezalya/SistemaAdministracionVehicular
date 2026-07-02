using SAV.Core.Interfaces.Repositories;
namespace SAV.Infrastructure.Persistence.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly SavDbContext _context;
        private readonly DbSet<T> _entities;

        public BaseRepository(SavDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public async Task<T> GetById(int id)
        {
            return await _entities.FindAsync(id);
        } 
            
        public async Task<IEnumerable<T>> GetAll()
        {
            return await _entities.ToListAsync();
        } 

        public async Task<T> Add(T entity)
        {
            _entities.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Update(T entity)
        {
            _entities.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        } 

        public async Task<T> Delete(int id)
        {
            var entity = await GetById(id);
            _entities.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;

        }
    }
}
