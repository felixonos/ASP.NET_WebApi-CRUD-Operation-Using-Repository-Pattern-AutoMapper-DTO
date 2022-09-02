using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeachersApi.AppDbContext;
using TeachersApi.ContractInterface;
using TeachersApi.Entities;

namespace TeachersApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly TeacherDbcontext _teacherDbcontext;
        private readonly ITeacherRepository _teacherRepository;

        public TeachersController(TeacherDbcontext teacherDbcontext, ITeacherRepository teacherRepository)
        {
            _teacherDbcontext = teacherDbcontext;
            _teacherRepository = teacherRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Teacher teacherInput)
        {
            var newTeacher = await _teacherRepository.CreateTeacher(teacherInput);

            return Ok(newTeacher);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTeachers()
        {
            var allTeachers = await _teacherRepository.GetAllTeacher();

            return Ok(allTeachers);
        }

        [HttpGet("{teacherId}")]
        public async Task<IActionResult> GetTeacherById(int teacherId)
        {
            var teacher = await _teacherRepository.GetTeacherById(teacherId);
            if (teacher == null)
            {
                return NotFound();
            }

            return Ok(teacher);
        }

        [HttpPut("{teacherId}")]
        public async Task<IActionResult> UpdateTeacher(int teacherId, [FromBody] Teacher teacherInput)
        {
            var doesTeacherExist = await _teacherRepository.IsTeacherExisting(teacherId);
            if (doesTeacherExist == false)
            {
                return NotFound();
            }

            await _teacherRepository.UpdateTeacher(teacherId, teacherInput);
            return Ok();
        }

        [HttpDelete("{teacherId}")]
        public async Task<IActionResult> DeleteTeacher(int teacherId)
        {
            var doesTeacherExist = await _teacherRepository.IsTeacherExisting(teacherId);
            if (doesTeacherExist == false)
            {
                return NotFound();
            }

            await _teacherRepository.DeleteTeacher(teacherId);

            return Ok();
        }
    }
}