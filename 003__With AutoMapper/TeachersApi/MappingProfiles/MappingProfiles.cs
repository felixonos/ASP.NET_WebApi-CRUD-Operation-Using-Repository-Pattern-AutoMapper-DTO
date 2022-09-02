using AutoMapper;
using TeachersApi.DTOs;
using TeachersApi.Entities;

namespace TeachersApi.MappingProfiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //CreateMap<TeacherInputDto, Teacher>().ForMember(
            //    dest => dest.FirstName,
            //    opt => opt.MapFrom(opt => opt.FirstName + " " + opt.LastName));

            CreateMap<TeacherInputDto, Teacher>();
            CreateMap<Teacher, TeacherOutputDto>();

            //CreateMap<Teacher, TeacherInputDto>();
        }
    }
}