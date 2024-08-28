using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateApp.Migrations
{
    /// <inheritdoc />
    public partial class AddOwnerToRealEstate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "RealEstates",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_RealEstates_OwnerId",
                table: "RealEstates",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_RealEstates_AspNetUsers_OwnerId",
                table: "RealEstates",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RealEstates_AspNetUsers_OwnerId",
                table: "RealEstates");

            migrationBuilder.DropIndex(
                name: "IX_RealEstates_OwnerId",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "RealEstates");
        }
    }
}
