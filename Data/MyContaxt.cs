using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class MyContaxt : DbContext
    {
        public MyContaxt(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Friend> Friends { get; set; }
    }
}
