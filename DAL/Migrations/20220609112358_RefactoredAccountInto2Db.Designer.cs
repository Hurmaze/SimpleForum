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
    [Migration("20220609112358_RefactoredAccountInto2Db")]
    partial class RefactoredAccountInto2Db
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DAL.Entities.Authentication.AccountAuth", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AccountAuths");
                });

            modelBuilder.Entity("DAL.Entities.Authentication.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("DAL.Entities.Forum.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Nickname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("DAL.Entities.Forum.ForumThread", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AuthorId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ThemeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("TimeCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("ThemeId");

                    b.ToTable("Threads", (string)null);
                });

            modelBuilder.Entity("DAL.Entities.Forum.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AuthorId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ThreadId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("ThreadId");

                    b.ToTable("ThreadPosts", (string)null);
                });

            modelBuilder.Entity("DAL.Entities.Forum.Theme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ThemeName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Themes");
                });

            modelBuilder.Entity("DAL.Entities.Authentication.AccountAuth", b =>
                {
                    b.HasOne("DAL.Entities.Authentication.Role", "Role")
                        .WithMany("AccountAuths")
                        .HasForeignKey("RoleId");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("DAL.Entities.Forum.ForumThread", b =>
                {
                    b.HasOne("DAL.Entities.Forum.Account", "Author")
                        .WithMany("Threads")
                        .HasForeignKey("AuthorId");

                    b.HasOne("DAL.Entities.Forum.Theme", "Theme")
                        .WithMany("ForumThreads")
                        .HasForeignKey("ThemeId");

                    b.Navigation("Author");

                    b.Navigation("Theme");
                });

            modelBuilder.Entity("DAL.Entities.Forum.Post", b =>
                {
                    b.HasOne("DAL.Entities.Forum.Account", "Author")
                        .WithMany("ThreadPosts")
                        .HasForeignKey("AuthorId");

                    b.HasOne("DAL.Entities.Forum.ForumThread", "Thread")
                        .WithMany("ThreadPosts")
                        .HasForeignKey("ThreadId");

                    b.Navigation("Author");

                    b.Navigation("Thread");
                });

            modelBuilder.Entity("DAL.Entities.Authentication.Role", b =>
                {
                    b.Navigation("AccountAuths");
                });

            modelBuilder.Entity("DAL.Entities.Forum.Account", b =>
                {
                    b.Navigation("ThreadPosts");

                    b.Navigation("Threads");
                });

            modelBuilder.Entity("DAL.Entities.Forum.ForumThread", b =>
                {
                    b.Navigation("ThreadPosts");
                });

            modelBuilder.Entity("DAL.Entities.Forum.Theme", b =>
                {
                    b.Navigation("ForumThreads");
                });
#pragma warning restore 612, 618
        }
    }
}
