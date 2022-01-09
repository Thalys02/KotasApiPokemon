using Microsoft.EntityFrameworkCore.Migrations;

namespace KotasPokemon.Infrastructure.Migrations
{
    public partial class addColumnPercentageSuccess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "PercentageSuccess",
                table: "pokemon_captured",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PercentageSuccess",
                table: "pokemon_captured");
        }
    }
}
