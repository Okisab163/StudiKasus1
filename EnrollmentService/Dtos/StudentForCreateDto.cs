using System;
using System.ComponentModel.DataAnnotations;

namespace EnrollmentService.Dtos
{
    public class StudentForCreateDto
    {
        [Required(ErrorMessage = "Kolom FirstName Tidak Boleh Kosong")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Kolom LastName Tidak Boleh Kosong")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Kolom EnrollmentDate Tidak Boleh Kosong")]
        public DateTime EnrollmentDate { get; set; }
    }
}
