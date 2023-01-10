using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Better.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    SourceName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Balance = table.Column<decimal>(type: "money", nullable: false, defaultValue: 0m),
                    Pix = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HomeScored = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    AwayScored = table.Column<int>(type: "int", nullable: false, defaultValue: -1),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HomeTeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AwayTeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Teams_AwayTeamId",
                        column: x => x.AwayTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Games_Teams_HomeTeamId",
                        column: x => x.HomeTeamId,
                        principalTable: "Teams",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HomeGuess = table.Column<int>(type: "int", nullable: false),
                    AwayGuess = table.Column<int>(type: "int", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bets_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Email", "EmailConfirmed", "Password" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000101"), "adriandallegrave@gmail.com", true, "passwordgmail" });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "Name", "ShortName", "SourceName" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "Catar", "CAT", "Qatar" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "Equador", "EQU", "Ecuador" },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "Holanda", "HOL", "Netherlands" },
                    { new Guid("00000000-0000-0000-0000-000000000004"), "Senegal", "SEN", "Senegal" },
                    { new Guid("00000000-0000-0000-0000-000000000005"), "Estados Unidos", "USA", "United States" },
                    { new Guid("00000000-0000-0000-0000-000000000006"), "Inglaterra", "ING", "England" },
                    { new Guid("00000000-0000-0000-0000-000000000007"), "Ira", "IRA", "Iran" },
                    { new Guid("00000000-0000-0000-0000-000000000008"), "Pais de Gales", "PDG", "Wales" },
                    { new Guid("00000000-0000-0000-0000-000000000009"), "Argentina", "ARG", "Argentina" },
                    { new Guid("00000000-0000-0000-0000-000000000010"), "Arabia Saudita", "ARA", "Saudi Arabia" },
                    { new Guid("00000000-0000-0000-0000-000000000011"), "Mexico", "MEX", "Mexico" },
                    { new Guid("00000000-0000-0000-0000-000000000012"), "Polonia", "POL", "Poland" },
                    { new Guid("00000000-0000-0000-0000-000000000013"), "Australia", "AUS", "Australia" },
                    { new Guid("00000000-0000-0000-0000-000000000014"), "Dinamarca", "DIN", "Denmark" },
                    { new Guid("00000000-0000-0000-0000-000000000015"), "Franca", "FRA", "France" },
                    { new Guid("00000000-0000-0000-0000-000000000016"), "Tunisia", "TUN", "Tunisia" },
                    { new Guid("00000000-0000-0000-0000-000000000017"), "Alemanha", "ALE", "Germany" },
                    { new Guid("00000000-0000-0000-0000-000000000018"), "Costa Rica", "COS", "Costa Rica" },
                    { new Guid("00000000-0000-0000-0000-000000000019"), "Espanha", "ESP", "Spain" },
                    { new Guid("00000000-0000-0000-0000-000000000020"), "Japao", "JAP", "Japan" },
                    { new Guid("00000000-0000-0000-0000-000000000021"), "Belgica", "BEL", "Belgium" },
                    { new Guid("00000000-0000-0000-0000-000000000022"), "Canada", "CAN", "Canada" },
                    { new Guid("00000000-0000-0000-0000-000000000023"), "Croacia", "CRO", "Croatia" },
                    { new Guid("00000000-0000-0000-0000-000000000024"), "Marrocos", "MAR", "Morocco" },
                    { new Guid("00000000-0000-0000-0000-000000000025"), "Brasil", "BRA", "Brazil" },
                    { new Guid("00000000-0000-0000-0000-000000000026"), "Camaroes", "CAM", "Cameroon" },
                    { new Guid("00000000-0000-0000-0000-000000000027"), "Suica", "SUI", "Switzerland" },
                    { new Guid("00000000-0000-0000-0000-000000000028"), "Servia", "SER", "Serbia" },
                    { new Guid("00000000-0000-0000-0000-000000000029"), "Coreia do Sul", "COR", "South Korea" },
                    { new Guid("00000000-0000-0000-0000-000000000030"), "Gana", "GAN", "Ghana" },
                    { new Guid("00000000-0000-0000-0000-000000000031"), "Portugal", "POR", "Portugal" },
                    { new Guid("00000000-0000-0000-0000-000000000032"), "Uruguai", "URU", "Uruguay" }
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "AwayScored", "AwayTeamId", "HomeScored", "HomeTeamId", "StartTime" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000001001"), -1, new Guid("00000000-0000-0000-0000-000000000002"), -1, new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2022, 11, 20, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000001002"), -1, new Guid("00000000-0000-0000-0000-000000000003"), -1, new Guid("00000000-0000-0000-0000-000000000004"), new DateTime(2022, 11, 21, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000001003"), -1, new Guid("00000000-0000-0000-0000-000000000004"), -1, new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2022, 11, 25, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000001004"), -1, new Guid("00000000-0000-0000-0000-000000000002"), -1, new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(2022, 11, 25, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000001005"), -1, new Guid("00000000-0000-0000-0000-000000000001"), -1, new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(2022, 11, 29, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000001006"), -1, new Guid("00000000-0000-0000-0000-000000000004"), -1, new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2022, 11, 29, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000001007"), -1, new Guid("00000000-0000-0000-0000-000000000007"), -1, new Guid("00000000-0000-0000-0000-000000000006"), new DateTime(2022, 11, 21, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000001008"), -1, new Guid("00000000-0000-0000-0000-000000000008"), -1, new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2022, 11, 21, 16, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000001009"), -1, new Guid("00000000-0000-0000-0000-000000000007"), -1, new Guid("00000000-0000-0000-0000-000000000008"), new DateTime(2022, 11, 25, 7, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000001010"), -1, new Guid("00000000-0000-0000-0000-000000000005"), -1, new Guid("00000000-0000-0000-0000-000000000006"), new DateTime(2022, 11, 25, 16, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000001011"), -1, new Guid("00000000-0000-0000-0000-000000000005"), -1, new Guid("00000000-0000-0000-0000-000000000007"), new DateTime(2022, 11, 29, 16, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000001012"), -1, new Guid("00000000-0000-0000-0000-000000000006"), -1, new Guid("00000000-0000-0000-0000-000000000008"), new DateTime(2022, 11, 29, 16, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000001013"), -1, new Guid("00000000-0000-0000-0000-000000000010"), -1, new Guid("00000000-0000-0000-0000-000000000009"), new DateTime(2022, 11, 22, 7, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000001014"), -1, new Guid("00000000-0000-0000-0000-000000000012"), -1, new Guid("00000000-0000-0000-0000-000000000011"), new DateTime(2022, 11, 22, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000001015"), -1, new Guid("00000000-0000-0000-0000-000000000010"), -1, new Guid("00000000-0000-0000-0000-000000000012"), new DateTime(2022, 11, 26, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000001016"), -1, new Guid("00000000-0000-0000-0000-000000000011"), -1, new Guid("00000000-0000-0000-0000-000000000009"), new DateTime(2022, 11, 26, 16, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000001017"), -1, new Guid("00000000-0000-0000-0000-000000000009"), -1, new Guid("00000000-0000-0000-0000-000000000012"), new DateTime(2022, 11, 30, 16, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000001018"), -1, new Guid("00000000-0000-0000-0000-000000000011"), -1, new Guid("00000000-0000-0000-0000-000000000010"), new DateTime(2022, 11, 30, 16, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000001019"), -1, new Guid("00000000-0000-0000-0000-000000000016"), -1, new Guid("00000000-0000-0000-0000-000000000014"), new DateTime(2022, 11, 22, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000001020"), -1, new Guid("00000000-0000-0000-0000-000000000013"), -1, new Guid("00000000-0000-0000-0000-000000000015"), new DateTime(2022, 11, 22, 16, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000001021"), -1, new Guid("00000000-0000-0000-0000-000000000013"), -1, new Guid("00000000-0000-0000-0000-000000000016"), new DateTime(2022, 11, 26, 7, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000001022"), -1, new Guid("00000000-0000-0000-0000-000000000014"), -1, new Guid("00000000-0000-0000-0000-000000000015"), new DateTime(2022, 11, 26, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000001023"), -1, new Guid("00000000-0000-0000-0000-000000000015"), -1, new Guid("00000000-0000-0000-0000-000000000016"), new DateTime(2022, 11, 30, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000001024"), -1, new Guid("00000000-0000-0000-0000-000000000014"), -1, new Guid("00000000-0000-0000-0000-000000000013"), new DateTime(2022, 11, 30, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000001025"), -1, new Guid("00000000-0000-0000-0000-000000000020"), -1, new Guid("00000000-0000-0000-0000-000000000017"), new DateTime(2022, 11, 23, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000001026"), -1, new Guid("00000000-0000-0000-0000-000000000018"), -1, new Guid("00000000-0000-0000-0000-000000000019"), new DateTime(2022, 11, 23, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000001027"), -1, new Guid("00000000-0000-0000-0000-000000000018"), -1, new Guid("00000000-0000-0000-0000-000000000020"), new DateTime(2022, 11, 27, 7, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000001028"), -1, new Guid("00000000-0000-0000-0000-000000000017"), -1, new Guid("00000000-0000-0000-0000-000000000019"), new DateTime(2022, 11, 27, 16, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000001029"), -1, new Guid("00000000-0000-0000-0000-000000000019"), -1, new Guid("00000000-0000-0000-0000-000000000020"), new DateTime(2022, 12, 1, 16, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000001030"), -1, new Guid("00000000-0000-0000-0000-000000000017"), -1, new Guid("00000000-0000-0000-0000-000000000018"), new DateTime(2022, 12, 1, 16, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000001031"), -1, new Guid("00000000-0000-0000-0000-000000000023"), -1, new Guid("00000000-0000-0000-0000-000000000024"), new DateTime(2022, 11, 23, 7, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000001032"), -1, new Guid("00000000-0000-0000-0000-000000000022"), -1, new Guid("00000000-0000-0000-0000-000000000021"), new DateTime(2022, 11, 23, 16, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000001033"), -1, new Guid("00000000-0000-0000-0000-000000000024"), -1, new Guid("00000000-0000-0000-0000-000000000021"), new DateTime(2022, 11, 27, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000001034"), -1, new Guid("00000000-0000-0000-0000-000000000022"), -1, new Guid("00000000-0000-0000-0000-000000000023"), new DateTime(2022, 11, 27, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000001035"), -1, new Guid("00000000-0000-0000-0000-000000000021"), -1, new Guid("00000000-0000-0000-0000-000000000023"), new DateTime(2022, 12, 1, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000001036"), -1, new Guid("00000000-0000-0000-0000-000000000024"), -1, new Guid("00000000-0000-0000-0000-000000000022"), new DateTime(2022, 12, 1, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000001037"), -1, new Guid("00000000-0000-0000-0000-000000000026"), -1, new Guid("00000000-0000-0000-0000-000000000027"), new DateTime(2022, 11, 24, 7, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000001038"), -1, new Guid("00000000-0000-0000-0000-000000000028"), -1, new Guid("00000000-0000-0000-0000-000000000025"), new DateTime(2022, 11, 24, 16, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000001039"), -1, new Guid("00000000-0000-0000-0000-000000000028"), -1, new Guid("00000000-0000-0000-0000-000000000026"), new DateTime(2022, 11, 28, 7, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000001040"), -1, new Guid("00000000-0000-0000-0000-000000000027"), -1, new Guid("00000000-0000-0000-0000-000000000025"), new DateTime(2022, 11, 28, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000001041"), -1, new Guid("00000000-0000-0000-0000-000000000025"), -1, new Guid("00000000-0000-0000-0000-000000000026"), new DateTime(2022, 12, 2, 16, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000001042"), -1, new Guid("00000000-0000-0000-0000-000000000027"), -1, new Guid("00000000-0000-0000-0000-000000000028"), new DateTime(2022, 12, 2, 16, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "AwayScored", "AwayTeamId", "HomeScored", "HomeTeamId", "StartTime" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000001043"), -1, new Guid("00000000-0000-0000-0000-000000000029"), -1, new Guid("00000000-0000-0000-0000-000000000032"), new DateTime(2022, 11, 24, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000001044"), -1, new Guid("00000000-0000-0000-0000-000000000030"), -1, new Guid("00000000-0000-0000-0000-000000000031"), new DateTime(2022, 11, 24, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000001045"), -1, new Guid("00000000-0000-0000-0000-000000000030"), -1, new Guid("00000000-0000-0000-0000-000000000029"), new DateTime(2022, 11, 28, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000001046"), -1, new Guid("00000000-0000-0000-0000-000000000032"), -1, new Guid("00000000-0000-0000-0000-000000000031"), new DateTime(2022, 11, 28, 16, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000001047"), -1, new Guid("00000000-0000-0000-0000-000000000031"), -1, new Guid("00000000-0000-0000-0000-000000000029"), new DateTime(2022, 12, 2, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000001048"), -1, new Guid("00000000-0000-0000-0000-000000000032"), -1, new Guid("00000000-0000-0000-0000-000000000030"), new DateTime(2022, 12, 2, 12, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccountId", "Pix" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000202"), new Guid("00000000-0000-0000-0000-000000000101"), "51997561418" });

            migrationBuilder.CreateIndex(
                name: "IX_Bets_GameId",
                table: "Bets",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Bets_UserId",
                table: "Bets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_AwayTeamId",
                table: "Games",
                column: "AwayTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_HomeTeamId",
                table: "Games",
                column: "HomeTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserId",
                table: "Transactions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AccountId",
                table: "Users",
                column: "AccountId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bets");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
