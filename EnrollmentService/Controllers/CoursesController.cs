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
    public class CoursesController : ControllerBase
    {
        private readonly IEnrollmentRepo _repository;
        private IMapper _mapper;

        public CoursesController(IEnrollmentRepo repository,
        IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CourseDto>> GetCourses()
        {
            try
            {
                Console.WriteLine("--> Getting Courses .....");
                var courseItem = _repository.GetAllCourses();
                return Ok(_mapper.Map<IEnumerable<CourseDto>>(courseItem));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}", Name = "GetCourseById")]
        public ActionResult<CourseDto> GetCourseById(int id)
        {
            try
            {
                Console.WriteLine($"--> Getting Course With ID: {id} .....");
                var courseItem = _repository.GetCourseById(id);
                if (courseItem != null)
                {
                    return Ok(_mapper.Map<StudentDto>(courseItem));
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CourseDto>> UpdateCourse(int id, CourseForCreateDto courseForCreateDto)
        {
            try
            {
                Console.WriteLine($"--> Update Course With ID: {id} .....");
                var courseModel = _mapper.Map<Course>(courseForCreateDto);
                _repository.UpdateCourse(id, courseModel);
                _repository.SaveChanges();

                var courseReadDto = _mapper.Map<CourseDto>(courseModel);

                if (courseReadDto != null)
                {
                    return Ok(courseReadDto);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            try
            {
                Console.WriteLine($"--> Delete Course With ID: {id} .....");
                _repository.DeleteCourse(id);
                _repository.SaveChanges();

                return Ok($"Data Course {id} Berhasil Didelete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CourseDto>> CreateCourse(CourseForCreateDto courseForCreateDto)
        {
            try
            {
                Console.WriteLine("--> Creating Course .....");
                var courseModel = _mapper.Map<Course>(courseForCreateDto);
                _repository.CreateCourse(courseModel);
                _repository.SaveChanges();

                var courseReadDto = _mapper.Map<CourseDto>(courseModel);

                if (courseReadDto != null)
                {
                    return Ok(courseReadDto);
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
