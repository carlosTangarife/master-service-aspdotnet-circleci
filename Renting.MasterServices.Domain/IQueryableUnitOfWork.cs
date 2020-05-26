using Microsoft.EntityFrameworkCore;
using Renting.MasterServices.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Renting.MasterServices.Domain
{
    public interface IQueryableUnitOfWork : IDisposable
    {
        DbSet<TEntity> GetSet<TEntity>() where TEntity : EntityBase;
        void Commit();
        Task CommitAsync();
        DbContext GetContext();
        Task<IList<TEntity>> ExecWithStoreProcedureAsync<TEntity>(string query) where TEntity : EntityBase;
        Task<IList<TEntity>> ExecWithStoreProcedureAsync<TEntity>(string query, params object[] parameters) where TEntity : EntityBase;
        IList<TEntity> ExecWithStoreProcedure<TEntity>(string query) where TEntity : EntityBase;
        IList<TEntity> ExecWithStoreProcedure<TEntity>(string query, params object[] parameters) where TEntity : EntityBase;
        Task ExecuteWithStoreProcedureAsync(string query);
        Task ExecuteWithStoreProcedureAsync(string query, params object[] parameters);
        void ExecuteWithStoreProcedure(string query);
        void ExecuteWithStoreProcedure(string query, params object[] parameters);
    }
}
