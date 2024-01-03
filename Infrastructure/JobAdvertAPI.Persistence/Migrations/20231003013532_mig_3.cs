using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobAdvertAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserJobPost_AspNetUsers_AppUserId",
                table: "UserJobPost");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "UserJobPost",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "f863a2a9-8ec5-473d-b406-7a21d84084d5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "75b36120-03f9-4b44-8203-e733b39be36c");

            migrationBuilder.AddForeignKey(
                name: "FK_UserJobPost_AspNetUsers_AppUserId",
                table: "UserJobPost",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserJobPost_AspNetUsers_AppUserId",
                table: "UserJobPost");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "UserJobPost",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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

            migrationBuilder.AddForeignKey(
                name: "FK_UserJobPost_AspNetUsers_AppUserId",
                table: "UserJobPost",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
