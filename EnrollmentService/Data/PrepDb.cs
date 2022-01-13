using EnrollmentService.Models;
using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EnrollmentService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool isProd)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<ApplicationDbContext>(), isProd);
            }
        }

        private static void SeedData(ApplicationDbContext context, bool isProd)
        {
            if (isProd)
            {
                Console.WriteLine("--> Menjalankan Migrasi");
                try
                {
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Gagal melakukan migrasi {ex.Message}");
                }
            }

            if (!context.Courses.Any())
            {
                Console.WriteLine("--> Seeding data....");
                context.Courses.AddRange(
                    new Course() { Title = "Dotnet Core", Credits = 3},
                    new Course() { Title = "SQL Server Express", Credits = 4},
                    new Course() { Title = "Kubernetes", Credits = 6}
                );
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> Already have data...");
            }

            if (!context.Students.Any())
            {
                Console.WriteLine("--> Seeding data....");
                context.Students.AddRange(
                    new Student() { FirstName = "Oki", LastName = "Sabeni", EnrollmentDate = DateTime.Now},
                    new Student() { FirstName = "Ines", LastName = "Indah", EnrollmentDate = DateTime.Now },
                    new Student() { FirstName = "Alviery", LastName = "Julian", EnrollmentDate = DateTime.Now }
                );
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> Already have data...");
            }

            if (!context.Enrollments.Any())
            {
                Console.WriteLine("--> Seeding data....");
                context.Enrollments.AddRange(
                    new Enrollment() { CourseID = 1, StudentID = 1, Grade = Grade.A },
                    new Enrollment() { CourseID = 2, StudentID = 2, Grade = Grade.B },
                    new Enrollment() { CourseID = 3, StudentID = 3, Grade = Grade.C }
                );
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> Already have data...");
            }
        }
    }
}
