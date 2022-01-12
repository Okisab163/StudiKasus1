using System.ComponentModel.DataAnnotations;

namespace EnrollmentService.Dtos
{
    public class CourseForCreateDto
    {
        [Required(ErrorMessage = "Kolom Title Tidak Boleh Kosong")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Kolom Credit Tidak Boleh Kosong")]
        public int Credits { get; set; }
    }
}
