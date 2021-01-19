using ForMemory.Repository.Configs.Family;
using Microsoft.EntityFrameworkCore;

namespace ForMemory.Repository
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {

        }


        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FamilyConfig).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}