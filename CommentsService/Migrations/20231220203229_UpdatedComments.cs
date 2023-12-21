using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommentsService.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentImages_Comments_Id",
                table: "CommentImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentImages",
                table: "CommentImages");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Comments",
                newName: "CommentId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CommentImages",
                newName: "CommentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentImages",
                table: "CommentImages",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentImages_CommentId",
                table: "CommentImages",
                column: "CommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentImages_Comments_CommentId",
                table: "CommentImages",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "CommentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentImages_Comments_CommentId",
                table: "CommentImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommentImages",
                table: "CommentImages");

            migrationBuilder.DropIndex(
                name: "IX_CommentImages_CommentId",
                table: "CommentImages");

            migrationBuilder.RenameColumn(
                name: "CommentId",
                table: "Comments",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CommentId",
                table: "CommentImages",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommentImages",
                table: "CommentImages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentImages_Comments_Id",
                table: "CommentImages",
                column: "Id",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
