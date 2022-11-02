﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyCollection.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MyCollection.Data.Migrations
{
    [DbContext(typeof(MyCollectionContext))]
    [Migration("20221102230108_Add_Level_Into_Location")]
    partial class Add_Level_Into_Location
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MyCollection.Domain.Entities.CollectionItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Autor")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Edition")
                        .HasColumnType("varchar(100)");

                    b.Property<int>("ItemType")
                        .HasColumnType("integer");

                    b.Property<Guid?>("LocationId")
                        .HasColumnType("uuid");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("CollectionItems");
                });

            modelBuilder.Entity("MyCollection.Domain.Entities.Contact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CollectionItemId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("CollectionItemId");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("MyCollection.Domain.Entities.Location", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Initials")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<int>("Level")
                        .HasColumnType("integer");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdateAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("MyCollection.Domain.Entities.CollectionItem", b =>
                {
                    b.HasOne("MyCollection.Domain.Entities.Location", "Location")
                        .WithMany("CollectionItems")
                        .HasForeignKey("LocationId");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("MyCollection.Domain.Entities.Contact", b =>
                {
                    b.HasOne("MyCollection.Domain.Entities.CollectionItem", null)
                        .WithMany("Contacts")
                        .HasForeignKey("CollectionItemId");
                });

            modelBuilder.Entity("MyCollection.Domain.Entities.Location", b =>
                {
                    b.HasOne("MyCollection.Domain.Entities.Location", "Parent")
                        .WithMany("Childrens")
                        .HasForeignKey("ParentId");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("MyCollection.Domain.Entities.CollectionItem", b =>
                {
                    b.Navigation("Contacts");
                });

            modelBuilder.Entity("MyCollection.Domain.Entities.Location", b =>
                {
                    b.Navigation("Childrens");

                    b.Navigation("CollectionItems");
                });
#pragma warning restore 612, 618
        }
    }
}
