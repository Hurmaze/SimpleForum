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
    [Migration("20220706145959_RoleChanged")]
    partial class RoleChanged
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DAL.Entities.Credentials", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Credentials", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            PasswordHash = new byte[] { 22, 85, 217, 244, 121, 2, 104, 166, 233, 197, 69, 64, 51, 118, 251, 189, 166, 237, 224, 208, 83, 103, 19, 66, 108, 19, 48, 120, 151, 1, 69, 108, 108, 80, 238, 56, 83, 60, 207, 200, 111, 237, 208, 51, 223, 53, 66, 66, 33, 251, 135, 128, 92, 184, 54, 180, 11, 221, 22, 248, 3, 162, 99, 209 },
                            PasswordSalt = new byte[] { 17, 100, 6, 92, 115, 187, 109, 231, 120, 192, 65, 53, 186, 253, 10, 165, 136, 133, 163, 56, 146, 78, 28, 206, 225, 105, 170, 255, 103, 135, 141, 173, 22, 233, 248, 98, 181, 165, 116, 203, 48, 79, 144, 175, 81, 85, 6, 103, 149, 92, 170, 56, 110, 170, 243, 117, 189, 175, 250, 211, 242, 21, 101, 116, 18, 216, 38, 111, 98, 87, 58, 102, 117, 150, 213, 190, 166, 232, 231, 153, 227, 21, 173, 149, 128, 152, 87, 14, 142, 63, 25, 103, 28, 20, 152, 34, 99, 77, 168, 108, 21, 134, 2, 190, 169, 252, 145, 14, 237, 54, 29, 189, 107, 14, 22, 70, 168, 237, 58, 167, 150, 199, 216, 82, 111, 224, 78, 247 },
                            RoleId = 1,
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            PasswordHash = new byte[] { 118, 16, 17, 88, 172, 106, 105, 100, 62, 180, 100, 37, 10, 8, 174, 106, 118, 71, 205, 109, 181, 24, 79, 238, 56, 18, 228, 252, 138, 150, 4, 67, 134, 175, 59, 153, 117, 120, 13, 91, 134, 242, 244, 227, 126, 117, 126, 241, 235, 59, 132, 110, 251, 186, 150, 89, 207, 145, 144, 47, 233, 10, 234, 97 },
                            PasswordSalt = new byte[] { 0, 26, 168, 154, 148, 147, 246, 99, 211, 214, 50, 26, 60, 255, 187, 98, 163, 25, 23, 197, 231, 111, 42, 148, 90, 13, 128, 184, 239, 157, 93, 186, 60, 134, 5, 75, 116, 120, 154, 52, 136, 181, 152, 59, 8, 133, 116, 152, 217, 220, 192, 46, 148, 94, 198, 169, 243, 225, 66, 29, 214, 239, 60, 186, 122, 207, 150, 250, 180, 111, 148, 174, 241, 17, 110, 48, 69, 194, 63, 48, 6, 41, 230, 220, 27, 21, 127, 243, 199, 238, 97, 117, 130, 48, 25, 2, 72, 159, 222, 142, 53, 166, 221, 193, 253, 122, 121, 207, 98, 253, 144, 100, 208, 87, 145, 212, 23, 117, 55, 108, 186, 239, 129, 74, 102, 77, 203, 251 },
                            RoleId = 1,
                            UserId = 2
                        },
                        new
                        {
                            Id = 3,
                            PasswordHash = new byte[] { 56, 109, 146, 252, 87, 225, 128, 242, 105, 128, 9, 176, 144, 126, 3, 132, 218, 29, 105, 194, 11, 150, 39, 112, 182, 253, 253, 102, 37, 32, 74, 35, 202, 199, 68, 85, 28, 59, 44, 156, 148, 42, 123, 202, 196, 63, 31, 42, 164, 188, 122, 151, 140, 193, 37, 97, 99, 241, 33, 54, 215, 64, 238, 208 },
                            PasswordSalt = new byte[] { 104, 106, 117, 196, 209, 213, 168, 146, 54, 42, 180, 152, 62, 188, 116, 205, 166, 2, 22, 129, 72, 41, 55, 87, 197, 178, 152, 195, 162, 118, 76, 206, 107, 25, 81, 212, 63, 209, 70, 190, 46, 111, 94, 194, 159, 208, 18, 228, 141, 96, 78, 122, 230, 188, 244, 6, 98, 122, 118, 12, 209, 208, 27, 156, 117, 252, 10, 202, 201, 91, 209, 70, 128, 200, 50, 253, 24, 118, 36, 103, 110, 131, 181, 77, 33, 175, 7, 198, 184, 111, 55, 164, 226, 99, 196, 195, 243, 109, 5, 23, 60, 146, 6, 12, 44, 110, 220, 209, 204, 139, 133, 204, 208, 178, 92, 14, 248, 123, 107, 180, 111, 138, 231, 81, 180, 169, 8, 147 },
                            RoleId = 1,
                            UserId = 3
                        },
                        new
                        {
                            Id = 4,
                            PasswordHash = new byte[] { 240, 171, 251, 15, 91, 59, 47, 18, 87, 202, 161, 32, 110, 194, 148, 72, 248, 178, 86, 216, 141, 139, 241, 69, 111, 84, 46, 13, 72, 5, 211, 147, 235, 36, 165, 88, 93, 41, 124, 160, 111, 124, 3, 50, 248, 75, 42, 247, 180, 161, 237, 131, 178, 12, 212, 197, 24, 76, 4, 201, 135, 125, 136, 105 },
                            PasswordSalt = new byte[] { 138, 82, 74, 202, 110, 65, 60, 245, 62, 218, 169, 234, 114, 96, 204, 39, 174, 232, 75, 131, 39, 234, 111, 104, 86, 168, 236, 243, 201, 55, 25, 134, 54, 154, 66, 12, 126, 26, 103, 172, 166, 255, 133, 48, 231, 87, 178, 12, 34, 243, 33, 142, 150, 190, 91, 5, 157, 213, 52, 165, 231, 240, 171, 255, 173, 137, 152, 121, 97, 224, 254, 236, 84, 116, 1, 157, 76, 202, 82, 238, 191, 113, 170, 147, 157, 7, 9, 199, 126, 67, 55, 161, 169, 120, 217, 183, 36, 111, 51, 55, 119, 14, 116, 0, 66, 61, 81, 19, 18, 243, 245, 184, 44, 237, 215, 93, 68, 46, 133, 187, 82, 143, 123, 107, 48, 137, 134, 74 },
                            RoleId = 2,
                            UserId = 4
                        },
                        new
                        {
                            Id = 5,
                            PasswordHash = new byte[] { 219, 117, 26, 251, 206, 75, 202, 135, 248, 10, 143, 21, 98, 87, 15, 26, 25, 251, 96, 131, 38, 218, 131, 51, 194, 9, 87, 24, 8, 65, 211, 202, 56, 82, 1, 228, 230, 25, 124, 107, 154, 132, 63, 30, 40, 33, 212, 80, 204, 28, 16, 89, 6, 100, 61, 39, 83, 235, 61, 109, 135, 206, 178, 232 },
                            PasswordSalt = new byte[] { 248, 201, 33, 245, 173, 31, 122, 232, 21, 39, 34, 110, 200, 46, 239, 108, 232, 33, 139, 243, 167, 125, 116, 136, 76, 205, 93, 236, 198, 46, 28, 184, 134, 195, 4, 131, 129, 162, 227, 152, 69, 36, 15, 21, 37, 158, 137, 89, 74, 43, 245, 25, 152, 186, 21, 0, 211, 183, 199, 90, 173, 149, 148, 208, 217, 167, 207, 16, 94, 195, 234, 70, 94, 182, 195, 26, 127, 200, 151, 116, 127, 238, 218, 68, 193, 134, 173, 55, 44, 89, 118, 220, 70, 226, 172, 98, 173, 149, 153, 5, 153, 144, 212, 198, 163, 111, 60, 79, 30, 13, 2, 74, 237, 227, 70, 193, 23, 101, 108, 11, 9, 114, 73, 113, 52, 232, 13, 155 },
                            RoleId = 3,
                            UserId = 5
                        });
                });

            modelBuilder.Entity("DAL.Entities.ForumThread", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AuthorId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ThemeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("TimeCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Title")
                        .IsRequired()
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
                            TimeCreated = new DateTime(2022, 7, 6, 17, 59, 58, 698, DateTimeKind.Local).AddTicks(9231),
                            Title = "Super elephants"
                        },
                        new
                        {
                            Id = 2,
                            AuthorId = 2,
                            Content = "Let`s talk about Mykola Khvylovy and his novel 'I(Romance)' ",
                            ThemeId = 2,
                            TimeCreated = new DateTime(2022, 7, 6, 17, 59, 58, 698, DateTimeKind.Local).AddTicks(9235),
                            Title = "Mykola Khvylovy"
                        });
                });

            modelBuilder.Entity("DAL.Entities.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AuthorId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
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

                    b.ToTable("Posts", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AuthorId = 2,
                            Content = "Man i love elephants!I recently learned that elephants drink up to 300 liters of water a day!",
                            ThreadId = 1,
                            TimeCreated = new DateTime(2022, 7, 6, 17, 59, 58, 698, DateTimeKind.Local).AddTicks(9247)
                        },
                        new
                        {
                            Id = 2,
                            AuthorId = 3,
                            Content = "My favourite elephant is Asian elephant",
                            ThreadId = 1,
                            TimeCreated = new DateTime(2022, 7, 6, 17, 59, 58, 698, DateTimeKind.Local).AddTicks(9250)
                        },
                        new
                        {
                            Id = 3,
                            AuthorId = 5,
                            Content = "Books are great you know.",
                            ThreadId = 2,
                            TimeCreated = new DateTime(2022, 7, 6, 17, 59, 58, 698, DateTimeKind.Local).AddTicks(9252)
                        },
                        new
                        {
                            Id = 4,
                            AuthorId = 1,
                            Content = "Read recently about Segriy Zhadan... He is cool.",
                            ThreadId = 2,
                            TimeCreated = new DateTime(2022, 7, 6, 17, 59, 58, 698, DateTimeKind.Local).AddTicks(9254)
                        });
                });

            modelBuilder.Entity("DAL.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("BasicRole")
                        .HasColumnType("bit");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BasicRole = true,
                            RoleName = "user"
                        },
                        new
                        {
                            Id = 2,
                            BasicRole = true,
                            RoleName = "moderator"
                        },
                        new
                        {
                            Id = 3,
                            BasicRole = true,
                            RoleName = "admin"
                        });
                });

            modelBuilder.Entity("DAL.Entities.Theme", b =>
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

                    b.ToTable("Themes", (string)null);

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
                        },
                        new
                        {
                            Id = 3,
                            ThemeName = "Other"
                        });
                });

            modelBuilder.Entity("DAL.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CredentialsId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Nickname")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime>("RegistrationTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CredentialsId = 0,
                            Email = "user1@gmail.com",
                            Nickname = "user1",
                            RegistrationTime = new DateTime(2022, 7, 6, 17, 59, 58, 698, DateTimeKind.Local).AddTicks(9080)
                        },
                        new
                        {
                            Id = 2,
                            CredentialsId = 0,
                            Email = "user2@gmail.com",
                            RegistrationTime = new DateTime(2022, 7, 6, 17, 59, 58, 698, DateTimeKind.Local).AddTicks(9113)
                        },
                        new
                        {
                            Id = 3,
                            CredentialsId = 0,
                            Email = "user3@gmail.com",
                            Nickname = "user3",
                            RegistrationTime = new DateTime(2022, 7, 6, 17, 59, 58, 698, DateTimeKind.Local).AddTicks(9116)
                        },
                        new
                        {
                            Id = 4,
                            CredentialsId = 0,
                            Email = "moderator1@gmail.com",
                            Nickname = "moderator1",
                            RegistrationTime = new DateTime(2022, 7, 6, 17, 59, 58, 698, DateTimeKind.Local).AddTicks(9117)
                        },
                        new
                        {
                            Id = 5,
                            CredentialsId = 0,
                            Email = "admin1@gmail.com",
                            Nickname = "admin1",
                            RegistrationTime = new DateTime(2022, 7, 6, 17, 59, 58, 698, DateTimeKind.Local).AddTicks(9119)
                        });
                });

            modelBuilder.Entity("DAL.Entities.Credentials", b =>
                {
                    b.HasOne("DAL.Entities.Role", "Role")
                        .WithMany("Credentials")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("DAL.Entities.User", "User")
                        .WithOne("Credentials")
                        .HasForeignKey("DAL.Entities.Credentials", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.Entities.ForumThread", b =>
                {
                    b.HasOne("DAL.Entities.User", "Author")
                        .WithMany("Threads")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("DAL.Entities.Theme", "Theme")
                        .WithMany("ForumThreads")
                        .HasForeignKey("ThemeId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Author");

                    b.Navigation("Theme");
                });

            modelBuilder.Entity("DAL.Entities.Post", b =>
                {
                    b.HasOne("DAL.Entities.User", "Author")
                        .WithMany("ThreadPosts")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("DAL.Entities.ForumThread", "Thread")
                        .WithMany("ThreadPosts")
                        .HasForeignKey("ThreadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Thread");
                });

            modelBuilder.Entity("DAL.Entities.ForumThread", b =>
                {
                    b.Navigation("ThreadPosts");
                });

            modelBuilder.Entity("DAL.Entities.Role", b =>
                {
                    b.Navigation("Credentials");
                });

            modelBuilder.Entity("DAL.Entities.Theme", b =>
                {
                    b.Navigation("ForumThreads");
                });

            modelBuilder.Entity("DAL.Entities.User", b =>
                {
                    b.Navigation("Credentials");

                    b.Navigation("ThreadPosts");

                    b.Navigation("Threads");
                });
#pragma warning restore 612, 618
        }
    }
}