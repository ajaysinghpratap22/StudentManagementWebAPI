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

        public ActionResult<IEnumerable<StudentDTO>> GetAllStudents()
        {
            try
            {

                var students = _studentManagementDBContext.Students
                    .Select(s => new StudentDTO
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Email = s.Email,
                        Age = s.Age
                    })
                    .ToList();
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

        public ActionResult<StudentDTO> GetStudentById(int id)
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
                var studentDto = new StudentDTO
                {
                    Id = student.Id,
                    Name = student.Name,
                    Email = student.Email,
                    Age = student.Age
                };
                return Ok(studentDto);
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
        public ActionResult<StudentDTO> DeleteStudentById(int id)
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
                var studentDto = new StudentDTO
                {
                    Id = student.Id,
                    Name = student.Name,
                    Email = student.Email,
                    Age = student.Age
                };
                _studentManagementDBContext.Students.Remove(student);
                _studentManagementDBContext.SaveChanges();
                return Ok(studentDto);
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
        public ActionResult<StudentDTO> GetStudentByName(string name)
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
                var studentDto = new StudentDTO
                {
                    Id = student.Id,
                    Name = student.Name,
                    Email = student.Email,
                    Age = student.Age
                };
                return Ok(studentDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching the student with Name {name}.");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPost("CreateStudent")]
        public ActionResult<StudentDTO> CreateStudent([FromBody] StudentDTO studentDTO)
        {
            try
            {
                if (studentDTO == null)
                {
                    return BadRequest("Student data is required");
                }
                var student = new Student
                {
                    Name = studentDTO.Name,
                    Email = studentDTO.Email,
                    Age = studentDTO.Age,
                    CreatedDate = DateTime.UtcNow
                };
                _studentManagementDBContext.Students.Add(student);
                _studentManagementDBContext.SaveChanges();
                studentDTO.Id = student.Id;
                return Ok(studentDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while inserting student");
                return StatusCode(500, "Internal server error");
            }

        }
        [HttpPut("UpdateStudent")]
        public ActionResult<StudentDTO> UpdateStudent([FromBody] StudentDTO studentDTO)
        {
            try
            {
                if (studentDTO == null || studentDTO.Id <= 0)
                {
                    return BadRequest("Valid student data is required");
                }
                var existingStudent = _studentManagementDBContext.Students.Where(x => x.Id == studentDTO.Id).FirstOrDefault();
                if (existingStudent == null)
                {
                    return NotFound($"The Student with id {studentDTO.Id} not found");
                }
                existingStudent.Name = studentDTO.Name;
                existingStudent.Email = studentDTO.Email;
                existingStudent.Age = studentDTO.Age;
                _studentManagementDBContext.SaveChanges();
                return Ok(studentDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating student");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
