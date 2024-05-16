using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RHP.Migrations
{
    /// <inheritdoc />
    public partial class AddPlayerHallRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Player_Hall_HallId",
                table: "Player");

            migrationBuilder.DropIndex(
                name: "IX_Player_HallId",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "HallId",
                table: "Player");

            migrationBuilder.CreateTable(
                name: "HallPlayer",
                columns: table => new
                {
                    HallsId = table.Column<int>(type: "int", nullable: false),
                    PlayersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HallPlayer", x => new { x.HallsId, x.PlayersId });
                    table.ForeignKey(
                        name: "FK_HallPlayer_Hall_HallsId",
                        column: x => x.HallsId,
                        principalTable: "Hall",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HallPlayer_Player_PlayersId",
                        column: x => x.PlayersId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_HallPlayer_PlayersId",
                table: "HallPlayer",
                column: "PlayersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HallPlayer");

            migrationBuilder.AddColumn<int>(
                name: "HallId",
                table: "Player",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Player_HallId",
                table: "Player",
                column: "HallId");

            migrationBuilder.AddForeignKey(
                name: "FK_Player_Hall_HallId",
                table: "Player",
                column: "HallId",
                principalTable: "Hall",
                principalColumn: "Id");
        }
    }
}
