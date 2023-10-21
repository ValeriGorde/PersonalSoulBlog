using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalSoulBlog.DAL.Data.Migrations
{
    public partial class TablesArticlesAndCommentsChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_AspNetUsers_UsersId",
                table: "Articles");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "Articles",
                newName: "UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_Articles_UsersId",
                table: "Articles",
                newName: "IX_Articles_UserId1");

            migrationBuilder.AddColumn<int>(
                name: "ArticleId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ArticleId",
                table: "Comments",
                column: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_AspNetUsers_UserId1",
                table: "Articles",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Articles_ArticleId",
                table: "Comments",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_AspNetUsers_UserId1",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Articles_ArticleId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ArticleId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "UserId1",
                table: "Articles",
                newName: "UsersId");

            migrationBuilder.RenameIndex(
                name: "IX_Articles_UserId1",
                table: "Articles",
                newName: "IX_Articles_UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_AspNetUsers_UsersId",
                table: "Articles",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
