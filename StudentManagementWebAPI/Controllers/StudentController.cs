using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementWebAPI.Models;

namespace StudentManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        public readonly ILogger<StudentController> _logger;
        public readonly StudentManagementDBContext _studentManagementDBContext;
        public StudentController(ILogger<StudentController> logger,StudentManagementDBContext studentManagementDBContext)
        {
            _logger = logger;
            _studentManagementDBContext = studentManagementDBContext;
        }
        [HttpGet("GetAllStudents")]

        public ActionResult<IEnumerable<Student>> GetAllStudents()
        {
            try
            {
                var students = _studentManagementDBContext.Students.ToList();
                return Ok(students);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all students.");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("GetStudentById/{id}")]
        public ActionResult<Student> GetStudentById(int id)
        {
            try
            {
                var student = _studentManagementDBContext.Students.Where(x=>x.Id==id);
                if (student == null)
                {
                    return NotFound();
                }
                return Ok(student);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching the student with ID {id}.");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpDelete("DeleteStudentByID/{id}")]
        public ActionResult<Student> DeleteStudentById(int id)
        {
            try
            {
                var student = _studentManagementDBContext.Students.Find(id);
                if (student == null)
                {
                    return NotFound();
                }
                _studentManagementDBContext.Students.Remove(student);
                _studentManagementDBContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the student with ID {id}.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
