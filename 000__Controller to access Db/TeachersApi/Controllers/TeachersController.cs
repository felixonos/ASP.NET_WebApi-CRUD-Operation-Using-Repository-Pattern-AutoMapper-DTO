using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeachersApi.AppDbContext;
using TeachersApi.Entities;

namespace TeachersApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly TeacherDbcontext _teacherDbcontext;

        public TeachersController(TeacherDbcontext teacherDbcontext)
        {
            _teacherDbcontext = teacherDbcontext;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Teacher teacherInput)
        {
            await _teacherDbcontext.AddAsync(teacherInput);
            await _teacherDbcontext.SaveChangesAsync();

            return Ok(teacherInput);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTeachers()
        {
            List<Teacher> allTeachers = await _teacherDbcontext.Teachers.ToListAsync();

            return Ok(allTeachers);
        }

        [HttpGet("{teacherId}")]
        public async Task<IActionResult> GetTeacherById(int teacherId)
        {
            //bool doesTeacherExist = await _teacherDbcontext.Teachers.AnyAsync(t => t.Id == teacherId);

            //if (!doesTeacherExist)
            //{
            //    return NotFound();
            //}

            //var teacher = await _teacherDbcontext.Teachers.FindAsync(teacherId);
            //return Ok(teacher);

            var teacher = await _teacherDbcontext.Teachers.FirstOrDefaultAsync(t => t.Id == teacherId);
            if (teacher == null)
            {
                return NotFound();
            }

            return Ok(teacher);
        }

        [HttpPut("{teacherId}")]
        public async Task<IActionResult> UpdateTeacher(int teacherId, [FromBody] Teacher teacherInput)
        {
            var teacher = await _teacherDbcontext.Teachers.FirstOrDefaultAsync(t => t.Id == teacherId);
            if (teacher == null)
            {
                return NotFound();
            }

            teacher.FirstName = teacherInput.FirstName;
            teacher.LastName = teacherInput.LastName;
            teacher.Age = teacherInput.Age;
            teacher.Email = teacherInput.Email;

            await _teacherDbcontext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{teacherId}")]
        public async Task<IActionResult> DeleteTeacher(int teacherId)
        {
            var teacher = await _teacherDbcontext.Teachers.FirstOrDefaultAsync(t => t.Id == teacherId);
            if (teacher == null)
            {
                return NotFound();
            }

            _teacherDbcontext.Remove(teacher);
            await _teacherDbcontext.SaveChangesAsync();
            return Ok();
        }
    }
}