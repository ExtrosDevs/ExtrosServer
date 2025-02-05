﻿// <auto-generated />
using System;
using ExtrosServer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ExtrosServer.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20240815184652_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ExtrosServer.Comment", b =>
                {
                    b.Property<Guid>("CommentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("PostID")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ReplyToCommentID")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uuid");

                    b.HasKey("CommentID");

                    b.HasIndex("PostID");

                    b.HasIndex("ReplyToCommentID");

                    b.HasIndex("UserID");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("ExtrosServer.Field", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Exp")
                        .HasColumnType("integer");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Fields");
                });

            modelBuilder.Entity("ExtrosServer.Like", b =>
                {
                    b.Property<Guid>("PostID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("PostID1")
                        .HasColumnType("uuid");

                    b.Property<string>("TagValue")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("PostID");

                    b.HasIndex("PostID1");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("ExtrosServer.Models.Article", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AdminId")
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Views")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("ExtrosServer.Models.Course", b =>
                {
                    b.Property<Guid>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("CourseId");

                    b.HasIndex("UserId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("ExtrosServer.Models.UserFollow", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("FollowerId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("FollowingId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("FollowerId");

                    b.HasIndex("FollowingId");

                    b.ToTable("UserFollows");
                });

            modelBuilder.Entity("ExtrosServer.Post", b =>
                {
                    b.Property<Guid>("PostID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Media")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("OwnerID")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("PostID");

                    b.HasIndex("OwnerID");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("ExtrosServer.Tag", b =>
                {
                    b.Property<string>("TagValue")
                        .HasColumnType("text");

                    b.Property<Guid>("PostID")
                        .HasColumnType("uuid");

                    b.HasKey("TagValue");

                    b.HasIndex("PostID");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("ExtrosServer.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("BOD")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Bio")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LastLoginDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("integer");

                    b.Property<int>("PostalCode")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserFieldId")
                        .HasColumnType("uuid");

                    b.Property<string>("UserImage")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Verified")
                        .HasColumnType("boolean");

                    b.HasKey("UserId");

                    b.HasIndex("UserFieldId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ExtrosServer.Comment", b =>
                {
                    b.HasOne("ExtrosServer.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExtrosServer.Comment", "ReplyToComment")
                        .WithMany("Replies")
                        .HasForeignKey("ReplyToCommentID");

                    b.HasOne("ExtrosServer.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("ReplyToComment");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ExtrosServer.Like", b =>
                {
                    b.HasOne("ExtrosServer.Post", "Post")
                        .WithMany("Likes")
                        .HasForeignKey("PostID1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");
                });

            modelBuilder.Entity("ExtrosServer.Models.Article", b =>
                {
                    b.HasOne("ExtrosServer.User", "Admin")
                        .WithMany()
                        .HasForeignKey("AdminId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Admin");
                });

            modelBuilder.Entity("ExtrosServer.Models.Course", b =>
                {
                    b.HasOne("ExtrosServer.User", "Admin")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Admin");
                });

            modelBuilder.Entity("ExtrosServer.Models.UserFollow", b =>
                {
                    b.HasOne("ExtrosServer.User", "Follower")
                        .WithMany()
                        .HasForeignKey("FollowerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExtrosServer.User", "Following")
                        .WithMany()
                        .HasForeignKey("FollowingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Follower");

                    b.Navigation("Following");
                });

            modelBuilder.Entity("ExtrosServer.Post", b =>
                {
                    b.HasOne("ExtrosServer.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("ExtrosServer.Tag", b =>
                {
                    b.HasOne("ExtrosServer.Post", "Post")
                        .WithMany("Tags")
                        .HasForeignKey("PostID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");
                });

            modelBuilder.Entity("ExtrosServer.User", b =>
                {
                    b.HasOne("ExtrosServer.Field", "UserField")
                        .WithMany()
                        .HasForeignKey("UserFieldId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserField");
                });

            modelBuilder.Entity("ExtrosServer.Comment", b =>
                {
                    b.Navigation("Replies");
                });

            modelBuilder.Entity("ExtrosServer.Post", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Likes");

                    b.Navigation("Tags");
                });
#pragma warning restore 612, 618
        }
    }
}
