using Microsoft.EntityFrameworkCore;
using TeachersApi.Entities;

namespace TeachersApi.AppDbContext
{
    public class TeacherDbcontext : DbContext
    {
        public TeacherDbcontext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Teacher> Teachers { get; set; }
    }
}