﻿namespace EnrollmentService.Dtos
{
    public class EnrollmentForCreateDto
    {
        public int CourseID { get; set; }

        public int StudentID { get; set; }
        public Grade Grade { get; set; }
    }
}
