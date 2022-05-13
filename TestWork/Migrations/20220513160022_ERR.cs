using Microsoft.EntityFrameworkCore.Migrations;

namespace TestWork.Migrations
{
    public partial class ERR : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_Errs_ErrId",
                table: "Advertisements");

            migrationBuilder.DropTable(
                name: "Errs");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_ErrId",
                table: "Advertisements");

            migrationBuilder.RenameColumn(
                name: "ErrId",
                table: "Advertisements",
                newName: "ERR");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ERR",
                table: "Advertisements",
                newName: "ErrId");

            migrationBuilder.CreateTable(
                name: "Errs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AvarageERR = table.Column<double>(type: "float", nullable: false),
                    ERR = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Errs", x => x.Id);
                });

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
    }
}
