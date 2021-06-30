using Microsoft.EntityFrameworkCore.Migrations;

namespace PerformanceDbApp.Migrations.PostgresDb
{
    public partial class number : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "Orders");
        }
    }
}
