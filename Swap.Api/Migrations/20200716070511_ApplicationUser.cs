using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Swap.Api.Migrations
{
    public partial class ApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exchanges_User_UserId",
                table: "Exchanges");

            migrationBuilder.DropForeignKey(
                name: "FK_Exchanges_User_UserOfferId",
                table: "Exchanges");

            migrationBuilder.DropForeignKey(
                name: "FK_Itens_User_UserId",
                table: "Itens");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropIndex(
                name: "IX_Itens_UserId",
                table: "Itens");

            migrationBuilder.DropIndex(
                name: "IX_Exchanges_UserId",
                table: "Exchanges");

            migrationBuilder.DropIndex(
                name: "IX_Exchanges_UserOfferId",
                table: "Exchanges");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Itens",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Exchanges",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserOfferId1",
                table: "Exchanges",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Itens_UserId1",
                table: "Itens",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Exchanges_UserId1",
                table: "Exchanges",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Exchanges_UserOfferId1",
                table: "Exchanges",
                column: "UserOfferId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Exchanges_AspNetUsers_UserId1",
                table: "Exchanges",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Exchanges_AspNetUsers_UserOfferId1",
                table: "Exchanges",
                column: "UserOfferId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Itens_AspNetUsers_UserId1",
                table: "Itens",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exchanges_AspNetUsers_UserId1",
                table: "Exchanges");

            migrationBuilder.DropForeignKey(
                name: "FK_Exchanges_AspNetUsers_UserOfferId1",
                table: "Exchanges");

            migrationBuilder.DropForeignKey(
                name: "FK_Itens_AspNetUsers_UserId1",
                table: "Itens");

            migrationBuilder.DropIndex(
                name: "IX_Itens_UserId1",
                table: "Itens");

            migrationBuilder.DropIndex(
                name: "IX_Exchanges_UserId1",
                table: "Exchanges");

            migrationBuilder.DropIndex(
                name: "IX_Exchanges_UserOfferId1",
                table: "Exchanges");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Itens");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Exchanges");

            migrationBuilder.DropColumn(
                name: "UserOfferId1",
                table: "Exchanges");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CellNumber = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    Role = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Itens_UserId",
                table: "Itens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Exchanges_UserId",
                table: "Exchanges",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Exchanges_UserOfferId",
                table: "Exchanges",
                column: "UserOfferId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exchanges_User_UserId",
                table: "Exchanges",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Exchanges_User_UserOfferId",
                table: "Exchanges",
                column: "UserOfferId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Itens_User_UserId",
                table: "Itens",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
