﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Appointments.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Appointments.Persistence.Migrations
{
    [DbContext(typeof(AppointmentsDbContext))]
    [Migration("20240117220318_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Appointments")
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Appointments.Features.Appointments.Domain.Appointment", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<string>("LastModifiedIp")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("State")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("VenueId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("State");

                    b.HasIndex("VenueId");

                    b.ToTable("Appointments", "Appointments");
                });

            modelBuilder.Entity("Appointments.Features.Venues.Domain.Venue", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("LastModifiedBy")
                        .HasColumnType("uuid");

                    b.Property<string>("LastModifiedIp")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.ComplexProperty<Dictionary<string, object>>("Coordinates", "Appointments.Features.Venues.Domain.Venue.Coordinates#Coordinates", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<double>("Latitude")
                                .HasColumnType("double precision");

                            b1.Property<double>("Longitude")
                                .HasColumnType("double precision");
                        });

                    b.HasKey("Id");

                    b.ToTable("Venues", "Appointments");
                });

            modelBuilder.Entity("Appointments.Features.Appointments.Domain.Appointment", b =>
                {
                    b.HasOne("Appointments.Features.Venues.Domain.Venue", "Venue")
                        .WithMany("Appointments")
                        .HasForeignKey("VenueId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Venue");
                });

            modelBuilder.Entity("Appointments.Features.Venues.Domain.Venue", b =>
                {
                    b.Navigation("Appointments");
                });
#pragma warning restore 612, 618
        }
    }
}