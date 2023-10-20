using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalSoulBlog.DAL.Data.Migrations
{
    public partial class UpdateArticlesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_AspNetUsers_UserId",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_UserId",
                table: "Articles");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Articles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsersId",
                table: "Articles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Articles_UsersId",
                table: "Articles",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_AspNetUsers_UsersId",
                table: "Articles",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_AspNetUsers_UsersId",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_UsersId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "Articles");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Articles",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_UserId",
                table: "Articles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_AspNetUsers_UserId",
                table: "Articles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
