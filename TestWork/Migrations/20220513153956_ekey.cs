using Microsoft.EntityFrameworkCore.Migrations;

namespace TestWork.Migrations
{
    public partial class ekey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ErrId",
                table: "Advertisements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_ErrId",
                table: "Advertisements",
                column: "ErrId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_Errs_ErrId",
                table: "Advertisements",
                column: "ErrId",
                principalTable: "Errs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_Errs_ErrId",
                table: "Advertisements");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_ErrId",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "ErrId",
                table: "Advertisements");
        }
    }
}
