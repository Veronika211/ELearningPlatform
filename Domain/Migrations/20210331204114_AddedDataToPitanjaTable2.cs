using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class AddedDataToPitanjaTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Pitanja",
                keyColumn: "PitanjeId",
                keyValue: 1,
                columns: new[] { "NetacanOdgovor1", "NetacanOdgovor2", "NetacanOdgovor3", "TacanBodovi", "TacanOdgovor" },
                values: new object[] { "Nasledjivanje, modularnost, apstrakcija, enkapsulacija", "Nasledjivanje, modularnost, asocijacija, enkapsulacija", "Klasifikacija, modularnost, apstrakcija, enkapsulacija", 5, "Nasledjivanje, modularnost, polumorfizam, enkapsulacija" });

            migrationBuilder.UpdateData(
                table: "Pitanja",
                keyColumn: "PitanjeId",
                keyValue: 2,
                columns: new[] { "Naziv", "TestId", "TestKursId", "NetacanOdgovor1", "NetacanOdgovor2", "NetacanOdgovor3", "TacanOdgovor" },
                values: new object[] { "Arhitektura informacionih sistema je?", 7, 3, "Dvonivojska", "Sestonivojska", "Petonivojska", "Tronivojska" });

            migrationBuilder.InsertData(
                table: "Pitanja",
                columns: new[] { "PitanjeId", "Discriminator", "Naziv", "TestId", "TestKursId", "NetacanOdgovor1", "NetacanOdgovor2", "NetacanOdgovor3", "TacanBodovi", "TacanOdgovor" },
                values: new object[,]
                {
                    { 3, "Dopuna", "Navedite validator u ASP.NET-u koji se koristi kako bismo se uverili da se vrednosti u dve razlicite kontrole podudaraju", 2, 1, null, null, null, 10, "Compare Validator control" },
                    { 4, "Checkbox", "Navedite tri vrste caching-a u ASP.NET-u", 3, 1, "Output Caching,In Caching,Data Caching", "Output Caching,Fragment Caching,Type Caching", "In Caching,Fragment Caching,Data Caching", 15, "Output Caching,Fragment Caching,Data Caching" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Pitanja",
                keyColumn: "PitanjeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Pitanja",
                keyColumn: "PitanjeId",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Pitanja",
                keyColumn: "PitanjeId",
                keyValue: 2,
                columns: new[] { "Naziv", "TestId", "TestKursId", "NetacanOdgovor1", "NetacanOdgovor2", "NetacanOdgovor3", "TacanOdgovor" },
                values: new object[] { "Koja su cetiri osnovna principa OOP?", 4, 2, "Nasledjivanje, modularnost, apstrakcija, enkapsulacija", "Nasledjivanje, modularnost, asocijacija, enkapsulacija", "Klasifikacija, modularnost, apstrakcija, enkapsulacija", "Nasledjivanje, modularnost, polumorfizam, enkapsulacija" });
        }
    }
}
