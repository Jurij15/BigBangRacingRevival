using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBRRevival.Server.Migrations
{
    /// <inheritdoc />
    public partial class MakeClientConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BoltsAtStart",
                table: "ClientConfigs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CarRefreshMinutes",
                table: "ClientConfigs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CoinsAtStart",
                table: "ClientConfigs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatorRank1",
                table: "ClientConfigs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatorRank2",
                table: "ClientConfigs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatorRank3",
                table: "ClientConfigs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatorRank4",
                table: "ClientConfigs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatorRank5",
                table: "ClientConfigs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatorRank6",
                table: "ClientConfigs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DailyGemAmount",
                table: "ClientConfigs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DiamondsAtStart",
                table: "ClientConfigs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FbConnectReward",
                table: "ClientConfigs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FreshFreeCoolDown",
                table: "ClientConfigs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FreshFreeCount",
                table: "ClientConfigs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FreshFreeInterval",
                table: "ClientConfigs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InRaceDiamondSpawnProbability",
                table: "ClientConfigs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "KeysAtStart",
                table: "ClientConfigs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinimumTournamentNitros",
                table: "ClientConfigs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OfferCooldownMinutes",
                table: "ClientConfigs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OfferDurationMinutes",
                table: "ClientConfigs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SuperLikeRefreshMinutes",
                table: "ClientConfigs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TriesForAd",
                table: "ClientConfigs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TriesForGems",
                table: "ClientConfigs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TriesGemPrice",
                table: "ClientConfigs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VideoAdCoolDown",
                table: "ClientConfigs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VideoAdCount",
                table: "ClientConfigs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BoltsAtStart",
                table: "ClientConfigs");

            migrationBuilder.DropColumn(
                name: "CarRefreshMinutes",
                table: "ClientConfigs");

            migrationBuilder.DropColumn(
                name: "CoinsAtStart",
                table: "ClientConfigs");

            migrationBuilder.DropColumn(
                name: "CreatorRank1",
                table: "ClientConfigs");

            migrationBuilder.DropColumn(
                name: "CreatorRank2",
                table: "ClientConfigs");

            migrationBuilder.DropColumn(
                name: "CreatorRank3",
                table: "ClientConfigs");

            migrationBuilder.DropColumn(
                name: "CreatorRank4",
                table: "ClientConfigs");

            migrationBuilder.DropColumn(
                name: "CreatorRank5",
                table: "ClientConfigs");

            migrationBuilder.DropColumn(
                name: "CreatorRank6",
                table: "ClientConfigs");

            migrationBuilder.DropColumn(
                name: "DailyGemAmount",
                table: "ClientConfigs");

            migrationBuilder.DropColumn(
                name: "DiamondsAtStart",
                table: "ClientConfigs");

            migrationBuilder.DropColumn(
                name: "FbConnectReward",
                table: "ClientConfigs");

            migrationBuilder.DropColumn(
                name: "FreshFreeCoolDown",
                table: "ClientConfigs");

            migrationBuilder.DropColumn(
                name: "FreshFreeCount",
                table: "ClientConfigs");

            migrationBuilder.DropColumn(
                name: "FreshFreeInterval",
                table: "ClientConfigs");

            migrationBuilder.DropColumn(
                name: "InRaceDiamondSpawnProbability",
                table: "ClientConfigs");

            migrationBuilder.DropColumn(
                name: "KeysAtStart",
                table: "ClientConfigs");

            migrationBuilder.DropColumn(
                name: "MinimumTournamentNitros",
                table: "ClientConfigs");

            migrationBuilder.DropColumn(
                name: "OfferCooldownMinutes",
                table: "ClientConfigs");

            migrationBuilder.DropColumn(
                name: "OfferDurationMinutes",
                table: "ClientConfigs");

            migrationBuilder.DropColumn(
                name: "SuperLikeRefreshMinutes",
                table: "ClientConfigs");

            migrationBuilder.DropColumn(
                name: "TriesForAd",
                table: "ClientConfigs");

            migrationBuilder.DropColumn(
                name: "TriesForGems",
                table: "ClientConfigs");

            migrationBuilder.DropColumn(
                name: "TriesGemPrice",
                table: "ClientConfigs");

            migrationBuilder.DropColumn(
                name: "VideoAdCoolDown",
                table: "ClientConfigs");

            migrationBuilder.DropColumn(
                name: "VideoAdCount",
                table: "ClientConfigs");
        }
    }
}
