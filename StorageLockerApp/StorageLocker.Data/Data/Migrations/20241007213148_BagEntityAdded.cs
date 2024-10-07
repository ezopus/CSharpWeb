using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StorageLocker.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class BagEntityAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("23ef67df-365b-4e4c-a04e-bc6f4804d9a7"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("6c9fa0d2-bc52-4161-ba20-39c9bd483258"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("80844f88-9aba-4dfa-9f28-1c1bf82fdd96"));

            migrationBuilder.CreateTable(
                name: "Bags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BagType = table.Column<int>(type: "int", nullable: false),
                    BagSize = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bags_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "37ffcbaf-96bf-4938-8f51-4c174844451f", 0, "070efa94-248a-4947-bb97-a76a2dbdd0c2", "ApplicationUser", "elena@kovacheva.com", false, "Elena", "Kovacheva", false, null, null, null, null, "+359877554423", false, "b4c2dace-5217-4a9b-8b40-6fb96d22bd91", false, null },
                    { "e1f8eb68-4899-4f1b-9b55-a6a467dfa398", 0, "ed9a2642-b82b-4d2c-a233-b48e8f54641c", "ApplicationUser", "georgi@kovachev.com", false, "Georgi", "Kovachev", false, null, null, null, null, "+359888123456", false, "432fb5c4-f70f-41f3-ad1d-08522bf1d882", false, null },
                    { "fba496cd-628d-462a-beb8-0571b1a63eb3", 0, "70cb83b1-20ba-413a-93d0-b436e6265e45", "ApplicationUser", null, false, "Genadii", "Krokodilov", false, null, null, null, null, null, false, "076dbe87-175f-4a98-bc84-d5bffa6a9ed8", false, null }
                });

            migrationBuilder.InsertData(
                table: "Bags",
                columns: new[] { "Id", "BagSize", "BagType", "CustomerId" },
                values: new object[,]
                {
                    { new Guid("181fd2ad-695c-4e0a-891a-33b0409f2943"), 3, 3, "37ffcbaf-96bf-4938-8f51-4c174844451f" },
                    { new Guid("21f24817-c6d5-4383-b4fe-119cbdfc60f8"), 5, 4, "e1f8eb68-4899-4f1b-9b55-a6a467dfa398" },
                    { new Guid("a3cc3b42-73c8-43e1-ba39-9c93ad77c110"), 2, 2, "fba496cd-628d-462a-beb8-0571b1a63eb3" }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "Address", "Details", "HasFreeLockers", "ManagerName", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("5ae935ef-01de-445f-9d36-ac800b1a118e"), "ul. Maria Luiza 32", null, true, null, "Leksi", "+359888123321" },
                    { new Guid("f07097a8-5bec-463b-82ef-9ab853bb6d53"), "ul. Rayko Daskalov 103", null, true, null, "Billa", "+359888188842" },
                    { new Guid("f97d32ed-0f7b-4c38-91d6-39fd7929f840"), "pl. Centralen 1", null, false, null, "Store Bags Here", "+359898977977" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bags_CustomerId",
                table: "Bags",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bags");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "14ba5e7d-49b4-434e-8310-89cbe05c5df9");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9a783b96-d171-4fc2-ae47-597478ed77bd");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b5285880-af24-40ec-8560-89dbd97deeba");

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("5ae935ef-01de-445f-9d36-ac800b1a118e"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("f07097a8-5bec-463b-82ef-9ab853bb6d53"));

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "Id",
                keyValue: new Guid("f97d32ed-0f7b-4c38-91d6-39fd7929f840"));

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
    }
}
