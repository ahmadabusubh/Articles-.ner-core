using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Articles.data.Migrations
{
    public partial class Student : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorPost_Author_AuthorId",
                table: "AuthorPost");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "AuthorPost",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "AuthorPost",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Grade = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorPost_Author_AuthorId",
                table: "AuthorPost",
                column: "AuthorId",
                principalTable: "Author",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorPost_Author_AuthorId",
                table: "AuthorPost");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AuthorPost",
                newName: "id");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "AuthorPost",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorPost_Author_AuthorId",
                table: "AuthorPost",
                column: "AuthorId",
                principalTable: "Author",
                principalColumn: "Id");
        }
    }
}
