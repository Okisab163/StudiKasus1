﻿namespace EnrollmentService.Dtos
{
    public enum Grade
    {
        A, B, C, D, F
    }

    public class EnrollmentDto
    {
        public int EnrollmentID { get; set; }

        public int CourseID { get; set; }

        public int StudentID { get; set; }
        public Grade Grade { get; set; }
    }
}
