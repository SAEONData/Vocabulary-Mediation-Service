﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using VocabularyMediationService.Database;

namespace VocabularyMediationService.Migrations
{
    [DbContext(typeof(SQLDBContext))]
    [Migration("20180913202656_Initial create (with Sectors and Hazards)")]
    partial class InitialcreatewithSectorsandHazards
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("VocabularyMediationService.Database.Models.Hazard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.ToTable("Hazards");
                });

            modelBuilder.Entity("VocabularyMediationService.Database.Models.Sector", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ParentSectorId");

                    b.Property<int?>("SectorTypeId");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("ParentSectorId");

                    b.HasIndex("SectorTypeId");

                    b.ToTable("Sectors");
                });

            modelBuilder.Entity("VocabularyMediationService.Database.Models.SectorType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.ToTable("SectorTypes");
                });

            modelBuilder.Entity("VocabularyMediationService.Database.Models.Sector", b =>
                {
                    b.HasOne("VocabularyMediationService.Database.Models.Sector", "ParentSector")
                        .WithMany()
                        .HasForeignKey("ParentSectorId");

                    b.HasOne("VocabularyMediationService.Database.Models.SectorType", "SectorType")
                        .WithMany()
                        .HasForeignKey("SectorTypeId");
                });
#pragma warning restore 612, 618
        }
    }
}