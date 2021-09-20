using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ParkingEntity.Migrations
{
    public partial class intial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ParkingRate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", nullable: true),
                    IsValid = table.Column<bool>(type: "bit", nullable: false),
                    RateType = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingRate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParkingRateRule",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParkingRateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RuleName = table.Column<string>(type: "varchar(255)", nullable: true),
                    Rate = table.Column<double>(type: "float", nullable: false),
                    IsValid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingRateRule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParkingRateRule_ParkingRate_ParkingRateId",
                        column: x => x.ParkingRateId,
                        principalTable: "ParkingRate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParkingRateRuleDefinition",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParkingRateRuleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RateCondition = table.Column<string>(type: "varchar(255)", nullable: false),
                    RateLinkingCondition = table.Column<string>(type: "varchar(255)", nullable: false),
                    RateComparisionCondition = table.Column<string>(type: "varchar(255)", nullable: false),
                    ComparisonValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RateValueComparisonType = table.Column<string>(type: "varchar(255)", nullable: false),
                    RuleDefinitionOrder = table.Column<int>(type: "int", nullable: false),
                    IsConverToDateAndCheck = table.Column<bool>(type: "bit", nullable: false),
                    IsEndDateToBeAddedToOneDay = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingRateRuleDefinition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParkingRateRuleDefinition_ParkingRateRule_ParkingRateRuleId",
                        column: x => x.ParkingRateRuleId,
                        principalTable: "ParkingRateRule",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ParkingRate",
                columns: new[] { "Id", "IsValid", "Name", "RateType" },
                values: new object[,]
                {
                    { new Guid("eee75372-941f-47d8-8549-e02c9f4c0b1d"), true, "StandardRate", "HourlyRate" },
                    { new Guid("70043bb6-5e5e-4074-835c-bffa2a9dd5f5"), true, "NightRate", "FlatRate" },
                    { new Guid("be58c81e-2663-40c1-b5bc-98542645fe4d"), true, "EarlyBirdRate", "FlatRate" },
                    { new Guid("ed78bae5-a977-4d17-acda-c384f31f928f"), true, "WeekdEndRate", "FlatRate" }
                });

            migrationBuilder.InsertData(
                table: "ParkingRateRule",
                columns: new[] { "Id", "IsValid", "ParkingRateId", "Rate", "RuleName" },
                values: new object[,]
                {
                    { new Guid("6d72f04d-0f25-40e7-b912-95143e1bf259"), true, new Guid("eee75372-941f-47d8-8549-e02c9f4c0b1d"), 5.0, "OneHourRateRule" },
                    { new Guid("43dc0571-87ed-4bd5-abff-1eb289f6f48e"), true, new Guid("eee75372-941f-47d8-8549-e02c9f4c0b1d"), 10.0, "TwoHourRateRule" },
                    { new Guid("eef5d09a-0c66-437a-9889-be78d3d6db01"), true, new Guid("eee75372-941f-47d8-8549-e02c9f4c0b1d"), 15.0, "ThreeHourRateRule" },
                    { new Guid("de2c0d91-64f5-487e-ba48-daa4f5f9d7ae"), true, new Guid("eee75372-941f-47d8-8549-e02c9f4c0b1d"), 20.0, "ThreePlusHourRateRule" },
                    { new Guid("52fb9da8-d34e-44ed-a31c-432cc9cbc7fa"), true, new Guid("70043bb6-5e5e-4074-835c-bffa2a9dd5f5"), 6.5, "NightRateRule" },
                    { new Guid("4749ae4a-bfef-419a-8e77-b7660fd8dde3"), true, new Guid("be58c81e-2663-40c1-b5bc-98542645fe4d"), 13.0, "EarlyBirdRateRule" },
                    { new Guid("906259c8-ebc8-457d-9f1f-0bafbcf97cd7"), true, new Guid("ed78bae5-a977-4d17-acda-c384f31f928f"), 10.0, "WeekEndRateRule" }
                });

            migrationBuilder.InsertData(
                table: "ParkingRateRuleDefinition",
                columns: new[] { "Id", "ComparisonValue", "IsConverToDateAndCheck", "IsEndDateToBeAddedToOneDay", "ParkingRateRuleId", "RateComparisionCondition", "RateCondition", "RateLinkingCondition", "RateValueComparisonType", "RuleDefinitionOrder" },
                values: new object[,]
                {
                    { new Guid("b8bc2dd9-fe64-4788-93ce-7cae021de630"), "1", false, false, new Guid("6d72f04d-0f25-40e7-b912-95143e1bf259"), "LessThanOrEqualTo", "ParkingDuration", "None", "RateDouble", 1 },
                    { new Guid("e0da2e8d-d142-4e8e-93ca-a7c46564de46"), "23:30", false, false, new Guid("4749ae4a-bfef-419a-8e77-b7660fd8dde3"), "LessThanOrEqualTo", "EndDateEndTime", "And", "RateTimeSpan", 4 },
                    { new Guid("ec7ee784-f033-4be5-a228-28e18ca3e2b7"), "15:30", false, false, new Guid("4749ae4a-bfef-419a-8e77-b7660fd8dde3"), "GreaterThanOrEqualTo", "EndDateStartTime", "And", "RateTimeSpan", 3 },
                    { new Guid("b815ece2-3152-4f5b-849c-ef2cc76b333d"), "9:00", false, false, new Guid("4749ae4a-bfef-419a-8e77-b7660fd8dde3"), "LessThanOrEqualTo", "StartDateEndTime", "And", "RateTimeSpan", 2 },
                    { new Guid("2c99db14-fca3-427b-9bed-f708c28dd730"), "6:00", false, false, new Guid("4749ae4a-bfef-419a-8e77-b7660fd8dde3"), "GreaterThanOrEqualTo", "StartDateStartTime", "And", "RateTimeSpan", 1 },
                    { new Guid("84a6178d-7765-4a12-967b-eceaf0a18a92"), "true", false, false, new Guid("52fb9da8-d34e-44ed-a31c-432cc9cbc7fa"), "EqualTo", "IsWeekDayEndDate", "And", "IsWeekDay", 4 },
                    { new Guid("d582c136-949a-4c36-9282-cc456deeb39b"), "true", false, false, new Guid("52fb9da8-d34e-44ed-a31c-432cc9cbc7fa"), "EqualTo", "IsWeekDayStartDate", "And", "IsWeekDay", 4 },
                    { new Guid("cba196b0-b62b-40d9-bcd1-0156e2de98e1"), "true", false, false, new Guid("906259c8-ebc8-457d-9f1f-0bafbcf97cd7"), "EqualTo", "IsWeekendStartDate", "And", "IsWeekend", 1 },
                    { new Guid("9f536b32-f978-4905-a0d1-321b3f48ffb4"), "08:00", true, true, new Guid("52fb9da8-d34e-44ed-a31c-432cc9cbc7fa"), "LessThanOrEqualTo", "EndDateEndTime", "And", "RateTimeSpan", 3 },
                    { new Guid("de0ba9b7-5acf-40df-823f-016f719f5548"), "18:00", false, false, new Guid("52fb9da8-d34e-44ed-a31c-432cc9cbc7fa"), "GreaterThanOrEqualTo", "StartDateStartTime", "And", "RateTimeSpan", 1 },
                    { new Guid("75ae4d36-97dc-4f2c-a8ed-083e620a6cf4"), "3", false, false, new Guid("de2c0d91-64f5-487e-ba48-daa4f5f9d7ae"), "GreaterThanOrEqualTo", "ParkingDuration", "None", "RateDouble", 1 },
                    { new Guid("395b2d81-09ac-4fa1-a0be-efe94e6b10c9"), "3", false, false, new Guid("eef5d09a-0c66-437a-9889-be78d3d6db01"), "LessThanOrEqualTo", "ParkingDuration", "And", "RateDouble", 2 },
                    { new Guid("3ac48a3a-e79d-4d11-8d88-a3ffe282b641"), "2", false, false, new Guid("eef5d09a-0c66-437a-9889-be78d3d6db01"), "GreaterThanOrEqualTo", "ParkingDuration", "And", "RateDouble", 1 },
                    { new Guid("e2a1335d-b704-452e-a60f-0e5ceccb79b2"), "2", false, false, new Guid("43dc0571-87ed-4bd5-abff-1eb289f6f48e"), "LessThanOrEqualTo", "ParkingDuration", "And", "RateDouble", 2 },
                    { new Guid("f3fad5f7-8045-4384-86d9-bcea3dfa1fc8"), "1", false, false, new Guid("43dc0571-87ed-4bd5-abff-1eb289f6f48e"), "GreaterThanOrEqualTo", "ParkingDuration", "And", "RateDouble", 1 },
                    { new Guid("1858894c-c16c-4bd2-af3c-aa182c46351f"), "23:59", true, true, new Guid("52fb9da8-d34e-44ed-a31c-432cc9cbc7fa"), "LessThanOrEqualTo", "StartDateEndTime", "And", "RateTimeSpan", 2 },
                    { new Guid("a01d8e8f-9608-40be-847f-8624f0caee83"), "true", false, false, new Guid("906259c8-ebc8-457d-9f1f-0bafbcf97cd7"), "EqualTo", "IsWeekendEndate", "And", "IsWeekend", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParkingRateRule_ParkingRateId",
                table: "ParkingRateRule",
                column: "ParkingRateId");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingRateRuleDefinition_ParkingRateRuleId",
                table: "ParkingRateRuleDefinition",
                column: "ParkingRateRuleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParkingRateRuleDefinition");

            migrationBuilder.DropTable(
                name: "ParkingRateRule");

            migrationBuilder.DropTable(
                name: "ParkingRate");
        }
    }
}
