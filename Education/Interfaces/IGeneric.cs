using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Ocean_Home.Interfaces
{
    public interface IGeneric <T>
    {
        Task<bool> Add(T obj);
        IEnumerable<T> Get(Expression<Func<T, bool>> expression);
        IEnumerable<T> GetAll();
        Task<bool> IsExist(Expression<Func<T, bool>> expression);
        Task<bool> Update(T obj);
        Task<bool> Delete(T obj);
    }
}
