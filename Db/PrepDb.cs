using ColoursAPi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace ColoursAPi.Db
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder applicationBuilder) {
            using (var serviceScope=applicationBuilder.ApplicationServices.CreateScope()) {
                SeedData(serviceScope.ServiceProvider.GetService<ColorDbContext>());
            }
        }

        public static void SeedData(ColorDbContext context){
            System.Console.WriteLine("Applying Migration .....");
            context.Database.Migrate();

            if (!context.Colors.Any())
            {
                System.Console.WriteLine("Adding Data- Seeding....");
                context.Colors.AddRange(new Color() { ColourName = "Red" },
                                        new Color() { ColourName = "Orange" },
                                          new Color() { ColourName = "Yellow" },
                                        new Color() { ColourName = "Green" },
                                        new Color() { ColourName = "Blue" });

                context.SaveChanges();
            }
            else {
                System.Console.WriteLine("Already Have Data .....So Not Seeding....");
            }

        }
    }
}
