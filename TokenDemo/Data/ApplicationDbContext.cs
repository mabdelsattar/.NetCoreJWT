using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TokenDemo.Data
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        //asp.net identity vs asp.net adi
        //db set

        //we will use identity db that contains users authentication and Ready data
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
                //for dependacy injection
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
