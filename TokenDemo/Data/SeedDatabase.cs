using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TokenDemo.Data
{
    public class SeedDatabase
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            context.Database.EnsureCreated();

            if (!context.Users.Any())
            {
                ApplicationUser applicationUser = new ApplicationUser()
                {
                    Email = "abdo@abdo.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "abdo"
                 
                };
                userManager.CreateAsync(applicationUser, "Windows.2000");

            }

        }
    }
}
