using ForMemory.Domain.Interfaces.Repositories.Family;
using Formemory.Repository;
using Formemory.Repository.Family;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
            services.AddControllers()
                .AddNewtonsoftJson();

            services.AddDbContextPool<MyDbContext>(options =>
            {
                options.UseMySql("Server=127.0.0.1;database=blog;uid=root;pwd=123456;Character Set=utf8mb4",
                    optionsBuilder => optionsBuilder.MigrationsAssembly("ForMemory.Server"));
            }, 64);

            services.AddEntityFrameworkMySql();

          

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
