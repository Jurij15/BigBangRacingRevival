using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBRRevival.Server.Migrations
{
    /// <inheritdoc />
    public partial class MinorPlayerDataFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NameChangesDone",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameChangesDone",
                table: "Players");
        }
    }
}
