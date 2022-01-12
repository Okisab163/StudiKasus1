using AutoMapper;

namespace EnrollmentService.Profiles
{
    public class CoursesProfile : Profile
    {
        public CoursesProfile()
        {
            CreateMap<Models.Course, Dtos.CourseDto>().ForMember(dest => dest.TotalHours,
                                                       opt => opt.MapFrom(
                                                       src => $"{src.Credits * 1.5}"));

            CreateMap<Dtos.CourseForCreateDto, Models.Course>();
        }
    }
}
