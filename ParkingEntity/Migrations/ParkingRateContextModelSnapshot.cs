﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ParkingEntity.Context;

namespace ParkingEntity.Migrations
{
    [DbContext(typeof(ParkingRateContext))]
    partial class ParkingRateContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ParkingEntity.Entity.ParkingRate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsValid")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RateType")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.ToTable("ParkingRate");

                    b.HasData(
                        new
                        {
                            Id = new Guid("eee75372-941f-47d8-8549-e02c9f4c0b1d"),
                            IsValid = true,
                            Name = "StandardRate",
                            RateType = "HourlyRate"
                        },
                        new
                        {
                            Id = new Guid("70043bb6-5e5e-4074-835c-bffa2a9dd5f5"),
                            IsValid = true,
                            Name = "NightRate",
                            RateType = "FlatRate"
                        },
                        new
                        {
                            Id = new Guid("be58c81e-2663-40c1-b5bc-98542645fe4d"),
                            IsValid = true,
                            Name = "EarlyBirdRate",
                            RateType = "FlatRate"
                        },
                        new
                        {
                            Id = new Guid("ed78bae5-a977-4d17-acda-c384f31f928f"),
                            IsValid = true,
                            Name = "WeekdEndRate",
                            RateType = "FlatRate"
                        });
                });

            modelBuilder.Entity("ParkingEntity.Entity.ParkingRateRule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsValid")
                        .HasColumnType("bit");

                    b.Property<Guid>("ParkingRateId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Rate")
                        .HasColumnType("float");

                    b.Property<string>("RuleName")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("ParkingRateId");

                    b.ToTable("ParkingRateRule");

                    b.HasData(
                        new
                        {
                            Id = new Guid("6d72f04d-0f25-40e7-b912-95143e1bf259"),
                            IsValid = true,
                            ParkingRateId = new Guid("eee75372-941f-47d8-8549-e02c9f4c0b1d"),
                            Rate = 5.0,
                            RuleName = "OneHourRateRule"
                        },
                        new
                        {
                            Id = new Guid("43dc0571-87ed-4bd5-abff-1eb289f6f48e"),
                            IsValid = true,
                            ParkingRateId = new Guid("eee75372-941f-47d8-8549-e02c9f4c0b1d"),
                            Rate = 10.0,
                            RuleName = "TwoHourRateRule"
                        },
                        new
                        {
                            Id = new Guid("eef5d09a-0c66-437a-9889-be78d3d6db01"),
                            IsValid = true,
                            ParkingRateId = new Guid("eee75372-941f-47d8-8549-e02c9f4c0b1d"),
                            Rate = 15.0,
                            RuleName = "ThreeHourRateRule"
                        },
                        new
                        {
                            Id = new Guid("de2c0d91-64f5-487e-ba48-daa4f5f9d7ae"),
                            IsValid = true,
                            ParkingRateId = new Guid("eee75372-941f-47d8-8549-e02c9f4c0b1d"),
                            Rate = 20.0,
                            RuleName = "ThreePlusHourRateRule"
                        },
                        new
                        {
                            Id = new Guid("52fb9da8-d34e-44ed-a31c-432cc9cbc7fa"),
                            IsValid = true,
                            ParkingRateId = new Guid("70043bb6-5e5e-4074-835c-bffa2a9dd5f5"),
                            Rate = 6.5,
                            RuleName = "NightRateRule"
                        },
                        new
                        {
                            Id = new Guid("4749ae4a-bfef-419a-8e77-b7660fd8dde3"),
                            IsValid = true,
                            ParkingRateId = new Guid("be58c81e-2663-40c1-b5bc-98542645fe4d"),
                            Rate = 13.0,
                            RuleName = "EarlyBirdRateRule"
                        },
                        new
                        {
                            Id = new Guid("906259c8-ebc8-457d-9f1f-0bafbcf97cd7"),
                            IsValid = true,
                            ParkingRateId = new Guid("ed78bae5-a977-4d17-acda-c384f31f928f"),
                            Rate = 10.0,
                            RuleName = "WeekEndRateRule"
                        });
                });

            modelBuilder.Entity("ParkingEntity.Entity.ParkingRateRuleDefinition", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ComparisonValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsConverToDateAndCheck")
                        .HasColumnType("bit");

                    b.Property<bool>("IsEndDateToBeAddedToOneDay")
                        .HasColumnType("bit");

                    b.Property<Guid>("ParkingRateRuleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RateComparisionCondition")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RateCondition")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RateLinkingCondition")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RateValueComparisonType")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<int>("RuleDefinitionOrder")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParkingRateRuleId");

                    b.ToTable("ParkingRateRuleDefinition");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b8bc2dd9-fe64-4788-93ce-7cae021de630"),
                            ComparisonValue = "1",
                            IsConverToDateAndCheck = false,
                            IsEndDateToBeAddedToOneDay = false,
                            ParkingRateRuleId = new Guid("6d72f04d-0f25-40e7-b912-95143e1bf259"),
                            RateComparisionCondition = "LessThanOrEqualTo",
                            RateCondition = "ParkingDuration",
                            RateLinkingCondition = "None",
                            RateValueComparisonType = "RateDouble",
                            RuleDefinitionOrder = 1
                        },
                        new
                        {
                            Id = new Guid("f3fad5f7-8045-4384-86d9-bcea3dfa1fc8"),
                            ComparisonValue = "1",
                            IsConverToDateAndCheck = false,
                            IsEndDateToBeAddedToOneDay = false,
                            ParkingRateRuleId = new Guid("43dc0571-87ed-4bd5-abff-1eb289f6f48e"),
                            RateComparisionCondition = "GreaterThanOrEqualTo",
                            RateCondition = "ParkingDuration",
                            RateLinkingCondition = "And",
                            RateValueComparisonType = "RateDouble",
                            RuleDefinitionOrder = 1
                        },
                        new
                        {
                            Id = new Guid("e2a1335d-b704-452e-a60f-0e5ceccb79b2"),
                            ComparisonValue = "2",
                            IsConverToDateAndCheck = false,
                            IsEndDateToBeAddedToOneDay = false,
                            ParkingRateRuleId = new Guid("43dc0571-87ed-4bd5-abff-1eb289f6f48e"),
                            RateComparisionCondition = "LessThanOrEqualTo",
                            RateCondition = "ParkingDuration",
                            RateLinkingCondition = "And",
                            RateValueComparisonType = "RateDouble",
                            RuleDefinitionOrder = 2
                        },
                        new
                        {
                            Id = new Guid("3ac48a3a-e79d-4d11-8d88-a3ffe282b641"),
                            ComparisonValue = "2",
                            IsConverToDateAndCheck = false,
                            IsEndDateToBeAddedToOneDay = false,
                            ParkingRateRuleId = new Guid("eef5d09a-0c66-437a-9889-be78d3d6db01"),
                            RateComparisionCondition = "GreaterThanOrEqualTo",
                            RateCondition = "ParkingDuration",
                            RateLinkingCondition = "And",
                            RateValueComparisonType = "RateDouble",
                            RuleDefinitionOrder = 1
                        },
                        new
                        {
                            Id = new Guid("395b2d81-09ac-4fa1-a0be-efe94e6b10c9"),
                            ComparisonValue = "3",
                            IsConverToDateAndCheck = false,
                            IsEndDateToBeAddedToOneDay = false,
                            ParkingRateRuleId = new Guid("eef5d09a-0c66-437a-9889-be78d3d6db01"),
                            RateComparisionCondition = "LessThanOrEqualTo",
                            RateCondition = "ParkingDuration",
                            RateLinkingCondition = "And",
                            RateValueComparisonType = "RateDouble",
                            RuleDefinitionOrder = 2
                        },
                        new
                        {
                            Id = new Guid("75ae4d36-97dc-4f2c-a8ed-083e620a6cf4"),
                            ComparisonValue = "3",
                            IsConverToDateAndCheck = false,
                            IsEndDateToBeAddedToOneDay = false,
                            ParkingRateRuleId = new Guid("de2c0d91-64f5-487e-ba48-daa4f5f9d7ae"),
                            RateComparisionCondition = "GreaterThanOrEqualTo",
                            RateCondition = "ParkingDuration",
                            RateLinkingCondition = "None",
                            RateValueComparisonType = "RateDouble",
                            RuleDefinitionOrder = 1
                        },
                        new
                        {
                            Id = new Guid("de0ba9b7-5acf-40df-823f-016f719f5548"),
                            ComparisonValue = "18:00",
                            IsConverToDateAndCheck = false,
                            IsEndDateToBeAddedToOneDay = false,
                            ParkingRateRuleId = new Guid("52fb9da8-d34e-44ed-a31c-432cc9cbc7fa"),
                            RateComparisionCondition = "GreaterThanOrEqualTo",
                            RateCondition = "StartDateStartTime",
                            RateLinkingCondition = "And",
                            RateValueComparisonType = "RateTimeSpan",
                            RuleDefinitionOrder = 1
                        },
                        new
                        {
                            Id = new Guid("1858894c-c16c-4bd2-af3c-aa182c46351f"),
                            ComparisonValue = "23:59",
                            IsConverToDateAndCheck = true,
                            IsEndDateToBeAddedToOneDay = true,
                            ParkingRateRuleId = new Guid("52fb9da8-d34e-44ed-a31c-432cc9cbc7fa"),
                            RateComparisionCondition = "LessThanOrEqualTo",
                            RateCondition = "StartDateEndTime",
                            RateLinkingCondition = "And",
                            RateValueComparisonType = "RateTimeSpan",
                            RuleDefinitionOrder = 2
                        },
                        new
                        {
                            Id = new Guid("9f536b32-f978-4905-a0d1-321b3f48ffb4"),
                            ComparisonValue = "08:00",
                            IsConverToDateAndCheck = true,
                            IsEndDateToBeAddedToOneDay = true,
                            ParkingRateRuleId = new Guid("52fb9da8-d34e-44ed-a31c-432cc9cbc7fa"),
                            RateComparisionCondition = "LessThanOrEqualTo",
                            RateCondition = "EndDateEndTime",
                            RateLinkingCondition = "And",
                            RateValueComparisonType = "RateTimeSpan",
                            RuleDefinitionOrder = 3
                        },
                        new
                        {
                            Id = new Guid("2c99db14-fca3-427b-9bed-f708c28dd730"),
                            ComparisonValue = "6:00",
                            IsConverToDateAndCheck = false,
                            IsEndDateToBeAddedToOneDay = false,
                            ParkingRateRuleId = new Guid("4749ae4a-bfef-419a-8e77-b7660fd8dde3"),
                            RateComparisionCondition = "GreaterThanOrEqualTo",
                            RateCondition = "StartDateStartTime",
                            RateLinkingCondition = "And",
                            RateValueComparisonType = "RateTimeSpan",
                            RuleDefinitionOrder = 1
                        },
                        new
                        {
                            Id = new Guid("b815ece2-3152-4f5b-849c-ef2cc76b333d"),
                            ComparisonValue = "9:00",
                            IsConverToDateAndCheck = false,
                            IsEndDateToBeAddedToOneDay = false,
                            ParkingRateRuleId = new Guid("4749ae4a-bfef-419a-8e77-b7660fd8dde3"),
                            RateComparisionCondition = "LessThanOrEqualTo",
                            RateCondition = "StartDateEndTime",
                            RateLinkingCondition = "And",
                            RateValueComparisonType = "RateTimeSpan",
                            RuleDefinitionOrder = 2
                        },
                        new
                        {
                            Id = new Guid("ec7ee784-f033-4be5-a228-28e18ca3e2b7"),
                            ComparisonValue = "15:30",
                            IsConverToDateAndCheck = false,
                            IsEndDateToBeAddedToOneDay = false,
                            ParkingRateRuleId = new Guid("4749ae4a-bfef-419a-8e77-b7660fd8dde3"),
                            RateComparisionCondition = "GreaterThanOrEqualTo",
                            RateCondition = "EndDateStartTime",
                            RateLinkingCondition = "And",
                            RateValueComparisonType = "RateTimeSpan",
                            RuleDefinitionOrder = 3
                        },
                        new
                        {
                            Id = new Guid("e0da2e8d-d142-4e8e-93ca-a7c46564de46"),
                            ComparisonValue = "23:30",
                            IsConverToDateAndCheck = false,
                            IsEndDateToBeAddedToOneDay = false,
                            ParkingRateRuleId = new Guid("4749ae4a-bfef-419a-8e77-b7660fd8dde3"),
                            RateComparisionCondition = "LessThanOrEqualTo",
                            RateCondition = "EndDateEndTime",
                            RateLinkingCondition = "And",
                            RateValueComparisonType = "RateTimeSpan",
                            RuleDefinitionOrder = 4
                        },
                        new
                        {
                            Id = new Guid("cba196b0-b62b-40d9-bcd1-0156e2de98e1"),
                            ComparisonValue = "true",
                            IsConverToDateAndCheck = false,
                            IsEndDateToBeAddedToOneDay = false,
                            ParkingRateRuleId = new Guid("906259c8-ebc8-457d-9f1f-0bafbcf97cd7"),
                            RateComparisionCondition = "EqualTo",
                            RateCondition = "IsWeekendStartDate",
                            RateLinkingCondition = "And",
                            RateValueComparisonType = "IsWeekend",
                            RuleDefinitionOrder = 1
                        },
                        new
                        {
                            Id = new Guid("a01d8e8f-9608-40be-847f-8624f0caee83"),
                            ComparisonValue = "true",
                            IsConverToDateAndCheck = false,
                            IsEndDateToBeAddedToOneDay = false,
                            ParkingRateRuleId = new Guid("906259c8-ebc8-457d-9f1f-0bafbcf97cd7"),
                            RateComparisionCondition = "EqualTo",
                            RateCondition = "IsWeekendEndate",
                            RateLinkingCondition = "And",
                            RateValueComparisonType = "IsWeekend",
                            RuleDefinitionOrder = 2
                        },
                        new
                        {
                            Id = new Guid("d582c136-949a-4c36-9282-cc456deeb39b"),
                            ComparisonValue = "true",
                            IsConverToDateAndCheck = false,
                            IsEndDateToBeAddedToOneDay = false,
                            ParkingRateRuleId = new Guid("52fb9da8-d34e-44ed-a31c-432cc9cbc7fa"),
                            RateComparisionCondition = "EqualTo",
                            RateCondition = "IsWeekDayStartDate",
                            RateLinkingCondition = "And",
                            RateValueComparisonType = "IsWeekDay",
                            RuleDefinitionOrder = 4
                        },
                        new
                        {
                            Id = new Guid("84a6178d-7765-4a12-967b-eceaf0a18a92"),
                            ComparisonValue = "true",
                            IsConverToDateAndCheck = false,
                            IsEndDateToBeAddedToOneDay = false,
                            ParkingRateRuleId = new Guid("52fb9da8-d34e-44ed-a31c-432cc9cbc7fa"),
                            RateComparisionCondition = "EqualTo",
                            RateCondition = "IsWeekDayEndDate",
                            RateLinkingCondition = "And",
                            RateValueComparisonType = "IsWeekDay",
                            RuleDefinitionOrder = 4
                        });
                });

            modelBuilder.Entity("ParkingEntity.Entity.ParkingRateRule", b =>
                {
                    b.HasOne("ParkingEntity.Entity.ParkingRate", "ParkingRate")
                        .WithMany("ParkingRateRule")
                        .HasForeignKey("ParkingRateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParkingRate");
                });

            modelBuilder.Entity("ParkingEntity.Entity.ParkingRateRuleDefinition", b =>
                {
                    b.HasOne("ParkingEntity.Entity.ParkingRateRule", "ParkingRateRule")
                        .WithMany("ParkingRateRuleDefinition")
                        .HasForeignKey("ParkingRateRuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParkingRateRule");
                });

            modelBuilder.Entity("ParkingEntity.Entity.ParkingRate", b =>
                {
                    b.Navigation("ParkingRateRule");
                });

            modelBuilder.Entity("ParkingEntity.Entity.ParkingRateRule", b =>
                {
                    b.Navigation("ParkingRateRuleDefinition");
                });
#pragma warning restore 612, 618
        }
    }
}