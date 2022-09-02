using TeachersApi.DTOs;
using TeachersApi.Entities;

namespace TeachersApi.ContractInterface
{
    public interface ITeacherRepository
    {
        public Task<Teacher> CreateTeacher(Teacher teacherInput);

        public Task<List<Teacher>> GetAllTeacher();

        public Task<Teacher> GetTeacherById(int id);

        public Task UpdateTeacher(int id, Teacher teacherInput);

        public Task DeleteTeacher(int id);

        public Task<bool> IsTeacherExisting(int id);
    }
}