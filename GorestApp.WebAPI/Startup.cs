using AutoMapper;
using GorestApp.Business.Abstract.UserManager;
using GorestApp.Business.Abstract.Users;
using GorestApp.Business.Concrete.UserManager;
using GorestApp.Business.Configuration;
using GorestApp.Core.DependecyResolvers;
using GorestApp.Core.DependecyResolvers.Interfaces;
using GorestApp.Core.Utilities.Extensions;
using GorestApp.Core.Utilities.Helpers;
using GorestApp.DataAccess.Abstract;
using GorestApp.DataAccess.Abstract.Repositories;
using GorestApp.DataAccess.Concrete;
using GorestApp.DataAccess.Concrete.EntityFramework.Repositories;
using GorestApp.Entities.Concrete.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GorestApp.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
            services.AddSingleton(mapper);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddDbContext<GorestAppContext>(options => options.UseSqlServer(AppConfigurationHelper.GetConnectionString()));
            services.AddScoped(typeof(IEntityRepository<>), typeof(EfEntityRepositoryBase<>));
            services.AddTransient<IUserReadService, UserReadManager>();
            services.AddTransient<IUserWriteService, UserWriteManager>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddDependencyResolvers(new ICoreModule[]
            {
                new CoreModule()
            });

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GorestApp.WebAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GorestApp.WebAPI v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
