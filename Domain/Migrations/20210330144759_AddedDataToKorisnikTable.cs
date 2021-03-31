using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Migrations
{
    public partial class AddedDataToKorisnikTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Korisnici",
                columns: new[] { "KorisnikId", "BrPoena", "Ime", "Password", "Prezime", "Username" },
                values: new object[] { 1, 0, "Aleksandra", "am", "Markovic", "am" });

            migrationBuilder.InsertData(
                table: "Korisnici",
                columns: new[] { "KorisnikId", "BrPoena", "Ime", "Password", "Prezime", "Username" },
                values: new object[] { 2, 0, "Veronika", "vm", "Markovic", "vm" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Korisnici",
                keyColumn: "KorisnikId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Korisnici",
                keyColumn: "KorisnikId",
                keyValue: 2);
        }
    }
}
