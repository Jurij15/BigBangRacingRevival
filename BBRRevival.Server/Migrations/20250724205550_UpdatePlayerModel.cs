using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBRRevival.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePlayerModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AcceptNotifications",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "AdventureLevelsCompleted",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AgeGroup",
                table: "Players",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "BigBangPoints",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Boosters",
                table: "Players",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BundlesPurchased",
                table: "Players",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "CarBoosterRefreshTimeLeft",
                table: "Players",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<float>(
                name: "CarHandicap",
                table: "Players",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "CarTrophies",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CardPurchases",
                table: "Players",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ClaimedTutorials",
                table: "Players",
                type: "TEXT",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<bool>(
                name: "CoinDoubler",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CompletedSurvey",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "CreatorLikes",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatorRankingDelta",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "DirtBikeBundle",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "EditorResources",
                table: "Players",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "FbClaimed",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "FollowerCount",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "ForumClaimed",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "GachaData",
                table: "Players",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Players",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "HasJoinedTeam",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Hash",
                table: "Players",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HatsPurchased",
                table: "Players",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IgClaimed",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LastSeasonEndCarTrophies",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LastSeasonEndMcTrophies",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "McBoosterRefreshTimeLeft",
                table: "Players",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<float>(
                name: "McHandicap",
                table: "Players",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "McRank",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "McTrophies",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NewLevelsRated",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PendingSpecialOfferChests",
                table: "Players",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PublishedMinigameCount",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RacesThisSeason",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RacesWon",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SeasonReward",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "SuperLikeRefreshTimeLeft",
                table: "Players",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "TeamId",
                table: "Players",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TeamKickReason",
                table: "Players",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TeamName",
                table: "Players",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TeamRoleName",
                table: "Players",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TotalCoinsEarned",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalLikes",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalSuperLikes",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TrailsPurchased",
                table: "Players",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Upgrades",
                table: "Players",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "VarRank",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Xp",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "teamRole",
                table: "Players",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceptNotifications",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "AdventureLevelsCompleted",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "AgeGroup",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "BigBangPoints",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Boosters",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "BundlesPurchased",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "CarBoosterRefreshTimeLeft",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "CarHandicap",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "CarTrophies",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "CardPurchases",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "ClaimedTutorials",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "CoinDoubler",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "CompletedSurvey",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "CreatorLikes",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "CreatorRankingDelta",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "DirtBikeBundle",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "EditorResources",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "FbClaimed",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "FollowerCount",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "ForumClaimed",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "GachaData",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "HasJoinedTeam",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Hash",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "HatsPurchased",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "IgClaimed",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "LastSeasonEndCarTrophies",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "LastSeasonEndMcTrophies",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "McBoosterRefreshTimeLeft",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "McHandicap",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "McRank",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "McTrophies",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "NewLevelsRated",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "PendingSpecialOfferChests",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "PublishedMinigameCount",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "RacesThisSeason",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "RacesWon",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "SeasonReward",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "SuperLikeRefreshTimeLeft",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "TeamKickReason",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "TeamName",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "TeamRoleName",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "TotalCoinsEarned",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "TotalLikes",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "TotalSuperLikes",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "TrailsPurchased",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Upgrades",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "VarRank",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Xp",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "teamRole",
                table: "Players");
        }
    }
}
