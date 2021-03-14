using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class Polaganje : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.CreateTable(
                name: "Polaganja",
                columns: table => new
                {
                    KorisnikId = table.Column<int>(nullable: false),
                    TestId = table.Column<int>(nullable: false),
                    TestKursId = table.Column<int>(nullable: false),
                    BodoviT = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Polaganja", x => new { x.KorisnikId, x.TestId });
                    table.ForeignKey(
                        name: "FK_Polaganja_Korisnici_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnici",
                        principalColumn: "KorisnikId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Polaganja_Testovi_TestId_TestKursId",
                        columns: x => new { x.TestId, x.TestKursId },
                        principalTable: "Testovi",
                        principalColumns: new[] { "TestId", "KursId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Polaganja_TestId_TestKursId",
                table: "Polaganja",
                columns: new[] { "TestId", "TestKursId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Polaganja");

          
        }
    }
}
