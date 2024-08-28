using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateApp.Migrations
{
    /// <inheritdoc />
    public partial class AddFeaturesCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "RealEstateTypes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.CreateTable(
                name: "FeatureCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeatureCategoryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Features_FeatureCategories_FeatureCategoryId",
                        column: x => x.FeatureCategoryId,
                        principalTable: "FeatureCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RealEstateTypeFeatureCategories",
                columns: table => new
                {
                    RealEstateTypeId = table.Column<int>(type: "int", nullable: false),
                    FeatureCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealEstateTypeFeatureCategories", x => new { x.RealEstateTypeId, x.FeatureCategoryId });
                    table.ForeignKey(
                        name: "FK_RealEstateTypeFeatureCategories_FeatureCategories_FeatureCategoryId",
                        column: x => x.FeatureCategoryId,
                        principalTable: "FeatureCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RealEstateTypeFeatureCategories_RealEstateTypes_RealEstateTypeId",
                        column: x => x.RealEstateTypeId,
                        principalTable: "RealEstateTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RealEstateFeatures",
                columns: table => new
                {
                    RealEstateId = table.Column<int>(type: "int", nullable: false),
                    FeatureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealEstateFeatures", x => new { x.RealEstateId, x.FeatureId });
                    table.ForeignKey(
                        name: "FK_RealEstateFeatures_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RealEstateFeatures_RealEstates_RealEstateId",
                        column: x => x.RealEstateId,
                        principalTable: "RealEstates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Features_FeatureCategoryId",
                table: "Features",
                column: "FeatureCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RealEstateFeatures_FeatureId",
                table: "RealEstateFeatures",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_RealEstateTypeFeatureCategories_FeatureCategoryId",
                table: "RealEstateTypeFeatureCategories",
                column: "FeatureCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RealEstateFeatures");

            migrationBuilder.DropTable(
                name: "RealEstateTypeFeatureCategories");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "FeatureCategories");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "RealEstateTypes",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
