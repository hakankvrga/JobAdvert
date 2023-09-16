using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobAdvertAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "JobPost");

            migrationBuilder.AddColumn<int>(
                name: "JobPostId",
                table: "Files",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Files_JobPostId",
                table: "Files",
                column: "JobPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_JobPost_JobPostId",
                table: "Files",
                column: "JobPostId",
                principalTable: "JobPost",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_JobPost_JobPostId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_JobPostId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "JobPostId",
                table: "Files");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "JobPost",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
