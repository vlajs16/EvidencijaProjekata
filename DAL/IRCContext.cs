using Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace DAL
{
    public class IRCContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<ExternalMentor> ExternalMentors { get; set; }
        public DbSet<ExternalMentorContact> ExternalMentorContacts { get; set; }
        public DbSet<ProjectProposal> ProjectProposals { get; set; }
        public DbSet<ProjectCoveringSubject> ProjectCoveringSubjects { get; set; }
        public DbSet<ScientificArea> ScientificAreas { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<EmployeePosition> EmployeePositions { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectContract> ProjectContracts { get; set; }
        public IRCContext(DbContextOptions<IRCContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>().HasKey(l => new { l.CityID, l.CompanyID });
            modelBuilder.Entity<ExternalMentor>(e =>
            {
                e.HasKey(em => new { em.CompanyID, em.MentorID });
                e.Property(em => em.MentorID).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<ExternalMentorContact>(emc =>
            {
                emc.HasKey(e => new { e.ExternalMentorCompanyID, e.ExternalMentorMentorID, e.SerialNumber });
                emc.Property(e => e.SerialNumber).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<EmployeePosition>().HasKey(e => new { e.EmployeeID, e.PositionID });
            modelBuilder.Entity<Contact>(b =>
            {
                b.HasKey(e => new { e.CompanyID, e.ContactID });
                b.Property(e => e.ContactID).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<ProjectCoveringSubject>(b =>
            {
                b.HasKey(e => new { e.ProjectProposalID, e.ProjectCoveringSubjectID });
                b.Property(e => e.ProjectCoveringSubjectID).ValueGeneratedOnAdd();
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
