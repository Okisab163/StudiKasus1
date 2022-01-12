using AuthServer.Dtos;
using AuthServer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Data
{
    public class PrepDb
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
        }
    }
}
