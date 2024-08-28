using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExtrosServer.Migrations
{
    /// <inheritdoc />
    public partial class tagsAndPost3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostTags_Tags_TagValue1",
                table: "PostTags");

            migrationBuilder.DropIndex(
                name: "IX_PostTags_TagValue1",
                table: "PostTags");

            migrationBuilder.DropColumn(
                name: "TagValue1",
                table: "PostTags");

            migrationBuilder.CreateIndex(
                name: "IX_PostTags_TagValue",
                table: "PostTags",
                column: "TagValue");

            migrationBuilder.AddForeignKey(
                name: "FK_PostTags_Tags_TagValue",
                table: "PostTags",
                column: "TagValue",
                principalTable: "Tags",
                principalColumn: "TagValue",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostTags_Tags_TagValue",
                table: "PostTags");

            migrationBuilder.DropIndex(
                name: "IX_PostTags_TagValue",
                table: "PostTags");

            migrationBuilder.AddColumn<string>(
                name: "TagValue1",
                table: "PostTags",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_PostTags_TagValue1",
                table: "PostTags",
                column: "TagValue1");

            migrationBuilder.AddForeignKey(
                name: "FK_PostTags_Tags_TagValue1",
                table: "PostTags",
                column: "TagValue1",
                principalTable: "Tags",
                principalColumn: "TagValue",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
