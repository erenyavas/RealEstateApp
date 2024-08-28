using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateApp.Migrations
{
    /// <inheritdoc />
    public partial class AddCityAndDistrictToRealEstate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "RealEstates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DistrictId",
                table: "RealEstates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RealEstates_CityId",
                table: "RealEstates",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_RealEstates_DistrictId",
                table: "RealEstates",
                column: "DistrictId");

            migrationBuilder.AddForeignKey(
                name: "FK_RealEstates_Cities_CityId",
                table: "RealEstates",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RealEstates_Districts_DistrictId",
                table: "RealEstates",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RealEstates_Cities_CityId",
                table: "RealEstates");

            migrationBuilder.DropForeignKey(
                name: "FK_RealEstates_Districts_DistrictId",
                table: "RealEstates");

            migrationBuilder.DropIndex(
                name: "IX_RealEstates_CityId",
                table: "RealEstates");

            migrationBuilder.DropIndex(
                name: "IX_RealEstates_DistrictId",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "RealEstates");
        }
    }
}
