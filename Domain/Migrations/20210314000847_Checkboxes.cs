using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class Checkboxes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Checkboxes",
                columns: table => new
                {
                    PitanjeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TacanOdgovor = table.Column<string>(nullable: true),
                    TacanBodovi = table.Column<int>(nullable: true),
                    NetacanOdgovor1 = table.Column<string>(nullable: true),
                    NetacanOdgovor2 = table.Column<string>(nullable: true),
                    NetacanOdgovor3 = table.Column<string>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PitanjeIdC", x => x.PitanjeId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
        }
    }
}
