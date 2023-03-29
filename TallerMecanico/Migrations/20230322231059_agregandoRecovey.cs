using Microsoft.EntityFrameworkCore.Migrations;

namespace TallerMecanico.Migrations
{
    public partial class agregandoRecovey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Recovery_token",
                table: "Usuario",
                type: "varchar(255)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Recovery_token",
                table: "Usuario");
        }
    }
}
