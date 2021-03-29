using Microsoft.EntityFrameworkCore.Migrations;

namespace Cs.Persistence.Migrations
{
    public partial class add_extra_price_fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ExtraPrice",
                schema: "building",
                table: "WorkCategories",
                type: "decimal(18, 6)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ExtraPrice",
                schema: "building",
                table: "Phases",
                type: "decimal(18, 6)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ExtraPrice",
                schema: "building",
                table: "Buildings",
                type: "decimal(18, 6)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExtraPrice",
                schema: "building",
                table: "WorkCategories");

            migrationBuilder.DropColumn(
                name: "ExtraPrice",
                schema: "building",
                table: "Phases");

            migrationBuilder.DropColumn(
                name: "ExtraPrice",
                schema: "building",
                table: "Buildings");
        }
    }
}
