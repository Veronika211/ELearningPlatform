using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class INIT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Administratori",
                columns: table => new
                {
                    AdministratorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(nullable: true),
                    Prezime = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administratori", x => x.AdministratorId);
                });

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
                    NazivKursa = table.Column<string>(nullable: false)
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
                    TestId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KursId = table.Column<int>(nullable: false),
                    Nivo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Testovi", x => x.TestId);
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
                        name: "FK_Pitanja_Testovi_TestId",
                        column: x => x.TestId,
                        principalTable: "Testovi",
                        principalColumn: "TestId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Polaganje",
                columns: table => new
                {
                    PolaganjeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnikId = table.Column<int>(nullable: false),
                    TestId = table.Column<int>(nullable: true),
                    BodoviT = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Polaganje", x => x.PolaganjeId);
                    table.ForeignKey(
                        name: "FK_Polaganje_Korisnici_KorisnikId",
                        column: x => x.KorisnikId,
                        principalTable: "Korisnici",
                        principalColumn: "KorisnikId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Polaganje_Testovi_TestId",
                        column: x => x.TestId,
                        principalTable: "Testovi",
                        principalColumn: "TestId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Administratori",
                columns: new[] { "AdministratorId", "Ime", "Password", "Prezime", "Username" },
                values: new object[] { 1, "Tatjana", "ts", "Stojanovic", "ts" });

            migrationBuilder.InsertData(
                table: "Korisnici",
                columns: new[] { "KorisnikId", "BrPoena", "Ime", "Password", "Prezime", "Username" },
                values: new object[,]
                {
                    { 1, 0, "Aleksandra", "am", "Markovic", "am" },
                    { 2, 0, "Veronika", "vm", "Markovic", "vm" }
                });

            migrationBuilder.InsertData(
                table: "Kursevi",
                columns: new[] { "KursId", "NazivKursa" },
                values: new object[,]
                {
                    { 1, "Napredne .NET tehnologije" },
                    { 2, "Napredna Java" },
                    { 3, "Projektovanje informacionih sistema" },
                    { 4, "Osnove teorije igara" }
                });

            migrationBuilder.InsertData(
                table: "Lekcija",
                columns: new[] { "KursId", "LekcijaId", "Naziv", "Sadrzaj" },
                values: new object[,]
                {
                    { 1, 1, "Tipovi objekata", null },
                    { 4, 9, "Igre antikoordinacije", null },
                    { 4, 8, "Igre koordinacije", null },
                    { 4, 7, "Dilema zatvorenika", null },
                    { 4, 6, "Mesovite igre", null },
                    { 3, 4, "Tronivojska arhitektura", null },
                    { 3, 5, "Implementacija korisnickog interfejsa", null },
                    { 2, 3, "Osnove OOP", null },
                    { 1, 2, "Konstruktori", null }
                });

            migrationBuilder.InsertData(
                table: "Testovi",
                columns: new[] { "TestId", "KursId", "Nivo" },
                values: new object[,]
                {
                    { 5, 2, "II" },
                    { 6, 2, "III" },
                    { 11, 4, "II" },
                    { 7, 3, "I" },
                    { 8, 3, "II" },
                    { 9, 3, "III" },
                    { 3, 1, "III" },
                    { 2, 1, "II" },
                    { 1, 1, "I" },
                    { 10, 4, "I" },
                    { 4, 2, "I" },
                    { 12, 4, "III" }
                });

            migrationBuilder.InsertData(
                table: "Pitanja",
                columns: new[] { "PitanjeId", "Discriminator", "Naziv", "TestId", "NetacanOdgovor1", "NetacanOdgovor2", "NetacanOdgovor3", "TacanBodovi", "TacanOdgovor" },
                values: new object[,]
                {
                    { 3, "Dopuna", "Navedite validator u ASP.NET-u koji se koristi kako bismo se uverili da se vrednosti u dve razlicite kontrole podudaraju", 2, null, null, null, 10, "Compare Validator control" },
                    { 4, "Checkbox", "Navedite tri vrste caching-a u ASP.NET-u", 3, "Output Caching,In Caching,Data Caching", "Output Caching,Fragment Caching,Type Caching", "In Caching,Fragment Caching,Data Caching", 15, "Output Caching,Fragment Caching,Data Caching" },
                    { 1, "Checkbox", "Koja su cetiri osnovna principa OOP?", 4, "Nasledjivanje, modularnost, apstrakcija, enkapsulacija", "Nasledjivanje, modularnost, asocijacija, enkapsulacija", "Klasifikacija, modularnost, apstrakcija, enkapsulacija", 5, "Nasledjivanje, modularnost, polumorfizam, enkapsulacija" },
                    { 2, "Checkbox", "Arhitektura informacionih sistema je?", 7, "Dvonivojska", "Sestonivojska", "Petonivojska", 5, "Tronivojska" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pitanja_TestId",
                table: "Pitanja",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_Pohadjanje_KursId",
                table: "Pohadjanje",
                column: "KursId");

            migrationBuilder.CreateIndex(
                name: "IX_Polaganje_KorisnikId",
                table: "Polaganje",
                column: "KorisnikId");

            migrationBuilder.CreateIndex(
                name: "IX_Polaganje_TestId",
                table: "Polaganje",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_Testovi_KursId",
                table: "Testovi",
                column: "KursId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administratori");

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
