using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DuckDuckGoose.Migrations
{
    public partial class AddUserFollows : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DuckDuckGooseUserDuckDuckGooseUser",
                columns: table => new
                {
                    FollowersId = table.Column<string>(type: "text", nullable: false),
                    FollowsId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DuckDuckGooseUserDuckDuckGooseUser", x => new { x.FollowersId, x.FollowsId });
                    table.ForeignKey(
                        name: "FK_DuckDuckGooseUserDuckDuckGooseUser_AspNetUsers_FollowersId",
                        column: x => x.FollowersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DuckDuckGooseUserDuckDuckGooseUser_AspNetUsers_FollowsId",
                        column: x => x.FollowsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DuckDuckGooseUserDuckDuckGooseUser_FollowsId",
                table: "DuckDuckGooseUserDuckDuckGooseUser",
                column: "FollowsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DuckDuckGooseUserDuckDuckGooseUser");
        }
    }
}
