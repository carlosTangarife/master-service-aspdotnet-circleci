using Renting.MasterServices.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Renting.MasterServices.Core.Interfaces
{
    public interface IService<TEntity, TEntityDto> where TEntityDto : EntityBase
    {
        void Add(TEntityDto entity);
        Task AddAsync(TEntityDto entity);
        void Delete(dynamic entityId);
        void Delete(TEntityDto entity);
        Task DeleteAsync(dynamic entityId);
        Task DeleteAsync(TEntityDto entity);
        TEntityDto FindById(dynamic id);
        void Update(TEntityDto entity);
        Task UpdateAsync(TEntityDto entity);
        IEnumerable<TEntityDto> GetAll();
        IEnumerable<TEntityDto> GetAll(Expression<Func<TEntity, bool>> filter);
        IEnumerable<TEntityDto> GetAll(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);
        IEnumerable<TEntityDto> GetAll(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            string includeProperties);
    }
}
