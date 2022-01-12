using EnrollmentService.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EnrollmentService.Data
{
    public class EnrollmentRepo : IEnrollmentRepo
    {
        private readonly ApplicationDbContext _context;
        public EnrollmentRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreateCourse(Course course)
        {
            if (course == null)
            {
                throw new ArgumentNullException(nameof(course));
            }
            _context.Courses.Add(course);
        }

        public void CreateEnrollment(Enrollment enrollment)
        {
            if (enrollment == null)
            {
                throw new ArgumentNullException(nameof(enrollment));
            }
            _context.Enrollments.Add(enrollment);
        }

        public void CreateStudent(Student student)
        {
            if (student == null)
            {
                throw new ArgumentNullException(nameof(student));
            }
            _context.Students.Add(student);
        }

        public void DeleteCourse(int id)
        {
            var result = _context.Courses.FirstOrDefault(p => p.CourseID == id);
            _context.Courses.Remove(result);
        }

        public void DeleteEnrollment(int id)
        {
            var result = _context.Enrollments.FirstOrDefault(p => p.EnrollmentID == id);
            _context.Enrollments.Remove(result);
        }

        public void DeleteStudent(int id)
        {
            var result = _context.Students.FirstOrDefault(p => p.StudentID == id);
            _context.Students.Remove(result);
        }

        public IEnumerable<Course> GetAllCourses()
        {
            return _context.Courses.ToList();
        }

        public IEnumerable<Enrollment> GetAllEnrollments()
        {
            return _context.Enrollments.ToList();
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return _context.Students.ToList();
        }

        public Course GetCourseById(int id)
        {
            return _context.Courses.FirstOrDefault(p => p.CourseID == id);
        }

        public Enrollment GetEnrollmentById(int id)
        {
            return _context.Enrollments.FirstOrDefault(p => p.EnrollmentID == id);
        }

        public Student GetStudentById(int id)
        {
            return _context.Students.FirstOrDefault(p => p.StudentID == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateCourse(int id, Course obj)
        {
            var result = _context.Courses.FirstOrDefault(p => p.CourseID == id);
            result.Title = obj.Title;
            result.Credits = obj.Credits;
            _context.SaveChanges();
        }

        public void UpdateEnrollment(int id, Enrollment obj)
        {
            var result = _context.Enrollments.FirstOrDefault(p => p.EnrollmentID == id);
            result.StudentID = obj.StudentID;
            result.CourseID = obj.CourseID;
            result.Grade = obj.Grade;
            _context.SaveChanges();
        }

        public void UpdateStudent(int id, Student obj)
        {
            var result = _context.Students.FirstOrDefault(p => p.StudentID == id);
            result.FirstName = obj.FirstName;
            result.LastName = obj.LastName;
            result.EnrollmentDate = obj.EnrollmentDate;
            _context.SaveChanges();
        }
    }
}
