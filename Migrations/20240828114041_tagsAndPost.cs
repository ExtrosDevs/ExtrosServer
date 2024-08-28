using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExtrosServer.Migrations
{
    /// <inheritdoc />
    public partial class tagsAndPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Posts_PostID",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_PostID",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "PostID",
                table: "Tags");

            migrationBuilder.CreateTable(
                name: "PostTag",
                columns: table => new
                {
                    PostTagID = table.Column<Guid>(type: "uuid", nullable: false),
                    PostID = table.Column<Guid>(type: "uuid", nullable: false),
                    TagValue = table.Column<string>(type: "text", nullable: false),
                    TagValue1 = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTag", x => x.PostTagID);
                    table.ForeignKey(
                        name: "FK_PostTag_Posts_PostID",
                        column: x => x.PostID,
                        principalTable: "Posts",
                        principalColumn: "PostID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostTag_Tags_TagValue1",
                        column: x => x.TagValue1,
                        principalTable: "Tags",
                        principalColumn: "TagValue");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostTag_PostID",
                table: "PostTag",
                column: "PostID");

            migrationBuilder.CreateIndex(
                name: "IX_PostTag_TagValue1",
                table: "PostTag",
                column: "TagValue1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostTag");

            migrationBuilder.AddColumn<Guid>(
                name: "PostID",
                table: "Tags",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Tags_PostID",
                table: "Tags",
                column: "PostID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Posts_PostID",
                table: "Tags",
                column: "PostID",
                principalTable: "Posts",
                principalColumn: "PostID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
