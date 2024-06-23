using Ocean_Home.Models.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Ocean_Home.Models.data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<OurAffiliate> OurAffiliates { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<JobsDepartment> JobsDepartments { get; set; }
        public DbSet<ContactUS> ContactUs { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<AboutUS> AboutUs { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ProjectsDepartment> ProjectsDepartments { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<ProjectImage> ProjectImages { get; set; }
    }
}
