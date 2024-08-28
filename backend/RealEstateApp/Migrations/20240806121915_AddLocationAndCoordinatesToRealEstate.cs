using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateApp.Migrations
{
    /// <inheritdoc />
    public partial class AddLocationAndCoordinatesToRealEstate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NeighborhoodId",
                table: "RealEstates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "X",
                table: "RealEstates",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Y",
                table: "RealEstates",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Neighborhoods",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Districts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Cities",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_RealEstates_NeighborhoodId",
                table: "RealEstates",
                column: "NeighborhoodId");

            migrationBuilder.AddForeignKey(
                name: "FK_RealEstates_Neighborhoods_NeighborhoodId",
                table: "RealEstates",
                column: "NeighborhoodId",
                principalTable: "Neighborhoods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RealEstates_Neighborhoods_NeighborhoodId",
                table: "RealEstates");

            migrationBuilder.DropIndex(
                name: "IX_RealEstates_NeighborhoodId",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "NeighborhoodId",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "X",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "Y",
                table: "RealEstates");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Neighborhoods",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Districts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Cities",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
