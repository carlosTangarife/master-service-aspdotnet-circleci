using Microsoft.EntityFrameworkCore;
using Renting.MasterServices.Domain.Entities;
using Renting.MasterServices.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

[assembly: CLSCompliant(false)]
[assembly: System.Runtime.InteropServices.ComVisible(false)]
namespace Renting.MasterServices.Domain.Repository
{
    public class ERepository<TEntity> : IERepository<TEntity> where TEntity : EntityBase
    {
        private readonly IQueryableUnitOfWork unitOfWork;

        public ERepository(IQueryableUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return GetAll(null, null, string.Empty);
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter)
        {
            return GetAll(filter, null, string.Empty);
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        {
            return GetAll(filter, orderBy, string.Empty);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>,
              IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            return await GetAll(filter, orderBy, includeProperties).ToListAsync().ConfigureAwait(false);
        }

        public IQueryable<TEntity> GetAll(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            string includeProperties)
        {
            IQueryable<TEntity> query = unitOfWork.GetSet<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return (orderBy != null) ? orderBy(query) : query;
        }

        public TEntity FindById(dynamic id)
        {
            return unitOfWork.GetSet<TEntity>().Find(id);
        }

        public async Task AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("The Entity Object can not be null");
            }

            var item = unitOfWork.GetSet<TEntity>();
            item.Add(entity);
            await unitOfWork.CommitAsync().ConfigureAwait(false);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("The Entity Object can not be null");
            }

            var item = unitOfWork.GetSet<TEntity>();
            item.Update(entity);
            await unitOfWork.CommitAsync().ConfigureAwait(false);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("The Entity Object can not be null");
            }

            unitOfWork.GetSet<TEntity>().Remove(entity);
            await unitOfWork.CommitAsync().ConfigureAwait(false);
        }

        public async Task DeleteAsync(dynamic id)
        {
            var item = FindById(id);
            await DeleteAsync(item).ConfigureAwait(false);
        }

        public void Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("The Entity Object can not be null");
            }

            var item = unitOfWork.GetSet<TEntity>();
            item.Add(entity);
            unitOfWork.Commit();
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("The Entity Object can not be null");
            }

            var item = unitOfWork.GetSet<TEntity>();
            item.Update(entity);
            unitOfWork.Commit();
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("The Entity Object can not be null");
            }

            unitOfWork.GetSet<TEntity>().Remove(entity);
            unitOfWork.Commit();
        }

        public void Delete(dynamic id)
        {
            var item = FindById(id);
            Delete(item);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
        }

        public void ExecuteWithStoreProcedure(string query)
        {
            unitOfWork.ExecuteWithStoreProcedure(query);
        }

        public void ExecuteWithStoreProcedure(string query, params object[] parameters)
        {
            unitOfWork.ExecuteWithStoreProcedure(query, parameters);
        }

        public async Task ExecuteWithStoreProcedureAsync(string query)
        {
            await unitOfWork.ExecuteWithStoreProcedureAsync(query).ConfigureAwait(false);
        }

        public async Task ExecuteWithStoreProcedureAsync(string query, params object[] parameters)
        {
            await unitOfWork.ExecuteWithStoreProcedureAsync(query, parameters).ConfigureAwait(false);
        }

        public async Task<IList<TEntity>> ExecWithStoreProcedureAsync(string query)
        {
            return await unitOfWork.ExecWithStoreProcedureAsync<TEntity>(query).ConfigureAwait(false);
        }

        public async Task<IList<TEntity>> ExecWithStoreProcedureAsync(string query, params object[] parameters)
        {
            return await unitOfWork.ExecWithStoreProcedureAsync<TEntity>(query, parameters).ConfigureAwait(false);
        }

        public IList<TEntity> ExecWithStoreProcedure(string query)
        {
            return unitOfWork.ExecWithStoreProcedure<TEntity>(query);
        }

        public IList<TEntity> ExecWithStoreProcedure(string query, params object[] parameters)
        {
            return unitOfWork.ExecWithStoreProcedure<TEntity>(query, parameters);
        }
    }
}
