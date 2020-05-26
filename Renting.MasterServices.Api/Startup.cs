using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using log4net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Renting.MasterServices.Api.Auth;
using Renting.MasterServices.Api.Filters;
using Renting.MasterServices.Api.Helpers;
using Renting.MasterServices.Core;
using Renting.MasterServices.Core.Interfaces;
using Renting.MasterServices.Core.Interfaces.Client;
using Renting.MasterServices.Core.Interfaces.Provider;
using Renting.MasterServices.Core.Services;
using Renting.MasterServices.Core.Services.Client;
using Renting.MasterServices.Core.Services.Provider;
using Renting.MasterServices.Domain;
using Renting.MasterServices.Domain.IRepository.Client;
using Renting.MasterServices.Domain.IRepository.Provider;
using Renting.MasterServices.Domain.Repository.Client;
using Renting.MasterServices.Domain.Repository.Provider;
using Renting.TokenGenerator.Domain;
using Renting.TokenGenerator.Domain.IRepository;
using Renting.TokenGenerator.Domain.Repository;
using Renting.TokenGenerator.Services.IService;
using Renting.TokenGenerator.Services.Service;
using StackExchange.Redis;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using static Renting.MasterServices.Infraestructure.Enums;

[assembly: CLSCompliant(false)]
[assembly: System.Runtime.InteropServices.ComVisible(false)]
namespace Renting.MasterServices.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        private static IContainer ApplicationContainer { get; set; }
        public IConfiguration Configuration { get; }
        public IConfiguration JsonConfig { get; set; }
        public IConfigurationRoot ConfigurationRoot { get; set; }

        private readonly string AllowSpecificOrigins = "_allowSpecificOrigins";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConfigurationRoot = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            JsonConfig = ConfigurationRoot;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public virtual void ConfigureAuth(IServiceCollection services)
        {
            services.AddB2CAuth(JsonConfig);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var allowSpecificOrigins = JsonConfig.GetSection("AllowSpecificOrigins:domains").Get<IEnumerable<string>>();

            services.AddCors(options =>
            {
                options.AddPolicy(AllowSpecificOrigins, builder =>
                {
                    builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                    .WithExposedHeaders()
                    .WithOrigins(allowSpecificOrigins.ToArray());
                });
            });

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(CustomExceptionFilterAttribute));
                options.Filters.Add(typeof(ProcessTimeAttribute));
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            ConfigureAuth(services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Renting Master Services",
                    Description = "Servicio Rest de Master OnPremise"
                });
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme { In = "header", Description = "Please enter JWT with Bearer into field", Name = "Authorization", Type = "apiKey" });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {{
                        "Bearer", Enumerable.Empty<string>()
                    }
                });

                c.CustomSchemaIds(x => x.FullName);

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = "Renting.MasterServices.Api.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            CreateDependencyInjection(services);

            return new AutofacServiceProvider(ApplicationContainer);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddLog4Net();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(AllowSpecificOrigins);
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                await next();
            });

            app.UseAuthentication();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(Configuration.GetSection("swaggerUrl").Value, "Master API V1");
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void CreateDependencyInjection(IServiceCollection services)
        {
            // create a Autofac container builder
            ContainerBuilder builder = new ContainerBuilder();
            // read service collection to Autofac
            builder.Populate(services);
            var mapperConfiguration = new MapperConfiguration(configurationExpresion =>
            {
                configurationExpresion.AddProfile(new MappingProfile());
            });

            //Log4Net
            var log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            builder.Register(c => log).As<ILog>();

            //Renting.TokenGenerator
            var kvc = new RentingKeyVaultCredentials
            {
                AppId = JsonConfig.GetSection("kvc:AppId").Value,
                AppSecret = JsonConfig.GetSection("kvc:AppSecret").Value,
                KeyVaultUri = JsonConfig.GetSection("kvc:KeyVaultUri").Value
            };

            builder.RegisterType<TokenCredentialsFetchRepository>().As<ITokenCredentialsFetchRepository>()
                .WithParameter("keyVaultCredential", kvc);
            builder.RegisterType<JwtFetchRepository>().As<IJwtFetchRepository>();
            builder.RegisterType<JwtTokenFetcher>().As<IJwtTokenFetcher>();

            //AutoMapper
            builder.Register(componentContext => mapperConfiguration.CreateMapper()).As<IMapper>();

            //DataContext
            ConfigDataContext(builder);

            //Repository
            ConfigRepository(builder);

            //Services
            ConfigService(builder, JsonConfig);
        }

        private void ConfigDataContext(ContainerBuilder builder)
        {
            builder.RegisterType<GestionFlotaContext>().Keyed<IQueryableUnitOfWork>(DataBaseConnection.GestionFlota)
                .WithParameter("connectionString", Configuration.GetConnectionString("StringConnectionGestionFlota"));

            builder.RegisterType<SurentingContext>().Keyed<IQueryableUnitOfWork>(DataBaseConnection.Surenting)
               .WithParameter("connectionString", Configuration.GetConnectionString("StringConnectionSurenting"));

            builder.RegisterType<SurentingTransContext>().Keyed<IQueryableUnitOfWork>(DataBaseConnection.SurentingTrans)
              .WithParameter("connectionString", Configuration.GetConnectionString("StringConnectionSurentingTrans"));

            builder.RegisterType<FlotaContext>().Keyed<IQueryableUnitOfWork>(DataBaseConnection.Flota)
              .WithParameter("connectionString", Configuration.GetConnectionString("StringConnectionFlota"));

            builder.RegisterType<WebProdContext>().Keyed<IQueryableUnitOfWork>(DataBaseConnection.WebProd)
              .WithParameter("connectionString", Configuration.GetConnectionString("StringConnectionWebProd"));

            builder.RegisterType<GestionFlotaTransContext>().Keyed<IQueryableUnitOfWork>(DataBaseConnection.GestionFlotaTrans)
               .WithParameter("connectionString", Configuration.GetConnectionString("StringConnectionGestionFlotaTrans"));
        }

        private static void ConfigRepository(ContainerBuilder builder)
        {
            builder.RegisterType<PlateRepository>().As<IPlateRepository>();
            builder.RegisterType<VehicleTypeRepository>().As<IVehicleTypeRepository>();
            builder.RegisterType<ParameterRepository>().As<IParameterRepository>();
            builder.RegisterType<EconomicGroupRepository>().As<IEconomicGroupRepository>();
            builder.RegisterType<ClientUserRepository>().As<IClientUserRepository>();
            builder.RegisterType<StateRepository>().As<IStateRepository>();
            builder.RegisterType<AttributeRepository>().As<IAttributeRepository>();
            builder.RegisterType<UserProviderRepository>().As<IUserProviderRepository>();
            builder.RegisterType<ProviderRepository>().As<IProviderRepository>();
            builder.RegisterType<AnnouncementRepository>().As<IAnnouncementRepository>();
        }

        private static void ConfigService(ContainerBuilder builder, IConfiguration JsonConfig)
        {
            builder.RegisterType<PlateService>().As<IPlateService>();
            builder.RegisterType<VehicleTypeService>().As<IVehicleTypeService>();
            builder.RegisterType<ParameterService>().As<IParameterService>();
            builder.RegisterType<EconomicGroupService>().As<IEconomicGroupService>();
            builder.RegisterType<ClientUserService>().As<IClientUserService>();
            builder.RegisterType<StateService>().As<IStateService>();
            builder.RegisterType<AttributeService>().As<IAttributeService>();
            builder.RegisterType<UserProviderService>().As<IUserProviderService>();
            builder.RegisterType<ProviderService>().As<IProviderService>();
            builder.RegisterType<AnnouncementService>().As<IAnnouncementService>();
            builder.RegisterType<BlobStorageService>().As<IBlobStorageService>();
            builder.RegisterType<FileProcesingService>().As<IFileProcesingService>();

            //Helper
            builder.RegisterType<TokenHelper>().As<ITokenHelper>();
            builder.RegisterType<ConfigProvider>().As<IConfigProvider>();

            //Redis
            var connect = ConnectionMultiplexer.Connect(JsonConfig.GetSection("Redis:StringConnection").Value);
            var hoursToExpire = 2;
            int.TryParse(JsonConfig.GetSection("Redis:HoursToExpire").Value, out hoursToExpire);
            builder.Register(m => connect).As<IConnectionMultiplexer>().SingleInstance();
            builder.RegisterType<CacheService>().As<ICacheService>().SingleInstance().WithParameter("hoursToExpire", hoursToExpire);
            ApplicationContainer = builder.Build();
        }
    }
}
