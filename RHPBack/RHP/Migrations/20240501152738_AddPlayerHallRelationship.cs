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
                table: "player");

            migrationBuilder.DropIndex(
                name: "IX_Player_HallId",
                table: "player");

            migrationBuilder.DropColumn(
                name: "HallId",
                table: "player");

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
                        principalTable: "hall",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HallPlayer_Player_PlayersId",
                        column: x => x.PlayersId,
                        principalTable: "player",
                        principalColumn: "id",
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
                table: "player",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Player_HallId",
                table: "player",
                column: "HallId");

            migrationBuilder.AddForeignKey(
                name: "FK_Player_Hall_HallId",
                table: "player",
                column: "HallId",
                principalTable: "hall",
                principalColumn: "id");
        }
    }
}
