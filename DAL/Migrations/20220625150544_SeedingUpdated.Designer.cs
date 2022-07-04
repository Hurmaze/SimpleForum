﻿// <auto-generated />
using System;
using DAL.DbAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(ForumDbContext))]
    [Migration("20220625150544_SeedingUpdated")]
    partial class SeedingUpdated
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DAL.Entities.Forum.ForumThread", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ThemeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("TimeCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Title")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("ThemeId");

                    b.ToTable("Threads", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AuthorId = 1,
                            Content = "Elephants are the largest existing land animals. Three living species are currently recognised: the African bush elephant, the African forest elephant, and the Asian elephant. They are an informal grouping within the subfamily Elephantinae of the order Proboscidea; extinct members include the mastodons.",
                            ThemeId = 1,
                            TimeCreated = new DateTime(2022, 6, 25, 18, 5, 44, 645, DateTimeKind.Local).AddTicks(5626),
                            Title = "Super elephants"
                        },
                        new
                        {
                            Id = 2,
                            AuthorId = 2,
                            Content = "Let`s talk about Mykola Khvylovy and his novel 'I(Romance)' ",
                            ThemeId = 2,
                            TimeCreated = new DateTime(2022, 6, 25, 18, 5, 44, 645, DateTimeKind.Local).AddTicks(5631),
                            Title = "Mykola Khvylovy"
                        });
                });

            modelBuilder.Entity("DAL.Entities.Forum.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ThreadId")
                        .HasColumnType("int");

                    b.Property<DateTime>("TimeCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("ThreadId");

                    b.ToTable("ThreadPosts", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AuthorId = 2,
                            Content = "Man i love elephants!I recently learned that elephants drink up to 300 liters of water a day!",
                            ThreadId = 1,
                            TimeCreated = new DateTime(2022, 6, 25, 18, 5, 44, 645, DateTimeKind.Local).AddTicks(5645)
                        },
                        new
                        {
                            Id = 2,
                            AuthorId = 3,
                            Content = "My favourite elephant is Asian elephant",
                            ThreadId = 1,
                            TimeCreated = new DateTime(2022, 6, 25, 18, 5, 44, 645, DateTimeKind.Local).AddTicks(5650)
                        },
                        new
                        {
                            Id = 3,
                            AuthorId = 5,
                            Content = "Books are great you know.",
                            ThreadId = 2,
                            TimeCreated = new DateTime(2022, 6, 25, 18, 5, 44, 645, DateTimeKind.Local).AddTicks(5652)
                        },
                        new
                        {
                            Id = 4,
                            AuthorId = 1,
                            Content = "Read recently about Segriy Zhadan... He is cool.",
                            ThreadId = 2,
                            TimeCreated = new DateTime(2022, 6, 25, 18, 5, 44, 645, DateTimeKind.Local).AddTicks(5654)
                        });
                });

            modelBuilder.Entity("DAL.Entities.Forum.Theme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ThemeName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Themes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ThemeName = "Books"
                        },
                        new
                        {
                            Id = 2,
                            ThemeName = "Elephants"
                        });
                });

            modelBuilder.Entity("DAL.Entities.Forum.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Nickname")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "user1@gmail.com",
                            Nickname = "user1"
                        },
                        new
                        {
                            Id = 2,
                            Email = "user2@gmail.com",
                            Nickname = "user2"
                        },
                        new
                        {
                            Id = 3,
                            Email = "user3@gmail.com",
                            Nickname = "user3"
                        },
                        new
                        {
                            Id = 4,
                            Email = "moderator1@gmail.com",
                            Nickname = "moderator1"
                        },
                        new
                        {
                            Id = 5,
                            Email = "admin1@gmail.com",
                            Nickname = "admin1"
                        });
                });

            modelBuilder.Entity("DAL.Entities.Forum.ForumThread", b =>
                {
                    b.HasOne("DAL.Entities.Forum.User", "Author")
                        .WithMany("Threads")
                        .HasForeignKey("AuthorId")
                        .IsRequired();

                    b.HasOne("DAL.Entities.Forum.Theme", "Theme")
                        .WithMany("ForumThreads")
                        .HasForeignKey("ThemeId")
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Theme");
                });

            modelBuilder.Entity("DAL.Entities.Forum.Post", b =>
                {
                    b.HasOne("DAL.Entities.Forum.User", "Author")
                        .WithMany("ThreadPosts")
                        .HasForeignKey("AuthorId")
                        .IsRequired();

                    b.HasOne("DAL.Entities.Forum.ForumThread", "Thread")
                        .WithMany("ThreadPosts")
                        .HasForeignKey("ThreadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Thread");
                });

            modelBuilder.Entity("DAL.Entities.Forum.ForumThread", b =>
                {
                    b.Navigation("ThreadPosts");
                });

            modelBuilder.Entity("DAL.Entities.Forum.Theme", b =>
                {
                    b.Navigation("ForumThreads");
                });

            modelBuilder.Entity("DAL.Entities.Forum.User", b =>
                {
                    b.Navigation("ThreadPosts");

                    b.Navigation("Threads");
                });
#pragma warning restore 612, 618
        }
    }
}
