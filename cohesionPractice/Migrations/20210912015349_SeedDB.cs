using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace cohesionPractice.Migrations
{
    public partial class SeedDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ServiceRequests",
                columns: new[] { "Id", "BuildingCode", "CreatedBy", "CreatedDate", "CurrentStatus", "Description", "LastModifiedBy", "LastModifiedDate" },
                values: new object[,]
                {
                    { new Guid("727b376b-79ae-498e-9cff-a9f51b848ea4"), "COH", "Ricky Martin", new DateTime(2019, 8, 1, 14, 1, 23, 1, DateTimeKind.Unspecified), 1, "Please turn up the AC in suite 1200D. It is too hot here.", "Eric Iglesias", new DateTime(2019, 9, 1, 14, 1, 23, 1, DateTimeKind.Unspecified) },
                    { new Guid("727b376b-79ae-498e-9cff-a9f51b848ea5"), "ACH", "John Denver", new DateTime(2020, 9, 1, 14, 1, 23, 122, DateTimeKind.Unspecified), 1, "Take me home country roads cause It is freezing cold in here! So turn down the AC!", "Jane Tech support", new DateTime(2020, 12, 1, 14, 1, 23, 1, DateTimeKind.Unspecified) },
                    { new Guid("727b376b-79ae-498e-9cff-a9f51b848ea6"), "ATC", "Mick Jager", new DateTime(2019, 8, 1, 14, 1, 23, 1, DateTimeKind.Unspecified), 1, "Music Box is broken", "Matt White", new DateTime(2019, 9, 1, 14, 1, 23, 1, DateTimeKind.Unspecified) },
                    { new Guid("727b376b-79ae-498e-9cff-a9f51b848ea7"), "AFC", "Elton John", new DateTime(2020, 9, 1, 14, 1, 23, 122, DateTimeKind.Unspecified), 2, "I can feel something in the air", "Jack Black", new DateTime(2020, 12, 1, 14, 1, 23, 1, DateTimeKind.Unspecified) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ServiceRequests",
                keyColumn: "Id",
                keyValue: new Guid("727b376b-79ae-498e-9cff-a9f51b848ea4"));

            migrationBuilder.DeleteData(
                table: "ServiceRequests",
                keyColumn: "Id",
                keyValue: new Guid("727b376b-79ae-498e-9cff-a9f51b848ea5"));

            migrationBuilder.DeleteData(
                table: "ServiceRequests",
                keyColumn: "Id",
                keyValue: new Guid("727b376b-79ae-498e-9cff-a9f51b848ea6"));

            migrationBuilder.DeleteData(
                table: "ServiceRequests",
                keyColumn: "Id",
                keyValue: new Guid("727b376b-79ae-498e-9cff-a9f51b848ea7"));
        }
    }
}
