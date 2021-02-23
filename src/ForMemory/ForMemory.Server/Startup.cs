using System;
using ForMemory.Domain.Interfaces.Repositories.Family;
using ForMemory.Repository;
using ForMemory.Repository.Family;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ForMemory.Server
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
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddControllers(builder =>
                {
                    builder.AllowEmptyInputInBodyModelBinding = true;
                    foreach (var formatter in builder.InputFormatters)
                    {
                        if (formatter.GetType() == typeof(SystemTextJsonInputFormatter))
                            ((SystemTextJsonInputFormatter)formatter).SupportedMediaTypes.Add(
                                Microsoft.Net.Http.Headers.MediaTypeHeaderValue.Parse("text/plain"));
                    } 
                })
                .AddNewtonsoftJson();

            services.AddEntityFrameworkMySql();
            services.AddDbContextPool<MyDbContext>(options =>
            {
                options.UseMySql("Server=127.0.0.1;database=blog;uid=root;pwd=123456;Character Set=utf8mb4"
                    , new MySqlServerVersion(new Version(5, 7, 0)),
                    optionsBuilder => optionsBuilder.MigrationsAssembly("ForMemory.Server"));
            }, 64);



            services.AddScoped<IFamilyRepository, FamilyRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseMvc();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
