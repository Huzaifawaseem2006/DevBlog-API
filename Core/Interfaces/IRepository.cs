using DevBlog.Core.Entities;

namespace DevBlog.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(Guid id, CancellationToken token);
        Task<IEnumerable<T>> GetAllAsync(CancellationToken token);
        Task AddAsync(T entity, CancellationToken token);
        Task UpdateAsync(T entity, CancellationToken token);
        Task DeleteAsync(T entity, CancellationToken token);
    }
}
