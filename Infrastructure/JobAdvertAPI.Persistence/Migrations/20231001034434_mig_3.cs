using System;
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
                name: "FK_JobPostAppUsers_AspNetUsers_AppUserId",
                table: "JobPostAppUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_JobPostAppUsers_JobPost_JobPostId",
                table: "JobPostAppUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobPostAppUsers",
                table: "JobPostAppUsers");

            migrationBuilder.RenameTable(
                name: "JobPostAppUsers",
                newName: "JobPostAppUser");

            migrationBuilder.RenameIndex(
                name: "IX_JobPostAppUsers_JobPostId",
                table: "JobPostAppUser",
                newName: "IX_JobPostAppUser_JobPostId");

            migrationBuilder.RenameIndex(
                name: "IX_JobPostAppUsers_AppUserId",
                table: "JobPostAppUser",
                newName: "IX_JobPostAppUser_AppUserId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "JobPostAppUser",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "JobPostAppUser",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobPostAppUser",
                table: "JobPostAppUser",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "4e2c915b-33a0-470b-aa28-6f21bf669887");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "7d81adb5-3f76-4350-8d05-cc7688d0400b");

            migrationBuilder.AddForeignKey(
                name: "FK_JobPostAppUser_AppUser",
                table: "JobPostAppUser",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobPostAppUser_JobPost",
                table: "JobPostAppUser",
                column: "JobPostId",
                principalTable: "JobPost",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobPostAppUser_AppUser",
                table: "JobPostAppUser");

            migrationBuilder.DropForeignKey(
                name: "FK_JobPostAppUser_JobPost",
                table: "JobPostAppUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobPostAppUser",
                table: "JobPostAppUser");

            migrationBuilder.RenameTable(
                name: "JobPostAppUser",
                newName: "JobPostAppUsers");

            migrationBuilder.RenameIndex(
                name: "IX_JobPostAppUser_JobPostId",
                table: "JobPostAppUsers",
                newName: "IX_JobPostAppUsers_JobPostId");

            migrationBuilder.RenameIndex(
                name: "IX_JobPostAppUser_AppUserId",
                table: "JobPostAppUsers",
                newName: "IX_JobPostAppUsers_AppUserId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "JobPostAppUsers",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "JobPostAppUsers",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobPostAppUsers",
                table: "JobPostAppUsers",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "bc8a2cfb-714a-4863-b685-d25213611ea2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "40d043ad-04d6-4981-80c3-c12f2d021771");

            migrationBuilder.AddForeignKey(
                name: "FK_JobPostAppUsers_AspNetUsers_AppUserId",
                table: "JobPostAppUsers",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobPostAppUsers_JobPost_JobPostId",
                table: "JobPostAppUsers",
                column: "JobPostId",
                principalTable: "JobPost",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
