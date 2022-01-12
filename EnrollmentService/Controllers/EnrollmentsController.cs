using AutoMapper;
using EnrollmentService.Data;
using EnrollmentService.Dtos;
using EnrollmentService.Models;
using EnrollmentService.SyncDataServices.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnrollmentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentRepo _repository;
        private IMapper _mapper;
        private readonly IEnrollmentDataClient _enrollmentDataClient;

        public EnrollmentsController(IEnrollmentRepo repository,
        IMapper mapper, IEnrollmentDataClient enrollmentDataClient)
        {
            _repository = repository;
            _mapper = mapper;
            _enrollmentDataClient = enrollmentDataClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<EnrollmentDto>> GetEnrolments()
        {
            try
            {
                Console.WriteLine("--> Getting Enrollments .....");
                var enrollmentItem = _repository.GetAllEnrollments();
                return Ok(_mapper.Map<IEnumerable<EnrollmentDto>>(enrollmentItem));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}", Name = "GetEnrollmentById")]
        public ActionResult<EnrollmentDto> GetEnrollmentById(int id)
        {
            try
            {
                Console.WriteLine($"--> Getting Enrollment With ID: {id} .....");
                var enrollmentItem = _repository.GetEnrollmentById(id);
                if (enrollmentItem != null)
                {
                    return Ok(_mapper.Map<EnrollmentDto>(enrollmentItem));
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EnrollmentDto>> UpdateEnrollment(int id, EnrollmentForCreateDto enrollmentForCreateDto)
        {
            try
            {
                Console.WriteLine($"--> Update Enrollment With ID: {id} .....");
                var enrollmenteModel = _mapper.Map<Enrollment>(enrollmentForCreateDto);
                _repository.UpdateEnrollment(id, enrollmenteModel);
                _repository.SaveChanges();

                var enrollmentReadDto = _mapper.Map<EnrollmentDto>(enrollmenteModel);

                if (enrollmentReadDto != null)
                {
                    return Ok(enrollmentReadDto);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnrollment(int id)
        {
            try
            {
                Console.WriteLine($"--> Delete Enrollment With ID: {id} .....");
                _repository.DeleteEnrollment(id);
                _repository.SaveChanges();

                return Ok($"Data Enrollment {id} Berhasil Didelete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<EnrollmentDto>> CreateEnrollment(EnrollmentForCreateDto enrollmentForCreateDto)
        {
            try
            {
                Console.WriteLine("--> Creating Enrollment .....");
                var enrollmentModel = _mapper.Map<Enrollment>(enrollmentForCreateDto);
                _repository.CreateEnrollment(enrollmentModel);
                _repository.SaveChanges();

                var enrollmentReadDto = _mapper.Map<EnrollmentDto>(enrollmentModel);

                if (enrollmentReadDto != null)
                {
                    //send sync
                    try
                    {
                        await _enrollmentDataClient.SendEnrollmentToPaymentService(enrollmentReadDto);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"--> Could Not Send Synchronously: {ex.Message}");
                    }
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
