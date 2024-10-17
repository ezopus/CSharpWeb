using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoftUniBazar.Migrations
{
    /// <inheritdoc />
    public partial class DbUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ad_AspNetUsers_OwnerId",
                table: "Ad");

            migrationBuilder.DropForeignKey(
                name: "FK_Ad_Category_CategoryId",
                table: "Ad");

            migrationBuilder.DropForeignKey(
                name: "FK_AdBuyer_Ad_AdId",
                table: "AdBuyer");

            migrationBuilder.DropForeignKey(
                name: "FK_AdBuyer_AspNetUsers_BuyerId",
                table: "AdBuyer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdBuyer",
                table: "AdBuyer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ad",
                table: "Ad");

            migrationBuilder.RenameTable(
                name: "AdBuyer",
                newName: "AdsBuyers");

            migrationBuilder.RenameTable(
                name: "Ad",
                newName: "Ads");

            migrationBuilder.RenameIndex(
                name: "IX_AdBuyer_BuyerId",
                table: "AdsBuyers",
                newName: "IX_AdsBuyers_BuyerId");

            migrationBuilder.RenameIndex(
                name: "IX_Ad_OwnerId",
                table: "Ads",
                newName: "IX_Ads_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Ad_CategoryId",
                table: "Ads",
                newName: "IX_Ads_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdsBuyers",
                table: "AdsBuyers",
                columns: new[] { "AdId", "BuyerId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ads",
                table: "Ads",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ads_AspNetUsers_OwnerId",
                table: "Ads",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ads_Category_CategoryId",
                table: "Ads",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AdsBuyers_Ads_AdId",
                table: "AdsBuyers",
                column: "AdId",
                principalTable: "Ads",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AdsBuyers_AspNetUsers_BuyerId",
                table: "AdsBuyers",
                column: "BuyerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ads_AspNetUsers_OwnerId",
                table: "Ads");

            migrationBuilder.DropForeignKey(
                name: "FK_Ads_Category_CategoryId",
                table: "Ads");

            migrationBuilder.DropForeignKey(
                name: "FK_AdsBuyers_Ads_AdId",
                table: "AdsBuyers");

            migrationBuilder.DropForeignKey(
                name: "FK_AdsBuyers_AspNetUsers_BuyerId",
                table: "AdsBuyers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdsBuyers",
                table: "AdsBuyers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ads",
                table: "Ads");

            migrationBuilder.RenameTable(
                name: "AdsBuyers",
                newName: "AdBuyer");

            migrationBuilder.RenameTable(
                name: "Ads",
                newName: "Ad");

            migrationBuilder.RenameIndex(
                name: "IX_AdsBuyers_BuyerId",
                table: "AdBuyer",
                newName: "IX_AdBuyer_BuyerId");

            migrationBuilder.RenameIndex(
                name: "IX_Ads_OwnerId",
                table: "Ad",
                newName: "IX_Ad_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Ads_CategoryId",
                table: "Ad",
                newName: "IX_Ad_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdBuyer",
                table: "AdBuyer",
                columns: new[] { "AdId", "BuyerId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ad",
                table: "Ad",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ad_AspNetUsers_OwnerId",
                table: "Ad",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ad_Category_CategoryId",
                table: "Ad",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AdBuyer_Ad_AdId",
                table: "AdBuyer",
                column: "AdId",
                principalTable: "Ad",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AdBuyer_AspNetUsers_BuyerId",
                table: "AdBuyer",
                column: "BuyerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
