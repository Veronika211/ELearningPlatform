using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class INIT : Migration
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
                name: "Lekcija",
                columns: table => new
                {
                    LekcijaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KursId = table.Column<int>(nullable: false),
                    Naziv = table.Column<string>(nullable: true),
                    Sadrzaj = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lekcija", x => new { x.KursId, x.LekcijaId });
                    table.ForeignKey(
                        name: "FK_Lekcija_Kursevi_KursId",
                        column: x => x.KursId,
                        principalTable: "Kursevi",
                        principalColumn: "KursId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pohadjanje",
                columns: table => new
                {
                    KorisnikId = table.Column<int>(nullable: false),
                    KursId = table.Column<int>(nullable: false),
                    Bodovi = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pohadjanje", x => new { x.KorisnikId, x.KursId });
                    table.ForeignKey(
                        name: "FK_Pohadjanje_Korisnici_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnici",
                        principalColumn: "KorisnikId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pohadjanje_Kursevi_KursId",
                        column: x => x.KursId,
                        principalTable: "Kursevi",
                        principalColumn: "KursId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Testovi",
                columns: table => new
                {
                    TestId = table.Column<int>(nullable: false),
                    KursId = table.Column<int>(nullable: false),
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
                    Naziv = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    TestId = table.Column<int>(nullable: true),
                    TestKursId = table.Column<int>(nullable: true),
                    TacanOdgovor = table.Column<string>(nullable: true),
                    TacanBodovi = table.Column<int>(nullable: true),
                    NetacanOdgovor1 = table.Column<string>(nullable: true),
                    NetacanOdgovor2 = table.Column<string>(nullable: true),
                    NetacanOdgovor3 = table.Column<string>(nullable: true),
                    Dopuna_TacanOdgovor = table.Column<string>(nullable: true),
                    Dopuna_TacanBodovi = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pitanja", x => x.PitanjeId);
                    table.ForeignKey(
                        name: "FK_Pitanja_Testovi_TestId_TestKursId",
                        columns: x => new { x.TestId, x.TestKursId },
                        principalTable: "Testovi",
                        principalColumns: new[] { "TestId", "KursId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Polaganje",
                columns: table => new
                {
                    KorisnikId = table.Column<int>(nullable: false),
                    TestId = table.Column<int>(nullable: false),
                    TestId1 = table.Column<int>(nullable: false),
                    TestKursId = table.Column<int>(nullable: false),
                    BodoviT = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Polaganje", x => new { x.KorisnikId, x.TestId });
                    table.ForeignKey(
                        name: "FK_Polaganje_Korisnici_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnici",
                        principalColumn: "KorisnikId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Polaganje_Testovi_TestId1_TestKursId",
                        columns: x => new { x.TestId1, x.TestKursId },
                        principalTable: "Testovi",
                        principalColumns: new[] { "TestId", "KursId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pitanja_TestId_TestKursId",
                table: "Pitanja",
                columns: new[] { "TestId", "TestKursId" });

            migrationBuilder.CreateIndex(
                name: "IX_Pohadjanje_KursId",
                table: "Pohadjanje",
                column: "KursId");

            migrationBuilder.CreateIndex(
                name: "IX_Polaganje_TestId1_TestKursId",
                table: "Polaganje",
                columns: new[] { "TestId1", "TestKursId" });

            migrationBuilder.CreateIndex(
                name: "IX_Testovi_KursId",
                table: "Testovi",
                column: "KursId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lekcija");

            migrationBuilder.DropTable(
                name: "Pitanja");

            migrationBuilder.DropTable(
                name: "Pohadjanje");

            migrationBuilder.DropTable(
                name: "Polaganje");

            migrationBuilder.DropTable(
                name: "Korisnici");

            migrationBuilder.DropTable(
                name: "Testovi");

            migrationBuilder.DropTable(
                name: "Kursevi");
        }
    }
}
