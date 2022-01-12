using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EnrollmentService.Models
{
    public class Student
    {
        [Key]
        [Required]
        public int StudentID { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        public DateTime EnrollmentDate { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
