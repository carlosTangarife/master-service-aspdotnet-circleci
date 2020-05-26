/*
* Ceiba Software House SAS
* Copyright (C) 2019
* www.ceiba.com.co
* 
* Ceiba is a full-service custom software development 
* house dedicated to delivering maximum possible quality 
* in the minimum possible time. Utilizing agile methodology,
* we create beautiful, usable digital products engineered to perform.
* 
* Calle 8 b 65 191 oficina 409 Centro Empresarial Puertoseco.
* Medellín, Antioquia, Colombia.
* Conmutador: (574) 444 5 111, 
* or write us to the email contacts@ceiba.com.co
*/

using Microsoft.EntityFrameworkCore;
using Renting.MasterServices.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Renting.MasterServices.Domain
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    /// <seealso cref="Renting.MasterServices.Domain.IQueryableUnitOfWork" />
    public class BaseDbContext : DbContext, IQueryableUnitOfWork
    {
        public void Commit()
        {
            try
            {
                SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
            }
        }

        /// <summary>
        /// Commits the asynchronous.
        /// </summary>
        public async Task CommitAsync()
        {
            try
            {
                await SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException ex)
            {

                await ex.Entries.Single().ReloadAsync().ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Executes the with store procedure.
        /// </summary>
        /// <param name="query">The query.</param>
        public void ExecuteWithStoreProcedure(string query)
        {
            Database.ExecuteSqlCommand(query);
        }

        /// <summary>
        /// Executes the with store procedure.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        public void ExecuteWithStoreProcedure(string query, params object[] parameters)
        {
            Database.ExecuteSqlCommand(query, parameters);
        }

        /// <summary>
        /// Executes the with store procedure asynchronous.
        /// </summary>
        /// <param name="query">The query.</param>
        public async Task ExecuteWithStoreProcedureAsync(string query)
        {
            await Database.ExecuteSqlCommandAsync(query).ConfigureAwait(false);
        }

        /// <summary>
        /// Executes the with store procedure asynchronous.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        public async Task ExecuteWithStoreProcedureAsync(string query, params object[] parameters)
        {
            await Database.ExecuteSqlCommandAsync(query, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// Executes the with store procedure asynchronous.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public async Task<IList<TEntity>> ExecWithStoreProcedureAsync<TEntity>(string query) where TEntity : EntityBase
        {
            return await Query<TEntity>().FromSql(query).ToListAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Executes the with store procedure.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public IList<TEntity> ExecWithStoreProcedure<TEntity>(string query) where TEntity : EntityBase
        {
            return Query<TEntity>().FromSql(query).ToList();
        }

        /// <summary>
        /// Executes the with store procedure.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public IList<TEntity> ExecWithStoreProcedure<TEntity>(string query, params object[] parameters) where TEntity : EntityBase
        {
            return Query<TEntity>().FromSql(query, parameters).ToList();
        }

        public async Task<IList<TEntity>> ExecWithStoreProcedureAsync<TEntity>(string query, params object[] parameters) where TEntity : EntityBase
        {
            return await Query<TEntity>().FromSql(query, parameters).ToListAsync().ConfigureAwait(false);
        }

        public DbContext GetContext()
        {
            return this;
        }

        public DbSet<TEntity> GetSet<TEntity>() where TEntity : EntityBase
        {
            return Set<TEntity>();
        }
    }
}
