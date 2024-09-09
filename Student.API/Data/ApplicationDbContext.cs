using Microsoft.EntityFrameworkCore;
using Student.API.Models.Entities;

namespace Student.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Students> Students { get; set; }
    }
}


