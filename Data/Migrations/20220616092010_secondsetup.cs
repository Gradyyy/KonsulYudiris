using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KonsulYudiris.Data.Migrations
{
    public partial class secondsetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Consultant",
                columns: table => new
                {
                    ConsultantID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConsultantName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConsultantPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConsultantSpecialty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConsultantDetails = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultant", x => x.ConsultantID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Consultant");
        }
    }
}
