using Loto3000.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Loto3000Application.Repository
{
    public interface IRepository<T> where T : IEntity
    {
        Task<T?> GetByID(int id);
        IQueryable<T> GetAll();
        Task<T> Create(T entity);
        Task Update(T entity);
        Task<T> Delete(T entity);
        Task DeleteAll();

    }
}
