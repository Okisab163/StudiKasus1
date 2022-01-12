using System;

namespace EnrollmentService.Dtos
{
    public class StudentDto
    {
        public int StudentID { get; set; }
        public string Name { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}
