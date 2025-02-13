﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.Migrations
{
    [DbContext(typeof(IRCContext))]
    [Migration("20200423182424_ExernalMentorAndContactDrop")]
    partial class ExernalMentorAndContactDrop
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.City", b =>
                {
                    b.Property<long>("CityID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CityID");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Domain.Company", b =>
                {
                    b.Property<long>("CompanyID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CompanyUsername")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("CompanyID");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("Domain.Contact", b =>
                {
                    b.Property<long>("CompanyID")
                        .HasColumnType("bigint");

                    b.Property<long>("ContactID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ContactType")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CompanyID", "ContactID");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("Domain.Employee", b =>
                {
                    b.Property<long>("EmployeeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmployeeID");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Domain.EmployeePosition", b =>
                {
                    b.Property<long>("EmployeeID")
                        .HasColumnType("bigint");

                    b.Property<long>("PositionID")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DateFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateTo")
                        .HasColumnType("datetime2");

                    b.HasKey("EmployeeID", "PositionID");

                    b.HasIndex("PositionID");

                    b.ToTable("EmployeePositions");
                });

            modelBuilder.Entity("Domain.Location", b =>
                {
                    b.Property<long>("CityID")
                        .HasColumnType("bigint");

                    b.Property<long>("CompanyID")
                        .HasColumnType("bigint");

                    b.Property<string>("AppartmentNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Floor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CityID", "CompanyID");

                    b.HasIndex("CompanyID");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("Domain.Position", b =>
                {
                    b.Property<long>("PositionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PositionID");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("Domain.Project", b =>
                {
                    b.Property<long>("ProjectID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AdoptionDate")
                        .HasColumnType("datetime2");

                    b.Property<long?>("DecisionMakerEmployeeID")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("InternalMentorEmployeeID")
                        .HasColumnType("bigint");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("ProjectProposalID")
                        .HasColumnType("bigint");

                    b.HasKey("ProjectID");

                    b.HasIndex("DecisionMakerEmployeeID");

                    b.HasIndex("InternalMentorEmployeeID");

                    b.HasIndex("ProjectProposalID");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Domain.ProjectContract", b =>
                {
                    b.Property<long>("ProjectContractID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CompanySigner")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<long?>("InternalSignerEmployeeID")
                        .HasColumnType("bigint");

                    b.Property<long?>("ProjectID")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("SigningDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ProjectContractID");

                    b.HasIndex("InternalSignerEmployeeID");

                    b.HasIndex("ProjectID");

                    b.ToTable("ProjectContracts");
                });

            modelBuilder.Entity("Domain.ProjectCoveringSubject", b =>
                {
                    b.Property<long>("ProjectProposalID")
                        .HasColumnType("bigint");

                    b.Property<long>("ProjectCoveringSubjectID")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("ScientificAreaID")
                        .HasColumnType("bigint");

                    b.HasKey("ProjectProposalID", "ProjectCoveringSubjectID");

                    b.HasIndex("ScientificAreaID");

                    b.ToTable("ProjectCoveringSubjects");
                });

            modelBuilder.Entity("Domain.ProjectProposal", b =>
                {
                    b.Property<long>("ProjectProposalID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Activities")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("CompanyID")
                        .HasColumnType("bigint");

                    b.Property<int>("DaysDuration")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Goal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ProposalDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDateProjectProposal")
                        .HasColumnType("datetime2");

                    b.HasKey("ProjectProposalID");

                    b.HasIndex("CompanyID");

                    b.ToTable("ProjectProposals");
                });

            modelBuilder.Entity("Domain.ScientificArea", b =>
                {
                    b.Property<long>("ScientificAreaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ScientificAreaID");

                    b.ToTable("ScientificAreas");
                });

            modelBuilder.Entity("Domain.Contact", b =>
                {
                    b.HasOne("Domain.Company", null)
                        .WithMany("Contacts")
                        .HasForeignKey("CompanyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.EmployeePosition", b =>
                {
                    b.HasOne("Domain.Employee", "Employee")
                        .WithMany("Positions")
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Position", "Position")
                        .WithMany()
                        .HasForeignKey("PositionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Location", b =>
                {
                    b.HasOne("Domain.City", "City")
                        .WithMany()
                        .HasForeignKey("CityID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Company", "Company")
                        .WithMany("Locations")
                        .HasForeignKey("CompanyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Project", b =>
                {
                    b.HasOne("Domain.Employee", "DecisionMaker")
                        .WithMany()
                        .HasForeignKey("DecisionMakerEmployeeID");

                    b.HasOne("Domain.Employee", "InternalMentor")
                        .WithMany()
                        .HasForeignKey("InternalMentorEmployeeID");

                    b.HasOne("Domain.ProjectProposal", "ProjectProposal")
                        .WithMany()
                        .HasForeignKey("ProjectProposalID");
                });

            modelBuilder.Entity("Domain.ProjectContract", b =>
                {
                    b.HasOne("Domain.Employee", "InternalSigner")
                        .WithMany()
                        .HasForeignKey("InternalSignerEmployeeID");

                    b.HasOne("Domain.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectID");
                });

            modelBuilder.Entity("Domain.ProjectCoveringSubject", b =>
                {
                    b.HasOne("Domain.ProjectProposal", null)
                        .WithMany("Subjects")
                        .HasForeignKey("ProjectProposalID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.ScientificArea", "ScientificArea")
                        .WithMany()
                        .HasForeignKey("ScientificAreaID");
                });

            modelBuilder.Entity("Domain.ProjectProposal", b =>
                {
                    b.HasOne("Domain.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyID");
                });
#pragma warning restore 612, 618
        }
    }
}
