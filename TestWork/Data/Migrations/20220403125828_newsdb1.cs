using Microsoft.EntityFrameworkCore.Migrations;

namespace TestWork.Data.Migrations
{
    public partial class newsdb1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Photo",
                table: "News",
                newName: "ImageName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "News",
                newName: "Photo");
        }
    }
}
