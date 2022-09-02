using System.ComponentModel.DataAnnotations;

namespace TeachersApi.DTOs
{
    public class TeacherInputDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        [EmailAddress(ErrorMessage = "Email is not valid")]
        public string Email { get; set; }
    }
}