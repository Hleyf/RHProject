using System.Linq.Expressions;

namespace RHP.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        Task<List<T>> GetAllAsync();
        T GetById(int id);
        T GetByIdWithIncludes(int id);
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdWithIncludesAsync(int id);
        bool Remove(int id);
        void Add(T sender);
        void Update(T sender);

        public T Select(Expression<Func<T, bool>> predicate);
        public Task<T> SelectAsync(Expression<Func<T, bool>> predicate);
    }
}
