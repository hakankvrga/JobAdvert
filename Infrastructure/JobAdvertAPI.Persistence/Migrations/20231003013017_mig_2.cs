using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobAdvertAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "UserJobPost",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "53f9a52e-647a-48a8-82a9-90431e835302");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "88d982a4-40e0-4547-a3c3-4bee850a145c");

            migrationBuilder.CreateIndex(
                name: "IX_UserJobPost_AppUserId",
                table: "UserJobPost",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserJobPost_AspNetUsers_AppUserId",
                table: "UserJobPost",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserJobPost_AspNetUsers_AppUserId",
                table: "UserJobPost");

            migrationBuilder.DropIndex(
                name: "IX_UserJobPost_AppUserId",
                table: "UserJobPost");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "UserJobPost");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "bb4c5a8d-57ee-4807-8715-ae78de78f983");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "7e4a76d5-bceb-4e64-9cb1-af351b0760ef");
        }
    }
}
