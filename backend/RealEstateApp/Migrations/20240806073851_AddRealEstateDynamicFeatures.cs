using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateApp.Migrations
{
    /// <inheritdoc />
    public partial class AddRealEstateDynamicFeatures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photos",
                table: "RealEstates");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "RealEstateTypes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "DynamicFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DataType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MinValue = table.Column<int>(type: "int", nullable: true),
                    MaxValue = table.Column<int>(type: "int", nullable: true),
                    Options = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicFeatures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RealEstateFeatureValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RealEstateId = table.Column<int>(type: "int", nullable: false),
                    FeatureId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealEstateFeatureValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RealEstateFeatureValues_DynamicFeatures_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "DynamicFeatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RealEstateFeatureValues_RealEstates_RealEstateId",
                        column: x => x.RealEstateId,
                        principalTable: "RealEstates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RealEstateTypeFeatures",
                columns: table => new
                {
                    RealEstateTypeId = table.Column<int>(type: "int", nullable: false),
                    FeatureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealEstateTypeFeatures", x => new { x.RealEstateTypeId, x.FeatureId });
                    table.ForeignKey(
                        name: "FK_RealEstateTypeFeatures_DynamicFeatures_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "DynamicFeatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RealEstateTypeFeatures_RealEstateTypes_RealEstateTypeId",
                        column: x => x.RealEstateTypeId,
                        principalTable: "RealEstateTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RealEstateFeatureValues_FeatureId",
                table: "RealEstateFeatureValues",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_RealEstateFeatureValues_RealEstateId",
                table: "RealEstateFeatureValues",
                column: "RealEstateId");

            migrationBuilder.CreateIndex(
                name: "IX_RealEstateTypeFeatures_FeatureId",
                table: "RealEstateTypeFeatures",
                column: "FeatureId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RealEstateFeatureValues");

            migrationBuilder.DropTable(
                name: "RealEstateTypeFeatures");

            migrationBuilder.DropTable(
                name: "DynamicFeatures");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "RealEstateTypes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "Photos",
                table: "RealEstates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
