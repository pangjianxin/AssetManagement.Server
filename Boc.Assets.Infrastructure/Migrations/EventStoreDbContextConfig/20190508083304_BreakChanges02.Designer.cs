﻿// <auto-generated />
using System;
using Boc.Assets.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Boc.Assets.Infrastructure.migrations.eventstoredbcontextconfig
{
    [DbContext(typeof(EventStoreDbContext))]
    [Migration("20190508083304_BreakChanges02")]
    partial class BreakChanges02
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Boc.Assets.Domain.Events.NonAuditEvent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Org2")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<Guid>("OrgId");

                    b.Property<string>("OrgIdentifier")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("OrgNam")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("TimeStamp");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.ToTable("NonAuditEvents");
                });
#pragma warning restore 612, 618
        }
    }
}
