using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ALEXforums.Migrations
{
    /// <inheritdoc />
    public partial class ALEXforums_Update1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*
            migrationBuilder.DropForeignKey(
                name: "FK_ForumPostComments_ForumPosts_PostId",
                schema: "ALEXforums",
                table: "ForumPostComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ForumPostComments_Users_UserId",
                schema: "ALEXforums",
                table: "ForumPostComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ForumPosts_ForumCategories_CategoryId",
                schema: "ALEXforums",
                table: "ForumPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_ForumPosts_Users_UserId",
                schema: "ALEXforums",
                table: "ForumPosts");
            */

            migrationBuilder.AddColumn<string>(
                name: "FaIcon",
                schema: "ALEXforums",
                table: "ForumCategories",
                type: "longtext",
                nullable: false);

            /*
            migrationBuilder.AddForeignKey(
                name: "FK_ForumPostComments_ForumPosts_PostId",
                schema: "ALEXforums",
                table: "ForumPostComments",
                column: "PostId",
                principalSchema: "ALEXforums",
                principalTable: "ForumPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ForumPostComments_Users_UserId",
                schema: "ALEXforums",
                table: "ForumPostComments",
                column: "UserId",
                principalSchema: "ALEXforums",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ForumPosts_ForumCategories_CategoryId",
                schema: "ALEXforums",
                table: "ForumPosts",
                column: "CategoryId",
                principalSchema: "ALEXforums",
                principalTable: "ForumCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ForumPosts_Users_UserId",
                schema: "ALEXforums",
                table: "ForumPosts",
                column: "UserId",
                principalSchema: "ALEXforums",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            */
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /*
            migrationBuilder.DropForeignKey(
                name: "FK_ForumPostComments_ForumPosts_PostId",
                schema: "ALEXforums",
                table: "ForumPostComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ForumPostComments_Users_UserId",
                schema: "ALEXforums",
                table: "ForumPostComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ForumPosts_ForumCategories_CategoryId",
                schema: "ALEXforums",
                table: "ForumPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_ForumPosts_Users_UserId",
                schema: "ALEXforums",
                table: "ForumPosts");
            */

            migrationBuilder.DropColumn(
                name: "FaIcon",
                schema: "ALEXforums",
                table: "ForumCategories");

            /*

            migrationBuilder.AddForeignKey(
                name: "FK_ForumPostComments_ForumPosts_PostId",
                schema: "ALEXforums",
                table: "ForumPostComments",
                column: "PostId",
                principalSchema: "ALEXforums",
                principalTable: "ForumPosts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ForumPostComments_Users_UserId",
                schema: "ALEXforums",
                table: "ForumPostComments",
                column: "UserId",
                principalSchema: "ALEXforums",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ForumPosts_ForumCategories_CategoryId",
                schema: "ALEXforums",
                table: "ForumPosts",
                column: "CategoryId",
                principalSchema: "ALEXforums",
                principalTable: "ForumCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ForumPosts_Users_UserId",
                schema: "ALEXforums",
                table: "ForumPosts",
                column: "UserId",
                principalSchema: "ALEXforums",
                principalTable: "Users",
                principalColumn: "Id");
            */
        }
    }
}
