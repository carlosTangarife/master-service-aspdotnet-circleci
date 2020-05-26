using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Renting.MasterServices.Domain.Entities;

namespace Renting.MasterServices.Domain.IRepository
{
    public interface IERepository<TEntity>: IDisposable where TEntity : EntityBase
    {
        void Add(TEntity entity);
        Task AddAsync(TEntity entity);
        void Delete(dynamic id);
        void Delete(TEntity entity);
        Task DeleteAsync(dynamic id);
        Task DeleteAsync(TEntity entity);
        TEntity FindById(dynamic id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter);
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, string includeProperties);
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>,
              IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        void Update(TEntity entity);
        Task UpdateAsync(TEntity entity);
        void ExecuteWithStoreProcedure(string query);
        void ExecuteWithStoreProcedure(string query, params object[] parameters);
        Task ExecuteWithStoreProcedureAsync(string query);
        Task ExecuteWithStoreProcedureAsync(string query, params object[] parameters);
        Task<IList<TEntity>> ExecWithStoreProcedureAsync(string query);
        Task<IList<TEntity>> ExecWithStoreProcedureAsync(string query, params object[] parameters);
        IList<TEntity> ExecWithStoreProcedure(string query);
        IList<TEntity> ExecWithStoreProcedure(string query, params object[] parameters);
    }
}
