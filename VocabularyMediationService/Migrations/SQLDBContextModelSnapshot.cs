﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VocabularyMediationService.Database;

namespace VocabularyMediationService.Migrations
{
    [DbContext(typeof(SQLDBContext))]
    partial class SQLDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("VocabularyMediationService.Database.Models.Hazard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("HazardTypeId");

                    b.Property<int?>("ParentId");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("HazardTypeId");

                    b.HasIndex("ParentId");

                    b.ToTable("Hazards");
                });

            modelBuilder.Entity("VocabularyMediationService.Database.Models.HazardType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.ToTable("HazardTypes");
                });

            modelBuilder.Entity("VocabularyMediationService.Database.Models.Region", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double?>("BoundX1")
                        .HasColumnType("float");

                    b.Property<double?>("BoundX2")
                        .HasColumnType("float");

                    b.Property<double?>("BoundY1")
                        .HasColumnType("float");

                    b.Property<double?>("BoundY2")
                        .HasColumnType("float");

                    b.Property<string>("Desription");

                    b.Property<int?>("ParentId");

                    b.Property<string>("SimpleWKT")
                        .HasColumnType("text");

                    b.Property<string>("Source")
                        .HasColumnType("text");

                    b.Property<int?>("TypeId");

                    b.Property<string>("Value");

                    b.Property<string>("WKT")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.HasIndex("TypeId");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("VocabularyMediationService.Database.Models.RegionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.ToTable("RegionTypes");
                });

            modelBuilder.Entity("VocabularyMediationService.Database.Models.SAGovDept", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ParentId");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("SAGovDepts");
                });

            modelBuilder.Entity("VocabularyMediationService.Database.Models.Sector", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ParentId");

                    b.Property<string>("SIC_Code");

                    b.Property<int?>("SectorTypeId");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.HasIndex("SectorTypeId");

                    b.ToTable("Sectors");
                });

            modelBuilder.Entity("VocabularyMediationService.Database.Models.SectorType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.ToTable("SectorTypes");
                });

            modelBuilder.Entity("VocabularyMediationService.Database.Models.Hazard", b =>
                {
                    b.HasOne("VocabularyMediationService.Database.Models.HazardType", "HazardType")
                        .WithMany()
                        .HasForeignKey("HazardTypeId");

                    b.HasOne("VocabularyMediationService.Database.Models.Hazard", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("VocabularyMediationService.Database.Models.Region", b =>
                {
                    b.HasOne("VocabularyMediationService.Database.Models.Region", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId");

                    b.HasOne("VocabularyMediationService.Database.Models.RegionType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId");
                });

            modelBuilder.Entity("VocabularyMediationService.Database.Models.SAGovDept", b =>
                {
                    b.HasOne("VocabularyMediationService.Database.Models.SAGovDept", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("VocabularyMediationService.Database.Models.Sector", b =>
                {
                    b.HasOne("VocabularyMediationService.Database.Models.Sector", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId");

                    b.HasOne("VocabularyMediationService.Database.Models.SectorType", "SectorType")
                        .WithMany()
                        .HasForeignKey("SectorTypeId");
                });
#pragma warning restore 612, 618
        }
    }
}
