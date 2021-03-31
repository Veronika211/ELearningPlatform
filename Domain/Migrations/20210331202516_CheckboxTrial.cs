using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class CheckboxTrial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Pitanja",
                columns: new[] { "PitanjeId", "Discriminator", "Naziv", "TestId", "TestKursId", "NetacanOdgovor1", "NetacanOdgovor2", "NetacanOdgovor3", "TacanBodovi", "TacanOdgovor" },
                values: new object[] { 2, "Checkbox", "Koja su cetiri osnovna principa OOP?", 4, 2, "Nasledjivanje, modularnost, apstrakcija, enkapsulacija", "Nasledjivanje, modularnost, asocijacija, enkapsulacija", "Klasifikacija, modularnost, apstrakcija, enkapsulacija", 5, "Nasledjivanje, modularnost, polumorfizam, enkapsulacija" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Pitanja",
                keyColumn: "PitanjeId",
                keyValue: 2);
        }
    }
}
