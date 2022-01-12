using EnrollmentService.Models;
using System.Collections.Generic;

namespace EnrollmentService.Data
{
    public interface IEnrollmentRepo
    {
        bool SaveChanges();
        IEnumerable<Student> GetAllStudents();
        Student GetStudentById(int id);
        void UpdateStudent(int id, Student obj);
        void DeleteStudent(int id);
        void CreateStudent(Student student);

        IEnumerable<Course> GetAllCourses();
        Course GetCourseById(int id);
        void UpdateCourse(int id, Course obj);
        void DeleteCourse(int id);
        void CreateCourse(Course course);

        IEnumerable<Enrollment> GetAllEnrollments();
        Enrollment GetEnrollmentById(int id);
        void UpdateEnrollment(int id, Enrollment obj);
        void DeleteEnrollment(int id);
        void CreateEnrollment(Enrollment enrollment);
    }
}
