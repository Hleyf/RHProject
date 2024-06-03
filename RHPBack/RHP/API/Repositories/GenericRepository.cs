using Microsoft.EntityFrameworkCore;
using RHP.Data;
using RHP.Entities.Interfaces;
using System.Linq.Expressions;

namespace RHP.API.Repositories
{

    public class GenericRepository<T> : IBaseRepository<T> where T : class, IBaseEntity
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
            return _context.Set<T>().Where(entity => ids.Contains((entity as IBaseEntity)!.Id)).ToArray();
        }

        public async Task<T[]> GetByIdWithIncludesAsync(int[] ids)
        {
            return await _context.Set<T>().Where(entity => ids.Contains((entity as IBaseEntity)!.Id)).ToArrayAsync();
        }

        public virtual void Remove(int id)
        {
            var entity = _context.Find<T>(id);
            if (entity is null)
            {
                throw new Exception($"Entity: {typeof(T).Name} with id: {id} not found");
            }

            _context.Remove(entity);
            var changes = _context.SaveChanges();
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
