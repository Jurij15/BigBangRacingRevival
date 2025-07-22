using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBRRevival.Server.Migrations
{
    /// <inheritdoc />
    public partial class ImprovePlayer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Coins",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Copper",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "Players",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Diamonds",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FacebookId",
                table: "Players",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GameCenterId",
                table: "Players",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsCheater",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeveloper",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ItemDbVersion",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Players",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NinjaCreationTimestamp",
                table: "Players",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Shards",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Stars",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "Players",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "YoutubeId",
                table: "Players",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "YoutubeName",
                table: "Players",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "YoutubeSubscriberCount",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Players_ClientConfigID",
                table: "Players",
                column: "ClientConfigID");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_ClientConfigs_ClientConfigID",
                table: "Players",
                column: "ClientConfigID",
                principalTable: "ClientConfigs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_ClientConfigs_ClientConfigID",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_ClientConfigID",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Coins",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Copper",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Diamonds",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "FacebookId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "GameCenterId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "IsCheater",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "IsDeveloper",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "ItemDbVersion",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "NinjaCreationTimestamp",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Shards",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Stars",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "YoutubeId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "YoutubeName",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "YoutubeSubscriberCount",
                table: "Players");
        }
    }
}
