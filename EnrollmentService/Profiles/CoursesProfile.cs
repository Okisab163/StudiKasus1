using AutoMapper;
using EnrollmentService.Dtos;
using EnrollmentService.Models;

namespace EnrollmentService.Profiles
{
    public class CoursesProfile : Profile
    {
        public CoursesProfile()
        {
            CreateMap<Course, CourseDto>().ForMember(dest => dest.TotalHours,
                                                       opt => opt.MapFrom(
                                                       src => $"{src.Credits * 1.5}"));

            CreateMap<CourseForCreateDto, Course>();

            CreateMap<CourseForCreateDto, CourseDto>().ForMember(dest => dest.TotalHours,
                                                       opt => opt.MapFrom(
                                                       src => $"{src.Credits * 1.5}"));
        }
    }
}
