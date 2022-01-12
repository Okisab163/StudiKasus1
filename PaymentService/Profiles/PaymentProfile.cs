using AutoMapper;
using PaymentService.Dtos;
using PaymentService.Models;

namespace PaymentService.Profiles
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            CreateMap<PaymentForCreateDto, Payment>();
            CreateMap<Payment, PaymentDto>();
        }
    }
}
