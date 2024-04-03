
using LyncasSales.Domain.Data;
using LyncasSales.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LyncasSales.Repository
{
    public class BaseRepository <T> :IBaseRepository<T> where T : class
    {
        private readonly AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking();
        }
        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public async Task Update(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Create(T entity)
        {
             _context.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete (T entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
         return await _context.SaveChangesAsync()>0;
        }

       
    }


}

