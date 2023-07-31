using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using pro.Models;

namespace pro.Data
{
    public class Context : IdentityDbContext<User>
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
