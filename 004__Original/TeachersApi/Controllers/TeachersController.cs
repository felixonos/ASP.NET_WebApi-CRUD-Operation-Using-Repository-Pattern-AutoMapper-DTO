using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeachersApi.AppDbContext;
using TeachersApi.ContractInterface;
using TeachersApi.DTOs;
using TeachersApi.Entities;

namespace TeachersApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly TeacherDbcontext _teacherDbcontext;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IMapper _mapper;

        public TeachersController(TeacherDbcontext teacherDbcontext, ITeacherRepository teacherRepository, IMapper mapper)
        {
            _teacherDbcontext = teacherDbcontext;
            _teacherRepository = teacherRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TeacherInputDto teacherInputDto)
        {
            //var newTeacher = new Teacher()
            //{
            //    Age = teacherInputDto.Age,
            //    Email = teacherInputDto.Email,
            //    FirstName = teacherInputDto.FirstName,
            //    LastName = teacherInputDto.LastName
            //};

            var newTeacher = _mapper.Map<Teacher>(teacherInputDto);

            var teacher = await _teacherRepository.CreateTeacher(newTeacher);

            return Ok(teacher);
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
            var doesTeacherExist = await _teacherRepository.IsTeacherExisting(teacherId);
            if (doesTeacherExist == false)
            {
                return NotFound();
            }

            var teacher = await _teacherRepository.GetTeacherById(teacherId);

            //var newTeacherOutputDto = new TeacherOutputDto()
            //{
            //    LastName = teacher.LastName,
            //    FirstName = teacher.FirstName,
            //    Email = teacher.Email
            //};

            var newTeacherOutputDto = _mapper.Map<TeacherOutputDto>(teacher);

            return Ok(newTeacherOutputDto);
        }

        [HttpPut("{teacherId}")]
        public async Task<IActionResult> UpdateTeacher(int teacherId, [FromBody] TeacherInputDto teacherInputDto)
        {
            var doesTeacherExist = await _teacherRepository.IsTeacherExisting(teacherId);
            if (doesTeacherExist == false)
            {
                return NotFound();
            }

            var newTeacher = new Teacher()
            {
                FirstName = teacherInputDto.FirstName,
                LastName = teacherInputDto.LastName,
                Age = teacherInputDto.Age,
                Email = teacherInputDto.Email
            };

            await _teacherRepository.UpdateTeacher(teacherId, newTeacher);
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