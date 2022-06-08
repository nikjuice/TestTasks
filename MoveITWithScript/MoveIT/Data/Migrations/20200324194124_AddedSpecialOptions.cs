using Microsoft.EntityFrameworkCore.Migrations;

namespace MoveIT.Data.Migrations
{
    public partial class AddedSpecialOptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Options",
                table: "RelocationInquiry",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Options",
                table: "RelocationInquiry");
        }
    }
}
