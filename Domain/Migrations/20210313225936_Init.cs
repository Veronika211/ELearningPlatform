using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Korisnici",
                columns: table => new
                {
                    KorisnikId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(nullable: true),
                    Prezime = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    BrPoena = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnici", x => x.KorisnikId);
                });

            migrationBuilder.CreateTable(
                name: "Kursevi",
                columns: table => new
                {
                    KursId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivKursa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kursevi", x => x.KursId);
                });

            migrationBuilder.CreateTable(
                name: "Lekcije",
                columns: table => new
                {
                    KursId = table.Column<int>(nullable: false),
                    LekcijaId = table.Column<int>(nullable: false),
                    Naziv = table.Column<string>(nullable: true),
                    Sadrzaj = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lekcije", x => new { x.LekcijaId, x.KursId });
                    table.ForeignKey(
                        name: "FK_Lekcije_Kursevi_KursId",
                        column: x => x.KursId,
                        principalTable: "Kursevi",
                        principalColumn: "KursId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pohadjanja",
                columns: table => new
                {
                    KorisnikId = table.Column<int>(nullable: false),
                    KursId = table.Column<int>(nullable: false),
                    Bodovi = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pohadjanja", x => new { x.KorisnikId, x.KursId });
                    table.ForeignKey(
                        name: "FK_Pohadjanja_Korisnici_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnici",
                        principalColumn: "KorisnikId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pohadjanja_Kursevi_KursId",
                        column: x => x.KursId,
                        principalTable: "Kursevi",
                        principalColumn: "KursId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Testovi",
                columns: table => new
                {
                    KursId = table.Column<int>(nullable: false),
                    TestId = table.Column<int>(nullable: false),
                    Nivo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Testovi", x => new { x.TestId, x.KursId });
                    table.ForeignKey(
                        name: "FK_Testovi_Kursevi_KursId",
                        column: x => x.KursId,
                        principalTable: "Kursevi",
                        principalColumn: "KursId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pitanja",
                columns: table => new
                {
                    PitanjeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestId = table.Column<int>(nullable: true),
                    TestKursId = table.Column<int>(nullable: true),
                    Naziv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pitanja", x => x.PitanjeId);
                    table.ForeignKey(
                        name: "FK_Pitanja_Testovi_TestId_TestKursId",
                        columns: x => new { x.TestId, x.TestKursId },
                        principalTable: "Testovi",
                        principalColumns: new[] { "TestId", "KursId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lekcije_KursId",
                table: "Lekcije",
                column: "KursId");

            migrationBuilder.CreateIndex(
                name: "IX_Pitanja_TestId_TestKursId",
                table: "Pitanja",
                columns: new[] { "TestId", "TestKursId" });

            migrationBuilder.CreateIndex(
                name: "IX_Pohadjanja_KursId",
                table: "Pohadjanja",
                column: "KursId");

            migrationBuilder.CreateIndex(
                name: "IX_Testovi_KursId",
                table: "Testovi",
                column: "KursId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lekcije");

            migrationBuilder.DropTable(
                name: "Pitanja");

            migrationBuilder.DropTable(
                name: "Pohadjanja");

            migrationBuilder.DropTable(
                name: "Testovi");

            migrationBuilder.DropTable(
                name: "Korisnici");

            migrationBuilder.DropTable(
                name: "Kursevi");
        }
    }
}
