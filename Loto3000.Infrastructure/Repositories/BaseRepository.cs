

using Loto3000.Domain.Models;
using Loto3000.Infrastructure.EntityFrameWork;
using Loto3000Application.Repository;
using Microsoft.EntityFrameworkCore;

namespace Loto3000.Infrastructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly ApplicationDbContex dbContex;
        public BaseRepository(ApplicationDbContex dbContex)
        {
            this.dbContex = dbContex;
        }
        public async Task<T> Create(T entity)
        {
            await dbContex.AddAsync(entity);
            await dbContex.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Delete(T entity)
        {
            dbContex.Remove(entity);
            await dbContex.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAll()
        {
            var entity = await GetAll().ToListAsync();
            foreach(var item in entity)
            {
                dbContex.Remove(item);
            }
           await dbContex.SaveChangesAsync();
        }

        public IQueryable<T> GetAll()
        {
           return dbContex.Set<T>();
        }

        public async Task<T?> GetByID(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public async Task Update(T entity)
        {
            dbContex.Update(entity);
            await dbContex.SaveChangesAsync();
            
        }
    }
}
