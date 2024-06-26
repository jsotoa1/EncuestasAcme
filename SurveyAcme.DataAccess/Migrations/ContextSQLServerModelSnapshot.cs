﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SurveyAcme.DataAccess.Context;

#nullable disable

namespace SurveyAcme.DataAccess.Migrations
{
    [DbContext(typeof(ContextSQLServer))]
    partial class ContextSQLServerModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SurveyAcme.DataAccess.Entities.Survey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("survey_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("date_created");

                    b.Property<string>("CreationUser")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("user_created");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit")
                        .HasColumnName("deleted");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("survey_description");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("is_active");

                    b.Property<string>("Link")
                        .HasMaxLength(2147483647)
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("survey_link");

                    b.Property<DateTime?>("ModificationDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("date_modified");

                    b.Property<string>("ModificationUser")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("user_modified");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("survey_name");

                    b.HasKey("Id");

                    b.ToTable("survey");
                });

            modelBuilder.Entity("SurveyAcme.DataAccess.Entities.SurveyField", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("survey_field_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("date_created");

                    b.Property<string>("CreationUser")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("user_created");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit")
                        .HasColumnName("deleted");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("is_active");

                    b.Property<DateTime?>("ModificationDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("date_modified");

                    b.Property<string>("ModificationUser")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("user_modified");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("survey_field_name");

                    b.Property<string>("Required")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)")
                        .HasColumnName("survey_field_required");

                    b.Property<int>("SurveyId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("survey_field_title");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("survey_field_Type");

                    b.HasKey("Id");

                    b.HasIndex("SurveyId");

                    b.ToTable("survey_field");
                });

            modelBuilder.Entity("SurveyAcme.DataAccess.Entities.SurveyFieldData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("survey_field_data_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("date_created");

                    b.Property<string>("CreationUser")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("user_created");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit")
                        .HasColumnName("deleted");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("is_active");

                    b.Property<DateTime?>("ModificationDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("date_modified");

                    b.Property<string>("ModificationUser")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("user_modified");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("survey_field_data_name");

                    b.Property<int>("SurveyFieldId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SurveyFieldId");

                    b.ToTable("survey_field_data");
                });

            modelBuilder.Entity("SurveyAcme.DataAccess.Entities.SurveyField", b =>
                {
                    b.HasOne("SurveyAcme.DataAccess.Entities.Survey", "Survey")
                        .WithMany("SurveyField")
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Survey");
                });

            modelBuilder.Entity("SurveyAcme.DataAccess.Entities.SurveyFieldData", b =>
                {
                    b.HasOne("SurveyAcme.DataAccess.Entities.SurveyField", "SurveyField")
                        .WithMany("SurveyFieldData")
                        .HasForeignKey("SurveyFieldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SurveyField");
                });

            modelBuilder.Entity("SurveyAcme.DataAccess.Entities.Survey", b =>
                {
                    b.Navigation("SurveyField");
                });

            modelBuilder.Entity("SurveyAcme.DataAccess.Entities.SurveyField", b =>
                {
                    b.Navigation("SurveyFieldData");
                });
#pragma warning restore 612, 618
        }
    }
}
