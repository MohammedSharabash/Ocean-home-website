using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ocean_Home.Interfaces;
using Ocean_Home.Models.data;

namespace WebApplication1.Services
{
    public class GenericService<T> : IGeneric<T> where T: class
    {
        private readonly AppDbContext _context;
        public GenericService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(T obj)
        {
            await _context.Set<T>().AddAsync(obj);
            return await Save();
        }

        public async Task<bool> Delete(T obj)
        {
             _context.Set<T>().Remove(obj);
             return await Save();
        }

        public IEnumerable<T> Get(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            return  _context.Set<T>().Where(expression);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public async Task<bool> IsExist(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().AnyAsync(expression);
        }

        public async Task<bool> Update(T obj)
        {
            _context.Entry<T>(obj).State = EntityState.Modified;
            return await Save();
        }

        private async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
