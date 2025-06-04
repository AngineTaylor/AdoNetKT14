using Microsoft.AspNetCore.Mvc;
using EducationSystem.Data;
using EducationSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EducationSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            return await _context.Courses.Include(c => c.Teacher).Include(c => c.Students).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = await _context.Courses
                .Include(c => c.Teacher)
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null) return NotFound();

            return course;
        }

        [HttpPost]
        public async Task<ActionResult<Course>> CreateCourse(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, course);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, Course course)
        {
            if (id != course.Id) return BadRequest();

            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                if (!_context.Courses.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return NotFound();

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Дополнительно: добавь метод для записи студента на курс
        [HttpPost("{courseId}/students/{studentId}")]
        public async Task<IActionResult> AddStudentToCourse(int courseId, int studentId)
        {
            var course = await _context.Courses
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.Id == courseId);

            var student = await _context.Students.FindAsync(studentId);

            if (course == null || student == null) return NotFound();

            course.Students.Add(student);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}