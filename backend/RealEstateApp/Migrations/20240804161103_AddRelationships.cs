using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateApp.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "RealEstates");

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "RealEstates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RealEstateStatusId",
                table: "RealEstates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RealEstateTypeId",
                table: "RealEstates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RealEstates_CurrencyId",
                table: "RealEstates",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_RealEstates_RealEstateStatusId",
                table: "RealEstates",
                column: "RealEstateStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_RealEstates_RealEstateTypeId",
                table: "RealEstates",
                column: "RealEstateTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_RealEstates_Currencies_CurrencyId",
                table: "RealEstates",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RealEstates_RealEstateStatuses_RealEstateStatusId",
                table: "RealEstates",
                column: "RealEstateStatusId",
                principalTable: "RealEstateStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RealEstates_RealEstateTypes_RealEstateTypeId",
                table: "RealEstates",
                column: "RealEstateTypeId",
                principalTable: "RealEstateTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RealEstates_Currencies_CurrencyId",
                table: "RealEstates");

            migrationBuilder.DropForeignKey(
                name: "FK_RealEstates_RealEstateStatuses_RealEstateStatusId",
                table: "RealEstates");

            migrationBuilder.DropForeignKey(
                name: "FK_RealEstates_RealEstateTypes_RealEstateTypeId",
                table: "RealEstates");

            migrationBuilder.DropIndex(
                name: "IX_RealEstates_CurrencyId",
                table: "RealEstates");

            migrationBuilder.DropIndex(
                name: "IX_RealEstates_RealEstateStatusId",
                table: "RealEstates");

            migrationBuilder.DropIndex(
                name: "IX_RealEstates_RealEstateTypeId",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "RealEstateStatusId",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "RealEstateTypeId",
                table: "RealEstates");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "RealEstates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "RealEstates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
