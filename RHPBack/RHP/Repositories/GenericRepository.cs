using Microsoft.EntityFrameworkCore;
using RHP.Data;
using RHP.Interfaces;
using System.Linq.Expressions;

namespace RHP.Repositories
{

    public class GenericRepository<T> : IGenericRepository<T> where T : class 
    {
        private readonly ApplicationDbContext _context;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(T sender)
        {
            if (sender != null)
            {
                _context.Add(sender);
                _context.SaveChanges();
            }
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public T GetById(int id)
        {
            try
            {
                var entity = _context.Find<T>(id);
                return entity == null ? throw new Exception("Entity not found") : entity;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not find entity", ex);
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _context.FindAsync<T>(id);
                return entity == null ? throw new Exception("Entity not found") : entity;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not find entity", ex);
            }
        }

        public T[] GetByIdWithIncludes(int[] ids)
        {
            return _context.Set<T>().Where(entity => ids.Contains((entity as IBase)!.Id)).ToArray();
        }

        public async Task<T[]> GetByIdWithIncludesAsync(int[] ids)
        {
            return await _context.Set<T>().Where(entity => ids.Contains((entity as IBase)!.Id)).ToArrayAsync();
        }

        public bool Remove(int id)
        {
            var entity = _context.Find<T>(id);
            if (entity == null)
            {
                return false;
            }

            _context.Remove(entity);
            var changes = _context.SaveChanges();

            return changes > 0;
        }

        public T Select(Expression<Func<T, bool>> predicate)
        {
        var entity = _context.Set<T>().FirstOrDefault(predicate);
        return entity == null ? throw new Exception("Entity not found") : entity;
        }

        public async Task<T> SelectAsync(Expression<Func<T, bool>> predicate)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(predicate);
            return entity == null ? throw new Exception("Entity not found") : entity;
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

    }
}
