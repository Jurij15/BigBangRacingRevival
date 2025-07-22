using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBRRevival.Server.Migrations
{
    /// <inheritdoc />
    public partial class IImprovePlayer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarBoosters",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Cups",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ItemLevel",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxCarBoosters",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxMcBoosters",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "McBoosters",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TournamentBoosters",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarBoosters",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Cups",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "ItemLevel",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "MaxCarBoosters",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "MaxMcBoosters",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "McBoosters",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "TournamentBoosters",
                table: "Players");
        }
    }
}
