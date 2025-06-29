﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskFlow.Domain.Entities.Projects;
using TaskFlow.Domain.Entities.Labels;
using TaskFlow.Domain.Entities.Tasks;
namespace TaskFlow.Infrastructure.BL
{
    public sealed class WorkflowDBContext : DbContext
    {
        public WorkflowDBContext(DbContextOptions<WorkflowDBContext> options) : base(options) {}
        public DbSet<Project> Projects { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<UTask> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasMany(p => p.Sections)
                .WithOne()
                .HasForeignKey(s => s.ProjectId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Section>()
               .HasMany(p => p.Tasks)
               .WithOne()
               .HasForeignKey(s => s.SectionId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UTask>()
               .HasMany(p => p.Tasks)
               .WithOne()
               .HasForeignKey(s => s.ParentTaskId)
               .IsRequired()
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
