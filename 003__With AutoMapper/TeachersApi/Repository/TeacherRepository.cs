using Microsoft.EntityFrameworkCore;
using TeachersApi.AppDbContext;
using TeachersApi.ContractInterface;
using TeachersApi.DTOs;
using TeachersApi.Entities;

namespace TeachersApi.Repository
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly TeacherDbcontext _teacherDbcontext;

        public TeacherRepository(TeacherDbcontext teacherDbcontext)
        {
            _teacherDbcontext = teacherDbcontext;
        }

        public async Task<Teacher> CreateTeacher(Teacher teacherInput)
        {
            await _teacherDbcontext.Teachers.AddAsync(teacherInput);
            await _teacherDbcontext.SaveChangesAsync();

            return teacherInput;
        }

        public async Task<List<Teacher>> GetAllTeacher()
        {
            var allTeachers = await _teacherDbcontext.Teachers.ToListAsync();
            return allTeachers;
        }

        public async Task<Teacher> GetTeacherById(int id)
        {
            var teacher = await _teacherDbcontext.Teachers.FirstOrDefaultAsync(t => t.Id == id);
            return teacher;
        }

        public async Task<bool> IsTeacherExisting(int id)
        {
            var doesTeacherExist = await _teacherDbcontext.Teachers.AnyAsync(t => t.Id == id);
            return doesTeacherExist;
        }

        public async Task UpdateTeacher(int id, Teacher teacherInput)
        {
            var teacher = await _teacherDbcontext.Teachers.FirstOrDefaultAsync(t => t.Id == id);
            teacher.Email = teacherInput.Email;
            teacher.FirstName = teacherInput.FirstName;
            teacher.LastName = teacherInput.LastName;
            teacher.Age = teacherInput.Age;

            await _teacherDbcontext.SaveChangesAsync();
        }

        public async Task DeleteTeacher(int id)
        {
            var teacherToDelete = await _teacherDbcontext.Teachers.FirstOrDefaultAsync(t => t.Id == id);
            _teacherDbcontext.Remove(teacherToDelete);
            await _teacherDbcontext.SaveChangesAsync();
        }
    }
}