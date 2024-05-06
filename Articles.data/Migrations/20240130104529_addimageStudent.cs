using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Articles.data.Migrations
{
    public partial class addimageStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PostImageUrl",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostImageUrl",
                table: "Students");
        }
    }
}
