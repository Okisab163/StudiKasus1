using AutoMapper;
using EnrollmentService.Dtos;
using EnrollmentService.Models;

namespace EnrollmentService.Profiles
{
    public class EnrollmentProfile : Profile
    {
        public EnrollmentProfile()
        {
            CreateMap<Enrollment, EnrollmentDto>();

            CreateMap<EnrollmentForCreateDto, Enrollment>();

            CreateMap<EnrollmentForCreateDto, EnrollmentDto>();
        }
    }
}
