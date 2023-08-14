using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ALEXforums.Migrations
{
    /// <inheritdoc />
    public partial class ALEXforums_InitialSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ALEXforums");

            migrationBuilder.CreateTable(
                name: "ForumCategories",
                schema: "ALEXforums",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForumCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "ALEXforums",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Username = table.Column<string>(type: "longtext", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: false),
                    Avatar = table.Column<string>(type: "longtext", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ForumPosts",
                schema: "ALEXforums",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CategoryId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Title = table.Column<string>(type: "longtext", nullable: false),
                    BodyText = table.Column<string>(type: "longtext", nullable: true),
                    PublishDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    PictureAttachment = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForumPosts", x => x.Id);
                    // table.ForeignKey(
                    //     name: "FK_ForumPosts_ForumCategories_CategoryId",
                    //     column: x => x.CategoryId,
                    //     principalSchema: "ALEXforums",
                    //     principalTable: "ForumCategories",
                    //     principalColumn: "Id");
                    // table.ForeignKey(
                    //     name: "FK_ForumPosts_Users_UserId",
                    //     column: x => x.UserId,
                    //     principalSchema: "ALEXforums",
                    //     principalTable: "Users",
                    //     principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ForumPostComments",
                schema: "ALEXforums",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PostId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Text = table.Column<string>(type: "longtext", nullable: false),
                    PublishDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForumPostComments", x => x.Id);
                    // table.ForeignKey(
                    //     name: "FK_ForumPostComments_ForumPosts_PostId",
                    //     column: x => x.PostId,
                    //     principalSchema: "ALEXforums",
                    //     principalTable: "ForumPosts",
                    //     principalColumn: "Id");
                    // table.ForeignKey(
                    //     name: "FK_ForumPostComments_Users_UserId",
                    //     column: x => x.UserId,
                    //     principalSchema: "ALEXforums",
                    //     principalTable: "Users",
                    //     principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ForumPostComments_PostId",
                schema: "ALEXforums",
                table: "ForumPostComments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_ForumPostComments_UserId",
                schema: "ALEXforums",
                table: "ForumPostComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ForumPosts_CategoryId",
                schema: "ALEXforums",
                table: "ForumPosts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ForumPosts_UserId",
                schema: "ALEXforums",
                table: "ForumPosts",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ForumPostComments",
                schema: "ALEXforums");

            migrationBuilder.DropTable(
                name: "ForumPosts",
                schema: "ALEXforums");

            migrationBuilder.DropTable(
                name: "ForumCategories",
                schema: "ALEXforums");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "ALEXforums");
        }
    }
}
