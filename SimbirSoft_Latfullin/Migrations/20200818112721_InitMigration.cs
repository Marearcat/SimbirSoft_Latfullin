using Microsoft.EntityFrameworkCore.Migrations;

namespace SimbirSoft_Latfullin.Migrations
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UniqueResults",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Uri = table.Column<string>(nullable: true),
                    Result = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UniqueResults", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UniqueResults");
        }
    }
}
