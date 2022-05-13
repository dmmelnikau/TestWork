using Microsoft.EntityFrameworkCore.Migrations;

namespace TestWork.Migrations
{
    public partial class ERRFloat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "ERR",
                table: "Advertisements",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ERR",
                table: "Advertisements",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
