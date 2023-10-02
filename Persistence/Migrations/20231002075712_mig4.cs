using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class mig4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkerCategory_AspNetUsers_WorkerId",
                table: "WorkerCategory");

            migrationBuilder.RenameColumn(
                name: "WorkerId",
                table: "WorkerCategory",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkerCategory_WorkerId",
                table: "WorkerCategory",
                newName: "IX_WorkerCategory_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerCategory_AspNetUsers_UserId",
                table: "WorkerCategory",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkerCategory_AspNetUsers_UserId",
                table: "WorkerCategory");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "WorkerCategory",
                newName: "WorkerId");

            migrationBuilder.RenameIndex(
                name: "IX_WorkerCategory_UserId",
                table: "WorkerCategory",
                newName: "IX_WorkerCategory_WorkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerCategory_AspNetUsers_WorkerId",
                table: "WorkerCategory",
                column: "WorkerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
