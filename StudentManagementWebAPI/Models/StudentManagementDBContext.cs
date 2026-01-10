using Microsoft.EntityFrameworkCore;

namespace StudentManagementWebAPI.Models
{
    public class StudentManagementDBContext:DbContext
    {
        public StudentManagementDBContext(DbContextOptions<StudentManagementDBContext> options) : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
    }
}
