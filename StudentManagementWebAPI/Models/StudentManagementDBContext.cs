using Microsoft.EntityFrameworkCore;
using StudentManagementWebAPI.Models;

namespace StudentManagementWebAPI.Models
{
    public class StudentManagementDBContext:DbContext
    {
        public StudentManagementDBContext(DbContextOptions<StudentManagementDBContext> options) : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(new List<Student>()
            {
                new Student() { Id = 1, Name = "John Doe", Email ="jodnd@gmail.com", Age=21, CreatedDate=DateTime.Now },
                new Student() { Id = 2, Name = "Jane Smith", Email ="Janes@gmail.com", Age=22, CreatedDate=DateTime.Now }
            });

        }
    }
}
