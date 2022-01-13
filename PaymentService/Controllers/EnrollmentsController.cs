using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentService.Data;
using PaymentService.Dtos;
using PaymentService.Models;
using System;
using System.Collections.Generic;

namespace PaymentService.Controllers
{
    [Route("api/PaymentService/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IPaymentRepo _repository;
        private readonly IMapper _mapper;
        public EnrollmentsController(IPaymentRepo repository,
        IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<EnrollmentDto>> GetEnrollments()
        {
            try
            {
                Console.WriteLine("-->Ambil Enrollments dari PaymentService");
                var enrollmentItems = _repository.GetAllEnrollments();
                return Ok(_mapper.Map<IEnumerable<EnrollmentDto>>(enrollmentItems));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult CreateEnrollment(EnrollmentForCreateDto enrollmentForCreateDto)
        {
            try
            {
                Console.WriteLine("--> Inbound POST command services");

                var enrollmentModel = _mapper.Map<Enrollment>(enrollmentForCreateDto);
                _repository.CreateEnrollment(enrollmentModel);
                _repository.SaveChanges();

                var enrollmentReadDto = _mapper.Map<EnrollmentDto>(enrollmentModel);

                return Ok(enrollmentReadDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
