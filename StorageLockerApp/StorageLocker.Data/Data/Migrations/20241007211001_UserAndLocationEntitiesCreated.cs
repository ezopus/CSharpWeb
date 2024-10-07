using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StorageLocker.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserAndLocationEntitiesCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(85)",
                maxLength: 85,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManagerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    HasFreeLockers = table.Column<bool>(type: "bit", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "37ffcbaf-96bf-4938-8f51-4c174844451f", 0, "4cf050eb-4ed5-4ee4-aa3f-83669421919d", "ApplicationUser", "elena@kovacheva.com", false, "Elena", "Kovacheva", false, null, null, null, null, "+359877554423", false, "de5706a4-f503-49da-a83e-274b7bb22c28", false, null },
                    { "e1f8eb68-4899-4f1b-9b55-a6a467dfa398", 0, "d659e7cf-e9f9-438f-a7a6-222f6231b998", "ApplicationUser", "georgi@kovachev.com", false, "Georgi", "Kovachev", false, null, null, null, null, "+359888123456", false, "471e99b4-e4e0-4e54-a703-e30a1d424800", false, null },
                    { "fba496cd-628d-462a-beb8-0571b1a63eb3", 0, "cd154394-6573-4384-b90b-200ed04d7c6a", "ApplicationUser", null, false, "Genadii", "Krokodilov", false, null, null, null, null, null, false, "38c15687-4d70-419b-9d09-b8b59ebd444c", false, null }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "Address", "Details", "HasFreeLockers", "ManagerName", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("23ef67df-365b-4e4c-a04e-bc6f4804d9a7"), "ul. Rayko Daskalov 103", null, true, null, "Billa", "+359888188842" },
                    { new Guid("6c9fa0d2-bc52-4161-ba20-39c9bd483258"), "pl. Centralen 1", null, false, null, "Store Bags Here", "+359898977977" },
                    { new Guid("80844f88-9aba-4dfa-9f28-1c1bf82fdd96"), "ul. Maria Luiza 32", null, true, null, "Leksi", "+359888123321" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "37ffcbaf-96bf-4938-8f51-4c174844451f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e1f8eb68-4899-4f1b-9b55-a6a467dfa398");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fba496cd-628d-462a-beb8-0571b1a63eb3");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");
        }
    }
}
