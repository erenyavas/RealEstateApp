using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateApp.Migrations
{
    /// <inheritdoc />
    public partial class AddRealEstateDynamicFeaturesWithOptionsAndPhotos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Photos",
                table: "RealEstates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photos",
                table: "RealEstates");
        }
    }
}
