using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Domain.Entities.Projects;
using TaskFlow.Domain.Entities.Labels;
namespace TaskFlow.Infrastructure.BL
{
    public sealed class WorkflowDBContext : DbContext
    {
        public WorkflowDBContext(DbContextOptions<WorkflowDBContext> options) : base(options) {}
        public DbSet<Project> Projects { get; set; }
        public DbSet<Section> Sections { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasMany(c => c.Sections)
                .WithOne(c => c.Project)
                .IsRequired()
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
