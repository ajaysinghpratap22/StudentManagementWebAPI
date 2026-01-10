using System.ComponentModel.DataAnnotations;

namespace StudentManagementWebAPI.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Maximum 100 characters allowed.")]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [Range(6, 120, ErrorMessage = "Age must be greater than 5.")]
        public int Age { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }= DateTime.UtcNow;
    }
}
