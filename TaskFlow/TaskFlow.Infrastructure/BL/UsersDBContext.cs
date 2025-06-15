using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using TaskFlow.Domain.Entities.Users;

namespace TaskFlow.Infrastructure.BL
{
    public class UsersDBContext : DbContext
    {
        public UsersDBContext()
        {
        }
        public UsersDBContext(DbContextOptions<UsersDBContext> options)
            :base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .ToTable("users", "TaskFlow")
            .HasDiscriminator<string>("UserType")
            .HasValue<User>("User")
            .HasValue<Member>("Member")
            .HasValue<Admin>("Admin");
        }
    }
}
