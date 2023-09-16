using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobAdvertAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "JobPostJobPostImageFile",
                columns: table => new
                {
                    JobPostImageFilesId = table.Column<int>(type: "int", nullable: false),
                    JobPostsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobPostJobPostImageFile", x => new { x.JobPostImageFilesId, x.JobPostsId });
                    table.ForeignKey(
                        name: "FK_JobPostJobPostImageFile_Files_JobPostImageFilesId",
                        column: x => x.JobPostImageFilesId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobPostJobPostImageFile_JobPost_JobPostsId",
                        column: x => x.JobPostsId,
                        principalTable: "JobPost",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobPostJobPostImageFile_JobPostsId",
                table: "JobPostJobPostImageFile",
                column: "JobPostsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobPostJobPostImageFile");

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
    }
}
