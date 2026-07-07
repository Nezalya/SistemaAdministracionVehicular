namespace SAV.Infrastructure.Persistence.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        protected readonly SavDbContext _context;
        protected readonly DbSet<T> _entities;

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
            return entity;
        }

        public async Task<T> Update(T entity)
        {
            _entities.Update(entity);
            return entity;
    
        } 

        public async Task<T> Delete(int id)
        {
            var entity = await GetById(id);
            if (entity is not null)
                _entities.Remove(entity);
            return entity;

        }
        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();  
        }
    }
}
