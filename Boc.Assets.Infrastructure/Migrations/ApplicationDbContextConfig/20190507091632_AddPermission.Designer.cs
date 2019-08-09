﻿// <auto-generated />
using System;
using Boc.Assets.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Boc.Assets.Infrastructure.migrations.applicationdbcontextconfig
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190507091632_AddPermission")]
    partial class AddPermission
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Boc.Assets.Domain.Models.AssetStockTakings.AssetStockTaking", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDateTime");

                    b.Property<DateTime>("ExpiryDateTime");

                    b.Property<string>("ManagementLineDescription")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<Guid>("ManagementLineId");

                    b.Property<string>("ManagementLineName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<Guid>("PublisherId");

                    b.Property<string>("PublisherIdentifier")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("PublisherName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("PublisherOrg2")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("TaskComment")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("TaskName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("AssetStockTakings");
                });

            modelBuilder.Entity("Boc.Assets.Domain.Models.AssetStockTakings.AssetStockTakingDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AssetId");

                    b.Property<string>("AssetStockTakingLocation")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<Guid>("AssetStockTakingOrganizationId");

                    b.Property<string>("ResponsibilityIdentity")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("ResponsibilityName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("ResponsibilityOrg2")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<int>("StockTakingStatus");

                    b.HasKey("Id");

                    b.HasIndex("AssetId");

                    b.HasIndex("AssetStockTakingOrganizationId");

                    b.ToTable("AssetStockTakingDetails");
                });

            modelBuilder.Entity("Boc.Assets.Domain.Models.AssetStockTakings.AssetStockTakingOrganization", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AssetStockTakingId");

                    b.Property<Guid>("OrganizationId");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("AssetStockTakingId", "OrganizationId")
                        .IsUnique();

                    b.ToTable("AssetStockTakingOrganization");
                });

            modelBuilder.Entity("Boc.Assets.Domain.Models.Assets.Asset", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AssetCategoryId");

                    b.Property<string>("AssetDescription")
                        .HasMaxLength(100);

                    b.Property<string>("AssetLocation");

                    b.Property<string>("AssetName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("AssetNo")
                        .HasMaxLength(100);

                    b.Property<int>("AssetStatus");

                    b.Property<string>("AssetTagNumber")
                        .HasMaxLength(50);

                    b.Property<string>("AssetType")
                        .HasMaxLength(100);

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime?>("CreateDateTime");

                    b.Property<DateTime?>("InStoreDateTime");

                    b.Property<string>("LastModifyComment")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("LastModifyDateTime");

                    b.Property<Guid?>("OrganizationId");

                    b.Property<string>("SerialNumber")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("AssetCategoryId");

                    b.HasIndex("OrganizationId");

                    b.ToTable("Assets");
                });

            modelBuilder.Entity("Boc.Assets.Domain.Models.Assets.AssetCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AssetFirstLevelCategory")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("AssetMeteringUnit");

                    b.Property<string>("AssetSecondLevelCategory")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("AssetThirdLevelCategory")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<Guid>("ManagementLineId");

                    b.HasKey("Id");

                    b.HasIndex("ManagementLineId");

                    b.ToTable("AssetCategories");
                });

            modelBuilder.Entity("Boc.Assets.Domain.Models.Assets.AssetDeploy", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AssetDeployCategory");

                    b.Property<string>("AssetName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("AssetTagNumber")
                        .HasMaxLength(100);

                    b.Property<DateTime>("CreateDateTime");

                    b.HasKey("Id");

                    b.ToTable("AssetDeploys");
                });

            modelBuilder.Entity("Boc.Assets.Domain.Models.Assets.Maintainer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AssetCategoryId");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("MaintainerName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("OfficePhone")
                        .HasMaxLength(20);

                    b.Property<string>("Org2")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<Guid>("OrganizationId");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.HasIndex("AssetCategoryId");

                    b.ToTable("Maintainers");
                });

            modelBuilder.Entity("Boc.Assets.Domain.Models.ManagementLines.ManagementLine", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDateTime");

                    b.Property<string>("ManagementLineDescription")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("ManagementLineName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("ManagementLine");
                });

            modelBuilder.Entity("Boc.Assets.Domain.Models.Organizations.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Identifier")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("OfficePhone")
                        .HasMaxLength(20);

                    b.Property<string>("Org2");

                    b.Property<string>("Telephone")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.HasIndex("Identifier")
                        .IsUnique();

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Boc.Assets.Domain.Models.Organizations.Organization", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ManagementLineId");

                    b.Property<string>("Org1")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("Org2")
                        .HasMaxLength(10);

                    b.Property<string>("Org3")
                        .HasMaxLength(10);

                    b.Property<string>("OrgIdentifier");

                    b.Property<string>("OrgLvl")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("OrgNam")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("OrgNam1")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("OrgNam2")
                        .HasMaxLength(100);

                    b.Property<string>("OrgNam3")
                        .HasMaxLength(100);

                    b.Property<string>("OrgShortNam")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Password");

                    b.Property<Guid>("RoleId");

                    b.Property<int>("Status");

                    b.Property<string>("UpOrg")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.HasIndex("ManagementLineId");

                    b.HasIndex("OrgIdentifier")
                        .IsUnique()
                        .HasFilter("[OrgIdentifier] IS NOT NULL");

                    b.HasIndex("RoleId");

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("Boc.Assets.Domain.Models.Organizations.OrganizationRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("RoleNam")
                        .IsRequired()
                        .HasColumnName("RoleNam")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("OrganizationRoles");
                });

            modelBuilder.Entity("Boc.Assets.Domain.Models.Organizations.OrganizationSpace", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("OrgId");

                    b.Property<string>("OrgIdentifier");

                    b.Property<string>("OrgName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("SpaceDescription")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("SpaceName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("OrgId");

                    b.ToTable("OrganizationSpaces");
                });

            modelBuilder.Entity("Boc.Assets.Domain.Models.Organizations.Permission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ActionName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("ControllerName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<Guid>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("Boc.Assets.Domain.Models.AssetStockTakings.AssetStockTakingDetail", b =>
                {
                    b.HasOne("Boc.Assets.Domain.Models.Assets.Asset", "Asset")
                        .WithMany("AssetStockTakingDetails")
                        .HasForeignKey("AssetId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Boc.Assets.Domain.Models.AssetStockTakings.AssetStockTakingOrganization", "AssetStockTakingOrganization")
                        .WithMany("AssetStockTakingDetails")
                        .HasForeignKey("AssetStockTakingOrganizationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Boc.Assets.Domain.Models.AssetStockTakings.AssetStockTakingOrganization", b =>
                {
                    b.HasOne("Boc.Assets.Domain.Models.AssetStockTakings.AssetStockTaking", "AssetStockTaking")
                        .WithMany("AssetStockTakingOrganizations")
                        .HasForeignKey("AssetStockTakingId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Boc.Assets.Domain.Models.Organizations.Organization", "Organization")
                        .WithMany("AssetStockTakingOrganizations")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Boc.Assets.Domain.Models.Assets.Asset", b =>
                {
                    b.HasOne("Boc.Assets.Domain.Models.Assets.AssetCategory", "AssetCategory")
                        .WithMany("Assets")
                        .HasForeignKey("AssetCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Boc.Assets.Domain.Models.Organizations.Organization", "Organization")
                        .WithMany("Assets")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("Boc.Assets.Domain.Models.Assets.AssetCategory", b =>
                {
                    b.HasOne("Boc.Assets.Domain.Models.ManagementLines.ManagementLine", "ManagementLine")
                        .WithMany("AssetCategories")
                        .HasForeignKey("ManagementLineId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Boc.Assets.Domain.Models.Assets.AssetDeploy", b =>
                {
                    b.OwnsOne("Boc.Assets.Domain.Models.Assets.OrganizationInfo", "AuthorizeOrgInfo", b1 =>
                        {
                            b1.Property<Guid>("AssetDeployId");

                            b1.Property<string>("Org2");

                            b1.Property<Guid>("OrgId");

                            b1.Property<string>("OrgIdentifier")
                                .IsRequired()
                                .HasMaxLength(20);

                            b1.Property<string>("OrgNam")
                                .IsRequired()
                                .HasMaxLength(50);

                            b1.HasKey("AssetDeployId");

                            b1.ToTable("AssetDeploys");

                            b1.HasOne("Boc.Assets.Domain.Models.Assets.AssetDeploy")
                                .WithOne("AuthorizeOrgInfo")
                                .HasForeignKey("Boc.Assets.Domain.Models.Assets.OrganizationInfo", "AssetDeployId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("Boc.Assets.Domain.Models.Assets.OrganizationInfo", "ExportOrgInfo", b1 =>
                        {
                            b1.Property<Guid>("AssetDeployId");

                            b1.Property<string>("Org2");

                            b1.Property<Guid>("OrgId");

                            b1.Property<string>("OrgIdentifier")
                                .IsRequired()
                                .HasMaxLength(20);

                            b1.Property<string>("OrgNam")
                                .IsRequired()
                                .HasMaxLength(50);

                            b1.HasKey("AssetDeployId");

                            b1.ToTable("AssetDeploys");

                            b1.HasOne("Boc.Assets.Domain.Models.Assets.AssetDeploy")
                                .WithOne("ExportOrgInfo")
                                .HasForeignKey("Boc.Assets.Domain.Models.Assets.OrganizationInfo", "AssetDeployId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("Boc.Assets.Domain.Models.Assets.OrganizationInfo", "ImportOrgInfo", b1 =>
                        {
                            b1.Property<Guid>("AssetDeployId");

                            b1.Property<string>("Org2");

                            b1.Property<Guid>("OrgId");

                            b1.Property<string>("OrgIdentifier")
                                .IsRequired()
                                .HasMaxLength(20);

                            b1.Property<string>("OrgNam")
                                .IsRequired()
                                .HasMaxLength(50);

                            b1.HasKey("AssetDeployId");

                            b1.ToTable("AssetDeploys");

                            b1.HasOne("Boc.Assets.Domain.Models.Assets.AssetDeploy")
                                .WithOne("ImportOrgInfo")
                                .HasForeignKey("Boc.Assets.Domain.Models.Assets.OrganizationInfo", "AssetDeployId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("Boc.Assets.Domain.Models.Assets.Maintainer", b =>
                {
                    b.HasOne("Boc.Assets.Domain.Models.Assets.AssetCategory", "AssetCategory")
                        .WithMany("Maintainers")
                        .HasForeignKey("AssetCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Boc.Assets.Domain.Models.Organizations.Organization", b =>
                {
                    b.HasOne("Boc.Assets.Domain.Models.ManagementLines.ManagementLine", "ManagementLine")
                        .WithMany("Organizations")
                        .HasForeignKey("ManagementLineId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Boc.Assets.Domain.Models.Organizations.OrganizationRole", "Role")
                        .WithMany("Organizations")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Boc.Assets.Domain.Models.Organizations.OrganizationSpace", b =>
                {
                    b.HasOne("Boc.Assets.Domain.Models.Organizations.Organization", "Organization")
                        .WithMany("OrganizationSpaces")
                        .HasForeignKey("OrgId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Boc.Assets.Domain.Models.Organizations.Permission", b =>
                {
                    b.HasOne("Boc.Assets.Domain.Models.Organizations.OrganizationRole", "OrganizationRole")
                        .WithMany("Permissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
