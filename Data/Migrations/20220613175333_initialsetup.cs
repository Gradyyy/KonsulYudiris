using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KonsulYudiris.Data.Migrations
{
    public partial class initialsetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Case",
                columns: table => new
                {
                    CaseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseHolder = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Consultant = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CaseType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Case", x => x.CaseID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Case");
        }
    }
}
