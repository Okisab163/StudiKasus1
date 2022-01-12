using AutoMapper;
using EnrollmentService.Data;
using EnrollmentService.Dtos;
using EnrollmentService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnrollmentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IEnrollmentRepo _repository;
        private IMapper _mapper;

        public StudentsController(IEnrollmentRepo repository,
        IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<StudentDto>> GetStudents()
        {
            try
            {
                Console.WriteLine("--> Getting Students .....");
                var studentItem = _repository.GetAllStudents();
                return Ok(_mapper.Map<IEnumerable<StudentDto>>(studentItem));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}", Name = "GetStudentById")]
        public ActionResult<StudentDto> GetStudentById(int id)
        {
            try
            {
                Console.WriteLine($"--> Getting Student With ID: {id} .....");
                var studentItem = _repository.GetStudentById(id);
                if (studentItem != null)
                {
                    return Ok(_mapper.Map<StudentDto>(studentItem));
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<StudentDto>> UpdateStudent(int id, StudentForCreateDto studentForCreateDto)
        {
            try
            {
                Console.WriteLine($"--> Update Student With ID: {id} .....");
                var studentModel = _mapper.Map<Student>(studentForCreateDto);
                _repository.UpdateStudent(id, studentModel);
                _repository.SaveChanges();

                var studentReadDto = _mapper.Map<StudentDto>(studentModel);

                if (studentReadDto != null)
                {
                    return Ok(studentReadDto);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                Console.WriteLine($"--> Delete Student With ID: {id} .....");
                _repository.DeleteStudent(id);
                _repository.SaveChanges();

                return Ok($"Data Student {id} Berhasil Didelete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<StudentDto>> CreateStudent(StudentForCreateDto studentForCreateDto)
        {
            try
            {
                Console.WriteLine("--> Creating Student .....");
                var studentModel = _mapper.Map<Student>(studentForCreateDto);
                _repository.CreateStudent(studentModel);
                _repository.SaveChanges();

                var studentReadDto = _mapper.Map<StudentDto>(studentModel);

                if (studentReadDto != null)
                {
                    return Ok(studentReadDto);
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
