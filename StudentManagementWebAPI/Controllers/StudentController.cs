using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementWebAPI.Models;

namespace StudentManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public class StudentController : ControllerBase
    {
        public readonly ILogger<StudentController> _logger;
        public readonly StudentManagementDBContext _studentManagementDBContext;
        public StudentController(ILogger<StudentController> logger, StudentManagementDBContext studentManagementDBContext)
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<Student> GetStudentById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid student ID.");
                }
                var student = _studentManagementDBContext.Students.Where(x => x.Id == id).FirstOrDefault();
                if (student == null)
                {
                    return NotFound($"The Student with id {id} not found");
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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Student> DeleteStudentById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid student ID.");
                }
                var student = _studentManagementDBContext.Students.Where(x => x.Id == id).FirstOrDefault();
                if (student == null)
                {
                    return NotFound($"The Student with id {id} not found");
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
        [HttpGet("GetStudentByName/{name}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Student> GetStudentByName(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    return BadRequest("Invalid student name.");
                }
                var student = _studentManagementDBContext.Students.Where(x => x.Name == name).FirstOrDefault();
                if (student == null)
                {
                    return NotFound($"The Student with name {name} not found");
                }
                return Ok(student);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching the student with Name {name}.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
