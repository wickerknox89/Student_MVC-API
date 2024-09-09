using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student.API.Data;
using Student.API.Models.Entities;

namespace Student.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public StudentAPIController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Students>>> GetAllStudents()
        {
            var lstStudents = await _dbContext.Students.ToListAsync();
            return Ok(lstStudents);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Students>> GetStudentById(int id)
        {
            var student = _dbContext.Students.Find(id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        [HttpPost]
        public async Task<ActionResult<Students>> AddStudent(Students model)
        {
            await _dbContext.Students.AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return Ok(model);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Students>> UpdateStudent(int id, Students std)
        {
            if (id != std.Id)
            {
                return BadRequest();
            }
            _dbContext.Entry(std).State= EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return Ok(std);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Students>> DeleteStudent(int id)
        {
            var student = await _dbContext.Students.FindAsync(id);
            if(student == null)
            {
                return NotFound();
            }
            
            _dbContext.Students.Remove(student);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
