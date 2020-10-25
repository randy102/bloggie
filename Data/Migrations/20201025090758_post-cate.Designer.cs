﻿// <auto-generated />
using System;
using Bloggie.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Bloggie.Data.Migrations
{
    [DbContext(typeof(BloggieContext))]
    [Migration("20201025090758_post-cate")]
    partial class postcate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8");

            modelBuilder.Entity("Bloggie.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Công nghệ"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Giải trí"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Bóng đá"
                        });
                });

            modelBuilder.Entity("Bloggie.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AuthorId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Img")
                        .HasColumnType("TEXT");

                    b.Property<int>("State")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Posts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AuthorId = 2,
                            CategoryId = 1,
                            Content = "The [ForeignKey] attribute overrides the default convention for a foreign key It allows us to specify the foreign key property in the dependent entity whose name does not match with the primary key property of the principal entity.",
                            CreatedAt = new DateTime(2020, 10, 25, 9, 7, 57, 784, DateTimeKind.Local).AddTicks(3277),
                            State = 0,
                            Title = "The [ForeignKey(name)] attribute"
                        },
                        new
                        {
                            Id = 2,
                            AuthorId = 1,
                            CategoryId = 3,
                            Content = "In the above example, the [ForeignKey] attribute is applied on the StandardRefId and specified in the name of the navigation property Standard. ",
                            CreatedAt = new DateTime(2020, 10, 25, 9, 7, 57, 784, DateTimeKind.Local).AddTicks(3910),
                            State = 1,
                            Title = "Learn Entity Framework"
                        });
                });

            modelBuilder.Entity("Bloggie.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FullName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<int>("Role")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Active = true,
                            Email = "admin@admin.com",
                            FullName = "Admin",
                            Password = "8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92",
                            Role = 2
                        },
                        new
                        {
                            Id = 2,
                            Active = true,
                            Email = "mod@mod.com",
                            FullName = "Moderator",
                            Password = "8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92",
                            Role = 1
                        },
                        new
                        {
                            Id = 3,
                            Active = true,
                            Email = "writer@writer.com",
                            FullName = "Writer",
                            Password = "8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92",
                            Role = 0
                        });
                });

            modelBuilder.Entity("Bloggie.Models.Post", b =>
                {
                    b.HasOne("Bloggie.Models.User", "Author")
                        .WithMany("Posts")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bloggie.Models.Category", "Category")
                        .WithMany("Posts")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
