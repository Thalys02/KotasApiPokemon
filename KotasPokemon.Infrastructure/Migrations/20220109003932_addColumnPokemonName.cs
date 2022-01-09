using Microsoft.EntityFrameworkCore.Migrations;

namespace KotasPokemon.Infrastructure.Migrations
{
    public partial class addColumnPokemonName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PokemonName",
                table: "pokemon_captured",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_pokemon_captured_PokemonMasterId",
                table: "pokemon_captured",
                column: "PokemonMasterId");

            migrationBuilder.AddForeignKey(
                name: "FK_pokemon_captured_pokemon_masters_PokemonMasterId",
                table: "pokemon_captured",
                column: "PokemonMasterId",
                principalTable: "pokemon_masters",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pokemon_captured_pokemon_masters_PokemonMasterId",
                table: "pokemon_captured");

            migrationBuilder.DropIndex(
                name: "IX_pokemon_captured_PokemonMasterId",
                table: "pokemon_captured");

            migrationBuilder.DropColumn(
                name: "PokemonName",
                table: "pokemon_captured");
        }
    }
}
