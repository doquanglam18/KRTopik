using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Infrastructure.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        public IQueryable<T> GetAll();

        public Task<T> GetByIdAsync(int id);

        public Task Add(T entity);
        public Task Update(T entity);
        public Task Delete(T entity);

    }
}
