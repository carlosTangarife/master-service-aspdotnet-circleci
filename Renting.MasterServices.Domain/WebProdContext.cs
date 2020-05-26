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
using Renting.MasterServices.Domain.Entities.Provider;
using System.Diagnostics.CodeAnalysis;

namespace Renting.MasterServices.Domain
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Renting.MasterServices.Domain.BaseDbContext" />
    [ExcludeFromCodeCoverage]
    public class WebProdContext : BaseDbContext
    {
        private readonly string connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="FlotaContext"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public WebProdContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// <para>
        /// Override this method to configure the database (and other options) to be used for this context.
        /// This method is called for each instance of the context that is created.
        /// The base implementation does nothing.
        /// </para>
        /// <para>
        /// In situations where an instance of <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions" /> may or may not have been passed
        /// to the constructor, you can use <see cref="P:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.IsConfigured" /> to determine if
        /// the options have already been set, and skip some or all of the logic in
        /// <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" />.
        /// </para>
        /// </summary>
        /// <param name="optionsBuilder">A builder used to create or modify options for this context. Databases (and other extensions)
        /// typically define extension methods on this object that allow you to configure the context.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        /// <summary>
        /// Override this method to further configure the model that was discovered by convention from the entity types
        /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
        /// and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
        /// define extension methods on this object that allow you to configure aspects of the model that are specific
        /// to a given database.</param>
        /// <remarks>
        /// If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
        /// then this method will not be run.
        /// </remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                return;
            }

            modelBuilder.Entity<UserProvider>().ToTable("tblUserProveedores").HasKey(x => x.Id);
            base.OnModelCreating(modelBuilder);
        }
    }
}
