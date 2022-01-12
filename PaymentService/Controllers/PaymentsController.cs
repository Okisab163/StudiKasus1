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
    [ApiController]
    [Route("api/PaymentService/{enrollmentId}/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentRepo _repository;
        private readonly IMapper _mapper;
        public PaymentsController(IPaymentRepo repository,
        IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PaymentDto>> GetPaymentsForEnrollment(int enrollmentId)
        {
            try
            {
                Console.WriteLine($"--> GetPaymentsForEnrollment: {enrollmentId}");
                if (!_repository.EnrollmentExist(enrollmentId))
                {
                    return NotFound();
                }
                var payments = _repository.GetPaymentsForEnrollment(enrollmentId);
                return Ok(_mapper.Map<IEnumerable<PaymentDto>>(payments));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{paymentId}", Name = "GetPaymentsForEnrollment")]
        public ActionResult<PaymentDto> GetPaymentsForEnrollment(int enrollmentId, int paymentId)
        {
            try
            {
                Console.WriteLine($"--> GetPaymentsForEnrollment: {enrollmentId} / {paymentId}");
                if (!_repository.EnrollmentExist(enrollmentId))
                {
                    return NotFound();
                }
                var payment = _repository.GetPayment(enrollmentId, paymentId);
                if (payment == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<PaymentDto>(payment));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<PaymentDto> CreatePaymentForEnrollment(int enrollmentId, PaymentForCreateDto paymentForCreateDto)
        {
            try
            {
                Console.WriteLine($"--> CreatePaymentForEnrollment: {enrollmentId}");
                if (!_repository.EnrollmentExist(enrollmentId))
                {
                    return NotFound();
                }

                var payment = _mapper.Map<Payment>(paymentForCreateDto);
                _repository.CreatePayment(enrollmentId, payment);
                _repository.SaveChanges();
                var paymentReadDto = _mapper.Map<PaymentDto>(payment);

                return CreatedAtRoute(nameof(GetPaymentsForEnrollment),
                    new
                    {
                        enrollmentId = enrollmentId,
                        paymentId = paymentReadDto.PaymentID
                    },
                        paymentReadDto
                    );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
