using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExtrosServer.Migrations
{
    /// <inheritdoc />
    public partial class tagsAndPost2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostTag_Posts_PostID",
                table: "PostTag");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTag_Tags_TagValue1",
                table: "PostTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostTag",
                table: "PostTag");

            migrationBuilder.RenameTable(
                name: "PostTag",
                newName: "PostTags");

            migrationBuilder.RenameIndex(
                name: "IX_PostTag_TagValue1",
                table: "PostTags",
                newName: "IX_PostTags_TagValue1");

            migrationBuilder.RenameIndex(
                name: "IX_PostTag_PostID",
                table: "PostTags",
                newName: "IX_PostTags_PostID");

            migrationBuilder.AlterColumn<string>(
                name: "TagValue1",
                table: "PostTags",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostTags",
                table: "PostTags",
                column: "PostTagID");

            migrationBuilder.AddForeignKey(
                name: "FK_PostTags_Posts_PostID",
                table: "PostTags",
                column: "PostID",
                principalTable: "Posts",
                principalColumn: "PostID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostTags_Tags_TagValue1",
                table: "PostTags",
                column: "TagValue1",
                principalTable: "Tags",
                principalColumn: "TagValue",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostTags_Posts_PostID",
                table: "PostTags");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTags_Tags_TagValue1",
                table: "PostTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostTags",
                table: "PostTags");

            migrationBuilder.RenameTable(
                name: "PostTags",
                newName: "PostTag");

            migrationBuilder.RenameIndex(
                name: "IX_PostTags_TagValue1",
                table: "PostTag",
                newName: "IX_PostTag_TagValue1");

            migrationBuilder.RenameIndex(
                name: "IX_PostTags_PostID",
                table: "PostTag",
                newName: "IX_PostTag_PostID");

            migrationBuilder.AlterColumn<string>(
                name: "TagValue1",
                table: "PostTag",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostTag",
                table: "PostTag",
                column: "PostTagID");

            migrationBuilder.AddForeignKey(
                name: "FK_PostTag_Posts_PostID",
                table: "PostTag",
                column: "PostID",
                principalTable: "Posts",
                principalColumn: "PostID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostTag_Tags_TagValue1",
                table: "PostTag",
                column: "TagValue1",
                principalTable: "Tags",
                principalColumn: "TagValue");
        }
    }
}
