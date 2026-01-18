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
                _logger.LogInformation("GetAllStudents methods started");
                var students = _studentManagementDBContext.Students
                    .Select(s => new StudentDTO
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Email = s.Email,
                        Age = s.Age,
                        CreatedDate= s.CreatedDate


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
                _logger.LogInformation("GetStudentById methods started");
                if (id <= 0)
                {
                    _logger.LogWarning("Invalid student ID provided: {Id}", id);
                    return BadRequest("Invalid student ID.");
                }
                var student = _studentManagementDBContext.Students.Where(x => x.Id == id).FirstOrDefault();
                if (student == null)
                {
                    _logger.LogWarning("Student with ID {Id} not found.", id);
                    return NotFound($"The Student with id {id} not found");
                }
                var studentDto = new StudentDTO
                {
                    Id = student.Id,
                    Name = student.Name,
                    Email = student.Email,
                    Age = student.Age,
                    CreatedDate = student.CreatedDate

                };
                _logger.LogInformation("Student with ID {Id} fetched successfully.", id);
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
                _logger.LogInformation("DeleteStudentById methods started");
                if (id <= 0)
                {
                    _logger.LogWarning("Invalid student ID provided: {Id}", id);
                    return BadRequest("Invalid student ID.");
                }
                var student = _studentManagementDBContext.Students.Where(x => x.Id == id).FirstOrDefault();
                if (student == null)
                {
                    _logger.LogWarning("Student with ID {Id} not found.", id);
                    return NotFound($"The Student with id {id} not found");
                }
                var studentDto = new StudentDTO
                {
                    Id = student.Id,
                    Name = student.Name,
                    Email = student.Email,
                    Age = student.Age,
                    CreatedDate = student.CreatedDate
                };
                _studentManagementDBContext.Students.Remove(student);
                _studentManagementDBContext.SaveChanges();
                _logger.LogInformation("Student with ID {Id} deleted successfully.", id);
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
                _logger.LogInformation("GetStudentByName methods started");
                if (string.IsNullOrWhiteSpace(name))
                {
                    _logger.LogWarning("Invalid student name provided: {Name}", name);
                    return BadRequest("Invalid student name.");
                }
                var student = _studentManagementDBContext.Students.Where(x => x.Name == name).FirstOrDefault();
                if (student == null)
                {
                    _logger.LogWarning("Student with Name {Name} not found.", name);
                    return NotFound($"The Student with name {name} not found");
                }
                var studentDto = new StudentDTO
                {
                    Id = student.Id,
                    Name = student.Name,
                    Email = student.Email,
                    Age = student.Age,
                    CreatedDate = student.CreatedDate23
                };
                _logger.LogInformation("Student with Name {Name} fetched successfully.", name);
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
                _logger.LogInformation("CreateStudent methods started");
                if (studentDTO == null)
                {
                    _logger.LogWarning("Student data is null");
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
                _logger.LogInformation("Student created successfully with ID {Id}", student.Id);
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
                _logger.LogInformation("UpdateStudent methods started");
                if (studentDTO == null || studentDTO.Id <= 0)
                {
                    _logger.LogWarning("Invalid student data provided for update");
                    return BadRequest("Valid student data is required");
                }
                var existingStudent = _studentManagementDBContext.Students.Where(x => x.Id == studentDTO.Id).FirstOrDefault();
                if (existingStudent == null)
                {
                    _logger.LogWarning("Student with ID {Id} not found for update.", studentDTO.Id);
                    return NotFound($"The Student with id {studentDTO.Id} not found");
                }
                existingStudent.Name = studentDTO.Name;
                existingStudent.Email = studentDTO.Email;
                existingStudent.Age = studentDTO.Age;
                _studentManagementDBContext.SaveChanges();
                _logger.LogInformation("Student with ID {Id} updated successfully.", studentDTO.Id);
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
