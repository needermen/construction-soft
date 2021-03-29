using Microsoft.EntityFrameworkCore.Migrations;

namespace Cs.Persistence.Migrations
{
    public partial class resources_coef_default_value : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Coefficient",
                schema: "technic",
                table: "Technics",
                type: "decimal(18, 6)",
                nullable: false,
                defaultValue: 1m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 6)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Coefficient",
                schema: "material",
                table: "MainMaterials",
                type: "decimal(18, 6)",
                nullable: false,
                defaultValue: 1m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 6)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Coefficient",
                schema: "material",
                table: "ConsumptionMaterials",
                type: "decimal(18, 6)",
                nullable: false,
                defaultValue: 1m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 6)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Coefficient",
                schema: "material",
                table: "BuildingMaterials",
                type: "decimal(18, 6)",
                nullable: false,
                defaultValue: 1m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 6)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Coefficient",
                schema: "hr",
                table: "Workers",
                type: "decimal(18, 6)",
                nullable: false,
                defaultValue: 1m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 6)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Coefficient",
                schema: "technic",
                table: "Technics",
                type: "decimal(18, 6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 6)",
                oldDefaultValue: 1m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Coefficient",
                schema: "material",
                table: "MainMaterials",
                type: "decimal(18, 6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 6)",
                oldDefaultValue: 1m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Coefficient",
                schema: "material",
                table: "ConsumptionMaterials",
                type: "decimal(18, 6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 6)",
                oldDefaultValue: 1m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Coefficient",
                schema: "material",
                table: "BuildingMaterials",
                type: "decimal(18, 6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 6)",
                oldDefaultValue: 1m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Coefficient",
                schema: "hr",
                table: "Workers",
                type: "decimal(18, 6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 6)",
                oldDefaultValue: 1m);
        }
    }
}
