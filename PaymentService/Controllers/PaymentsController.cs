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
    [Route("api/PaymentService/{platformId}/[controller]")]
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
        public ActionResult<IEnumerable<PaymentDto>> GetPaymentsForEnrollment(int enrollmentid)
        {
            try
            {
                Console.WriteLine($"--> GetPaymentsForEnrollment: {enrollmentid}");
                if (!_repository.EnrollmentExist(enrollmentid))
                {
                    return NotFound();
                }
                var payments = _repository.GetPaymentsForEnrollment(enrollmentid);
                return Ok(_mapper.Map<IEnumerable<PaymentDto>>(payments));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{commandId}", Name = "GetCommandForPlatform")]
        public ActionResult<PaymentDto> GetPaymentsForEnrollment(int enrollmentid, int paymentId)
        {
            try
            {
                Console.WriteLine($"--> GetPaymentsForEnrollment: {enrollmentid} / {paymentId}");
                if (!_repository.EnrollmentExist(enrollmentid))
                {
                    return NotFound();
                }
                var payment = _repository.GetPayment(enrollmentid, paymentId);
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
        public ActionResult<PaymentDto> CreatePaymentForEnrollment(int enrollmentid, PaymentForCreateDto paymentForCreateDto)
        {
            Console.WriteLine($"--> CreatePaymentForEnrollment: {enrollmentid}");
            if (!_repository.EnrollmentExist(enrollmentid))
            {
                return NotFound();
            }

            var payment = _mapper.Map<Payment>(paymentForCreateDto);
            _repository.CreatePayment(enrollmentid, payment);
            _repository.SaveChanges();
            var paymentReadDto = _mapper.Map<PaymentDto>(payment);

            return CreatedAtRoute(nameof(GetPaymentsForEnrollment),
                new 
                { 
                    platformId = enrollmentid, 
                    paymentId = paymentReadDto.Id }, 
                    paymentReadDto
                );
        }
    }
}
