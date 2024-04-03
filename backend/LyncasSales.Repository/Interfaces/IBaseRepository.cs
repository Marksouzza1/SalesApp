
using System.Linq.Expressions;

namespace LyncasSales.Repository.Interfaces
{
    public interface  IBaseRepository <T>
    {
        IQueryable<T> GetAll();
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task<T> GetById(int id);
        Task<bool> SaveChangesAsync();
    }
}
