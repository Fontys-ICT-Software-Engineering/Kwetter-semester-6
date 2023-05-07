﻿// <auto-generated />
using System;
using Kweet.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Kweet.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230426122806_standarddata")]
    partial class standarddata
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Kweet.Models.KweetModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsEdited")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("User")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Kweets");

                    b.HasData(
                        new
                        {
                            Id = new Guid("0871edc8-1916-4ce6-9ed9-15c1fc38bb65"),
                            Date = new DateTime(2023, 4, 26, 14, 28, 6, 377, DateTimeKind.Local).AddTicks(1610),
                            IsEdited = false,
                            Message = "testMessage 1",
                            User = "User1"
                        },
                        new
                        {
                            Id = new Guid("fb8afad9-dd53-4e02-8fe7-53eadce5992c"),
                            Date = new DateTime(2023, 4, 26, 14, 28, 6, 377, DateTimeKind.Local).AddTicks(1679),
                            IsEdited = true,
                            Message = "testMessage 2",
                            User = "User1"
                        },
                        new
                        {
                            Id = new Guid("ddd51194-7632-4d3b-9ab0-c9609ba5d73b"),
                            Date = new DateTime(2023, 4, 26, 14, 28, 6, 377, DateTimeKind.Local).AddTicks(1691),
                            IsEdited = false,
                            Message = "testMessage 3",
                            User = "User1"
                        },
                        new
                        {
                            Id = new Guid("9c555efc-1721-4844-a8cc-24043dc97651"),
                            Date = new DateTime(2023, 4, 26, 14, 28, 6, 377, DateTimeKind.Local).AddTicks(1702),
                            IsEdited = false,
                            Message = "testMessage 4",
                            User = "User1"
                        },
                        new
                        {
                            Id = new Guid("9fcb1430-a7ad-463e-9bd3-fa000656176e"),
                            Date = new DateTime(2023, 4, 26, 14, 28, 6, 377, DateTimeKind.Local).AddTicks(1713),
                            IsEdited = true,
                            Message = "testMessage 5",
                            User = "User2"
                        },
                        new
                        {
                            Id = new Guid("48ebef61-515d-4891-9aa3-82ff2d53bbdb"),
                            Date = new DateTime(2023, 4, 26, 14, 28, 6, 377, DateTimeKind.Local).AddTicks(1726),
                            IsEdited = false,
                            Message = "testMessage 6",
                            User = "User2"
                        });
                });

            modelBuilder.Entity("KweetService.Models.Like", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("KweetID")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Likes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            KweetID = "0871edc8-1916-4ce6-9ed9-15c1fc38bb65",
                            UserID = "User2"
                        },
                        new
                        {
                            Id = 2,
                            KweetID = "0871edc8-1916-4ce6-9ed9-15c1fc38bb65",
                            UserID = "User1"
                        },
                        new
                        {
                            Id = 3,
                            KweetID = "0871edc8-1916-4ce6-9ed9-15c1fc38bb65",
                            UserID = "User1"
                        });
                });

            modelBuilder.Entity("KweetService.Models.ReactionKweet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DateSend")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("KweetId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Reactions");
                });
#pragma warning restore 612, 618
        }
    }
}
