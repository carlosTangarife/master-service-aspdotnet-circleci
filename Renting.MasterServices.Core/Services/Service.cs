using AutoMapper;
using Renting.MasterServices.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using log4net;
using Renting.MasterServices.Domain.IRepository;

[assembly: CLSCompliant(false)]
[assembly: System.Runtime.InteropServices.ComVisible(false)]
namespace Renting.MasterServices.Core.Services
{
    /// <summary>
    /// Service
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TEntityDto">The type of the entity dto.</typeparam>
    /// <seealso cref="Renting.MasterServices.Core.Interfaces.IService{TEntity, TEntityDto}" />
    public class Service<TEntity, TEntityDto> : IService<TEntity, TEntityDto>
        where TEntity : Domain.Entities.EntityBase
        where TEntityDto : Dtos.EntityBase
    {
        private readonly IERepository<TEntity> repository;
        private readonly IMapper serviceMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="Service{TEntity, TEntityDto}"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="serviceMapper">The service mapper.</param>
        public Service(IERepository<TEntity> repository, ILog logger, IMapper serviceMapper)
        {
            this.repository = repository;
            this.serviceMapper = serviceMapper;
        }

        /// <summary>
        /// Adds the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual async Task AddAsync(TEntityDto entity)
        {
            await repository.AddAsync(serviceMapper.Map<TEntity>(entity)).ConfigureAwait(false);
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual async Task DeleteAsync(TEntityDto entity)
        {
            await repository.DeleteAsync(serviceMapper.Map<TEntity>(entity)).ConfigureAwait(false);
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="entityId">The entity identifier.</param>
        public virtual async Task DeleteAsync(dynamic entityId)
        {
            TEntity entity = repository.FindById(entityId);
            await repository.DeleteAsync(entity).ConfigureAwait(false);
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual async Task UpdateAsync(TEntityDto entity)
        {
            await repository.UpdateAsync(serviceMapper.Map<TEntity>(entity)).ConfigureAwait(false);
        }

        /// <summary>
        /// Finds the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual TEntityDto FindById(dynamic id)
        {
            TEntity book = repository.FindById(id);
            return book != null ? serviceMapper.Map<TEntityDto>(book) : null;
        }

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Add(TEntityDto entity)
        {
            repository.Add(serviceMapper.Map<TEntity>(entity));
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Update(TEntityDto entity)
        {
            repository.Update(serviceMapper.Map<TEntity>(entity));
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Delete(TEntityDto entity)
        {
            repository.Delete(serviceMapper.Map<TEntity>(entity));
        }

        /// <summary>
        /// Deletes the specified entity identifier.
        /// </summary>
        /// <param name="entityId">The entity identifier.</param>
        public virtual void Delete(dynamic entityId)
        {
            TEntity entity = repository.FindById(entityId);
            repository.Delete(entity);
        }


        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<TEntityDto> GetAll()
        {
            return GetAll(null, null, string.Empty);
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public virtual IEnumerable<TEntityDto> GetAll(Expression<Func<TEntity, bool>> filter)
        {
            return GetAll(filter, null, string.Empty);
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <returns></returns>
        public virtual IEnumerable<TEntityDto> GetAll(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        {
            return GetAll(filter, orderBy, string.Empty);
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        public virtual IEnumerable<TEntityDto> GetAll(
            Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            string includeProperties)
        {
            return from item in repository.GetAll(filter, orderBy, includeProperties)
                   select serviceMapper.Map<TEntityDto>(item);
        }
    }
}
