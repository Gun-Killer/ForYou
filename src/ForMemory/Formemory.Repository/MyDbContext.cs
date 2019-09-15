using Microsoft.EntityFrameworkCore;

namespace Formemory.Repository
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options)
            : base(options)
        {

        }
    }
}