using AutoMapper;
using PaymentService.Dtos;
using PaymentService.Models;

namespace PaymentService.Profiles
{
    public class EnrollmentProfile : Profile
    {
        public EnrollmentProfile() 
        {
            CreateMap<EnrollmentForCreateDto, Enrollment>();
            CreateMap<Enrollment, EnrollmentDto>();
            CreateMap<EnrollmentForCreateDto, EnrollmentDto>();
        }
    }
}
