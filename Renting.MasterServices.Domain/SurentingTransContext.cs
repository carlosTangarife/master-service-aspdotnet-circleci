using Microsoft.EntityFrameworkCore;
using Renting.MasterServices.Domain.Entities;
using Renting.MasterServices.Domain.Entities.Client;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Renting.MasterServices.Domain
{
    public class SurentingTransContext : DbContext, IQueryableUnitOfWork
    {
        private readonly string connectionString;

        public SurentingTransContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                return;
            }

            modelBuilder.Query<Plate>();

            base.OnModelCreating(modelBuilder);
        }

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

        public void ExecuteWithStoreProcedure(string query)
        {
            Database.ExecuteSqlCommand(query);
        }

        public void ExecuteWithStoreProcedure(string query, params object[] parameters)
        {
            Database.ExecuteSqlCommand(query, parameters);
        }

        public async Task ExecuteWithStoreProcedureAsync(string query)
        {
            await Database.ExecuteSqlCommandAsync(query).ConfigureAwait(false);
        }

        public async Task ExecuteWithStoreProcedureAsync(string query, params object[] parameters)
        {
            await Database.ExecuteSqlCommandAsync(query, parameters).ConfigureAwait(false);
        }

        public async Task<IList<TEntity>> ExecWithStoreProcedureAsync<TEntity>(string query) where TEntity : EntityBase
        {
            return await Query<TEntity>().FromSql(query).ToListAsync().ConfigureAwait(false);
        }

        public IList<TEntity> ExecWithStoreProcedure<TEntity>(string query) where TEntity : EntityBase
        {
            return Query<TEntity>().FromSql(query).ToList();
        }

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

        public void Reload()
        {
            // Method intentionally left empty
        }
    }
}
